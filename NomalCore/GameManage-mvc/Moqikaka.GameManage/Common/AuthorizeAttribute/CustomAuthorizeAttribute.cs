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
    public class CustomAuthorizeAttribute : AuthorizeAttribute, IExceptionFilter
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

        /// <summary>
        /// ajax发生异常时调用
        /// </summary>
        /// <param name="filterContext">错误上下文</param>
        public void OnException(ExceptionContext filterContext)
        {
            // 记录日志
            LogUtil.Error(filterContext.Exception.ToString());

            // 如果ajax请求，返回错误信息
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                // 组装结果
                var resultData = new
                {
                    Result = false,
                    Message = $"操作失败! 发生异常：{filterContext.Exception}",
                    Data = ""
                };

                // 返回
                filterContext.Result = new JsonResult()
                {
                    Data = resultData,
                    ContentType = null,
                    ContentEncoding = null,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };

                // 处理错误
                filterContext.ExceptionHandled = true;
            }
        }
    }
}