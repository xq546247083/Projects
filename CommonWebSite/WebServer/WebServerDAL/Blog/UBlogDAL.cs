/************************************************************************
* 标题: 博客表的DAL
* 描述: 博客表的DAL
* 数据表:u_blog
* 作者：xiaoqiang
* 日期：2017/9/23 2:04:16
* 版本：V1.0
*************************************************************************/

using System;
using System.Data;

namespace WebServer.DAL
{
    using MySql.Data.MySqlClient;

    /// <summary>
    /// 博客表的DAL
    /// </summary>
    public class UBlogDAL : BaseDal
    {
        #region 属性

        /// <summary>
        /// 数据库名
        /// </summary>
        private static readonly String tableName = "u_blog";

        #endregion

        #region 方法

        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns>获取所有数据</returns>
        public static DataTable GetAllList()
        {
            return ExecuteDataTable(SqlFactory.mData[tableName][SqlType.GetAllList]);
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="iD">主键</param>
        /// <returns>获取数据</returns>
        public static DataTable GetList(Guid iD)
        {
            MySqlParameter[] mySqlParameter = new MySqlParameter[]
            {
                new MySqlParameter(FiledConst.ID,iD),
            };

            return ExecuteDataTable(SqlFactory.mData[tableName][SqlType.GetList], mySqlParameter);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="iD">主键</param>
        /// <returns>受影响的行数</returns>
        public static Int32 Delete(Guid iD)
        {
            MySqlParameter[] mySqlParameter = new MySqlParameter[]
            {
                new MySqlParameter(FiledConst.ID,iD),
            };

            return ExecuteNonQuery(SqlFactory.mData[tableName][SqlType.Delete], mySqlParameter);
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="iD">主键</param>
        /// <param name="userId">用户id</param>
        /// <param name="title">标题</param>
        /// <param name="content">内容</param>
        /// <param name="tag">标签（用，号隔开）</param>
        /// <param name="aTUsers">@的用户</param>
        /// <param name="blogType">博客类型</param>
        /// <param name="status">状态【0：草稿，1：正常，2：删除，3：彻底删除】</param>
        /// <param name="crDate">创建时间</param>
        /// <param name="reDate">更新时间</param>
        /// <returns>受影响的行数</returns>
        public static Int32 Insert(Guid iD, Guid userId, String title, String content, String tag, String aTUsers, Int32 blogType, Byte status, DateTime crDate, DateTime reDate)
        {
            MySqlParameter[] mySqlParameter = new MySqlParameter[]
            {
                new MySqlParameter(FiledConst.ID,iD),
                new MySqlParameter(FiledConst.UserId,userId),
                new MySqlParameter(FiledConst.Title,title),
                new MySqlParameter(FiledConst.Content,content),
                new MySqlParameter(FiledConst.Tag,tag),
                new MySqlParameter(FiledConst.ATUsers,aTUsers),
                new MySqlParameter(FiledConst.BlogType,blogType),
                new MySqlParameter(FiledConst.Status,status),
                new MySqlParameter(FiledConst.CrDate,crDate),
                new MySqlParameter(FiledConst.ReDate,reDate),
            };

            return ExecuteNonQuery(SqlFactory.mData[tableName][SqlType.Insert], mySqlParameter);
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="iD">主键</param>
        /// <param name="userId">用户id</param>
        /// <param name="title">标题</param>
        /// <param name="content">内容</param>
        /// <param name="tag">标签（用，号隔开）</param>
        /// <param name="aTUsers">@的用户</param>
        /// <param name="blogType">博客类型</param>
        /// <param name="status">状态【0：草稿，1：正常，2：删除，3：彻底删除】</param>
        /// <param name="crDate">创建时间</param>
        /// <param name="reDate">更新时间</param>
        /// <returns>受影响的行数</returns>
        public static Int32 Update(Guid iD, Guid userId, String title, String content, String tag, String aTUsers, Int32 blogType, Byte status, DateTime crDate, DateTime reDate)
        {
            MySqlParameter[] mySqlParameter = new MySqlParameter[]
            {
                new MySqlParameter(FiledConst.ID,iD),
                new MySqlParameter(FiledConst.UserId,userId),
                new MySqlParameter(FiledConst.Title,title),
                new MySqlParameter(FiledConst.Content,content),
                new MySqlParameter(FiledConst.Tag,tag),
                new MySqlParameter(FiledConst.ATUsers,aTUsers),
                new MySqlParameter(FiledConst.BlogType,blogType),
                new MySqlParameter(FiledConst.Status,status),
                new MySqlParameter(FiledConst.CrDate,crDate),
                new MySqlParameter(FiledConst.ReDate,reDate),
            };

            return ExecuteNonQuery(SqlFactory.mData[tableName][SqlType.Update], mySqlParameter);
        }

        /// <summary>
        /// 查询记录数
        /// </summary>
        /// <returns>获取所有数据</returns>
        public static Int32 Count()
        {
            return ExecuteNonQuery(SqlFactory.mData[tableName][SqlType.GetCount]);
        }

        #endregion
    }
}