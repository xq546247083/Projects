/************************************************************************
* 描述:home控制器
*************************************************************************/
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI;

namespace Manage
{
    using Manage.BLL;
    using Manage.Common;
    using Manage.Model;

    /// <summary>
    /// home控制器
    /// </summary>
    public class HomeController : Controller
    {
        #region Action

        [CustomAuthorize]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 登录页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Login(String returnUrl = null)
        {
            FormsAuthenticationService.SignOut();
            Session.Clear();
            return View(new UserViewModel() { ReturnUrl = returnUrl });
        }

        /// <summary>
        /// 验证码
        /// </summary>
        [OutputCache(Location = OutputCacheLocation.None)]
        public void VCode()
        {
            var code = new ValidateCode();
            var codeStr = code.CreateValidateCode(out var retInfo);
            Session["vcode"] = retInfo;
            code.CreateValidateGraphic(codeStr);
        }

        /// <summary>
        /// 登录
        /// </summary>
        [HttpPost]
        public ActionResult Login(UserViewModel model, String vcode)
        {
            String message = String.Empty;
            Boolean result = false;

            if (Session["vcode"] == null)
            {
                message = "验证码过期";
            }
            else
            {
                if (Session["vcode"].ToString() != vcode)
                {
                    message = "验证码错误";
                }
                else
                {
                    if (String.IsNullOrEmpty(model.UserName) || String.IsNullOrEmpty(model.UserPwd))
                    {
                        message = "请输入账号、密码！";
                    }
                    else
                    {
                        var loginUserByDB = GetUser(model.UserName, model.UserPwd);
                        if (loginUserByDB == null)
                        {
                            message = "请输入正确的账号、密码！";
                        }
                        else
                        {
                            if (loginUserByDB.Status != 0)
                            {
                                message = "您的帐号已被锁定，请联系管理员！";
                            }
                            else
                            {
                                var loginUser = ModelConvert(loginUserByDB);
                                var loginIp = Request.UserHostAddress;
                                UpdateLoginInfo(loginUserByDB, loginIp);
                                FormsAuthenticationService.SignIn(loginUser);

                                result = true;

                                //日志记录
                                ManageBaseBLL.Insert(new UserOperationLog
                                {
                                    UserID = loginUser.UserID,
                                    UserName = loginUser.UserName,
                                    OperationMothod = "Home.Login",
                                    OperationName = "系统登录",
                                    OperationData = "",
                                    Crdate = DateTime.Now
                                });
                            }
                        }
                    }
                    Session["vcode"] = String.Empty;
                }
            }

            // 如果登录成功
            if (result)
            {
                if (String.IsNullOrEmpty(model.ReturnUrl) || model.ReturnUrl.Trim() == "/")
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return Redirect(model.ReturnUrl);
                }
            }
            else
            {
                model.Message = message;
                model.UserPwd = String.Empty;
                return View("Login", model);
            }
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <returns></returns>
        [OperationLog("登出", "")]
        public ActionResult LoginOut()
        {
            //登出
            FormsAuthenticationService.SignOut();
            Session.Clear();
            return View("Login", new UserViewModel());
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 获取玩家
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        private static User GetUser(String userName, String pwd)
        {
            // 获取用户
            String userPwd = MD5Tool.MD5(pwd);
            var user = ManageBaseBLL.GetDefinedList(new User
            {
                UserName = userName,
                UserPwd = userPwd
            }).FirstOrDefault();

            return user;
        }

        /// <summary>
        /// 更新登录信息
        /// </summary>
        /// <param name="user">玩家</param>
        /// <param name="loginIp">登录ip</param>
        private static void UpdateLoginInfo(User user, String loginIp)
        {
            user.LastLoginIP = loginIp;
            user.LastLoginTime = DateTime.Now;

            ManageBaseBLL.Update(user);
        }

        /// <summary>
        /// 模型转化
        /// </summary>
        private User ModelConvert(UserViewModel model)
        {
            if (model == null)
            {
                return null;
            }

            return new User()
            {
                UserID = model.UserID,
                UserName = model.UserName,
                UserPwd = model.UserPwd
            };
        }

        /// <summary>
        /// 模型转化
        /// </summary>
        private UserViewModel ModelConvert(User model)
        {
            if (model == null)
            {
                return null;
            }

            var role = ManageBaseBLL.GetList(new Role { ID = model.UserRole }).FirstOrDefault();

            return new UserViewModel()
            {
                UserID = model.UserID,
                Status = model.Status,
                UserName = model.UserName,
                UserPwd = model.UserPwd,
                IfSuper = model.IfSuper,
                UserRole = model.UserRole,
                LastLoginIP = model.LastLoginIP,
                LastLoginTime = model.LastLoginTime,
                Crdate = model.LastLoginTime,

                UserRoleName = role == null ? "角色不存在" : role.RolesName,
                MenuId = role == null ? "" : role.Page,
            };
        }

        #endregion
    }
}