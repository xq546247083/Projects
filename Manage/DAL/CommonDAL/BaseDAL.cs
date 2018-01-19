/************************************************************************
* 描述: 操作数据库
*************************************************************************/
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Manage.DAL
{
    using MySql.Data.MySqlClient;

    /// <summary>
    ///操作数据库类
    /// </summary>
    public abstract class BaseDAL
    {
        #region 数据库操作方法

        /// <summary>
        /// 执行无返回值的数据库操作，仅返回受影响的行数
        /// </summary>
        /// <param name="conn">连接</param>
        /// <param name="commandText">sql语句</param>
        /// <param name="param">参数</param>
        /// <returns>受影响的行数</returns>
        protected static Int32 ExecuteNonQuery(String conn, String commandText, object param = null)
        {
            using (MySqlConnection connection = new MySqlConnection(conn))
            {
                return connection.Execute(commandText, param);
            }
        }

        /// <summary>
        /// 执行数据库操作，返回一个列表
        /// </summary>
        /// <param name="conn">连接</param>
        /// <param name="commandText">sql语句</param>
        /// <param name="param">参数</param>
        /// <returns>包含结果的数据表</returns>
        protected static List<T> ExecuteDataTable<T>(String conn, String commandText, object param = null)
        {
            using (MySqlConnection connection = new MySqlConnection(conn))
            {
                return connection.Query<T>(commandText, param).ToList();
            }
        }

        /// <summary>
        /// 执行sql语句，返回数量
        /// </summary>
        /// <param name="conn">连接</param>
        /// <param name="commandText">sql语句</param>
        /// <returns>受影响的行数</returns>
        protected static Object ExecuteScalar(String conn, String commandText)
        {
            using (MySqlConnection connection = new MySqlConnection(conn))
            {
                return connection.ExecuteScalar(commandText);
            }
        }

        #endregion
    }
}