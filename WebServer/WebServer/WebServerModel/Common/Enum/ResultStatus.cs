/************************************************************************
* 标题: 返回枚举
* 描述: 返回枚举
* 作者：肖强
* 日期：2017-5-3 10:43:36
* 版本：V1
*************************************************************************/

using System.ComponentModel;

namespace WebServer.Model
{
    /// <summary>
    /// 返回枚举
    /// </summary>
    public enum ResultStatus
    {
        /// <summary>
        /// 成功
        /// </summary>
        [Description("成功")]
        Success = 0,

        /// <summary>
        /// 异常
        /// </summary>
        [Description("服务器异常")]
        Exception = 1,

        /// <summary>
        /// 失败
        /// </summary>
        [Description("失败")]
        Fail = 2,

        /// <summary>
        /// 客户端数据错误
        /// </summary>
        [Description("客户端数据错误")]
        ClientDataError = 3,

        /// <summary>
        /// 参数不能为空
        /// </summary>
        [Description("参数不能为空")]
        NullParameter = 4,

        /// <summary>
        /// 参数不足
        /// </summary>
        [Description("参数不足")]
        ClientParamCountNoEnough = 5,

        /// <summary>
        /// 客户端参数错误
        /// </summary>
        [Description("客户端参数错误")]
        ClientParamTypeError =6,

        /// <summary>
        /// 登录超时
        /// </summary>
        [Description("登录超时")]
        LoginIsOverTime = 7,

        #region SysUser

        /// <summary>
        /// 密码错误
        /// </summary>
        [Description("密码错误")]
        PwdError = 10001,

        /// <summary>
        /// 用户不存在
        /// </summary>
        [Description("用户不存在")]
        UserIsNotExist = 10002,

        /// <summary>
        /// 用户名已被注册
        /// </summary>
        [Description("用户名已被注册")]
        UserNameIsExist = 10003,

        #endregion

    }
}
