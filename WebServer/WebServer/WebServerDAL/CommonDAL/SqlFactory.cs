/************************************************************************
* 标题: 数据库基础语句工厂
* 描述: 生成操作数据库的基础语句
* 作者：肖强
* 日期：2017-5-3 10:43:36
* 版本：V1
*************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace WebServer.DAL
{
    using WebServer.Model;
    using Tool.Common;
    using Tool.CustomAttribute;

    /// <summary>
    /// 系统自动生成的sql语句
    /// </summary>
    public static class SqlFactory
    {
        #region 属性

        /// <summary>
        /// 要生成语句的表
        /// key:表名
        /// value:对应的操作Dic
        /// </summary>
        public static Dictionary<String, Dictionary<SqlType, String>> mData = new Dictionary<String, Dictionary<SqlType, String>>();

        /// <summary>
        /// 要生成语句的表
        /// key:表名
        /// value:对应的model
        /// </summary>
        private static Dictionary<String, Type> mTypeData = new Dictionary<String, Type>();

        private static readonly String Error = "SELECT 'Error:No PrimaryKey';";
        private static readonly String GetList = "SELECT {0} FROM {1} WHERE {2};";
        private static readonly String GetAllList = "SELECT {0} FROM {1};";
        private static readonly String Insert = "INSERT INTO {0} ({1}) VALUES ({2});";
        private static readonly String Update = "UPDATE {0} SET {1} WHERE {2};";
        private static readonly String Delete = "DELETE FROM {0} WHERE {1};";

        #endregion

        /// <summary>
        /// 构造方法
        /// </summary>
        static SqlFactory()
        {
            InitType();
        }

        /// <summary>
        /// 初始化mTypeData数据
        /// </summary>
        private static void InitType()
        {
            var types = ReflectionTool.GetTypeListOfAttribute(Assembly.GetAssembly(typeof(World)), typeof(DataBaseTableAttribute));
            //初始化要生成语句的类型数据
            foreach (var type in types)
            {
                var dataBaseTableAttribute = type.GetCustomAttribute(typeof(DataBaseTableAttribute), false) as DataBaseTableAttribute;
                if (dataBaseTableAttribute != null)
                {
                    mTypeData[dataBaseTableAttribute.Name] = type;
                }
            }
        }

        /// <summary>
        /// 生成语句
        /// </summary>
        /// <returns>表的语句</returns>
        public static void BuildCommond()
        {
            foreach (var type in mTypeData)
            {
                String tableName = type.Key;
                if (!mData.ContainsKey(tableName))
                {
                    mData[tableName] = new Dictionary<SqlType, String>();
                }

                var tableFiled = GetTableFiled(type.Value);
                var tableParamFiled = GetTableParamFiled(type.Value);
                //如果没有有主键，那么不能初始化GetList、Update、Delete
                if (HasPrimaryKey(type.Value))
                {
                    var whereStr = GetTableWhereStr(type.Value);
                    var setStr = GetTableSetStr(type.Value);

                    mData[tableName][SqlType.GetAllList] = String.Format(GetAllList, tableFiled, tableName);
                    mData[tableName][SqlType.GetList] = String.Format(GetList, tableFiled, tableName, whereStr);
                    mData[tableName][SqlType.Update] = String.Format(Update, tableName, setStr, whereStr);
                    mData[tableName][SqlType.Delete] = String.Format(Delete, tableName, whereStr);
                    mData[tableName][SqlType.Insert] = String.Format(Insert, tableName, tableFiled, tableParamFiled);
                }
                else
                {
                    mData[tableName][SqlType.GetAllList] = String.Format(GetAllList, tableFiled, tableName);
                    mData[tableName][SqlType.Insert] = String.Format(Insert, tableName, tableFiled, tableParamFiled);
                    mData[tableName][SqlType.GetList] = Error;
                    mData[tableName][SqlType.Update] = Error;
                    mData[tableName][SqlType.Delete] = Error;
                }
            }
        }

        /// <summary>
        /// 获取表的字段
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>返回值</returns>
        private static String GetTableFiled(Type type)
        {
            StringBuilder sb = new StringBuilder();
            var properties = type.GetProperties();
            foreach (var property in properties)
            {
                if (property.CustomAttributes.All(r => r.AttributeType != typeof(IgnoreAttribute)))
                {
                    sb.Append(String.Format("`{0}`,", property.Name));
                }
            }

            String result = sb.ToString();
            return result.Substring(0, result.Length - 1);
        }

        /// <summary>
        /// 获取表的字段
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>返回值</returns>
        private static String GetTableParamFiled(Type type)
        {
            StringBuilder sb = new StringBuilder();
            var properties = type.GetProperties();
            foreach (var property in properties)
            {
                if (property.CustomAttributes.All(r => r.AttributeType != typeof(IgnoreAttribute)))
                {

                    sb.Append(String.Format("@{0},", property.Name));
                }
            }

            String result = sb.ToString();
            return result.Substring(0, result.Length - 1);
        }

        /// <summary>
        /// 获取set的字符串
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>返回值</returns>
        private static String GetTableSetStr(Type type)
        {
            StringBuilder sb = new StringBuilder();
            var properties = type.GetProperties();
            foreach (var property in properties)
            {
                if (property.CustomAttributes.All(r => r.AttributeType != typeof(PrimaryKeyAttribute) && r.AttributeType != typeof(IgnoreAttribute)))
                {
                    sb.Append(String.Format("`{0}`=@{1},", property.Name, property.Name));
                }
            }

            String result = sb.ToString();
            return result.Substring(0, result.Length - 1);
        }

        /// <summary>
        /// 获取where的字符串
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>返回值</returns>
        private static String GetTableWhereStr(Type type)
        {
            StringBuilder sb = new StringBuilder("1=1");
            var properties = type.GetProperties();
            foreach (var property in properties)
            {
                if (property.CustomAttributes.Any(r => r.AttributeType == typeof(PrimaryKeyAttribute)))
                {
                    sb.Append(String.Format(" and `{0}`=@{1}", property.Name, property.Name));
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// 是否有主键
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>返回值</returns>
        private static Boolean HasPrimaryKey(Type type)
        {
            var properties = type.GetProperties();
            foreach (var property in properties)
            {
                if (property.CustomAttributes.Any(r => r.AttributeType == typeof(PrimaryKeyAttribute)))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
