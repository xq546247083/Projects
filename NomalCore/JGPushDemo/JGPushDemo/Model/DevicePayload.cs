//***********************************************************************************
// 文件名称：DevicePayload.cs
// 功能描述：设备
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
    /// 设备
    /// </summary>
    public class DevicePayload
    {
        /// <summary>
        /// 别名
        /// </summary>
        [JsonProperty("alias")]
        public String Alias { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        [JsonProperty("mobile")]
        public String Mobile { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        [JsonProperty("tags")]
        public Dictionary<String, Object> Tags { get; set; }

        /// <summary>
        /// 序列化对象
        /// </summary>
        /// <returns></returns>
        private String GetJson()
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
