/************************************************************************
* 标题: 菜单类
* 描述: 菜单类
* 作者： 肖强
* 日期：2017-7-17 15:38:04
* 版本：V1
*************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace WebServer.BLL
{
    using Tool.Common;
    using WebServer.DAL;
    using WebServer.Model;

    /// <summary>
    /// 菜单类
    /// </summary>
    public partial class SysMenuBLL : IInit
    {
        #region 属性


        /// <summary>
        /// 图标
        /// </summary>
        private const String strIcon = "<i class=\"{0}\"></i>";

        /// <summary>
        /// strUl
        /// </summary>
        private const String strUl = "<ul class=\"nav nav-second-level collapse\">{0}</ul>";

        /// <summary>
        /// 无子元素
        /// </summary>
        private const String strNode = "<li><a class=\"J_menuItem\" href=\"{0}\">{1}<span class=\"nav-label\">{2}</span></a></li>";

        /// <summary>
        /// 有子元素
        /// </summary>
        private const String strFNode = "<li><a href=\"#\">{0}<span class=\"nav-label\">{1}</span><span class=\"fa arrow\"></span></a>{2}</li>";

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
        /// 获取玩家菜单列表
        /// </summary>
        /// <param name="sysUser">用户对象</param>
        /// <returns>菜单列表</returns>
        public static List<SysMenu> GetMenuByUser(SysUser sysUser)
        {
            var menuInfoList = new List<SysMenu>();

            //用户角色id列表
            var roleIdList = StringTool.SplitToIntList(sysUser.RoleIDs);
            foreach (var roleId in roleIdList)
            {
                //获取玩家角色
                var roleInfo = SysRoleBLL.GetItem(roleId);
                if (roleInfo == null)
                {
                    continue;
                }

                //获取玩家的所有菜单列表
                var menuIdList = StringTool.SplitToIntList(roleInfo.MenuIDS);
                foreach (var menuID in menuIdList)
                {
                    //给角色菜单添加菜单信息
                    var menuInfo = GetItem(menuID);
                    if (menuInfo == null)
                    {
                        continue;
                    }

                    if (!menuInfoList.Contains(menuInfo))
                    {
                        menuInfoList.Add(menuInfo);
                    }
                }
            }

            return menuInfoList;
        }

        /// <summary>
        /// 更新菜单数据
        /// </summary>
        /// <param name="sysMenu">用户</param>
        /// <returns>用户</returns>
        public static void Update(SysMenu sysMenu)
        {
            SysMenuDAL.Update(sysMenu.MenuID, sysMenu.ParentMenuID, sysMenu.MenuName, sysMenu.MenuUrl, sysMenu.SortOrder, sysMenu.MenuIcon, sysMenu.BigMenuIcon, sysMenu.ShortCut, sysMenu.IsShow);
        }

        /// <summary>
        /// 插入菜单数据
        /// </summary>
        /// <param name="sysMenu">用户</param>
        /// <returns>用户</returns>
        public static void Insert(SysMenu sysMenu)
        {
            SysMenuDAL.Insert(sysMenu.MenuID, sysMenu.ParentMenuID, sysMenu.MenuName, sysMenu.MenuUrl, sysMenu.SortOrder, sysMenu.MenuIcon, sysMenu.BigMenuIcon, sysMenu.ShortCut, sysMenu.IsShow);
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 获取用户的菜单信息
        /// </summary>
        /// <param name="sysUser">用户</param>
        /// <returns>菜单信息</returns>
        private static String GetMenuScript(SysUser sysUser)
        {
            //循环玩家菜单信息，给玩家添加页面
            var menuInfoList = GetMenuByUser(sysUser);
            if (menuInfoList == null)
            {
                return null;
            }

            var mainMenuInfoList = menuInfoList.Where(r => r.ParentMenuID == 0).OrderBy(r=>r.SortOrder).ToList();

            return AppendMenuScript(mainMenuInfoList, menuInfoList);
        }

        /// <summary>
        /// 追加菜单元素
        /// </summary>
        /// <param name="menuList">当前的菜单列表</param>
        /// <param name="allMenuList">所有的菜单列表</param>
        /// <returns>菜单字符串</returns>
        private static String AppendMenuScript(List<SysMenu> menuList, List<SysMenu> allMenuList)
        {
            StringBuilder result = new StringBuilder();

            foreach (var menuInfo in menuList)
            {
                //如果没有地址，则认为有子页面，如果有，则认为没有子页面
                if (String.IsNullOrEmpty(menuInfo.MenuUrl))
                {
                    //追加子页面
                    var mainMenuInfoList = allMenuList.Where(r => r.ParentMenuID == menuInfo.MenuID).OrderBy(r => r.SortOrder).ToList();
                    var sonStr = AppendMenuScript(mainMenuInfoList, allMenuList);

                    var iconStr = "";
                    if (!String.IsNullOrEmpty(menuInfo.MenuIcon))
                    {
                        iconStr = String.Format(strIcon, menuInfo.MenuIcon);
                    }

                    var ulStr = "";
                    if (!string.IsNullOrEmpty(sonStr))
                    {
                        ulStr = String.Format(strUl, sonStr);
                    }
                    result.Append(String.Format(strFNode, iconStr, menuInfo.MenuName, ulStr));
                }
                else
                {
                    var iconStr = "";
                    if (!String.IsNullOrEmpty(menuInfo.MenuIcon))
                    {
                        iconStr = String.Format(strIcon, menuInfo.MenuIcon);
                    }

                    result.Append(String.Format(strNode, menuInfo.MenuUrl, iconStr, menuInfo.MenuName));
                }
            }

            return result.ToString();
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
