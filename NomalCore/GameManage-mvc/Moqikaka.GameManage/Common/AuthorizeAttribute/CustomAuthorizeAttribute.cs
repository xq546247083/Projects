/************************************************************************
* 描述:登录权限验证
*************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Moqikaka.GameManage
{
    using Moqikaka.Util.Log;

    /// <summary>
    /// 登录权限验证
    /// </summary>
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// 角色列表
        /// </summary>
        public new String[] Roles { get; set; }

        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="httpContext">http上下文</param>
        /// <returns>验证是否通过</returns>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            String action = httpContext.Request.RequestContext.RouteData.Values["action"].ToString();
            String controller = httpContext.Request.RequestContext.RouteData.Values["controller"].ToString();

            var user = FormsAuthenticationService.GetAuthenticatedUser();
            if (user == null)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 在过程请求授权时调用
        /// </summary>
        /// <param name="filterContext">过滤上下文</param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            var user = FormsAuthenticationService.GetAuthenticatedUser();
            if (user == null)
            {
                return;
            }

            String action = filterContext.ActionDescriptor.ActionName;
            String controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;

            if (user.UserName.ToLower() != "admin")
            {
                //所有菜单
                var menusDic = MenuProvider.GetMenuDic();
                //当前访问的菜单
                var menusList = new List<MenuItem>();

                //登录用户都可以访问该页面 
                if (controller == "Home" && action == "Index")
                {
                    return;
                }

                foreach (var item in menusDic)
                {
                    if (item.Value.Controller == controller)
                    {
                        menusList.Add(item.Value);
                    }
                }

                if (menusList.Count == 0)
                {
                    filterContext.Result = new ContentResult() { Content = "此账号没有该权限" };

                    LogUtil.Write(user.UserName + "没有访问[" + controller + "/" + action + "]的权限", LogType.Warn);
                }
                else
                {
                    var menuPerList = user.MenuId.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    if (menusList.Where(p => !menuPerList.Contains(p.ID)).ToList().Count > 0)
                    {
                        filterContext.Result = new ContentResult() { Content = "此账号没有该权限." };
                        LogUtil.Write(user.UserName + "没有访问[" + controller + "/" + action + "]的权限", LogType.Warn);
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// 权限未通过处理
        /// </summary>
        /// <param name="filterContext">过滤上下文</param>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            String returnUrl = filterContext.HttpContext.Request.RawUrl;
            String redirectUrl = $"~/Home/Login?ReturnUrl={returnUrl}";
            filterContext.Result = new RedirectResult(redirectUrl, true);
        }
    }
}