﻿/************************************************************************
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
            StartServer();

            Console.WriteLine("服务器启动成功！");
            Log.Info("服务器启动成功！");

            Console.ReadKey();
        }

        /// <summary>
        /// 服务器启动初始化
        /// </summary>
        static void Init()
        {
            try
            {
                // 配置文件检测
                SocketServerConfig.Check();

                // 日志设置
                Log.Set(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log"), SocketServerConfig.LogInfoFlag, SocketServerConfig.LogDebugFlag, SocketServerConfig.LogWarnFlag, SocketServerConfig.LogErrorFlag);

                // 设置邮件信息
                EmailTool.SetSenderInfo(SocketServerConfig.EmailHost, SocketServerConfig.EmailAddress, SocketServerConfig.EmailPass);
            }
            catch (Exception ex)
            {
                var exStr = $"服务器初始化失败。错误信息：{ex}";
                Console.WriteLine(exStr);
                Log.Error(exStr);
            }
        }

        /// <summary>
        /// 开启服务器
        /// </summary>
        static void StartServer()
        {
            try
            {
                // 启动回调服务器
                CallbackServerManager.Start(SocketServerConfig.CallbackServerUrl);
            }
            catch (Exception ex)
            {
                var exStr = $"服务器启动失败。错误信息：{ex}";
                Console.WriteLine(exStr);
                Log.Error(exStr);
            }
        }
    }
}
