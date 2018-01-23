/************************************************************************
* 启动
*************************************************************************/
using System;
using System.IO;

namespace SocketServer
{
    using Tool.Common;

    /// <summary>
    /// 启动
    /// </summary>
    class Program
    {
        /// <summary>
        /// 入口
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Init();
            Console.WriteLine("Hello World!");
        }

        /// <summary>
        /// 服务器启动初始化
        /// </summary>
        static void Init()
        {
            try
            {
                // 初始化配置
                WebConfig.Check();

                //设置日志地址
                Log.LogPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log");
                
                // 设置邮件信息
                EmailTool.SetSenderInfo(WebConfig.EmailHost, WebConfig.EmailAddress, WebConfig.EmailPass);
            }
            catch (Exception ex)
            {
                Log.Error($"服务器初始化失败。错误信息：{ex}");
            }
        }
    }
}
