/************************************************************************
* 聊天消息
*************************************************************************/

using System;

namespace ChatClient
{
    /// <summary>
    /// 消息
    /// </summary>
    public class Msg
    {
        /// <summary>
        /// 来自玩家
        /// </summary>
        public String FromUserID { set; get; }

        /// <summary>
        /// 消息
        /// </summary>
        public String Message { set; get; }
    }
}
