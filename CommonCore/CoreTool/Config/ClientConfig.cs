/************************************************************************
* 标题: 读取ClientConfig
* 描述: 读取ClientConfig
* 作者：肖强
* 日期：2017-5-3 10:43:36
* 版本：V1
*************************************************************************/
using System;
using System.Configuration;

namespace Tool.Common
{
    /// <summary>
    /// 读取ClientConfig
    /// </summary>
    public static class ClientConfig
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
        /// Password
        /// </summary>
        public static String Password;

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

            Password = ConfigurationManager.AppSettings["Password"];
            if (Password == null)
            {
                throw new Exception("Password没有配置");
            }
        }
    }
}
