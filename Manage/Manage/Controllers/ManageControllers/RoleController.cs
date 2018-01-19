/************************************************************************
* 描述:角色控制器
*************************************************************************/
using System;
using System.Linq;
using System.Web.Mvc;

namespace Manage
{
    using Manage.BLL;
    using Manage.Model;

    /// <summary>
    /// 角色管理
    /// </summary>
    [CustomAuthorize]
    public class RoleController : Controller
    {
        #region Action

        /// <summary>
        /// Index
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public ActionResult Search()
        {
            return View();
        }

        /// <summary>
        /// 数据查询
        /// </summary>
        /// <param name="serverGroupId"></param>
        /// <returns></returns>
        public ActionResult Data()
        {
            CommViewModel<RoleViewModel> vModel = new CommViewModel<RoleViewModel>();

            vModel.ViewModelList = ManageBaseBLL.GetList<Role>().ConvertAll(ModelConvert);

            return View(vModel);
        }

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            ViewBag.Action = "Create";
            ViewBag.Title = "添加";

            CommViewModel<RoleViewModel> vModel = new CommViewModel<RoleViewModel>();

            return View(vModel);
        }

        /// <summary>
        /// 添加角色 提交
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [OperationLog("添加角色", "Id|RolesName|Page|Remark")]
        public JsonResult Create(CommViewModel<RoleViewModel> model, FormCollection collection)
        {
            ViewBag.Action = "Create";
            ViewBag.Title = "添加";

            Boolean ret = false;
            String msg = String.Empty;

            try
            {
                Role role = new Role()
                {
                    ID = model.ViewModel.Id,
                    Page = model.ViewModel.Page,
                    RolesName = model.ViewModel.RolesName,
                    Remark = model.ViewModel.Remark
                };

                if (ManageBaseBLL.Insert<Role>(role) > 0)
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
        /// 删除角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationLog("删除角色", "id")]
        public JsonResult Delete(Int32 id)
        {
            Boolean ret = false;
            String msg = String.Empty;

            try
            {
                if (ManageBaseBLL.Delete<Role>(new Role() { ID = id }) > 0)
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
        /// 修改角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(Int32 id)
        {
            ViewBag.Action = "EditSubmit";
            ViewBag.Title = "修改";

            CommViewModel<RoleViewModel> vModel = new CommViewModel<RoleViewModel>();
            try
            {
                var role = ManageBaseBLL.GetList(new Role() { ID = id }).FirstOrDefault();
                if (role != null)
                {
                    vModel.ViewModel = ModelConvert(role);
                }
            }
            catch (Exception)
            {
                return View("Create", vModel);
            }

            return View("Create", vModel);
        }

        /// <summary>
        /// 修改角色 提交
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [OperationLog("修改角色", "ViewModel.Id|ViewModel.Page|ViewModel.RolesName|ViewModel.Remark")]
        public JsonResult EditSubmit(CommViewModel<RoleViewModel> model, FormCollection collection)
        {
            Boolean ret = false;
            String msg = String.Empty;

            try
            {
                var role = ManageBaseBLL.GetList(new Role() { ID = model.ViewModel.Id }).FirstOrDefault();
                if (role != null)
                {
                    role = new Role
                    {
                        ID = model.ViewModel.Id,
                        Page = model.ViewModel.Page,
                        RolesName = model.ViewModel.RolesName,
                        Remark = model.ViewModel.Remark
                    };

                    if (ManageBaseBLL.Update(role) > 0)
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
        /// 判断角色名是否存在
        /// </summary>        
        /// <returns></returns>
        public ActionResult Exist(CommViewModel<RoleViewModel> model)
        {
            if (model == null || model.ViewModel == null || string.IsNullOrEmpty(model.ViewModel.RolesName) || model.ViewModel.Id != 0)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            if (ManageBaseBLL.GetDefinedCount(new Role { RolesName = model.ViewModel.RolesName }, false) > 0)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region 页面模型转换

        private RoleViewModel ModelConvert(Role model)
        {
            if (model == null)
            {
                return null;
            }

            return new RoleViewModel()
            {
                Id = model.ID,
                Page = model.Page,
                Remark = model.Remark,
                RolesName = model.RolesName
            };
        }
        private Role ModelConvert(RoleViewModel model)
        {
            if (model == null)
            {
                return null;
            }

            return new Role()
            {
                ID = model.Id,
                Page = model.Page,
                Remark = model.Remark,
                RolesName = model.RolesName
            };
        }

        #endregion
    }
}
