//***********************************************************************************
// 连接接口
//***********************************************************************************

using System;

namespace SocketServer.Model
{
    /// <summary>
    /// 连接接口
    /// </summary>
    public interface IConnection
    {
        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="data">待发送的数据</param>
        void SendData(ReturnObject data);

        /// <summary>
        /// 检查是否超时
        /// </summary>
        /// <returns>是否超时</returns>
        Boolean CheckIfTimeout();

        /// <summary>
        /// 当前连接注册其用户
        /// </summary>
        /// <param name="userID"></param>
        void Register(String userID);

        /// <summary>
        /// 注销
        /// </summary>
        void UnRegister();
    }
}
