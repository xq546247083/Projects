using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HaoCodeBuilder.Business
{
    internal class Builder_Model
    {
        private IData.ICreateCode createInstance;
        private IData.IDatabase databaseInstance;
        private Import import;
        public Builder_Model(Model.DatabaseType dbType)
        {
            this.createInstance = Factory.Factory.CreateCreateCodeInstance(dbType);
            this.databaseInstance = Factory.Factory.CreateDatabaseInstance(dbType);
            this.import = new Import(dbType);
        }
        /// <summary>
        /// 得到实体层
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public string GetModelClass(Model.CodeCreate param)
        {
            Model.Servers server = Common.Config.GetServer(param.ServerID);
            if (server == null)
            {
                return string.Empty;
            }

            List<Model.Fields> fields = databaseInstance.GetFields(server.ID, param.DbName, param.TableName);
            StringBuilder model = new StringBuilder();
            model.Append("/************************************************************************" + Environment.NewLine);
            model.Append("* 标题: " + (!String.IsNullOrEmpty(param.TableDescrible) ? param.TableDescrible : param.TableName) + Environment.NewLine);
            model.Append("* 描述: " + (!String.IsNullOrEmpty(param.TableDescrible) ? param.TableDescrible : param.TableName) + Environment.NewLine);
            model.Append("* 数据表:" + param.TableName + Environment.NewLine);
            model.Append("* 作者：" + param.CNSC.UserName + Environment.NewLine);
            model.Append("* 日期：" + DateTime.Now + "" + Environment.NewLine);
            model.Append("* 版本：V1.0" + Environment.NewLine);
            model.Append("*************************************************************************/" + Environment.NewLine + Environment.NewLine);

            model.Append(import.GetImport_Model() + Environment.NewLine);
            model.Append("namespace " + param.NameSpace + (param.NameSpace.IsNullOrEmpty() ? "" : ".") + param.CNSC.Model + (param.NameSpace1.IsNullOrEmpty() ? "" : "." + param.NameSpace1) + "\r\n");
            model.Append("{\r\n");
            model.Append("\t" + import.GetImport_Model_Inside() + Environment.NewLine);
            model.Append("\t/// <summary>\r\n");
            model.Append("\t/// " + (!String.IsNullOrEmpty(param.TableDescrible) ? param.TableDescrible : param.TableName) + "\r\n");
            model.Append("\t/// </summary>\r\n");
            model.Append("\t[DataBaseTable(\"" + param.TableName + "\")]\r\n");
            model.Append("\tpublic class " + param.ClassName + "\r\n");
            model.Append("\t{\r\n");
            model.Append("\t\t#region 属性" + Environment.NewLine + Environment.NewLine);
            foreach (var field in fields)
            {
                model.Append("\t\t/// <summary>\r\n");
                model.Append("\t\t/// " + (field.Note.IsNullOrEmpty() ? field.Name : field.Note) + "\r\n");
                model.Append("\t\t/// </summary>\r\n");
                if (field.IsPrimaryKey)
                {
                    model.Append("\t\t[PrimaryKey]\r\n");
                }

                model.Append("\t\tpublic " + field.DotNetType + " " + field.Name + " { get; set; }\r\n\r\n");
            }
            model.Append("\t\t#endregion" + Environment.NewLine);
            model.Append("\t}\r\n");
            model.Append("}\r\n");
            return model.ToString();
        }
    }
}
