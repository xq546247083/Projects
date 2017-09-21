/************************************************************************
* 标题: 读取webconfig
* 描述: 读取webconfig
* 作者：肖强
* 日期：2017-5-3 10:43:36
* 版本：V1
*************************************************************************/
using System;
using System.Configuration;

namespace Tool.Common
{
    /// <summary>
    /// 读取webconfig
    /// </summary>
    public static class WebConfig
    {
        /// <summary>
        /// 玩家库连接字符串
        /// </summary>
        public static String CommonConnString { get; set; }

        /// <summary>
        /// 配置连接字字符串
        /// </summary>
        public static String ConfigConneString { get; set; }

        /// <summary>
        /// 是否写Info日志
        /// </summary>
        public static Boolean LogInfoFlag { get; set; }

        /// <summary>
        /// 是否写Debug日志
        /// </summary>
        public static Boolean LogDebugFlag { get; set; }

        /// <summary>
        /// 是否写Warn日志
        /// </summary>
        public static Boolean LogWarnFlag { get; set; }

        /// <summary>
        /// 是否写Error日志
        /// </summary>
        public static Boolean LogErrorFlag { get; set; }

        /// <summary>
        /// 密码过期时间
        /// </summary>
        public static Int32 PwdExpiredTime { get; set; }

        /// <summary>
        /// 邮箱主机
        /// </summary>
        public static String EmailHost { get; set; }

        /// <summary>
        /// 邮箱地址
        /// </summary>
        public static String EmailAddress { get; set; }

        /// <summary>
        /// 邮箱密码
        /// </summary>
        public static String EmailPass { get; set; }

        /// <summary>
        /// 构造方法
        /// </summary>
        static WebConfig()
        {
            //连接（此处两种连接方式都用的一个数据库）
            CommonConnString = ConfigurationManager.ConnectionStrings["CommonConnection"] != null ? ConfigurationManager.ConnectionStrings["CommonConnection"].ConnectionString : "";
            ConfigConneString = ConfigurationManager.ConnectionStrings["CommonConnection"] != null ? ConfigurationManager.ConnectionStrings["CommonConnection"].ConnectionString : "";

            //日志写入
            LogInfoFlag = ConfigurationManager.AppSettings["LogInfoFlag"] != null ? Boolean.Parse(ConfigurationManager.AppSettings["LogInfoFlag"]) : true;
            LogDebugFlag = ConfigurationManager.AppSettings["LogDebugFlag"] != null ? Boolean.Parse(ConfigurationManager.AppSettings["LogDebugFlag"]) : true;
            LogWarnFlag = ConfigurationManager.AppSettings["LogWarnFlag"] != null ? Boolean.Parse(ConfigurationManager.AppSettings["LogWarnFlag"]) : true;
            LogErrorFlag = ConfigurationManager.AppSettings["LogErrorFlag"] != null ? Boolean.Parse(ConfigurationManager.AppSettings["LogErrorFlag"]) : true;
            PwdExpiredTime = ConfigurationManager.AppSettings["PwdExpiredTime"] != null ? Int32.Parse(ConfigurationManager.AppSettings["PwdExpiredTime"]) : 0;

            EmailHost = ConfigurationManager.AppSettings["EmailHost"] != null ? ConfigurationManager.AppSettings["EmailHost"] : String.Empty;
            EmailAddress = ConfigurationManager.AppSettings["EmailAddress"] != null ? ConfigurationManager.AppSettings["EmailAddress"] : String.Empty;
            EmailPass = ConfigurationManager.AppSettings["EmailPass"] != null ? ConfigurationManager.AppSettings["EmailPass"] : String.Empty;
        }
    }
}
