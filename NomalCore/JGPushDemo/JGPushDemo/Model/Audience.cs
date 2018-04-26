//***********************************************************************************
// 文件名称：Audience.cs
// 功能描述：推送目标
// 数据表：
// 作者：xiaoqiang
// 日期：2018-4-26 16:21:46
//修改记录：
//***********************************************************************************
using System;
using System.Collections.Generic;

namespace JGPushDemo.Model
{
    using Newtonsoft.Json;

    /// <summary>
    /// 推送目标。
    /// </summary>
    public class Audience
    {
        /// <summary>
        /// 多个标签之间取并集（OR）。
        /// 每次最多推送 20 个。
        /// </summary>
        [JsonProperty("tag", NullValueHandling = NullValueHandling.Ignore)]
        public List<String> Tag { get; set; }

        /// <summary>
        /// 多个标签之间取交集（AND）。
        /// 每次最多推送 20 个。
        /// </summary>
        [JsonProperty("tag_and", NullValueHandling = NullValueHandling.Ignore)]
        public List<String> TagAnd { get; set; }

        /// <summary>
        /// 多个标签之间，先取并集，再对结果取补集。
        /// 每次最多推送 20 个。
        /// </summary>
        [JsonProperty("tag_not", NullValueHandling = NullValueHandling.Ignore)]
        public List<String> TagNot { get; set; }

        /// <summary>
        /// 多个别名之间取并集（OR）。
        /// 每次最多同时推送 1000 个。
        /// </summary>
        [JsonProperty("alias", NullValueHandling = NullValueHandling.Ignore)]
        public List<String> Alias { get; set; }

        /// <summary>
        /// 多个 registration id 之间取并集（OR）。
        /// 每次最多同时推送 1000 个。
        /// </summary>
        [JsonProperty("registration_id", NullValueHandling = NullValueHandling.Ignore)]
        public List<String> RegistrationID { get; set; }

        /// <summary>
        /// 在页面创建的用户分群 ID。
        /// 目前一次只能推送一个。
        /// </summary>
        [JsonProperty("segment", NullValueHandling = NullValueHandling.Ignore)]
        public List<String> Segment { get; set; }

        /// <summary>
        /// 在页面创建的 A/B 测试 ID。
        /// 目前一次只能推送一个。
        /// </summary>
        [JsonProperty("abtest", NullValueHandling = NullValueHandling.Ignore)]
        public List<String> Abtest { get; set; }
    }
}
