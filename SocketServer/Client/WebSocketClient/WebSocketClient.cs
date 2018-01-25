//***********************************************************************************
// WebSocketServer客户端
//***********************************************************************************
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebSocketClient
{
    using WebSocketSharp;
    using Logg = Tool.Common.Log;

    /// <summary>
    /// WebSocketServer客户端
    /// </summary>
    public class WebSocketClient
    {
        #region 字段

        /// <summary>
        /// 客户端实例对象
        /// </summary>
        private static WebSocket mClientInstance = null;

        /// <summary>
        /// 是否开启
        /// </summary>
        private static Boolean isOpen = false;

        #endregion

        #region 方法

        /// <summary>
        /// 开启客户端
        /// </summary>
        /// <param name="addr">连接地址</param>
        public static void Start(String addr)
        {
            if (mClientInstance != null)
            {
                throw new Exception("客户端连接已开启");
            }

            // websocket 协议控制
            if (addr.ToLower().StartsWith("ws:") == false)
            {
                addr = String.Format("ws://{0}/client", addr.TrimStart('/'));
            }

            var client = new WebSocket(addr);
            client.OnOpen += Client_OnOpen; ;
            client.OnMessage += Client_OnMessage; ;
            client.OnClose += Client_OnClose; ;
            client.OnError += Client_OnError; ;
            client.Connect();

            mClientInstance = client;

            Task.Run(() => { SendHeartData(); });
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="message">消息</param>
        public static void Send(String message)
        {
            if (!isOpen)
            {
                throw new Exception("链接未开启");
            }

            mClientInstance.Send(message);
        }

        /// <summary>
        /// 停止客户端
        /// </summary>
        public static void Stop()
        {
            if (mClientInstance == null)
            {
                return;
            }

            isOpen = false;
            mClientInstance.Close();
            mClientInstance = null;
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 发送心跳数据
        /// </summary>
        private static void SendHeartData()
        {
            var message = Encoding.UTF8.GetString(new Byte[0]);
            while (isOpen)
            {
                Send(message);

                Thread.Sleep(5000);
            }
        }

        /// <summary>
        /// 建立连接
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private static void Client_OnOpen(object sender, EventArgs e)
        {
            isOpen = true;
            Console.WriteLine("客户端连接成功！");
        }

        /// <summary>
        /// 收到消息
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private static void Client_OnMessage(object sender, MessageEventArgs e)
        {
            var message = Encoding.UTF8.GetString(e.RawData);
            if (!String.IsNullOrEmpty(message))
            {
                Console.WriteLine("收到来自服务器的消息：" + message);
            }
        }

        /// <summary>
        /// 发生错误
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private static void Client_OnError(object sender, ErrorEventArgs e)
        {
            Logg.Error(e.ToString());
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private static void Client_OnClose(object sender, CloseEventArgs e)
        {
            isOpen = false;
        }

        #endregion  
    }
}
