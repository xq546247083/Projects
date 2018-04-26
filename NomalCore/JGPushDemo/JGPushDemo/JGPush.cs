//***********************************************************************************
// 文件名称：JGPush.cs
// 功能描述：极光推送
// 数据表：
// 作者：xiaoqiang
// 日期：2018-4-26 16:26:05
// 修改记录：
//***********************************************************************************
using System;
using System.Collections.Generic;
using System.Text;

namespace JGPushDemo
{
    using JGPushDemo.Model;
    using System.Configuration;

    /// <summary>
    /// 极光推送
    /// </summary>
    public class JGPush
    {
        #region 字段

        /// <summary>
        /// AppKey
        /// </summary>
        private static String mAppKey = String.Empty;

        /// <summary>
        /// MasterSecret
        /// </summary>
        private static String mMasterSecret = String.Empty;

        /// <summary>
        /// url
        /// </summary>
        private static String mUrl = String.Empty;

        /// <summary>
        /// 消息保存目录
        /// </summary>
        private static String mMessageDir = "JGMessage";

        /// <summary>
        /// 内容类型
        /// </summary>
        private static String mContentType = "application/json";

        #endregion

        #region 私有方法

        /// <summary>
        /// 静态构造方法
        /// </summary>
        static JGPush()
        {
            // 初始化数据
            mAppKey = ConfigurationManager.AppSettings["AppKey"];
            mMasterSecret = ConfigurationManager.AppSettings["MasterSecret"];
            mUrl = ConfigurationManager.AppSettings["Url"];

            // 初始化工具
            var messageCountPerMinute = Convert.ToInt32(ConfigurationManager.AppSettings["MessageCountPerMinute"]);
            if (messageCountPerMinute < 600)
            {
                messageCountPerMinute = 600;
            }

            MessageTool.SetParam($"{AppDomain.CurrentDomain.BaseDirectory}\\{mMessageDir}", 1, 10, 100, messageCountPerMinute, mContentType);
        }

        /// <summary>
        /// base64
        /// </summary>
        /// <param name="str">str</param>
        /// <returns>结果</returns>
        private static String Base64Encode(String str)
        {
            var bytes = Encoding.Default.GetBytes(str);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="message">消息</param>
        /// <returns>返回值</returns>
        private static void Send(JGPushMessage jgPushMessage)
        {
            // 组装头
            var headers = new Dictionary<String, String>();
            headers["Authorization"] = $"Basic {Base64Encode($"{mAppKey}:{mMasterSecret}")}";

            var messageObject = new MessageObject(mUrl, jgPushMessage.ToString(), true, false, headers);
            MessageTool.SendMessage(messageObject);
        }

        #endregion

        #region 公有方法

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="jgIDList">极光id列表</param>
        /// <param name="title">标题</param>
        /// <param name="message">消息</param>
        public static void Send(List<String> jgIDList, String title, String message)
        {
            // 组装消息
            var jgPushMessage = new JGPushMessage()
            {
                Platform = new List<String> { "android", "ios" },
                Audience = new Audience
                {
                    RegistrationID = jgIDList
                },
                Notification = new Notification
                {
                    Alert = message,
                    Android = new Android
                    {
                        Alert = message,
                        Title = title
                    },
                    IOS = new IOS
                    {
                        Alert = message,
                        Badge = "+1"
                    }
                },
                Options = new Options
                {
                    IsApnsProduction = true
                }
            };

            Send(jgPushMessage);
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="message">消息</param>
        public static void SendAll(String title, String message)
        {
            // 组装消息
            var jgPushMessage = new JGPushMessage()
            {
                Platform = new List<String> { "android", "ios" },
                Audience = "all",
                Notification = new Notification
                {
                    Alert = message,
                    Android = new Android
                    {
                        Alert = message,
                        Title = title
                    },
                    IOS = new IOS
                    {
                        Alert = message,
                        Badge = "+1"
                    }
                },
                Options = new Options
                {
                    IsApnsProduction = true
                }
            };

            Send(jgPushMessage);
        }

        #endregion
    }
}
