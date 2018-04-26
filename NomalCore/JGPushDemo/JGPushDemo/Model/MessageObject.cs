//***********************************************************************************
// 文件名称：MessageObject.cs
// 功能描述：发送的消息对象
// 数据表：
// 作者：xiaoqiang
// 日期：2018-4-26 16:24:35
// 修改记录：
//***********************************************************************************
using System;
using System.Collections.Generic;

namespace JGPushDemo.Model
{
    /// <summary>
    /// 发送的消息对象
    /// </summary>
    public class MessageObject
    {
        /// <summary>
        /// 要发送的地址
        /// </summary>
        public String Url { get; set; }

        /// <summary>
        /// 要发送的消息
        /// </summary>
        public String Message { get; set; }

        /// <summary>
        /// 是否为POST方式发送
        /// </summary>
        public Boolean IsPost { get; set; }

        /// <summary>
        /// 失败后是否丢弃消息
        /// </summary>
        public Boolean IsThrowAway { get; set; }

        /// <summary>
        /// 头
        /// </summary>
        public Dictionary<String, String> Headers { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="url">要发送的地址</param>
        /// <param name="message">要发送的消息</param>
        /// <param name="isPost">是否为POST方式发送</param>
        /// <param name="isThrowAway">失败是否丢弃消息</param>
        public MessageObject(String url, String message, Boolean isPost, Boolean isThrowAway, Dictionary<String, String> headers)
        {
            this.Url = url;
            this.Message = message;
            this.IsPost = isPost;
            this.IsThrowAway = isThrowAway;
            this.Headers = headers;
        }
    }
}
