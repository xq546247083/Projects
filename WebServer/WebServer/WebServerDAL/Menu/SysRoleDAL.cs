/************************************************************************
* 标题: sys_role的DAL
* 描述: sys_role的DAL
* 数据表:sys_role
* 作者：xiaoqiang
* 日期：2017/9/8 19:24:58
* 版本：V1.0
*************************************************************************/

using System;
using System.Data;

namespace WebServer.DAL
{
    using MySql.Data.MySqlClient;

    /// <summary>
    /// sys_role的DAL
    /// </summary>
    public class SysRoleDAL : BaseDal
    {
        #region 属性

        /// <summary>
        /// 数据库名
        /// </summary>
        private static readonly String tableName = "sys_role";

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
        /// <param name="roleID">主键</param> 
        /// <returns>获取数据</returns>
        public static DataTable GetList(Int32 roleID)
        {
            MySqlParameter[] mySqlParameter = new MySqlParameter[]
            {
                new MySqlParameter(FiledConst.RoleID,roleID),
            };

            return ExecuteDataTable(SqlFactory.mData[tableName][SqlType.GetList], mySqlParameter);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="roleID">主键</param>
        /// <returns>删除</returns>
        public static Int32 Delete(Int32 roleID)
        {
            MySqlParameter[] mySqlParameter = new MySqlParameter[]
            {
                new MySqlParameter(FiledConst.RoleID,roleID),
            };

            return ExecuteNonQuery(SqlFactory.mData[tableName][SqlType.Delete], mySqlParameter);
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="roleID">主键</param>
        /// <param name="roleName">角色名称</param>
        /// <param name="menuIDS">菜单id（用,隔开）</param>
        /// <param name="isDefault">是否默认角色</param>
        /// <param name="isSupper">是否是超级管理员角色</param>
        /// <param name="notes">描述</param>
        /// <returns>更新</returns>
        public static Int32 Insert(Int32 roleID, String roleName, String menuIDS, Boolean isDefault, Boolean isSupper, String notes)
        {
            MySqlParameter[] mySqlParameter = new MySqlParameter[]
            {
                new MySqlParameter(FiledConst.RoleID,roleID),
                new MySqlParameter(FiledConst.RoleName,roleName),
                new MySqlParameter(FiledConst.MenuIDS,menuIDS),
                new MySqlParameter(FiledConst.IsDefault,isDefault),
                new MySqlParameter(FiledConst.IsSupper,isSupper),
                new MySqlParameter(FiledConst.Notes,notes),
            };

            return ExecuteNonQuery(SqlFactory.mData[tableName][SqlType.Insert], mySqlParameter);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="roleID">主键</param>
        /// <param name="roleName">角色名称</param>
        /// <param name="menuIDS">菜单id（用,隔开）</param>
        /// <param name="isDefault">是否默认角色</param>
        /// <param name="isSupper">是否是超级管理员角色</param>
        /// <param name="notes">描述</param>
        /// <returns>更新</returns>
        public static Int32 Update(Int32 roleID, String roleName, String menuIDS, Boolean isDefault, Boolean isSupper, String notes)
        {
            MySqlParameter[] mySqlParameter = new MySqlParameter[]
            {
                new MySqlParameter(FiledConst.RoleID,roleID),
                new MySqlParameter(FiledConst.RoleName,roleName),
                new MySqlParameter(FiledConst.MenuIDS,menuIDS),
                new MySqlParameter(FiledConst.IsDefault,isDefault),
                new MySqlParameter(FiledConst.IsSupper,isSupper),
                new MySqlParameter(FiledConst.Notes,notes),
            };

            return ExecuteNonQuery(SqlFactory.mData[tableName][SqlType.Update], mySqlParameter);
        }

        #endregion
    }
}
