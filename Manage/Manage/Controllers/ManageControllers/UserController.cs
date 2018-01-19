/************************************************************************
* 描述:用户控制器
*************************************************************************/
using System;
using System.Linq;
using System.Web.Mvc;

namespace Manage
{
    using Manage.BLL;
    using Manage.Common;
    using Manage.Model;

    /// <summary>
    /// 用户管理
    /// </summary>
    [CustomAuthorize]
    public class UserController : Controller
    {
        #region Action

        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 查找
        /// </summary>
        /// <returns></returns>
        public ActionResult Search()
        {
            CommViewModel<UserViewModel> vmodel = new CommViewModel<UserViewModel>();
            return View(vmodel);
        }

        /// <summary>
        /// 请求数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult Data(CommViewModel<UserViewModel> model)
        {
            CommViewModel<UserViewModel> vModel = new CommViewModel<UserViewModel>();

            var searchModel = new User
            {
                UserName = Request["userName"]
            };

            vModel.ViewModelList = ManageBaseBLL.GetDefinedList(searchModel).ConvertAll(ModelConvert);

            Int32 count = vModel.ViewModelList.Count;
            vModel.PageIndex = model.PageIndex;
            vModel.PageCount = (Int32)Math.Ceiling((Double)count / model.PageSize);

            return View(vModel);
        }

        /// <summary>
        /// 新建
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            ViewBag.Action = "Create";
            ViewBag.Title = "添加管理员";

            CommViewModel<UserViewModel> vModel = new CommViewModel<UserViewModel>();

