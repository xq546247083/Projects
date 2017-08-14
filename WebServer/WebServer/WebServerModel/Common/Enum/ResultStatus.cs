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

        /// <summary>
        /// 用户名必须以字母开头
        /// </summary>
        [Description("用户名必须以字母开头")]
        UserNameMustBeginWithLetter = 10004,

        /// <summary>
        /// 用户名只能由字母和数字构成
        /// </summary>
        [Description("用户名只能由字母和数字构成")]
        UserNameMustBeLetterOrNum = 10005,

        /// <summary>
        /// 密码不能为空
        /// </summary>
        [Description("密码不能为空")]
        UserPasswordCanBeNotEmpty = 10006,

        /// <summary>
        /// 邮箱不能为空
        /// </summary>
        [Description("邮箱不能为空")]
        EmailCanBeNotEmpty = 10007,

        /// <summary>
        /// 邮箱格式错误
        /// </summary>
        [Description("邮箱格式错误")]
        EmailStyleIsError = 10008,

        /// <summary>
        /// 邮箱已被注册
        /// </summary>
        [Description("邮箱已被注册")]
        EmailAlreadyExist = 10009,

        /// <summary>
        /// 用户名不能为空
        /// </summary>
        [Description("用户名不能为空")]
        UserNameCantBeEmpty = 10010,

        /// <summary>
        /// 电话号码格式错误
        /// </summary>
        [Description("电话号码格式错误")]
        PhoneStyleIsError = 10011,

        /// <summary>
        /// 请输入密码
        /// </summary>
        [Description("请输入密码")]
        PlsEnterPassword = 10011,

        /// <summary>
        /// 请输入验证码
        /// </summary>
        [Description("请输入验证码")]
        PlsEnterIdentifyCode = 10012,

        /// <summary>
        /// 该邮箱还未发送验证码
        /// </summary>
        [Description("该邮箱还未发送验证码")]
        IdentifyCodeNoThisEmail = 10013,

        /// <summary>
        /// 验证码错误
        /// </summary>
        [Description("验证码错误")]
        IdentifyCodeIsError = 10014,

        /// <summary>
        /// 发送邮件失败，请检查邮箱
        /// </summary>
        [Description("发送邮件失败，请检查邮箱")]
        SendEmailFail = 10015,

        #endregion

    }
}
