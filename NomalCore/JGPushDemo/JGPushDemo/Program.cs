using System;

namespace JGPushDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("请输入要推送的标题:");
                var title = Console.ReadLine();
                Console.Write("请输入要推送的内容:");
                var content = Console.ReadLine();

                JGPush.SendAll(title, content);
                Console.WriteLine("推送成功！");
                Console.WriteLine();
            }
        }
    }
}
