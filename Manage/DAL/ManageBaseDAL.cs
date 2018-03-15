/************************************************************************
* 描述: 后台数据库DAL
*************************************************************************/
using System;
using System.Collections.Generic;

namespace Manage.DAL
{
    using Manage.Common;

    /// <summary>
    /// 后台数据库DAL
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ManageBaseDAL<T> : BaseDAL where T : class
    {
        /// <summary>
        /// 连接
        /// </summary>
        private static String Conn => WebConfig.ManageConnString;

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="paramObj">条件模型</param>
        /// <param name="orderStr">排序字符串</param>
        /// <returns></returns>
        public static List<T> GetList(T paramObj = null)
        {
            // 如果有参数
            if (paramObj != null)
            {
                var sqlStr = SqlFactory.GetSqlStr<T>(SqlType.GetList);
                return ExecuteDataTable<T>(Conn, sqlStr, paramObj);
            }

            var sqlAllStr = SqlFactory.GetSqlStr<T>(SqlType.GetAllList);
            return ExecuteDataTable<T>(Conn, sqlAllStr);
        }

        /// <summary>
        /// 获取自定义筛选的数据(只支持字符串与数字筛选)
        /// </summary>
        /// <param name="paramObj">条件</param>
        /// <param name="pageNo">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="isLike">是否使用like查询</param>
        /// <param name="filterStr">自定义筛选字段</param>
        /// <param name="orderStr">排序字符串</param>
        /// <returns></returns>
        public static List<T> GetDefinedList(T paramObj, Int32 pageNo = -1, Int32 pageSize = -1, Boolean isLike = false, String filterStr = "", String orderStr = "")
        {
            var sqlStr = SqlFactory.GetDefinedSqlStr(SqlType.GeDefinedList, paramObj, pageNo, pageSize, isLike, filterStr, orderStr);
            return ExecuteDataTable<T>(Conn, sqlStr);
        }

        /// <summary>
        /// 获得自定义筛选的数据量(只支持字符串与数字筛选)
        /// </summary>
        /// <param name="paramObj">参数</param>
        /// <param name="isLike">是否使用like查询</param>
        /// <param name="filterStr">自定义筛选字段</param>
        /// <returns>受影响的函数</returns>
        public static Int32 GetDefinedCount(T paramObj, Boolean isLike = false, String filterStr = "")
        {
            var sqlStr = SqlFactory.GetDefinedSqlStr(SqlType.GetDefinedCount, paramObj, -1, -1, isLike, filterStr);
            return Convert.ToInt32(ExecuteScalar(Conn, sqlStr));
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="paramObj">参数</param>
        /// <returns>受影响的函数</returns>
        public static Int32 Insert(T paramObj)
        {
            var sqlStr = SqlFactory.GetSqlStr<T>(SqlType.Insert);
            return ExecuteNonQuery(Conn, sqlStr, paramObj);
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="paramObj">参数</param>
        /// <returns>受影响的函数</returns>
        public static Int32 Update(T paramObj)
        {
            var sqlStr = SqlFactory.GetSqlStr<T>(SqlType.Update);
            return ExecuteNonQuery(Conn, sqlStr, paramObj);
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="paramObj">参数</param>
        /// <returns>受影响的函数</returns>
        public static Int32 Delete(T paramObj)
        {
            var sqlStr = SqlFactory.GetSqlStr<T>(SqlType.Delete);
            return ExecuteNonQuery(Conn, sqlStr, paramObj);
        }
    }
}
