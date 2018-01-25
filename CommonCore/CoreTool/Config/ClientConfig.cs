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
        public static String WebSocketServerUrl
        {
            get
            {
                var temp = ConfigurationManager.AppSettings["WebSocketServerUrl"];
                if (temp == null)
                {
                    throw new Exception("WebSocketServerUrl没有配置");
                }

                return temp;
            }
        }

        /// <summary>
        /// UserID
        /// </summary>
        public static String UserID
        {
            get
            {
                var temp = ConfigurationManager.AppSettings["UserID"];
                if (temp == null)
                {
                    throw new Exception("UserID没有配置");
                }

                return temp;
            }
        }

        /// <summary>
        /// Password
        /// </summary>
        public static String Password
        {
            get
            {
                var temp = ConfigurationManager.AppSettings["Password"];
                if (temp == null)
                {
                    throw new Exception("Password没有配置");
                }

                return temp;
            }
        }


        /// <summary>
        /// check方法
        /// </summary>
        public static void Check()
        {
            object temp;
            temp = WebSocketServerUrl;
            temp = UserID;
            temp = Password;
        }
    }
}
