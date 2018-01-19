/************************************************************************
* 描述:菜单
*************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Manage
{
    /// <summary>
    /// 菜单
    /// </summary>
    public static class Menu
    {
        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <returns></returns>
        public static MvcHtmlString CreateMenu(this HtmlHelper html, IEnumerable<MenuItem> menuItems, Func<String, String, String> menuUrl, Boolean collapseAll = false)
        {
            TagBuilder ul = new TagBuilder("ul");
            ul.AddCssClass("layui-nav layui-nav-tree");
            ul.Attributes["id"] = "gamenamage_Nav";
            ul.InnerHtml = RecursionMenuList(html, menuItems, menuUrl, collapseAll, out _);

            return MvcHtmlString.Create(ul.ToString());
        }

        /// <summary>
        /// 递归子菜单
        /// </summary>
        public static string RecursionMenuList(HtmlHelper html, IEnumerable<MenuItem> menuItems, Func<String, String, String> menuUrl, Boolean collapseAll, out Boolean isCollapse)
        {
            isCollapse = false;
            string menuHtml = string.Empty;

            foreach (var menu in menuItems)
            {
                var li = new TagBuilder("li");
                li.AddCssClass("layui-nav-item");

                var a = new TagBuilder("a");

                a.Attributes["class"] = menu.CSSClass;
                a.InnerHtml = menu.Text;
                a.Attributes["href"] = menuUrl(menu.Action, menu.Controller);

                if (menu.Items.Any())
                {
                    var childdl = new TagBuilder("dl");
                    childdl.AddCssClass("layui-nav-child");

                    foreach (var item in menu.Items)
                    {
                        var childdd = new TagBuilder("dd");
                        if (html.ViewContext.RouteData.Values["controller"].ToString() == item.Controller && html.ViewContext.RouteData.Values["action"].ToString() == item.Action || isCollapse)
                        {
                            li.AddCssClass("layui-nav-itemed");
                            childdd.AddCssClass("layui-this");
                        }

                        var childa = new TagBuilder("a");

                        childa.Attributes["class"] = item.CSSClass;
                        childa.Attributes["href"] = menuUrl(item.Action, item.Controller);
                        childa.InnerHtml = item.Text;

                        childdd.InnerHtml = childa.ToString();
                        childdl.InnerHtml += childdd.ToString();
                    }

                    li.InnerHtml = a + childdl.ToString();
                }
                else
                {
                    li.InnerHtml = a.ToString();
                }

                menuHtml += li.ToString();
            }

            return menuHtml;
        }

        /// <summary>
        /// z树
        /// </summary>
        /// <param name="helper">帮助</param>
        /// <param name="selectedMenuPerList">选择菜单</param>
        /// <returns>菜单</returns>
        public static MvcHtmlString TreeListData(this HtmlHelper helper, string selectedMenuPerList)
        {
            var seleid = selectedMenuPerList.Split(',');
            var menus = MenuProvider.GetMenu().ToList();

            StringBuilder sbmenus = new StringBuilder();
            foreach (var menu in menus)
            {
                sbmenus.Append("{id:\"" + menu.ID + "\",pId:\"0\",name:\"" + menu.Text + "\" ,checked:" + seleid.Contains(menu.ID).ToString().ToLower() + ", open:true},");
                foreach (var im2 in menu.Items)
                {
                    sbmenus.Append("{id:\"" + im2.ID + "\",pId:\"" + menu.ID + "\",name:\"" + im2.Text + "\" ,checked:" + seleid.Contains(im2.ID).ToString().ToLower() + " },");

                    foreach (var im3 in im2.Items)
                    {
                        sbmenus.Append("{id:\"" + im3.ID + "\",pId:\"" + im2.ID + "\",name:\"" + im3.Text + "\" ,checked:" + seleid.Contains(im3.ID).ToString().ToLower() + " },");
                    }
                }
            }

            return MvcHtmlString.Create(sbmenus.ToString().Trim(','));
        }
    }
}