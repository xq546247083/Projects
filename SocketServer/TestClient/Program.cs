using System;
using System.Threading;

namespace Client
{
    using Tool.Common;

    /// <summary>
    /// 测试客户端
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            ClientConfig.Init();

            Console.WriteLine("点击任意键开启连接服务器...");
            Console.ReadKey();
            WebSocketClient.Start(ClientConfig.WebSocketServerUrl);

            Console.WriteLine("点击任意键开始登陆");
            Console.ReadKey();
            WebSocketClient.Send($"Api=SysUserLogin&UserID={ClientConfig.UserID}&NickName={ClientConfig.NickName}");

            Thread.Sleep(1000);
            while (true)
            {
                Console.WriteLine("输入要广播的消息");
                var message = Console.ReadLine();
                WebSocketClient.Send($"Api=SysUserBroadcast&Message={message}");
                Thread.Sleep(1000);
            }
        }
    }
}