            return View(vModel);
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="model">用户模型</param>
        /// <param name="collection"></param>
        /// <returns>添加</returns>
        [HttpPost]
        [OperationLog("添加用户", "ViewModel.UserName|ViewModel.UserPwd|ViewModel.Status|ViewModel.UserRole")]
        public ActionResult Create(CommViewModel<UserViewModel> model, FormCollection collection)
        {
            ViewBag.Action = "Create";
            ViewBag.Title = "添加";

            Boolean ret = false;
            String msg = String.Empty;

            try
            {
                User user = new User()
                {
                    UserName = model.ViewModel.UserName,
                    UserPwd = model.ViewModel.UserPwd,
                    Status = model.ViewModel.Status,
                    IfSuper = false,
                    UserRole = model.ViewModel.UserRole,
                    Crdate = DateTime.Now,
                };

                if (Insert(user))
                {
                    ret = true;
                }

            }
            catch (Exception ex)
            {
                ret = false;
                msg = "错误信息:" + ex.Message;
            }

            return Json(new
            {
                Result = ret,
                Message = ret ? "添加成功\r\n" : "添加失败\r\n" + (String.IsNullOrEmpty(msg) ? "" : ":" + msg),
                Data = ""
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>删除结果</returns>
        [OperationLog("删除用户", "id")]
        public ActionResult Delete(Int32 id)
        {
            Boolean ret = false;
            String msg = String.Empty;

            try
            {
                if (ManageBaseBLL.Delete(new User() { UserID = id }) > 0)
                {
                    ret = true;
                }
            }
            catch (Exception ex)
            {
                ret = false;
                msg = "错误信息:" + ex.Message;
            }

            return Json(new
            {
                Result = ret,
                Message = ret ? "成功\r\n" : "失败\r\n" + (String.IsNullOrEmpty(msg) ? "" : ":" + msg),
                Data = ""
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="model">模型</param>
        /// <returns>结果</returns>
        public ActionResult Exist(CommViewModel<UserViewModel> model)
        {
            if (string.IsNullOrEmpty(model?.ViewModel?.UserName))
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            if (ManageBaseBLL.GetDefinedCount(new User { UserName = model.ViewModel.UserName }) < 1)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>结果</returns>
        public ActionResult Edit(Int32 id)
        {
            ViewBag.Action = "EditSubmit";
            ViewBag.Title = "修改";

            CommViewModel<UserViewModel> vModel = new CommViewModel<UserViewModel>();
            try
            {
                var user = ManageBaseBLL.GetList(new User() { UserID = id }).FirstOrDefault();
                if (user != null)
                {
                    vModel.ViewModel = ModelConvert(user);
                }
            }
            catch (Exception)
            {
                return View("Create", vModel);
            }

            return View("Create", vModel);
        }

        /// <summary>
        /// 编辑提交
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="collection"></param>
        /// <returns>提交结果</returns>
        [HttpPost]
        [OperationLog("修改用户", "ViewModel.UserRole|ViewModel.Status")]
        public ActionResult EditSubmit(CommViewModel<UserViewModel> model, FormCollection collection)
        {
            Boolean ret = false;
            String msg = String.Empty;

            try
            {
                var user = ManageBaseBLL.GetList(new User() { UserID = model.ViewModel.UserID }).FirstOrDefault();
                if (user != null)
                {
                    user.UserRole = model.ViewModel.UserRole;
                    user.Status = model.ViewModel.Status;

                    if (ManageBaseBLL.Update(user) > 0)
                    {
                        ret = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ret = false;
                msg = "错误信息:" + ex.Message;
            }

            return Json(new
            {
                Result = ret,
                Message = ret ? "修改成功\r\n" : "修改失败\r\n" + (String.IsNullOrEmpty(msg) ? "" : ":" + msg),
                Data = ""
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns>结果</returns>
        public ActionResult EditPwd()
        {
            UserViewModel userViewModel = FormsAuthenticationService.GetAuthenticatedUser();

            CommViewModel<UserViewModel> vmodel = new CommViewModel<UserViewModel>();

            if (userViewModel != null)
            {
                vmodel.ViewModel = ModelConvert(GetUserById(userViewModel.UserID));
            }

            return View(vmodel);
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="model">模型</param>
        /// <returns>返回</returns>
        [HttpPost]
        [OperationLog("修改用户密码", "ViewModel.UserName")]
        public ActionResult EditPwd(CommViewModel<UserViewModel> model)
        {
            Boolean ret = false;
            String msg = String.Empty;

            try
            {
                var cacheUser = FormsAuthenticationService.GetAuthenticatedUser();
                var oldUser = GetUserById(cacheUser.UserID);
                if (oldUser != null)
                {
                    String newPwd = MD5Tool.MD5(model.ViewModel.OldPassword);
                    if (oldUser.UserPwd != newPwd)
                    {
                        throw new Exception("旧密码不正确!");
                    }

                    oldUser.UserPwd = MD5Tool.MD5(model.ViewModel.NewPassword);

                    ret = ManageBaseBLL.Update(oldUser) > 0;

                    if (ret)
                    {
                        FormsAuthenticationService.SignOut();
                    }
                }
            }
            catch (Exception ex)
            {
                ret = false;
                msg = "错误信息:" + ex.Message;
            }

            return Json(new
            {
                Result = ret,
                Message = ret ? "修改成功\r\n" : "修改失败\r\n" + (String.IsNullOrEmpty(msg) ? "" : ":" + msg),
                Data = ""
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region 页面模型转换

        /// <summary>
        /// 通过有用户id获取用户
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>用户</returns>
        private static User GetUserById(Int32 id)
        {
            var user = ManageBaseBLL.GetList(new User { UserID = id }).FirstOrDefault();

            return user;
        }

        /// <summary>
        /// 插入玩家
        /// </summary>
        /// <param name="user">玩家</param>
        /// <returns>是否插入成功</returns>
        private static Boolean Insert(User user)
        {
            user.UserPwd = MD5Tool.MD5(user.UserPwd);

            return ManageBaseBLL.Insert(user) > 0;
        }

        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private User ModelConvert(UserViewModel model)
        {
            if (model == null)
            {
                return null;
            }

            return new User()
            {
                UserID = model.UserID,
                Status = model.Status,
                UserName = model.UserName,
                UserPwd = model.UserPwd,
                IfSuper = model.IfSuper,
                UserRole = model.UserRole,
                LastLoginIP = model.LastLoginIP,
                LastLoginTime = model.LastLoginTime,
                Crdate = model.LastLoginTime
            };
        }

        #endregion
    }
}
