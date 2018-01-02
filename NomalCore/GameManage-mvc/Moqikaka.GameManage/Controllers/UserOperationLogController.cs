/************************************************************************
* 描述:用户操作日志管理
*************************************************************************/
using System;
using System.Web.Mvc;

namespace Moqikaka.GameManage.Controllers
{
    using Moqikaka.GameManage.BLL;
    using Moqikaka.GameManage.Model;

    /// <summary>
    /// 用户操作日志管理
    /// </summary>
    [CustomAuthorize]
    public class UserOperationLogController : Controller
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
        /// 搜索
        /// </summary>
        /// <returns></returns>
        public ActionResult Search()
        {
            CommViewModel<UserOperationLogViewModel> vmodel = new CommViewModel<UserOperationLogViewModel>();
            vmodel.PageSize = 20;

            return View(vmodel);
        }

        /// <summary>
        /// 数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult Data(CommViewModel<UserViewModel> model)
        {
            CommViewModel<UserOperationLogViewModel> vModel = new CommViewModel<UserOperationLogViewModel>();
            Int32 userId = 0;
            Int32.TryParse(Request["userId"], out userId);

            var searchModel = new UserOperationLog
            {
                UserID = userId
            };

            vModel.ViewModelList = GameManageBaseBLL.GetDefinedList(searchModel, model.PageIndex, model.PageSize, false).ConvertAll(ModelConvert);

            Int32 count = GameManageBaseBLL.GetDefinedCount(searchModel, false);
            vModel.PageIndex = model.PageIndex;
            vModel.PageSize = model.PageSize;
            vModel.PageCount = (Int32)Math.Ceiling((Double)count / model.PageSize);

            return View(vModel);
        }

        #endregion

        #region 页面模型转换

        private UserOperationLogViewModel ModelConvert(UserOperationLog model)
        {
            if (model == null)
            {
                return null;
            }

            return new UserOperationLogViewModel()
            {
                ID = model.ID,
                UserID = model.UserID,
                UserName = model.UserName,
                OperationName = model.OperationName,
                OperationMothod = model.OperationMothod,
                OperationData = model.OperationData,
                Crdate = model.Crdate
            };
        }
        private UserOperationLog ModelConvert(UserOperationLogViewModel model)
        {
            if (model == null)
            {
                return null;
            }

            return new UserOperationLog()
            {
                ID = model.ID,
                UserID = model.UserID,
                UserName = model.UserName,
                OperationName = model.OperationName,
                OperationMothod = model.OperationMothod,
                OperationData = model.OperationData,
                Crdate = model.Crdate
            };
        }

        #endregion
    }
}