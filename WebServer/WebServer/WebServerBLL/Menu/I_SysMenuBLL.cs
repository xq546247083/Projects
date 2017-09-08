/************************************************************************
* 标题: 菜单类
* 描述: 菜单类
* 作者： 肖强
* 日期：2017-5-3 10:43:36
* 版本：V1
*************************************************************************/
using System;

namespace WebServer.BLL
{
    using Tool.CustomAttribute;
    using WebServer.Model;

    /// <summary>
    /// 菜单类
    /// </summary>
    [InvokeClassAttribute("菜单类", "肖强", "2017-7-13 10:44:02")]
    public partial class SysMenuBLL
    {
        /// <summary>
        /// 获取菜单信息
        /// </summary>
        /// <param name="userName">userName</param>
        [MethodDescribe(
            "获取菜单信息", "肖强", "2017-7-13 10:59:13",
@"{
    UserName:用户名
}           ",
@"[
    MenuScript:菜单信息
]            ")]
        public static ResponseDataObject I_GetInfo(String userName)
        {
            ResponseDataObject result = new ResponseDataObject() { ResultStatus = ResultStatus.Fail };

            #region 检测请求

            SysUser sysUser = SysUserBLL.GetItemByUserNameOrEmail(userName);
            if (sysUser == null)
            {
                result.ResultStatus = ResultStatus.UserIsNotExist;
                return result;
            }

            #endregion

            #region 处理返回

            result.ResultStatus = ResultStatus.Success;
            result.Value = AssembleToClient(sysUser);

            return result;

            #endregion
        }
    }
}
