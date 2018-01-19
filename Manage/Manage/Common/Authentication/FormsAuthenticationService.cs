/************************************************************************
* 描述:验证服务
*************************************************************************/
using System;
using System.Web;
using System.Web.Security;

namespace Manage
{
    /// <summary>
    /// 身份验证实现
    /// </summary>
    public static class FormsAuthenticationService
    {
        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="user"></param>
        /// <param name="createPersistentCookie"></param>
        public static void SignIn(UserViewModel user, Boolean createPersistentCookie = true)
        {
            #region Session

            HttpContext.Current.Session["LoginUserName"] = user.UserName;
            HttpContext.Current.Session["LoginUserId"] = user.UserID;
            HttpContext.Current.Session["LoginUserData"] = user.MenuId;

            HttpContext.Current.Session.Timeout = 30;

            #endregion
        }

        /// <summary>
        /// 登出
        /// </summary>
        public static void SignOut()
        {
            FormsAuthentication.SignOut();
            HttpContext.Current.Session.Clear();
        }

        /// <summary>
        /// 获取验证的用户
        /// </summary>
        /// <returns></returns>
        public static UserViewModel GetAuthenticatedUser()
        {
            #region session

            var loginuserid = HttpContext.Current.Session["LoginUserId"];
            var loginname = HttpContext.Current.Session["LoginUserName"];
            var loginuserdata = HttpContext.Current.Session["LoginUserData"];

            if (loginname != null && loginuserid != null && loginuserdata != null)
            {
                var user = new UserViewModel() { UserID = Convert.ToInt32(loginuserid), UserName = loginname.ToString(), MenuId = loginuserdata.ToString() };

                return user;
            }

            return null;

            #endregion
        }
    }
}