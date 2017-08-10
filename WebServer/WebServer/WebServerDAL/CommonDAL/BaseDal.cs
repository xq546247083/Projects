/************************************************************************
* 标题: 操作数据库类
* 描述: 操作数据库
* 作者： 肖强
* 日期：2017-5-3 10:43:36
* 版本：V1
*************************************************************************/
using System;
using System.Data;

namespace WebServer.DAL
{
    using MySql.Data.MySqlClient;
    using Tool.Common;

    /// <summary>
    ///操作数据库类
    /// </summary>
    public abstract class BaseDal
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public static String Conn = WebConfig.CommonConnString;

        /// <summary>
        /// 执行无返回值的数据库操作，仅返回受影响的行数
        /// </summary>
        /// <param name="commandText">sql语句</param>
        /// <param name="commandParameters">参数</param>
        /// <returns>受影响的行数</returns>
        protected static Int32 ExecuteNonQuery(String commandText, params MySqlParameter[] commandParameters)
        {
            try
            {
                return MySqlHelper.ExecuteNonQuery(Conn, commandText, commandParameters);
            }
            catch (Exception ex)
            {
                throw new Exception(ExHandler.Handle(ex, commandText, commandParameters));
            }
        }

        /// <summary>
        /// 执行返回单个值的数据库操作
        /// </summary>
        /// <param name="commandText">sql语句</param>
        /// <param name="commandParameters">参数</param>
        /// <returns>返回单个值</returns>
        protected static Object ExecuteScalar(String commandText, params MySqlParameter[] commandParameters)
        {
            try
            {
                return MySqlHelper.ExecuteScalar(Conn, commandText, commandParameters);
            }
            catch (Exception ex)
            {
                throw new Exception(ExHandler.Handle(ex, commandText, commandParameters));
            }
        }

        /// <summary>
        /// 执行数据库操作，返回一个数据表
        /// </summary>
        /// <param name="commandText">sql语句</param>
        /// <param name="commandParameters">参数</param>
        /// <returns>包含结果的数据表</returns>
        protected static DataTable ExecuteDataTable(String commandText, params MySqlParameter[] commandParameters)
        {
            try
            {
                DataSet ds = MySqlHelper.ExecuteDataset(Conn, commandText, commandParameters);
                if (ds != null && ds.Tables.Count > 0)
                {
                    return ds.Tables[0];
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ExHandler.Handle(ex, commandText, commandParameters));
            }
        }
    }
}