/************************************************************************
* 标题: model连接处理类
* 描述: model连接处理类
* 作者：肖强
* 日期：2017-5-3 10:43:36
* 版本：V1
*************************************************************************/
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace WebServer.Model
{
    using Dapper;
    using MySql.Data.MySqlClient;
    using System.Linq;
    using Tool.Common;
    using Tool.CustomAttribute;

    /// <summary>
    /// model连接处理类
    /// </summary>
    public class BaseModelDal
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        private static readonly String Conn = WebConfig.ConfigConneString;

        /// <summary>
        /// 执行查询
        /// </summary>
        /// <typeparam name="T">类别</typeparam>
        /// <returns>需要返回的类型集合</returns>
        public static List<T> ExecuteQuery<T>() where T : class, new()
        {
            Type t = typeof(T);
            var objs = t.GetCustomAttributes(typeof(DataBaseTableAttribute), false);
            if (objs == null || objs.Length == 0)
            {
                throw new Exception(String.Format("类型{0}没有DataBaseTableAttribute标签，不能自动查询并且转换", t.FullName));
            }

            var info = objs[0] as DataBaseTableAttribute;
            if (info == null || String.IsNullOrWhiteSpace(info.Name))
            {
                throw new Exception(String.Format("类型{0}的DataBaseTableAttribute标签为空，不能自动查询并且转换", t.FullName));
            }

            String sql = String.Format("SELECT * FROM {0};", info.Name);
            var cType = CommandType.Text;
            if (info.Type == DataBaseTableAttribute.DataBaseType.StoredProcedure)
            {
                sql = info.Name;
                cType = CommandType.StoredProcedure;
            }

            MySqlCommand cmd = new MySqlCommand();

            using (MySqlConnection connection = new MySqlConnection(Conn))
            {
                switch (cType)
                {
                    case CommandType.StoredProcedure:
                        return connection.Query<T>(sql, null, null, true, null, CommandType.StoredProcedure).ToList();
                    case CommandType.TableDirect:
                    case CommandType.Text:
                    default:
                        return connection.Query<T>(sql).ToList();
                }
            }
        }

        /// <summary>
        /// dataset转list
        /// </summary>
        /// <typeparam name="T">转换类型</typeparam>
        /// <param name="dataSet">数据源</param>
        /// <param name="tableIndex">需要转换表的索引</param>
        /// <returns></returns>
        private static List<T> DataSetToList<T>(DataSet dataSet, int tableIndex)
        {
            List<T> list = new List<T>();

            //确认参数有效
            if (dataSet == null || dataSet.Tables.Count <= 0 || tableIndex < 0)
            {
                return list;
            }

            DataTable dt = dataSet.Tables[tableIndex];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //创建泛型对象
                T t = Activator.CreateInstance<T>();
                //获取对象所有属性
                PropertyInfo[] propertyInfo = t.GetType().GetProperties();
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    foreach (PropertyInfo info in propertyInfo)
                    {
                        //属性名称和列名相同时赋值
                        if (dt.Columns[j].ColumnName.ToUpper().Equals(info.Name.ToUpper()))
                        {
                            if (dt.Rows[i][j] != DBNull.Value)
                            {
                                info.SetValue(t, dt.Rows[i][j], null);
                            }
                            else
                            {
                                info.SetValue(t, null, null);
                            }
                            break;
                        }
                    }
                }
                list.Add(t);
            }

            return list;
        }
    }
}
