/************************************************************************
* 描述: 读取webconfig
*************************************************************************/
using System;
using System.Configuration;

namespace Manage.Common
{
    /// <summary>
    /// 读取webconfig
    /// </summary>
    public static class WebConfig
    {
        /// <summary>
        /// 平台
        /// </summary>
        public static String Platform => ConfigurationManager.AppSettings["Platform"] != null ? ConfigurationManager.AppSettings["Platform"] : String.Empty;

        /// <summary>
        /// 是否写Info日志
        /// </summary>
        public static Boolean LogInfoFlag => ConfigurationManager.AppSettings["LogInfoFlag"] == null || Boolean.Parse(ConfigurationManager.AppSettings["LogInfoFlag"]);

        /// <summary>
        /// 是否写Debug日志
        /// </summary>
        public static Boolean LogDebugFlag => ConfigurationManager.AppSettings["LogDebugFlag"] == null || Boolean.Parse(ConfigurationManager.AppSettings["LogDebugFlag"]);

        /// <summary>
        /// 是否写Warn日志
        /// </summary>
        public static Boolean LogWarnFlag => ConfigurationManager.AppSettings["LogWarnFlag"] == null || Boolean.Parse(ConfigurationManager.AppSettings["LogWarnFlag"]);

        /// <summary>
        /// 是否写Error日志
        /// </summary>
        public static Boolean LogErrorFlag => ConfigurationManager.AppSettings["LogErrorFlag"] == null || Boolean.Parse(ConfigurationManager.AppSettings["LogErrorFlag"]);

        /// <summary>
        /// 管理服务器数据库连接
        /// </summary>
        public static String ManageConnString
        {
            get
            {
                var temp = ConfigurationManager.ConnectionStrings["ManageConnString"];
                if (temp == null)
                {
                    throw new Exception("ManageConnString数据库连接没有配置");
                }

                return ConfigurationManager.ConnectionStrings["ManageConnString"].ConnectionString;
            }
        }

        /// <summary>
        ///回调服地址
        /// </summary>
        public static string CallbackServerUrl
        {
            get
            {
                var temp = ConfigurationManager.AppSettings["CallbackServerUrl"];
                if (temp == null)
                {
                    throw new Exception("CallbackServerUrl没有配置");
                }

                return temp;
            }
        }

        /// <summary>
        /// check方法
        /// </summary>
        public static void Check()
        {
        }
    }
}
