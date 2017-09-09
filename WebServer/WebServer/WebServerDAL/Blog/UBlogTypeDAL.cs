/************************************************************************
* 标题: u_blog_type的DAL
* 描述: u_blog_type的DAL
* 数据表:u_blog_type
* 作者：xiaoqiang
* 日期：2017/9/9 22:20:35
* 版本：V1.0
*************************************************************************/

using System;
using System.Data;

namespace WebServer.DAL
{
    using MySql.Data.MySqlClient;

    /// <summary>
    /// u_blog_type的DAL
    /// </summary>
    public class UBlogTypeDAL : BaseDal
    {
        #region 属性

        /// <summary>
        /// 数据库名
        /// </summary>
        private static readonly String tableName = "u_blog_type";

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
        public static DataTable GetList(Int32 iD)
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
        /// <returns>删除</returns>
        public static Int32 Delete(Int32 iD)
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
        /// <param name="name">类型名</param>
        /// <param name="icon">图标</param>
        /// <param name="isPublic">是否展示</param>
        /// <returns>更新</returns>
        public static Int32 Insert(Int32 iD, String name, String icon, Boolean isPublic)
        {
            MySqlParameter[] mySqlParameter = new MySqlParameter[]
            {
                new MySqlParameter(FiledConst.ID,iD),
                new MySqlParameter(FiledConst.Name,name),
                new MySqlParameter(FiledConst.Icon,icon),
                new MySqlParameter(FiledConst.IsPublic,isPublic),
            };

            return ExecuteNonQuery(SqlFactory.mData[tableName][SqlType.Insert], mySqlParameter);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="iD">主键</param>
        /// <param name="name">类型名</param>
        /// <param name="icon">图标</param>
        /// <param name="isPublic">是否展示</param>
        /// <returns>更新</returns>
        public static Int32 Update(Int32 iD, String name, String icon, Boolean isPublic)
        {
            MySqlParameter[] mySqlParameter = new MySqlParameter[]
            {
                new MySqlParameter(FiledConst.ID,iD),
                new MySqlParameter(FiledConst.Name,name),
                new MySqlParameter(FiledConst.Icon,icon),
                new MySqlParameter(FiledConst.IsPublic,isPublic),
            };

            return ExecuteNonQuery(SqlFactory.mData[tableName][SqlType.Update], mySqlParameter);
        }

        #endregion
    }
}
