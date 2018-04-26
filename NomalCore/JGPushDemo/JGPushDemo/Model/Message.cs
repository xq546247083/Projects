//***********************************************************************************
// 文件名称：Message.cs
// 功能描述：推送消息
// 数据表：
// 作者：xiaoqiang
// 日期：2018-4-26 16:21:46
//修改记录：
//***********************************************************************************
using System;
using System.Collections;

namespace JGPushDemo.Model
{
    using Newtonsoft.Json;

    /// <summary>
    /// 推送消息
    /// </summary>
    public class Message
    {
        /// <summary>
        /// 标题
        /// </summary>
        [JsonProperty("title")]
        public String Title { get; set; }

        /// <summary>
        /// 消息内容本身（必填）。
        /// </summary>
        [JsonProperty("msg_content")]
        public String Content { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [JsonProperty("content_type")]
        public String ContentType { get; set; }

        /// <summary>
        /// 额外信息
        /// </summary>
        [JsonProperty("extras")]
        public IDictionary Extras { get; set; }
    }
}
