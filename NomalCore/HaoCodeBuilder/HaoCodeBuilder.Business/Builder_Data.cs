using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HaoCodeBuilder.Business
{
    internal class Builder_Data
    {
        private IData.ICreateCode createInstance;
        private IData.IDatabase databaseInstance;
        private Import import;
        public Builder_Data(Model.DatabaseType dbType)
        {
            this.createInstance = Factory.Factory.CreateCreateCodeInstance(dbType);
            this.databaseInstance = Factory.Factory.CreateDatabaseInstance(dbType);
            this.import = new Import(dbType);
        }
        /// <summary>
        /// 得到数据层代码
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public string GetDataClass(Model.CodeCreate param)
        {
            Model.Servers server = Common.Config.GetServer(param.ServerID);
            if (server == null)
            {
                return string.Empty;
            }
            List<Model.Fields> fields = databaseInstance.GetFields(server.ID, param.DbName, param.TableName);
            StringBuilder data = new StringBuilder();
            data.Append("/************************************************************************" + Environment.NewLine);
            data.Append("* 标题: " + (!String.IsNullOrEmpty(param.TableDescrible) ? param.TableDescrible : param.TableName) + "的DAL" + Environment.NewLine);
            data.Append("* 描述: " + (!String.IsNullOrEmpty(param.TableDescrible) ? param.TableDescrible : param.TableName) + "的DAL" + Environment.NewLine);
            data.Append("* 数据表:" + param.TableName + Environment.NewLine);
            data.Append("* 作者：" + param.CNSC.UserName + Environment.NewLine);
            data.Append("* 日期：" + DateTime.Now + "" + Environment.NewLine);
            data.Append("* 版本：V1.0" + Environment.NewLine);
            data.Append("*************************************************************************/" + Environment.NewLine + Environment.NewLine);
            data.Append(import.GetImport_Model() + Environment.NewLine);
            data.Append("namespace " + param.NameSpace + (param.NameSpace.IsNullOrEmpty() ? "" : ".") + param.CNSC.Data + (param.NameSpace1.IsNullOrEmpty() ? "" : "." + param.NameSpace1) + "\r\n");
            data.Append("{\r\n");
            data.Append("\t" + import.GetImport_Data_Inside() + Environment.NewLine);
            data.Append("\t/// <summary>\r\n");
            data.Append("\t/// " + (!String.IsNullOrEmpty(param.TableDescrible) ? param.TableDescrible : param.TableName) + "的DAL" + "\r\n");
            data.Append("\t/// </summary>\r\n");
            data.Append("\tpublic class " + param.ClassName + (param.BuilderType == Model.BuilderType.Factory ? " : " + param.NameSpace + (param.NameSpace.IsNullOrEmpty() ? "" : ".") + param.CNSC.Interface + (param.NameSpace1.IsNullOrEmpty() ? "" : "." + param.NameSpace1) + ".I" + param.ClassName : "") + "DAL: BaseDal\r\n");
            data.Append("\t{\r\n");

            data.Append("\t\t#region 属性\r\n\r\n");
            data.Append("\t\t/// <summary>\r\n");
            data.Append("\t\t/// 数据库名\r\n");
            data.Append("\t\t/// </summary>\r\n");
            data.Append("\t\tprivate static readonly String tableName = \"" + param.TableName + "\";\r\n\r\n");
            data.Append("\t\t#endregion\r\n\r\n");
            data.Append("\t\t#region 方法\r\n\r\n");

            //查询所有记录
            if (param.MethodList.Contains(Model.BuilderMethods.SelectAll))
            {
                data.Append(GetAllMethod(fields, param));
            }

            //查询主键记录
            if (param.MethodList.Contains(Model.BuilderMethods.SelectByKey) && fields.Where(p => p.IsPrimaryKey).Count() > 0)
            {
                data.Append(GetByKeyMethod(fields, param));
            }

            //删除记录
            if (param.MethodList.Contains(Model.BuilderMethods.Delete) && fields.Where(p => p.IsPrimaryKey).Count() > 0)
            {
                data.Append(GetDeleteMethod(fields, param));
            }

            //查询记录数
            if (param.MethodList.Contains(Model.BuilderMethods.Count))
            {
                data.Append(GetCountMethod(fields, param));
            }

            //新增记录
            if (param.MethodList.Contains(Model.BuilderMethods.Add))
            {
                data.Append(GetAddMethod(fields, param));
            }

            //更新记录
            if (param.MethodList.Contains(Model.BuilderMethods.Update) && fields.Where(p => p.IsPrimaryKey).Count() > 0)
            {
                data.Append(GetUpdateMethod(fields, param));
            }

            //转换List
            //data.Append(GetConvertDataReaderToListMethod(fields, param));

            data.Append("\t\t#endregion\r\n");
            data.Append("\t}\r\n");
            data.Append("}");
            return data.ToString();
        }


        /// <summary>
        /// 新增记录方法
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        private string GetAddMethod(List<Model.Fields> fields, Model.CodeCreate param)
        {
            StringBuilder data = new StringBuilder();
            data.Append("\t\t/// <summary>\r\n");
            data.Append("\t\t/// 插入数据\r\n");
            data.Append("\t\t/// </summary>\r\n");
            foreach (var field in fields)
            {
                data.Append("\t\t/// <param name=\"" + field.NameLower + "\">" + field.Note + "</param>\r\n");
            }
            data.Append("\t\t/// <returns>受影响的行数</returns>\r\n");
            data.Append("\t\tpublic static Int32 Insert(");
            foreach (var field in fields)
            {
                data.Append(field.DotNetType + " " + field.NameLower);
                data.Append(field.Name != fields.Last().Name ? ", " : "");
            }

            data.Append(")\r\n");
            data.Append("\t\t{\r\n");

            data.Append("\t\t\tMySqlParameter[] mySqlParameter = new MySqlParameter[]\r\n");
            data.Append("\t\t\t{\r\n");
            foreach (var field in fields)
            {
                data.Append("\t\t\t\tnew MySqlParameter(FiledConst." + field.Name + "," + field.NameLower + "),\r\n");
            }
            data.Append("\t\t\t};\r\n\r\n");
            data.Append("\t\t\treturn ExecuteNonQuery(SqlFactory.mData[tableName][SqlType.Insert], mySqlParameter);\r\n");

            data.Append("\t\t}\r\n\r\n");

            return data.ToString();
        }

        /// <summary>
        /// 更新记录方法
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        private string GetUpdateMethod(List<Model.Fields> fields, Model.CodeCreate param)
        {
            StringBuilder data = new StringBuilder();
            data.Append("\t\t/// <summary>\r\n");
            data.Append("\t\t/// 更新数据\r\n");
            data.Append("\t\t/// </summary>\r\n");
            foreach (var field in fields)
            {
                data.Append("\t\t/// <param name=\"" + field.NameLower + "\">" + field.Note + "</param>\r\n");
            }
            data.Append("\t\t/// <returns>受影响的行数</returns>\r\n");
            data.Append("\t\tpublic static Int32 Update(");
            foreach (var field in fields)
            {
                data.Append(field.DotNetType + " " + field.NameLower);
                data.Append(field.Name != fields.Last().Name ? ", " : "");
            }

            data.Append(")\r\n");
            data.Append("\t\t{\r\n");

            data.Append("\t\t\tMySqlParameter[] mySqlParameter = new MySqlParameter[]\r\n");
            data.Append("\t\t\t{\r\n");
            foreach (var field in fields)
            {
                data.Append("\t\t\t\tnew MySqlParameter(FiledConst." + field.Name + "," + field.NameLower + "),\r\n");
            }
            data.Append("\t\t\t};\r\n\r\n");
            data.Append("\t\t\treturn ExecuteNonQuery(SqlFactory.mData[tableName][SqlType.Update], mySqlParameter);\r\n");

            data.Append("\t\t}\r\n\r\n");

            return data.ToString();
        }
        /// <summary>
        /// 删除记录方法
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        private string GetDeleteMethod(List<Model.Fields> fields, Model.CodeCreate param)
        {
            //主键
            var Primarykeys = fields.Where(p => p.IsPrimaryKey);
            var NotPrimarykeys = fields.Where(p => !p.IsPrimaryKey);
            bool HasPrimarykey = Primarykeys.Count() > 0;

            StringBuilder data = new StringBuilder();
            data.Append("\t\t/// <summary>\r\n");
            data.Append("\t\t/// 删除\r\n");
            data.Append("\t\t/// </summary>\r\n");
            foreach (var field in Primarykeys)
            {
                data.Append("\t\t/// <param name=\"" + field.NameLower + "\">" + field.Note + "</param>\r\n");
            }
            data.Append("\t\t/// <returns>受影响的行数</returns>\r\n");
            data.Append("\t\tpublic static Int32 Delete(");
            foreach (var field in Primarykeys)
            {
                data.Append(field.DotNetType + " " + field.NameLower);
                data.Append(field.Name != Primarykeys.Last().Name ? ", " : "");
            }
            data.Append(")\r\n");
            data.Append("\t\t{\r\n");

            data.Append("\t\t\tMySqlParameter[] mySqlParameter = new MySqlParameter[]\r\n");
            data.Append("\t\t\t{\r\n");
            foreach (var field in Primarykeys)
            {
                data.Append("\t\t\t\tnew MySqlParameter(FiledConst." + field.Name + "," + field.NameLower + "),\r\n");
            }
            data.Append("\t\t\t};\r\n\r\n");
            data.Append("\t\t\treturn ExecuteNonQuery(SqlFactory.mData[tableName][SqlType.Delete], mySqlParameter);\r\n");

            data.Append("\t\t}\r\n\r\n");
            return data.ToString();
        }

        /// <summary>
        /// 将DataReader转换为List方法
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        private string GetConvertDataReaderToListMethod(List<Model.Fields> fields, Model.CodeCreate param)
        {
            //自增列
            var Identitys = fields.Where(p => p.IsIdentity);
            var NotIdeneitys = fields.Where(p => !p.IsIdentity);
            bool HasIdentity = Identitys.Count() > 0;

            //主键
            var Primarykeys = fields.Where(p => p.IsPrimaryKey);
            var NotPrimarykeys = fields.Where(p => !p.IsPrimaryKey);
            bool HasPrimarykey = Primarykeys.Count() > 0;

            StringBuilder data = new StringBuilder();
            data.Append("\t\t/// <summary>\r\n");
            data.Append("\t\t/// 将DataRedar转换为List\r\n");
            data.Append("\t\t/// </summary>\r\n");
            data.Append("\t\tprivate List<" + param.NameSpace + (param.NameSpace.IsNullOrEmpty() ? "" : ".") + param.CNSC.Model + (param.NameSpace1.IsNullOrEmpty() ? "" : "." + param.NameSpace1) + "." + param.ClassName + "> DataReaderToList(" + createInstance.GetDataReaderType() + " dataReader)\r\n");
            data.Append("\t\t{\r\n");
            data.Append("\t\t\tList<" + param.NameSpace + (param.NameSpace.IsNullOrEmpty() ? "" : ".") + param.CNSC.Model + (param.NameSpace1.IsNullOrEmpty() ? "" : "." + param.NameSpace1) + "." + param.ClassName + "> List = new List<" + param.NameSpace + (param.NameSpace.IsNullOrEmpty() ? "" : ".") + param.CNSC.Model + (param.NameSpace1.IsNullOrEmpty() ? "" : "." + param.NameSpace1) + "." + param.ClassName + ">();\r\n");
            data.Append("\t\t\t" + param.NameSpace + (param.NameSpace.IsNullOrEmpty() ? "" : ".") + param.CNSC.Model + (param.NameSpace1.IsNullOrEmpty() ? "" : "." + param.NameSpace1) + "." + param.ClassName + " model = null;\r\n");
            data.Append("\t\t\twhile(dataReader.Read())\r\n");
            data.Append("\t\t\t{\r\n");

            data.Append("\t\t\t\tmodel = new " + param.NameSpace + (param.NameSpace.IsNullOrEmpty() ? "" : ".") + param.CNSC.Model + (param.NameSpace1.IsNullOrEmpty() ? "" : "." + param.NameSpace1) + "." + param.ClassName + "();\r\n");
            int i = 0;
            string nullString = string.Empty;
            foreach (var field in fields)
            {
                nullString = field.IsNull ? "\t\t\t\tif (!dataReader.IsDBNull(" + i + "))\r\n\t\t\t\t\t" : "\t\t\t\t";
                switch (field.DotNetType.Replace("?", "").ToLower())
                {
                    case "string":
                        data.Append(nullString + "model." + field.Name + " = dataReader.GetString(" + i.ToString() + ");\r\n");
                        break;
                    case "guid":
                        data.Append(nullString + "model." + field.Name + " = dataReader.GetGuid(" + i.ToString() + ");\r\n");
                        break;
                    case "long":
                        data.Append(nullString + "model." + field.Name + " = dataReader.GetInt64(" + i.ToString() + ");\r\n");
                        break;
                    case "int":
                        data.Append(nullString + "model." + field.Name + " = dataReader.GetInt32(" + i.ToString() + ");\r\n");
                        break;
                    case "short":
                        data.Append(nullString + "model." + field.Name + " = dataReader.GetInt16(" + i.ToString() + ");\r\n");
                        break;
                    case "byte":
                        data.Append(nullString + "model." + field.Name + " = dataReader.GetInt16(" + i.ToString() + ");\r\n");
                        break;
                    case "decimal":
                        data.Append(nullString + "model." + field.Name + " = dataReader.GetDecimal(" + i.ToString() + ");\r\n");
                        break;
                    case "float":
                        data.Append(nullString + "\tmodel." + field.Name + " = dataReader.GetFloat(" + i.ToString() + ");\r\n");
                        break;
                    case "double":
                        data.Append(nullString + "\tmodel." + field.Name + " = dataReader.GetDouble(" + i.ToString() + ");\r\n");
                        break;
                    case "datetime":
                        data.Append(nullString + "model." + field.Name + " = dataReader.GetDateTime(" + i.ToString() + ");\r\n");
                        break;
                    case "bool":
                        data.Append(nullString + "model." + field.Name + " = dataReader.GetBoolean(" + i.ToString() + ");\r\n");
                        break;
                }
                i++;
            }
            data.Append("\t\t\t\tList.Add(model);\r\n");
            data.Append("\t\t\t}\r\n");
            data.Append("\t\t\treturn List;\r\n");
            data.Append("\t\t}\r\n");

            return data.ToString();
        }

        /// <summary>
        /// 查询所有记录方法
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        private string GetAllMethod(List<Model.Fields> fields, Model.CodeCreate param)
        {
            StringBuilder data = new StringBuilder();
            data.Append("\t\t/// <summary>\r\n");
            data.Append("\t\t/// 获取所有数据\r\n");
            data.Append("\t\t/// </summary>\r\n");
            data.Append("\t\t/// <returns>获取所有数据</returns>\r\n");
            data.Append("\t\tpublic static DataTable GetAllList()\r\n");
            data.Append("\t\t{\r\n");
            data.Append("\t\t\treturn ExecuteDataTable(SqlFactory.mData[tableName][SqlType.GetAllList]);\r\n");
            data.Append("\t\t}\r\n\r\n");

            return data.ToString();
        }

        /// <summary>
        /// 查询主键记录方法
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        private string GetByKeyMethod(List<Model.Fields> fields, Model.CodeCreate param)
        {
            //主键
            var Primarykeys = fields.Where(p => p.IsPrimaryKey);
            var NotPrimarykeys = fields.Where(p => !p.IsPrimaryKey);
            bool HasPrimarykey = Primarykeys.Count() > 0;

            StringBuilder data = new StringBuilder();
            data.Append("\t\t/// <summary>\r\n");
            data.Append("\t\t/// 获取数据\r\n");
            data.Append("\t\t/// </summary>\r\n");
            foreach (var field in Primarykeys)
            {
                data.Append("\t\t/// <param name=\"" + field.NameLower + "\">" + field.Note + "</param>\r\n");
            }
            data.Append("\t\t/// <returns>获取数据</returns>\r\n");
            data.Append("\t\tpublic static DataTable GetList(");
            foreach (var field in Primarykeys)
            {
                data.Append(field.DotNetType + " " + field.NameLower);
                data.Append(field.Name != Primarykeys.Last().Name ? ", " : "");
            }
            data.Append(")\r\n");
            data.Append("\t\t{\r\n");

            data.Append("\t\t\tMySqlParameter[] mySqlParameter = new MySqlParameter[]\r\n");
            data.Append("\t\t\t{\r\n");
            foreach (var field in Primarykeys)
            {
                data.Append("\t\t\t\tnew MySqlParameter(FiledConst." + field.Name + "," + field.NameLower + "),\r\n");
            }
            data.Append("\t\t\t};\r\n\r\n");
            data.Append("\t\t\treturn ExecuteDataTable(SqlFactory.mData[tableName][SqlType.GetList], mySqlParameter);\r\n");

            data.Append("\t\t}\r\n\r\n");
            return data.ToString();
        }

        /// <summary>
        /// 查询记录条数方法
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        private string GetCountMethod(List<Model.Fields> fields, Model.CodeCreate param)
        {
            StringBuilder data = new StringBuilder();
            data.Append("\t\t/// <summary>\r\n");
            data.Append("\t\t/// 查询记录数\r\n");
            data.Append("\t\t/// </summary>\r\n");
            data.Append("\t\t/// <returns>获取所有数据</returns>\r\n");
            data.Append("\t\tpublic static Int32 Count()\r\n");
            data.Append("\t\t{\r\n");
            data.Append("\t\t\treturn ExecuteNonQuery(SqlFactory.mData[tableName][SqlType.GetCount]);\r\n");
            data.Append("\t\t}\r\n\r\n");

            return data.ToString();
        }

    }
}
