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
        public static String UserID => ConfigurationManager.AppSettings["UserID"];

        /// <summary>
        /// NickName
        /// </summary>
        public static String NickName => ConfigurationManager.AppSettings["NickName"];

        /// <summary>
        /// 修改item的值
        /// </summary>
        /// <param name="keyName"></param>
        /// <param name="newKeyValue"></param>
        public static void ModifyItem(string keyName, string newKeyValue)
        {
            //修改配置文件中键为keyName的项的值  
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings[keyName].Value = newKeyValue;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

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

            var userIDTemp = ConfigurationManager.AppSettings["UserID"];
            if (userIDTemp == null)
            {
                throw new Exception("UserID没有配置");
            }

            var nickNameTemp = ConfigurationManager.AppSettings["NickName"];
            if (nickNameTemp == null)
            {
                throw new Exception("NickName没有配置");
            }
        }
    }
}
