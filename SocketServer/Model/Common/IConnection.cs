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
        void SendData<T>(T data);
    }
}
