//***********************************************************************************
// WebSocketClient客户端
//***********************************************************************************
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatClient
{
    using Tool.Common;
    using WebSocketSharp;

    /// <summary>
    /// WebSocketClient客户端
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

        /// <summary>
        /// 处理消息事件
        /// </summary>
        public static Action<ReturnObject> HandleMessage = null;

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
                return;
            }

            // websocket 协议控制
            if (addr.ToLower().StartsWith("ws:") == false)
            {
                addr = $"ws://{addr.TrimStart('/')}/client";
            }

            var client = new WebSocket(addr);
            client.OnOpen += Client_OnOpen;
            client.OnMessage += Client_OnMessage;
            client.OnClose += Client_OnClose;
            client.OnError += Client_OnError;
            client.Connect();

            mClientInstance = client;

            Task.Run(() => { SendHeartData(); });
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="cmd">命令</param>
        /// <param name="request">请求数据</param>
        public static void Send(ClientCmdEnum cmd, Dictionary<String, Object> request)
        {
            if (!isOpen)
            {
                throw new Exception("链接未开启");
            }

            mClientInstance.Send($"Api={cmd}&{RequestTool.DicToString(request)}");
        }

        /// <summary>
        /// 停止客户端
        /// </summary>
        public static void Stop()
        {
            mClientInstance?.Close();
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
                mClientInstance.Send(message);

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
            HandleMessage?.Invoke(new ReturnObject() { Code = 0, Cmd = ClientCmdEnum.Connect, });
            Log.Info("连接成功！");
        }

        /// <summary>
        /// 收到消息
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private static void Client_OnMessage(object sender, MessageEventArgs e)
        {
            var message = Encoding.UTF8.GetString(e.RawData);
            if (String.IsNullOrEmpty(message))
            {
                return;
            }

            // 处理消息
            Log.Info($"收到服务器消息：{message}");
            try
            {
                HandleMessage?.Invoke(JsonTool.Deserialize<ReturnObject>(message));
            }
            catch (Exception ex)
            {
                Log.Error($"消息处理错误:{ex}");
            }
        }

        /// <summary>
        /// 发生错误
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private static void Client_OnError(object sender, ErrorEventArgs e)
        {
            isOpen = false;
            Log.Error(e.ToString());
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private static void Client_OnClose(object sender, CloseEventArgs e)
        {
            Log.Info("连接关闭！");

            HandleMessage?.Invoke(new ReturnObject() { Code = -1, Cmd = ClientCmdEnum.Connect, Message = "连接服务器失败！" });
            isOpen = false;
            mClientInstance = null;
        }

        #endregion  
    }
}
