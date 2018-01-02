/************************************************************************
* 描述:菜单提供者
*************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Moqikaka.GameManage
{
    using Moqikaka.GameManage.Model;
    using Moqikaka.GameManage.BLL;

    /// <summary>
    /// 菜单提供者
    /// </summary>
    public class MenuProvider
    {
        private static int menuCacheTime = 60;

        /// <summary>
        /// 菜单列表
        /// </summary>
        private static Dictionary<String, MenuItem> menusDic = new Dictionary<String, MenuItem>();

        #region Public Methods

        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<MenuItem> GetMenu()
        {
            String menuPath = HttpContext.Current.Server.MapPath("~/App_Data/Menu.xml");
            IEnumerable<MenuItem> menuItems = new List<MenuItem>();
            String cacheName = "UI.Menu";

            // 自定义菜单 需要重新加载菜单
            if (MemoryCacheManager.IsSet(cacheName))
            {
                menuItems = MemoryCacheManager.Get<IEnumerable<MenuItem>>(cacheName);
            }
            else
            {
                try
                {
                    XElement menuElement = XElement.Load(menuPath);
                    menusDic = new Dictionary<String, MenuItem>();
                    menuItems = RecursionMenu(menuElement);
                }
                catch (Exception)
                {
                }

                if (menuItems.Any())
                {
                    MemoryCacheManager.Set(cacheName, menuItems, menuCacheTime);
                }
            }

            return menuItems;
        }

        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <returns></returns>
        public static Dictionary<String, MenuItem> GetMenuDic()
        {
            if (menusDic.Count == 0)
            {
                GetMenu();
            }

            return menusDic;
        }

        /// <summary>
        /// 根据指定ID获取菜单
        /// </summary>
        /// <param name="selectedMenuPerList"></param>
        /// <returns></returns>
        public static List<MenuItem> GetMenuFromSelected(String selectedMenuPerList)
        {
            if (String.IsNullOrWhiteSpace(selectedMenuPerList))
            {
                return new List<MenuItem>();
            }

            String[] menuIDS = selectedMenuPerList.Split(new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            return RecursionMenuForSelected(GetMenu().ToList(), menuIDS);
        }

        /// <summary>
        /// 获取当前登录用户菜单
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<MenuItem> GetMenuFromCurrentLoginUser()
        {
            IEnumerable<MenuItem> menus = new List<MenuItem>();
            var ticketUser = FormsAuthenticationService.GetAuthenticatedUser();
            if (ticketUser == null)
            {
                return menus;
            }

            var loginUser = GameManageBaseBLL.GetDefinedList(new User { UserID = ticketUser.UserID, UserName = ticketUser.UserName }).FirstOrDefault();
            if (loginUser != null)
            {
                if (loginUser.IfSuper)
                {
                    menus = GetMenu();
                }
                else
                {
                    if (!String.IsNullOrEmpty(ticketUser.MenuId))
                    {
                        menus = GetMenuFromSelected(ticketUser.MenuId);
                    }
                }
            }

            return menus;
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// 递归菜单XML
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="menuElement"></param>
        /// <returns></returns>
        private static IEnumerable<MenuItem> RecursionMenu(XElement menuElement, MenuItem parent = null)
        {
            List<MenuItem> listMenuItem = new List<MenuItem>();
            var menus = menuElement.Elements("MenuItem");
            MenuItem item = new MenuItem();
            foreach (var p in menus)
            {
                item = new MenuItem();
                item.ID = p.Attribute("ID")?.Value.Trim();
                item.Text = p.Attribute("Text")?.Value.Trim();
                item.Controller = p.Attribute("Controller")?.Value.Trim();
                item.Action = p.Attribute("Action")?.Value.Trim();
                if (p.Attribute("ChildAction") != null)
                {
                    item.ChildAction = p.Attribute("ChildAction")?.Value.Trim();
                }

                item.Items = RecursionMenu(p, item);
                if (p.Attribute("CSSClass") != null)
                {
                    item.CSSClass = p.Attribute("CSSClass")?.Value.Trim();
                }
                item.Parent = parent;
                listMenuItem.Add(item);
                if (!menusDic.ContainsKey(item.ID))
                {
                    menusDic.Add(item.ID, item);
                }
            }

            return listMenuItem;
        }

        /// <summary>
        /// 递归选中菜单
        /// </summary>
        private static List<MenuItem> RecursionMenuForSelected(List<MenuItem> menus, String[] menuIDS)
        {
            List<MenuItem> list = new List<MenuItem>();
            foreach (var menu in menus)
            {
                if (menuIDS.Contains(menu.ID))
                {
                    var item = new MenuItem()
                    {
                        ID = menu.ID,
                        Text = menu.Text,
                        Action = menu.Action,
                        Controller = menu.Controller,
                        CSSClass = menu.CSSClass,
                        Items = RecursionMenuForSelected(menu.Items.ToList(), menuIDS)
                    };
                    list.Add(item);
                }
            }

            return list;
        }

        #endregion
    }
}