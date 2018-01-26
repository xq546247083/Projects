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
        SysUser_Login = 1,

        /// <summary>
        /// 广播消息
        /// </summary>
        SysUser_Broadcast = 2,

        /// <summary>
        /// 个人消息
        /// </summary>
        SysUser_Chat = 3,

        /// <summary>
        /// 退出
        /// </summary>
        SysUser_Logout=4,

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
    }
}
