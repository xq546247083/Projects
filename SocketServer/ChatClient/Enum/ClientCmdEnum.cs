//***********************************************************************************
// 客户端命令
//***********************************************************************************

namespace ChatClient
{
    /// <summary>
    /// 发客户端命令
    /// </summary>
    public enum ClientCmdEnum
    {
        /// <summary>
        /// 登录
        /// </summary>
        SysUserLogin = 1,

        /// <summary>
        /// 广播消息
        /// </summary>
        SysUserBroadcast = 2,

        /// <summary>
        /// 个人消息
        /// </summary>
        SysUserChat = 3,

        /// <summary>
        /// 退出
        /// </summary>
        SysUserLogout = 4,

        /// <summary>
        /// 推送登录了
        /// </summary>
        Push_Login = 10001,

        /// <summary>
        /// 推送广播消息
        /// </summary>
        Push_Broadcast = 10002,

        /// <summary>
        /// 推送个人消息
        /// </summary>
        Push_Chat = 10003,

        /// <summary>
        /// 推送退出
        /// </summary>
        Push_Logout = 10004,

        /// <summary>
        /// 客户端连接
        /// </summary>
        Connect = 5,
    }
}
