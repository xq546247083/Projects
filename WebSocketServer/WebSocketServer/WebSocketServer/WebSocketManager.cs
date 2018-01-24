//***********************************************************************************
// WebSocket管理对象
//***********************************************************************************
using System;
using System.Collections.Generic;
using System.Threading;

namespace WebSocketServer
{
    using WebSocketSharp.Server;

    /// <summary>
    /// WebSocket管理对象
    /// </summary>
    public class WebSocketManager
    {
        #region 字段

        /// <summary>
        /// 连接适配器对象
        /// </summary>
        private static Dictionary<Guid, WebSocketConnection> mConnectionAdapterData = new Dictionary<Guid, WebSocketConnection>();

        /// <summary>
        /// 锁对象
        /// </summary>
        private static ReaderWriterLockSlim mLockObj = new ReaderWriterLockSlim();

        /// <summary>
        /// 服务实例对象
        /// </summary>
        private static WebSocketServer mServerInstance = null;

        #endregion

        #region 方法

        /// <summary>
        /// 开启服务
        /// </summary>
        /// <param name="addr">监听地址</param>
        /// <returns>服务开启后的实例对象</returns>
        public static WebSocketServer StartServer(String addr)
        {
            if (mServerInstance != null)
            {
                throw new Exception("服务已开启");
            }

            // websocket 协议控制
            if (addr.ToLower().StartsWith("ws:") == false)
            {
                addr = String.Format("ws://{0}", addr.TrimStart('/'));
            }

            var server = new WebSocketServer(addr);

            // 开启服务
            server.AddWebSocketService<WebSocketConnection>("/client");
            server.AllowForwardedRequest = true;
            server.Start();

            mServerInstance = server;

            return server;
        }

        /// <summary>
        /// 停止服务
        /// </summary>
        public static void StopServer()
        {
            if (mServerInstance == null)
            {
                return;
            }

            mServerInstance.Stop();

            mServerInstance = null;
        }

        #endregion
    }
}
