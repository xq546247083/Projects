/************************************************************************
* 标题: 读取SocketServerConfig
* 描述: 读取SocketServerConfig
* 作者：肖强
* 日期：2017-5-3 10:43:36
* 版本：V1
*************************************************************************/
using System;
using System.Configuration;

namespace Tool.Common
{
    /// <summary>
    /// 读取SocketServerConfig
    /// </summary>
    public static class SocketServerConfig
    {
        /// <summary>
        /// 是否写Info日志
        /// </summary>
        public static Boolean LogInfoFlag = ConfigurationManager.AppSettings["LogInfoFlag"] == null || Boolean.Parse(ConfigurationManager.AppSettings["LogInfoFlag"]);

        /// <summary>
        /// 是否写Debug日志
        /// </summary>
        public static Boolean LogDebugFlag = ConfigurationManager.AppSettings["LogDebugFlag"] == null || Boolean.Parse(ConfigurationManager.AppSettings["LogDebugFlag"]);

        /// <summary>
        /// 是否写Warn日志
        /// </summary>
        public static Boolean LogWarnFlag = ConfigurationManager.AppSettings["LogWarnFlag"] == null || Boolean.Parse(ConfigurationManager.AppSettings["LogWarnFlag"]);

        /// <summary>
        /// 是否写Error日志
        /// </summary>
        public static Boolean LogErrorFlag = ConfigurationManager.AppSettings["LogErrorFlag"] == null || Boolean.Parse(ConfigurationManager.AppSettings["LogErrorFlag"]);

        /// <summary>
        /// 邮箱主机
        /// </summary>
        public static String EmailHost = ConfigurationManager.AppSettings["EmailHost"] != null ? ConfigurationManager.AppSettings["EmailHost"] : String.Empty;

        /// <summary>
        /// 邮箱地址
        /// </summary>
        public static String EmailAddress = ConfigurationManager.AppSettings["EmailAddress"] != null ? ConfigurationManager.AppSettings["EmailAddress"] : String.Empty;

        /// <summary>
        /// 邮箱密码
        /// </summary>
        public static String EmailPass = ConfigurationManager.AppSettings["EmailPass"] != null ? ConfigurationManager.AppSettings["EmailPass"] : String.Empty;

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public static String CommonConnString = ConfigurationManager.ConnectionStrings["CommonConnection"] != null ? ConfigurationManager.ConnectionStrings["CommonConnection"].ConnectionString : String.Empty;

        /// <summary>
        /// WebSocket服务器地址
        /// </summary>
        public static String WebSocketServerUrl;

        /// <summary>
        /// 回调服务器地址
        /// </summary>
        public static String CallbackServerUrl;

        /// <summary>
        /// 初始化配置
        /// </summary>
        public static void Init()
        {
            WebSocketServerUrl = ConfigurationManager.AppSettings["WebSocketServerUrl"];
            if (WebSocketServerUrl == null)
            {
                throw new Exception("WebSocketServerUrl没有配置");
            }

            CallbackServerUrl = ConfigurationManager.AppSettings["CallbackServerUrl"];
            if (CallbackServerUrl == null)
            {
                throw new Exception("CallbackServerUrl没有配置");
            }
        }
    }
}