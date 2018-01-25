//***********************************************************************************
// 给客户端推送消息工具
//***********************************************************************************
using System;

namespace SocketServer.BLL
{
    /// <summary>
    /// 给客户端推送消息工具
    /// </summary>
    public class PushTool
    {
        #region 方法

        /// <summary>
        /// 增加连接
        /// </summary>
        /// <param name="userID">玩家Id</param>
        /// <param name="data">要推送的数据</param>
        public static void Send<T>(Guid userID, T data)
        {
            var connetion = ConnectionManager.GetConnection(userID);
            connetion?.SendData(data);
        }

        #endregion
    }
}
