/************************************************************************
* 标题: 菜单类
* 描述: 菜单类
* 作者： 肖强
* 日期：2017-7-17 15:38:04
* 版本：V1
*************************************************************************/
using System;
using System.Collections.Generic;

namespace WebServer.BLL
{
    using System.Text;
    using WebServer.DAL;
    using WebServer.Model;

    /// <summary>
    /// 菜单类
    /// </summary>
    public partial class SysMenuBLL : IInit
    {
        #region 属性

        /// <summary>
        /// 类名
        /// </summary>
        private const String mClassName = "SysMenuBLL";

        /// <summary>
        /// 菜单数据集合
        /// key:菜单id
        /// value:菜单对象
        /// </summary>
        private static Dictionary<Int32, SysMenu> mData = new Dictionary<Int32, SysMenu>();

        #endregion

        #region 初始化

        /// <summary>
        /// 初始化数据
        /// </summary>
        public void Init()
        {
            //查询数据
            var dataList = BaseModelDal.ExecuteQuery<SysMenu>();
            foreach (var dr in dataList)
            {
                mData[dr.MenuID] = dr;
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns>数据</returns>
        public static Dictionary<Int32, SysMenu> GetData()
        {
            return mData;
        }

        /// <summary>
        /// 获取某一个菜单
        /// </summary>
        /// <param name="sysMenuId">菜单id</param>
        /// <param name="ifCastException">是否抛出异常</param>
        /// <returns>菜单</returns>
        public static SysMenu GetItem(Int32 sysMenuId, Boolean ifCastException = false)
        {
            if (GetData().ContainsKey(sysMenuId))
            {
                return mData[sysMenuId];
            }

            if (ifCastException)
            {
                throw new SelfDefinedException(ResultStatus.Exception, String.Format("SysMenu未找到menuId为{0}的菜单", sysMenuId));
            }

            return null;
        }

        /// <summary>
        /// 更新菜单数据
        /// </summary>
        /// <param name="sysMenu">用户</param>
        /// <returns>用户</returns>
        public static void Update(SysMenu sysMenu)
        {
            SysMenuDAL.Update(sysMenu.MenuID, sysMenu.ParentMenuID, sysMenu.MenuName, sysMenu.MenuUrl, sysMenu.MenuLevel, sysMenu.SortOrder, sysMenu.MenuIcon, sysMenu.BigMenuIcon, sysMenu.ShortCut, sysMenu.IsShow);
        }

        /// <summary>
        /// 插入菜单数据
        /// </summary>
        /// <param name="sysMenu">用户</param>
        /// <returns>用户</returns>
        public static void Insert(SysMenu sysMenu)
        {
            SysMenuDAL.Insert(sysMenu.MenuID, sysMenu.ParentMenuID, sysMenu.MenuName, sysMenu.MenuUrl, sysMenu.MenuLevel, sysMenu.SortOrder, sysMenu.MenuIcon, sysMenu.BigMenuIcon, sysMenu.ShortCut, sysMenu.IsShow);
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 获取用户的菜单信息
        /// </summary>
        /// <param name="sysUser">用户</param>
        /// <returns>菜单信息</returns>
        private static string GetMenuScript(SysUser sysUser)
        {
            //第一层级字符串,无子元素
            var strOneA = "<li><a class=\"J_menuItem\" href=\"{0}\"><i class=\"{1}\"></i><span class=\"nav-label\">{2}</span></a></li>";
            //第一层级字符串,有子元素
            var strOneB = "<li><a class=\"J_menuItem\" href=\"{0}\"><i class=\"{1}\"></i><span class=\"nav-label\">{2}</span><span class=\"fa arrow\"></span></a></li>";


            StringBuilder sb = new StringBuilder();
            //添加主页
            sb.Append(String.Format(strOneA, "/Main/Menu/menu.html", "fa fa-home","菜单"));

            return sb.ToString();
        }

        #endregion

        #region 组装客户端数据

        /// <summary>
        /// 组装客户端数据
        /// </summary>
        /// <param name="sysUser">玩家对象</param>
        /// <returns>客户端数据</returns>
        public static Dictionary<String, Object> AssembleToClient(SysUser sysUser)
        {
            Dictionary<String, Object> clientInfo = new Dictionary<String, Object>();
            clientInfo[PropertyConst.MenuScript] = GetMenuScript(sysUser);

            return clientInfo;
        }

        #endregion
    }
}
