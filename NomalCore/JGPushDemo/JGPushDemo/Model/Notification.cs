//***********************************************************************************
// 文件名称：Notification.cs
// 功能描述：内容提示
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
    /// 内容提示
    /// </summary>
    public class Notification
    {
        /// <summary>
        /// 通知内容
        /// </summary>
        [JsonProperty("alert")]
        public String Alert { get; set; }

        /// <summary>
        /// 安卓
        /// </summary>
        [JsonProperty("android", NullValueHandling = NullValueHandling.Ignore)]
        public Android Android { get; set; }

        /// <summary>
        /// ios
        /// </summary>
        [JsonProperty("ios", NullValueHandling = NullValueHandling.Ignore)]
        public IOS IOS { get; set; }
    }

    /// <summary>
    /// 安卓
    /// </summary>
    public class Android
    {
        /// <summary>
        /// 通知内容
        /// </summary>
        [JsonProperty("alert")]
        public String Alert { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public String Title { get; set; }

        /// <summary>
        /// 通知栏样式ID
        /// </summary>
        [JsonProperty("builder_id", NullValueHandling = NullValueHandling.Ignore)]
        public Int32? BuilderId { get; set; }

        /// <summary>
        /// 通知栏展示优先级
        /// </summary>
        [JsonProperty("priority", NullValueHandling = NullValueHandling.Ignore)]
        public Int32? Priority { get; set; }

        /// <summary>
        /// 通知栏条目过滤或排序
        /// </summary>
        [JsonProperty("category", NullValueHandling = NullValueHandling.Ignore)]
        public String Category { get; set; }

        /// <summary>
        /// 通知栏样式类型
        /// </summary>
        [JsonProperty("style", NullValueHandling = NullValueHandling.Ignore)]
        public Int32? Style { get; set; }

        /// <summary>
        /// 通知提醒方式
        /// </summary>
        [JsonProperty("alert_type", NullValueHandling = NullValueHandling.Ignore)]
        public Int32? AlertType { get; set; }

        /// <summary>
        /// 大文本通知栏样式
        /// </summary>
        [JsonProperty("big_text", NullValueHandling = NullValueHandling.Ignore)]
        public String BigText { get; set; }

        /// <summary>
        /// 文本条目通知栏样式
        /// </summary>
        [JsonProperty("inbox", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<String, Object> Inbox { get; set; }

        /// <summary>
        /// 大图片通知栏样式
        /// </summary>
        [JsonProperty("big_pic_path", NullValueHandling = NullValueHandling.Ignore)]
        public String BigPicturePath { get; set; }

        /// <summary>
        /// 扩展字段
        /// </summary>
        [JsonProperty("extras", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<String, Object> Extras { get; set; }

        // ------------------------------VIP only-------------------------------

        /// <summary>
        /// (VIP only)指定开发者想要打开的 Activity，值为 <activity> 节点的 "android:name" 属性值。
        /// </summary>
        [JsonProperty("url_activity", NullValueHandling = NullValueHandling.Ignore)]
        public String URLActivity { get; set; }

        /// <summary>
        /// (VIP only)指定打开 Activity 的方式，值为 Intent.java 中预定义的 "access flags" 的取值范围。
        /// </summary>
        [JsonProperty("url_flag", NullValueHandling = NullValueHandling.Ignore)]
        public String URLFlag { get; set; }

        /// <summary>
        /// (VIP only)指定开发者想要打开的 Activity，值为 <activity> -> <intent-filter> -> <action> 节点中的 "android:name" 属性值。
        /// </summary>
        [JsonProperty("uri_action", NullValueHandling = NullValueHandling.Ignore)]
        public String URIAction { get; set; }

        // ------------------------------VIP only-------------------------------
    }

    /// <summary>
    /// Ios
    /// </summary>
    public class IOS
    {
        /// <summary>
        /// 通知内容
        /// </summary>
        [JsonProperty("alert")]
        public Object Alert { get; set; }

        /// <summary>
        /// 声音
        /// </summary>
        [JsonProperty("sound", NullValueHandling = NullValueHandling.Ignore)]
        public String Sound { get; set; }

        /// <summary>
        /// 默认角标 +1。
        /// </summary>
        [JsonProperty("badge")]
        public String Badge { get; set; } = "+1";

        /// <summary>
        /// 推送唤醒
        /// </summary>
        [JsonProperty("content-available", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ContentAvailable { get; set; }

        /// <summary>
        /// 通知扩展
        /// </summary>
        [JsonProperty("mutable-content", NullValueHandling = NullValueHandling.Ignore)]
        public bool? MutableContent { get; set; }

        /// <summary>
        /// IOS8才支持。设置APNs payload中的"category"字段值
        /// </summary>
        [JsonProperty("category", NullValueHandling = NullValueHandling.Ignore)]
        public String Category { get; set; }

        /// <summary>
        /// 附加字段
        /// </summary>
        [JsonProperty("extras", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<String, Object> Extras { get; set; }
    }
}
