/************************************************************************
* 标题: 返回枚举
* 描述: 返回枚举
* 作者：肖强
* 日期：2017-5-3 10:43:36
* 版本：V1
*************************************************************************/

namespace GameServer.Model
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

        #region player
        
        /// <summary>
        /// 密码错误
        /// </summary>
        PwdError=10001,

        #endregion

    }
}
