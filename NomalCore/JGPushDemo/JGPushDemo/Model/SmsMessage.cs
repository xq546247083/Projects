//***********************************************************************************
// 文件名称：SmsMessage.cs
// 功能描述：短信补充
// 数据表：
// 作者：xiaoqiang
// 日期：2018-4-26 16:21:46
//修改记录：
//***********************************************************************************
using System;

namespace JGPushDemo.Model
{
    using Newtonsoft.Json;

    /// <summary>
    /// 短信补充。
    /// </summary>
    public class SmsMessage
    {
        /// <summary>
        /// 内容
        /// </summary>
        [JsonProperty("content")]
        public String Content { get; set; }

        /// <summary>
        /// 延迟时间
        /// </summary>
        [JsonProperty("delay_time", DefaultValueHandling = DefaultValueHandling.Include)]
        public Int32 DelayTime { get; set; }
    }
}
