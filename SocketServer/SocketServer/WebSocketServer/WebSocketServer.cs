//***********************************************************************************
// WebSocketServer服务器
//***********************************************************************************
using System;

namespace WebSocketServer
{
    using WebSocketSharpServer = WebSocketSharp.Server;

    /// <summary>
    /// WebSocketServer服务器
    /// </summary>
    public class WebSocketServer
    {
        #region 字段

        /// <summary>
        /// 服务实例对象
        /// </summary>
        private static WebSocketSharpServer.WebSocketServer mServerInstance = null;

        #endregion

        #region 方法

        /// <summary>
        /// 开启服务
        /// </summary>
        /// <param name="addr">监听地址</param>
        /// <returns>服务开启后的实例对象</returns>
        public static void Start(String addr)
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

            var server = new WebSocketSharpServer.WebSocketServer(addr);

            // 开启服务
            server.AddWebSocketService<Connection>("/client");
            server.AllowForwardedRequest = true;
            server.Start();

            mServerInstance = server;
        }

        /// <summary>
        /// 停止服务
        /// </summary>
        public static void Stop()
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
