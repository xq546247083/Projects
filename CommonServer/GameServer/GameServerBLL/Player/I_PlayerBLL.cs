/************************************************************************
* 标题: 玩家类
* 描述: 玩家类
* 作者： 肖强
* 日期：2017-5-3 10:43:36
* 版本：V1
*************************************************************************/
using System;


namespace GameServer.BLL
{
    using Tool.CustomAttribute;

    /// <summary>
    /// 玩家类
    /// </summary>
    [InvokeClassAttribute("玩家类", "肖强", "2017-7-13 10:44:02")]
    public static partial class PlayerBLL
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="serverId">serverId</param>
        /// <param name="userId">userId</param>
        /// <param name="userPwd">userPwd</param>
        /// <param name="inputEncryptedString">inputEncryptedString</param>
        /// <param name="random">random</param>
        [MethodDescribe(
            "登录", "肖强", "2017-7-13 10:59:13",
            @"
[
]
            ",
            @"
[
]
            ")]
        public static void I_Login(String serverId, String userId, String userPwd, String inputEncryptedString, String random)
        {

        }
    }
}
