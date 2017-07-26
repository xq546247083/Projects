using Spider.Model;
using System;
using System.Collections.Concurrent;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Spider.BLL
{
    class Spider
    {
        /// <summary>
        /// 要抓取的深度
        /// </summary>
        public static Int32 DeepCount = 0;

        /// <summary>
        /// 下载图片队列
        /// </summary>
        private static readonly ConcurrentQueue<Img> mImgQueue = new ConcurrentQueue<Img>();

        /// <summary>
        /// 图片下载的信号量
        /// </summary>
        private static AutoResetEvent mImgResetEvent = new AutoResetEvent(true);

        /// <summary>
        /// 连接队列
        /// </summary>
        private static readonly ConcurrentQueue<Link> mLinkQueue = new ConcurrentQueue<Link>();

        /// <summary>
        /// url队列信号量
        /// </summary>
        private static AutoResetEvent mLinkResetEvent = new AutoResetEvent(true);

        /// <summary>
        /// 启动
        /// </summary>
        /// <param name="linkUrl"></param>
        public void Start(string linkUrl)
        {
            Task.Run(() => { DownImage(); });
            Task.Run(() => { CapterWeb(); });
            LadySpider(linkUrl);
        }

        /// <summary>
        /// 抓取美女图片
        /// </summary>
        public void LadySpider(string linkUrl)
        {
            //最多抓五层
            DeepCount++;
            if (DeepCount == 5) return;

            var simpleCrawler = new SimpleCrawler();
            simpleCrawler.OnStart += (s, e) =>
            {
                Console.WriteLine("爬虫开始抓取:{0}", linkUrl);
            };
            simpleCrawler.OnError += (s, e) =>
            {
                Console.WriteLine("爬虫抓取出现错误：{0}，异常消息：{1}",linkUrl, e.Exception.Message);
            };

            simpleCrawler.OnCompleted += (s, e) =>
            {
                //使用正则表达式清洗网页源代码中的数据
                var links = Regex.Matches(e.PageSource, @"<a[^>]+href=""*(?<href>/[^>\s]+)""\s*[^>]*>(?<text>(?!.*img).*?)</a>", RegexOptions.IgnoreCase);
                var linkImgs = Regex.Matches(e.PageSource, @"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", RegexOptions.IgnoreCase);

                
                //获取连接
                foreach (Match item in links)
                {
                    var link = new Link
                    {
                        Name = item.Groups["text"].Value,
                    };

                    if (item.Groups["href"].Value.StartsWith("http"))
                    {
                        link.Uri = new Uri(item.Groups["href"].Value);
                    }
                    else
                    {
                        link.Uri = new Uri(linkUrl + item.Groups["href"].Value);
                    }
                    mLinkQueue.Enqueue(link);
                    mLinkResetEvent.Set();
                }

                //获取图片连接
                foreach (Match item in linkImgs)
                {
                    var img = new Img();
                    if (item.Groups["imgUrl"].Value.StartsWith("http"))
                    {
                        img.Uri = new Uri(item.Groups["imgUrl"].Value);
                    }
                    else
                    {
                        img.Uri = new Uri(linkUrl + item.Groups["imgUrl"].Value);
                    }
                    img.WebUri = linkUrl;

                    mImgQueue.Enqueue(img);
                    mImgResetEvent.Set();
                }
            };
            simpleCrawler.Start(new Uri(linkUrl)).Wait();
        }

        /// <summary>
        /// 下载图片任务
        /// </summary>
        public void CapterWeb()
        {
            while (true)
            {
                try
                {
                    Link content = null;
                    while (mLinkQueue.TryDequeue(out content))
                    {
                        LadySpider(content.Uri.AbsoluteUri);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("访问Url出错,ex:{0}", ex.Message);
                }

                mLinkResetEvent.WaitOne(1000);
            }
        }

        /// <summary>
        /// 下载图片任务
        /// </summary>
        public void DownImage()
        {
            while (true)
            {
                try
                {
                    Img content = null;
                    while (mImgQueue.TryDequeue(out content))
                    {
                        DownImage(content);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("下载图片出错:{0}", ex.Message);
                }

                mImgResetEvent.WaitOne(1000);
            }
        }

        /// <summary>
        /// 下载图片
        /// </summary>
        /// <param name="img">下载的图片</param>
        public void DownImage(Img img)
        {
            //创建一个request 同时可以配置requst其余属性
            WebRequest imgRequst = WebRequest.Create(img.Uri);

            using (var imgStream=imgRequst.GetResponse().GetResponseStream())
            {
                if (imgStream == null) return;

                //获取地址
                Image downImage = Image.FromStream(imgStream);
                var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, String.Format("Img\\{0}\\", img.WebUri.Replace(@"\", "").Replace(@"/", "").Replace("http", "").Replace(":", "")));
                string fileName = string.Format("{0}.jpg", DateTime.Now.ToString("HHmmssffff"));

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                downImage.Save(path + fileName);
                downImage.Dispose();
                Console.WriteLine("下载图片成功：{0}", fileName);
            }
        }
    }
}
