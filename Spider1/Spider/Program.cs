using System;

namespace Spider
{
    class Program
    {
        static void Main(string[] args)
        {
            BLL.Spider spider = new BLL.Spider();
            spider.Start();

            Console.ReadKey();
        }
    }
}
