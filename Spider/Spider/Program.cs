using System;
using System.Configuration;

namespace Spider
{
    class Program
    {
        static void Main(string[] args)
        {
            BLL.Spider spider = new BLL.Spider();
            spider.Start(ConfigurationManager.AppSettings["url"]);
            Console.WriteLine("已经爬完了！！！");
            Console.ReadKey();
        }
    }
}
