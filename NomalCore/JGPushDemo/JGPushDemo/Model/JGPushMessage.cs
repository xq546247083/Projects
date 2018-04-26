//***********************************************************************************
// 文件名称：JGPushMessage.cs
// 功能描述：极光推送消息
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
    /// 极光推送消息
    /// </summary>
    public class JGPushMessage
    {
        /// <summary>
        /// 消息ID
        /// </summary>
        [JsonProperty("cid", NullValueHandling = NullValueHandling.Ignore)]
        public String CId { get; set; }

        /// <summary>
        /// 推送平台。可以为 "android" / "ios" / "all"。
        /// </summary>
        [JsonProperty("platform", DefaultValueHandling = DefaultValueHandling.Include)]
        public Object Platform { get; set; } = "all";

        /// <summary>
        /// 设备
        /// </summary>
        [JsonProperty("audience", DefaultValueHandling = DefaultValueHandling.Include)]
        public Object Audience { get; set; } = "all";

        /// <summary>
        /// 通知
        /// </summary>
        [JsonProperty("notification", NullValueHandling = NullValueHandling.Ignore)]
        public Notification Notification { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        [JsonProperty("message", NullValueHandling = NullValueHandling.Ignore)]
        public Message Message { get; set; }

        /// <summary>
        /// 短信
        /// </summary>
        [JsonProperty("sms_message", NullValueHandling = NullValueHandling.Ignore)]
        public SmsMessage SMSMessage { get; set; }

        /// <summary>
        /// 设置
        /// </summary>
        [JsonProperty("options", DefaultValueHandling = DefaultValueHandling.Include)]
        public Options Options { get; set; } = new Options
        {
            IsApnsProduction = false
        };

        /// <summary>
        /// 序列化对象
        /// </summary>
        /// <returns></returns>
        internal String GetJson()
        {
            return JsonConvert.SerializeObject(this, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore
            });
        }

        /// <summary>
        /// 序列化对象
        /// </summary>
        /// <returns></returns>
        public override String ToString()
        {
            return GetJson();
        }
    }
}
