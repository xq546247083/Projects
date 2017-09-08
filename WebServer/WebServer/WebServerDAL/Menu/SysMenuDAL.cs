/************************************************************************
* 标题: sys_menu的DAL
* 描述: sys_menu的DAL
* 数据表:sys_menu
* 作者：xiaoqiang
* 日期：2017/9/8 19:12:17
* 版本：V1.0
*************************************************************************/

using System;
using System.Data;

namespace WebServer.DAL
{
    using MySql.Data.MySqlClient;

    /// <summary>
    /// sys_menu的DAL
    /// </summary>
    public class SysMenuDAL : BaseDal
    {
        #region 属性

        /// <summary>
        /// 数据库名
        /// </summary>
        private static readonly String tableName = "sys_menu";

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
        /// <param name="menuID">菜单标识</param> 
        /// <returns>获取数据</returns>
        public static DataTable GetList(Int32 menuID)
        {
            MySqlParameter[] mySqlParameter = new MySqlParameter[]
            {
                new MySqlParameter(FiledConst.MenuID,menuID),
            };

            return ExecuteDataTable(SqlFactory.mData[tableName][SqlType.GetList], mySqlParameter);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="menuID">菜单标识</param>
        /// <returns>删除</returns>
        public static Int32 Delete(Int32 menuID)
        {
            MySqlParameter[] mySqlParameter = new MySqlParameter[]
            {
                new MySqlParameter(FiledConst.MenuID,menuID),
            };

            return ExecuteNonQuery(SqlFactory.mData[tableName][SqlType.Delete], mySqlParameter);
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="menuID">菜单标识</param>
        /// <param name="parentMenuID">上级ID</param>
        /// <param name="menuName">菜单名称</param>
        /// <param name="menuUrl">菜单地址</param>
        /// <param name="menuLevel">菜单层级</param>
        /// <param name="sortOrder">排序号</param>
        /// <param name="menuIcon">菜单图标路径（未用到）</param>
        /// <param name="bigMenuIcon">常用菜单图标（未用到）</param>
        /// <param name="shortCut">快捷键（未用到）</param>
        /// <param name="isShow">是否显示</param>
        /// <returns>更新</returns>
        public static Int32 Insert(Int32 menuID, Int32 parentMenuID, String menuName, String menuUrl, Int32 menuLevel, Int32 sortOrder, String menuIcon, String bigMenuIcon, String shortCut, Boolean isShow)
        {
            MySqlParameter[] mySqlParameter = new MySqlParameter[]
            {
                new MySqlParameter(FiledConst.MenuID,menuID),
                new MySqlParameter(FiledConst.ParentMenuID,parentMenuID),
                new MySqlParameter(FiledConst.MenuName,menuName),
                new MySqlParameter(FiledConst.MenuUrl,menuUrl),
                new MySqlParameter(FiledConst.MenuLevel,menuLevel),
                new MySqlParameter(FiledConst.SortOrder,sortOrder),
                new MySqlParameter(FiledConst.MenuIcon,menuIcon),
                new MySqlParameter(FiledConst.BigMenuIcon,bigMenuIcon),
                new MySqlParameter(FiledConst.ShortCut,shortCut),
                new MySqlParameter(FiledConst.IsShow,isShow),
            };

            return ExecuteNonQuery(SqlFactory.mData[tableName][SqlType.Insert], mySqlParameter);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="menuID">菜单标识</param>
        /// <param name="parentMenuID">上级ID</param>
        /// <param name="menuName">菜单名称</param>
        /// <param name="menuUrl">菜单地址</param>
        /// <param name="menuLevel">菜单层级</param>
        /// <param name="sortOrder">排序号</param>
        /// <param name="menuIcon">菜单图标路径（未用到）</param>
        /// <param name="bigMenuIcon">常用菜单图标（未用到）</param>
        /// <param name="shortCut">快捷键（未用到）</param>
        /// <param name="isShow">是否显示</param>
        /// <returns>更新</returns>
        public static Int32 Update(Int32 menuID, Int32 parentMenuID, String menuName, String menuUrl, Int32 menuLevel, Int32 sortOrder, String menuIcon, String bigMenuIcon, String shortCut, Boolean isShow)
        {
            MySqlParameter[] mySqlParameter = new MySqlParameter[]
            {
                new MySqlParameter(FiledConst.MenuID,menuID),
                new MySqlParameter(FiledConst.ParentMenuID,parentMenuID),
                new MySqlParameter(FiledConst.MenuName,menuName),
                new MySqlParameter(FiledConst.MenuUrl,menuUrl),
                new MySqlParameter(FiledConst.MenuLevel,menuLevel),
                new MySqlParameter(FiledConst.SortOrder,sortOrder),
                new MySqlParameter(FiledConst.MenuIcon,menuIcon),
                new MySqlParameter(FiledConst.BigMenuIcon,bigMenuIcon),
                new MySqlParameter(FiledConst.ShortCut,shortCut),
                new MySqlParameter(FiledConst.IsShow,isShow),
            };

            return ExecuteNonQuery(SqlFactory.mData[tableName][SqlType.Update], mySqlParameter);
        }

        #endregion
    }
}
