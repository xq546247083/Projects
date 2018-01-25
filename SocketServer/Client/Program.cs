using System;

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
            WebSocketClient.Send("Api=PlayerLogin&UserName=SysUser1&password=123456");

            Console.WriteLine("点击任意键结束...");
            Console.ReadKey();
        }
    }
}
