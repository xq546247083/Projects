/************************************************************************
* 描述: 后台推送信息到聊天服务器log
*************************************************************************/
using System;

namespace Moqikaka.GameManage.Model
{
    using Moqikaka.GameManage.Common;

    /// <summary>
    /// 后台推送信息到聊天服务器log
    /// </summary>
    [DataBaseTable("system_push_to_chat_log")]
    public sealed class SystemPushToChatLog
    {
        #region 属性

        /// <summary>
        /// id
        /// </summary>
        [PrimaryKey]
        public Int32 ID { get; set; }

        /// <summary>
        /// 推送到的服务器
        /// </summary>
        public String PushServers { get; set; }

        /// <summary>
        /// 推送的消息
        /// </summary>
        public String PushMsg { get; set; }

        /// <summary>
        /// 推送次数
        /// </summary>
        public Byte PushNum { get; set; }

        /// <summary>
        /// 推送者
        /// </summary>
        public String OpUser { get; set; }

        /// <summary>
        /// 推送起始时间
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 推送结束时间
        /// </summary>
        public DateTime EndDate { get; set; }

        #endregion
    }
}