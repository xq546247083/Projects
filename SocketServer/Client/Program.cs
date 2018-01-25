using System;
using System.Threading;

namespace Client
{
    using Tool.Common;
    using WebSocketClient;

    /// <summary>
    /// 测试客户端
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("点击任意键开启连接服务器...");
            Console.ReadKey();
            WebSocketClient.Start(SocketServerConfig.WebSocketServerUrl);

            Console.WriteLine("点击任意键开始登陆");
            Console.ReadKey();
            WebSocketClient.Send("Api=SysUserLogin&UserID=c3c3825c-b479-4b42-88db-352bab1b4381&Password=123456");

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
