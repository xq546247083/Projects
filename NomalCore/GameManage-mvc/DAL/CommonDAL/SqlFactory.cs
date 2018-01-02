/************************************************************************
* 描述: 生成操作数据库的基础语句的工厂
*************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Moqikaka.GameManage.DAL
{
    using Moqikaka.GameManage.Common;
    using Moqikaka.GameManage.Model;
    using Moqikaka.Util.Json;

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
        private static Dictionary<String, Dictionary<SqlType, String>> mData = new Dictionary<String, Dictionary<SqlType, String>>();

        /// <summary>
        /// 要生成语句的表
        /// key:表名
        /// value:对应的model
        /// </summary>
        private static Dictionary<String, Type> mTypeData = new Dictionary<String, Type>();

        private static readonly String ErrorSqlStr = "SELECT 'Error:No PrimaryKey';";
        private static readonly String GetListSqlStr = "SELECT {0} FROM {1} WHERE {2};";
        private static readonly String GetAllListSqlStr = "SELECT {0} FROM {1};";
        private static readonly String InsertSqlStr = "INSERT INTO {0} ({1}) VALUES ({2});";
        private static readonly String UpdateSqlStr = "UPDATE {0} SET {1} WHERE {2};";
        private static readonly String DeleteSqlStr = "DELETE FROM {0} WHERE {1};";

        private static readonly String GetDefinedListSqlStr = "SELECT {0} FROM {1} WHERE 1=1 ";
        private static readonly String GetDefinedCountSqlStr = " SELECT Count(*) FROM {0} WHERE 1=1 ";

        #endregion

        #region 初始

        /// <summary>
        /// 构造方法
        /// </summary>
        static SqlFactory()
        {
            InitType();
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 初始化mTypeData数据
        /// </summary>
        private static void InitType()
        {
            var types = ReflectionTool.GetTypeListOfAttribute(Assembly.GetAssembly(typeof(NoThing)), typeof(DataBaseTableAttribute));
            //初始化要生成语句的类型数据
            foreach (var type in types)
            {
                if (type.GetCustomAttribute(typeof(DataBaseTableAttribute), false) is DataBaseTableAttribute dataBaseTableAttribute)
                {
                    mTypeData[dataBaseTableAttribute.Name] = type;
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
                    sb.Append($"`{property.Name}`,");
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

                    sb.Append($"@{property.Name},");
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
                    sb.Append($"`{property.Name}`=@{property.Name},");
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
                    sb.Append($" and `{property.Name}`=@{property.Name}");
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

        /// <summary>
        /// 获取类型
        /// </summary>
        /// <param name="sqlType">数据库语句类型</param>
        /// <returns>找到的类型</returns>
        private static KeyValuePair<String, Type> GetType<T>(SqlType sqlType)
        {
            // 获取当前表模型的属性
            var mType = mTypeData.FirstOrDefault(r => r.Value == typeof(T));
            if (mType.Value == null)
            {
                throw new Exception($"表Model没有设置DataBaseTable属性,表名：{mType}，T:{typeof(T)}");
            }

            // 数据没有该表
            if (!mData.ContainsKey(mType.Key))
            {
                throw new Exception($"数据库语句工厂没有生成该表的类型,表名：{mType}，T:{typeof(T)}");
            }

            // 没有该操作
            if (!mData[mType.Key].ContainsKey(sqlType))
            {
                throw new Exception($"数据库语句工厂没有生成该表的该操作,表名：{mType}，T:{typeof(T)}，SqlType{sqlType}");
            }

            return mType;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 获取sql语句（只能筛选主键）
        /// </summary>
        /// <param name="sqlType">语句类型</param>
        /// <returns>操作字符串</returns>
        public static String GetSqlStr<T>(SqlType sqlType)
        {
            var mType = GetType<T>(sqlType);
            return mData[mType.Key][sqlType];
        }

        /// <summary>
        /// 获取自定义筛选的sql语句（只能用于GeDefinedList、GetDefinedCount）
        /// </summary>
        /// <param name="sqlType">数据库类型（只能用于GeDefinedList、GetDefinedCount）</param>
        /// <param name="paramObj">参数</param>
        /// <param name="pageNo">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="isLike">是否用like</param>
        /// <returns>操作字符串</returns>
        public static String GetDefinedSqlStr<T>(SqlType sqlType, T paramObj = null, Int32 pageNo = -1, Int32 pageSize = -1, Boolean isLike = true) where T : class
        {
            var mType = GetType<T>(sqlType);
            var andStr = isLike ? "like" : "=";

            // 处理参数
            var result = mData[mType.Key][sqlType];
            if (paramObj != null)
            {
                // 取得实体的所有字段
                Dictionary<String, Object> modelFields = JsonUtil.Deserialize<Dictionary<String, Object>>(JsonUtil.Serialize(paramObj));
                foreach (KeyValuePair<String, Object> field in modelFields)
                {
                    // 过滤空数据,bool类型，int类型，过滤时间（只支持字符串）
                    if (field.Value == null) continue;

                    var value = field.Value.ToString();
                    if (StringTool.IsDateTime(value)) continue;
                    if (String.IsNullOrEmpty(value.Trim(' '))) continue;

                    switch (value)
                    {
                        case "True":
                        case "False":
                        case "0":
                            break;
                        default:
                            result += $" and `{field.Key}` " + andStr + $" '%{value}%'";
                            break;
                    }

                }
            }

            // page参数添加
            if (pageNo != -1 && pageSize != -1)
            {
                result += $" Limit {(pageNo - 1) * pageSize},{pageSize}";
            }

            return result + ";";
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

                // 初始化语句
                mData[tableName][SqlType.GeDefinedList] = String.Format(GetDefinedListSqlStr, tableFiled, tableName);
                mData[tableName][SqlType.GetDefinedCount] = String.Format(GetDefinedCountSqlStr, tableName);

                mData[tableName][SqlType.GetAllList] = String.Format(GetAllListSqlStr, tableFiled, tableName);
                mData[tableName][SqlType.Insert] = String.Format(InsertSqlStr, tableName, tableFiled, tableParamFiled);
                //如果没有有主键，那么不能初始化GetList、Update、Delete
                if (HasPrimaryKey(type.Value))
                {
                    var whereStr = GetTableWhereStr(type.Value);
                    var setStr = GetTableSetStr(type.Value);

                    mData[tableName][SqlType.GetList] = String.Format(GetListSqlStr, tableFiled, tableName, whereStr);
                    mData[tableName][SqlType.Update] = String.Format(UpdateSqlStr, tableName, setStr, whereStr);
                    mData[tableName][SqlType.Delete] = String.Format(DeleteSqlStr, tableName, whereStr);

                }
                else
                {
                    mData[tableName][SqlType.GetList] = ErrorSqlStr;
                    mData[tableName][SqlType.Update] = ErrorSqlStr;
                    mData[tableName][SqlType.Delete] = ErrorSqlStr;
                }
            }
        }

        #endregion
    }
}
