/************************************************************************
* 标题: 返回枚举
* 描述: 返回枚举
* 作者：肖强
* 日期：2017-5-3 10:43:36
* 版本：V1
*************************************************************************/

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
        Success = 0,

        /// <summary>
        /// 出现异常
        /// </summary>
        Exception = 1,

        /// <summary>
        /// 失败
        /// </summary>
        Fail = 2,

        /// <summary>
        /// 客户端数据错误
        /// </summary>
        ClientDataError = 3,

        /// <summary>
        /// 参数不能为空
        /// </summary>
        NullParameter = 4,

        /// <summary>
        /// 参数不足
        /// </summary>
        ClientParamCountNoEnough = 5,

        /// <summary>
        /// 客户端参数错误
        /// </summary>
        ClientParamTypeError=6,

        #region SysUser

        /// <summary>
        /// 密码错误
        /// </summary>
        PwdError = 10001,

        /// <summary>
        /// 用户不存在
        /// </summary>
        UserIsNotExist = 10002,

        #endregion

    }
}
