using System;
using System.Collections.Generic;

namespace JGPushDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("推送类型：是否全部推送（Y/N）:");
                var flag = Console.ReadLine();
                var dasd = flag.ToLower()[0];
                if (flag.Length >= 1 && flag.ToLower()[0].Equals('y'))
                {
                    Console.Write("请输入要推送的标题:");
                    var title = Console.ReadLine();
                    Console.Write("请输入要推送的内容:");
                    var content = Console.ReadLine();

                    JGPush.SendAll(title, content);
                }
                else
                {
                    Console.Write("请输入要推送的注册ID:");
                    var regID = Console.ReadLine();
                    Console.Write("请输入要推送的标题:");
                    var title = Console.ReadLine();
                    Console.Write("请输入要推送的内容:");
                    var content = Console.ReadLine();

                    JGPush.Send(new List<String>() { regID }, title, content);
                }

                Console.WriteLine("推送成功！");
                Console.WriteLine();
            }
        }
    }
}
