﻿//***********************************************************************************
// socket连接处理类
//***********************************************************************************
using System;
using System.Net;

namespace WebSocketServer
{
    using Tool.Common;
    using WebSocketSharp;
    using WebSocketSharp.Server;

    /// <summary>
    /// websocket 连接处理类，每个新连接均会新建一个此对象
    /// </summary>
    public class WebSocketConnection : WebSocketBehavior
    {
        #region 字段

        /// <summary>
        /// 链接是否打开
        /// </summary>
        private Boolean isOpen = false;

        /// <summary>
        /// 存活最长时间（单位：秒）
        /// </summary>
        private const Int32 MAX_KEEP_ALIVE_TIME = 5 * 60;

        /// <summary>
        /// 锁对象
        /// </summary>
        private Object lockObj = new Object();

        #endregion

        #region 属性

        /// <summary>
        /// 玩家Id
        /// </summary>
        public Guid PlayerID { get; private set; }

        /// <summary>
        /// 活跃时间
        /// </summary>
        public DateTime AliveTime { get; private set; }

        /// <summary>
        /// 客户端地址
        /// </summary>
        private IPEndPoint ClientIPEndPoint => new IPEndPoint(Context.UserEndPoint.Address, Context.UserEndPoint.Port);

        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public WebSocketConnection()
        {
        }

        #region 私有方法

        /// <summary>
        /// 连接打开时
        /// </summary>
        protected override void OnOpen()
        {
            this.isOpen = true;
            Log.Debug("收到新连接：" + ClientIPEndPoint);
        }

        /// <summary>
        /// 收到消息时
        /// </summary>
        /// <param name="e">事件参数信息</param>
        protected override void OnMessage(MessageEventArgs e)
        {
            KeepAlive();

            // 约定len(message) == 0,为心跳请求
            if (e.RawData.Length <= 0)
            {
                // 发送心跳回复
                Send(new Byte[] { });
                return;
            }

            try
            {
                var message = System.Text.Encoding.UTF8.GetString(e.RawData);
                HandleMessage(message);
            }
            catch (Exception ex)
            {
                Log.Error($"接受的数据处理异常:{ex}");
            }
        }

        /// <summary>
        /// 连接关闭处理
        /// </summary>
        /// <param name="e">事件参数</param>
        protected override void OnClose(CloseEventArgs e)
        {
            this.isOpen = false;
            Log.Debug($"连接关闭：Addr:{ClientIPEndPoint} Reason:{e.Reason}");

            base.OnClose(e);
        }

        /// <summary>
        /// socket错误时
        /// </summary>
        /// <param name="e">事件参数</param>
        protected override void OnError(WebSocketSharp.ErrorEventArgs e)
        {
            Log.Error($"Address:{ClientIPEndPoint} 出现未知异常：Message:{e.Message} Exception:{e.Exception}");
        }

        /// <summary>
        /// 处理消息
        /// </summary>
        /// <param name="message">消息</param>
        private void HandleMessage(String message)
        {

        }

        #endregion

        #region 方法

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="data">待发送的数据</param>
        public void SendData<T>(T data)
        {
            // 如果链接关闭，则直接丢掉数据
            if (isOpen == false)
            {
                return;
            }

            // 反序列化数据
            var jsonStr = JsonTool.Serialize(data);
            byte[] byteData = System.Text.Encoding.UTF8.GetBytes(jsonStr);

            // 锁住等待发送完成
            lock (lockObj)
            {
                Send(byteData);
            }
        }

        /// <summary>
        /// 活跃时间
        /// </summary>
        public void KeepAlive()
        {
            this.AliveTime = DateTime.Now;
        }

        /// <summary>
        /// 检查是否超时
        /// </summary>
        /// <returns>是否超时</returns>
        public Boolean CheckIfTimeout()
        {
            return DateTime.Now.AddSeconds(-1 * MAX_KEEP_ALIVE_TIME) > this.AliveTime;
        }

        #endregion
    }
}
