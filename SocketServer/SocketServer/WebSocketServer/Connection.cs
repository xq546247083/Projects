//***********************************************************************************
// socket连接处理类
//***********************************************************************************
using System;
using System.Net;
using System.Text;

namespace WebSocketServer
{
    using SocketServer.BLL;
    using SocketServer.Model;
    using Tool.Common;
    using WebSocketSharp;
    using WebSocketSharp.Server;
    using Logg = Tool.Common.Log;

    /// <summary>
    /// websocket 连接处理类，每个新连接均会新建一个此对象
    /// </summary>
    public class Connection : WebSocketBehavior, IConnection
    {
        #region 字段

        /// <summary>
        /// 存活最长时间（单位：秒）
        /// </summary>
        private const Int32 MAX_KEEP_ALIVE_TIME = 5 * 60;

        /// <summary>
        /// 锁对象
        /// </summary>
        private Object lockObj = new Object();

        /// <summary>
        /// 链接是否打开
        /// </summary>
        private Boolean isOpen = false;

        /// <summary>
        /// 客户端地址
        /// </summary>
        private IPEndPoint clientIPEndPoint = null;

        /// <summary>
        /// 玩家Id
        /// </summary>
        public String UserID { get; set; }

        /// <summary>
        /// 活跃时间
        /// </summary>
        private DateTime AliveTime { get; set; }

        #endregion

        #region 私有方法

        /// <summary>
        /// 连接打开时
        /// </summary>
        protected override void OnOpen()
        {
            isOpen = true;
            Logg.Debug("收到新连接：" + GetClientAddr());
        }

        /// <summary>
        /// 收到消息时
        /// </summary>
        /// <param name="e">事件参数信息</param>
        protected override void OnMessage(MessageEventArgs e)
        {
            // 约定len(message) == 0,为心跳请求
            if (e.RawData.Length <= 0)
            {
                // 发送心跳回复
                Send(new Byte[] { });
                return;
            }

            Logg.Debug($"收到来自【{GetClientAddr()}】的消息:{Encoding.UTF8.GetString(e.RawData)}");
            ConnectionManager.HandleMessage(this, e.RawData);
        }

        /// <summary>
        /// 连接关闭处理
        /// </summary>
        /// <param name="e">事件参数</param>
        protected override void OnClose(CloseEventArgs e)
        {
            this.isOpen = false;
            SysUserBLL.UpdateUserStatus(this.UserID, false);
            Logg.Debug($"连接关闭：Addr:{GetClientAddr()} Reason:{e.Reason}");

            base.OnClose(e);
        }

        /// <summary>
        /// socket错误时
        /// </summary>
        /// <param name="e">事件参数</param>
        protected override void OnError(ErrorEventArgs e)
        {
            Logg.Error($"Address:{GetClientAddr()} 出现未知异常：Message:{e.Message} Exception:{e.Exception}");
        }

        /// <summary>
        /// 获取客户端地址
        /// </summary>
        /// <returns></returns>
        private IPEndPoint GetClientAddr()
        {
            if (clientIPEndPoint == null)
            {
                clientIPEndPoint = new IPEndPoint(Context.UserEndPoint.Address, Context.UserEndPoint.Port);
            }

            return clientIPEndPoint;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="data">待发送的数据</param>
        public void SendData(ReturnObject data)
        {
            // 如果链接关闭，则直接丢掉数据
            if (isOpen == false)
            {
                return;
            }

            // 反序列化数据
            var jsonStr = JsonTool.Serialize(data); ;
            Logg.Debug($"给【{GetClientAddr()}】发送消息:{jsonStr}");
            byte[] byteData = Encoding.UTF8.GetBytes(jsonStr);

            // 锁住等待发送完成
            lock (lockObj)
            {
                Send(byteData);
            }
        }

        /// <summary>
        /// 保持活跃
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

        /// <summary>
        /// 当前连接注册其用户
        /// </summary>
        /// <param name="userID"></param>
        public void Register(String userID)
        {
            UserID = userID;
        }

        /// <summary>
        /// 注销
        /// </summary>
        public void UnRegister()
        {
            UserID = String.Empty;
        }

        #endregion
    }
}
