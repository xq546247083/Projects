//***********************************************************************************
// 给客户端推送消息工具
//***********************************************************************************
using SocketServer.Enum;
using System;

namespace SocketServer.BLL
{
    using SocketServer.Model;

    /// <summary>
    /// 给客户端推送消息工具
    /// </summary>
    public class PushTool
    {
        #region 方法

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="userID">玩家Id</param>
        /// <param name="cmd">返回值</param>
        /// <param name="code">状态值</param>
        /// <param name="data">数据</param>
        /// <param name="message">消息</param>
        public static void Send(String userID, ClientCmdEnum cmd, Int32 code, Object data, String message = "")
        {
            var returnObject = new ReturnObject { Cmd = cmd, Code = code, Data = data, Message = message };
            var connetion = ConnectionManager.GetConnection(userID);

            connetion?.SendData(returnObject);
        }

        #endregion
    }
}
