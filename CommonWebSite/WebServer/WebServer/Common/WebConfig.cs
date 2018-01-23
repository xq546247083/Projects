/************************************************************************
* 标题: 读取webconfig
* 描述: 读取webconfig
* 作者：肖强
* 日期：2017-5-3 10:43:36
* 版本：V1
*************************************************************************/
using System;
using System.Configuration;

namespace WebSite
{
    /// <summary>
    /// 读取webconfig
    /// </summary>
    public static class WebConfig
    {
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
        /// 数据库连接字符串
        /// </summary>
        public static String CommonConnString => ConfigurationManager.ConnectionStrings["CommonConnection"] != null ? ConfigurationManager.ConnectionStrings["CommonConnection"].ConnectionString : "";

        /// <summary>
        /// 配置连接字字符串
        /// </summary>
        public static String ConfigConneString => ConfigurationManager.ConnectionStrings["CommonConnection"] != null ? ConfigurationManager.ConnectionStrings["CommonConnection"].ConnectionString : "";

        /// <summary>
        /// 密码过期时间
        /// </summary>
        public static Int32 PwdExpiredTime => ConfigurationManager.AppSettings["PwdExpiredTime"] != null ? Int32.Parse(ConfigurationManager.AppSettings["PwdExpiredTime"]) : 0;

        /// <summary>
        /// 邮箱主机
        /// </summary>
        public static String EmailHost => ConfigurationManager.AppSettings["EmailHost"] != null ? ConfigurationManager.AppSettings["EmailHost"] : String.Empty;

        /// <summary>
        /// 邮箱地址
        /// </summary>
        public static String EmailAddress => ConfigurationManager.AppSettings["EmailAddress"] != null ? ConfigurationManager.AppSettings["EmailAddress"] : String.Empty;

        /// <summary>
        /// 邮箱密码
        /// </summary>
        public static String EmailPass => ConfigurationManager.AppSettings["EmailPass"] != null ? ConfigurationManager.AppSettings["EmailPass"] : String.Empty;
    }
}
