/************************************************************************
* 标题: ChatClientConfig
* 描述: ChatClientConfig
* 作者：肖强
* 日期：2017-5-3 10:43:36
* 版本：V1
*************************************************************************/
using System;
using System.Configuration;

namespace Tool.Common
{
    /// <summary>
    /// ChatClientConfig
    /// </summary>
    public static class ChatClientConfig
    {
        /// <summary>
        /// WebSocket服务器地址
        /// </summary>
        public static String WebSocketServerUrl;

        /// <summary>
        /// UserID
        /// </summary>
        public static String UserID;

        /// <summary>
        /// NickName
        /// </summary>
        public static String NickName;

        /// <summary>
        /// check方法
        /// </summary>
        public static void Init()
        {
            WebSocketServerUrl = ConfigurationManager.AppSettings["WebSocketServerUrl"];
            if (WebSocketServerUrl == null)
            {
                throw new Exception("WebSocketServerUrl没有配置");
            }

            UserID = ConfigurationManager.AppSettings["UserID"];
            if (UserID == null)
            {
                throw new Exception("UserID没有配置");
            }

            NickName = ConfigurationManager.AppSettings["NickName"];
            if (NickName == null)
            {
                throw new Exception("NickName没有配置");
            }
        }
    }
}
