//***********************************************************************************
// 文件名称：Trigger.cs
// 功能描述：定期任务触发器
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
    /// 定期任务触发器。
    /// </summary>
    public class Trigger
    {
        /// <summary>
        /// 定期任务开始日期，必须为 24 小时制。
        /// 类似："2017-08-01 12:00:00"
        /// </summary>
        [JsonProperty("start")]
        public String StartDate { get; set; }

        /// <summary>
        /// 定期任务终止日期，必须为 24 小时制。
        /// 类似："2017-12-30 12:00:00"
        /// </summary>
        [JsonProperty("end")]
        public String EndDate { get; set; }

        /// <summary>
        /// 具体的触发时间。
        /// 类似："12:00:00"
        /// </summary>
        [JsonProperty("time")]
        public String TriggerTime { get; set; }

        /// <summary>
        /// 定期任务执行的最小时间单位。
        /// 必须为 "day" / "week" / "month" 中的一种。
        /// </summary>
        [JsonProperty("time_unit")]
        public String TimeUnit { get; set; }

        /// <summary>
        /// 定期任务的执行周期（目前最大支持 100）。
        /// 比如，当 TimeUnit 为 "day"，Frequency 为 2 时，表示每两天触发一次推送。
        /// </summary>
        [JsonProperty("frequency")]
        public Int32 Frequency { get; set; }

        /// <summary>
        /// 当 TimeUnit 为 "week" 或 "month"时，具体的时间表。
        /// - 如果 TimeUnit 为 "week": {"mon", "tue", "wed", "thu", "fri", "sat", "sun"};
        /// - 如果 TimeUnit 为 "month": {"01", "02"...};
        /// </summary>
        [JsonProperty("point")]
        public List<String> TimeList { get; set; }

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
