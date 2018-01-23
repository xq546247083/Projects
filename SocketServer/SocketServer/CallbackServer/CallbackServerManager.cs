
/************************************************************************
* 描述:回调服务处理
*************************************************************************/
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace SocketServer
{
    using Tool.Common;

    /// <summary>
    /// 回调服务处理
    /// </summary>
    public class CallbackServerManager
    {
        #region 属性

        /// <summary>
        /// http 服务监听对象
        /// </summary>
        private static HttpListener mHttpListner = null;

        /// <summary>
        /// 接收处理线程
        /// </summary>
        private static Thread mAcceptThread = null;

        /// <summary>
        /// api集合
        /// </summary>
        private static Dictionary<String, Type> mApiData = new Dictionary<String, Type>();

        /// <summary>
        /// 是否已经停止
        /// </summary>
        private static Boolean mIfStop = false;

        #endregion

        #region 私有方法

        /// <summary>
        /// 注册api
        /// </summary>
        private static void RegisterApi()
        {
            // 添加所有需要监听处理的API
            AddApi<TestApi>("Test");
        }

        /// <summary>
        /// 添加一个Api
        /// </summary>
        /// <typeparam name="T">对应的类型</typeparam>
        /// <param name="url">要监听的Url</param>
        private static void AddApi<T>(String url) where T : BaseApi, new()
        {
            // 添加/
            if (!url.StartsWith("/"))
            {
                url = $"/{url}";
            }

            if (mApiData.ContainsKey(url))
            {
                throw new Exception($"存在重复的Url注册：{url}");
            }

            mApiData[url] = typeof(T);
        }

        /// <summary>
        /// 处理Url
        /// </summary>
        /// <param name="url">要监听的Url</param>
        private static String HandleUrl(String url)
        {
            // 添加监听地址
            if (url.ToLower().StartsWith("http://") == false && url.ToLower().StartsWith("https://") == false)
            {
                url = $"http://{url}";
            }

            if (url.EndsWith("/") == false)
            {
                url = $"{url}/";
            }

            return url;
        }

        /// <summary>
        /// 接受和处理请求
        /// </summary>
        private static void Accept()
        {
            var httpListner = mHttpListner;
            while (mIfStop == false)
            {
                var context = httpListner.GetContext();

                // 进行一个请求处理
                Task.Factory.StartNew((tmpData) =>
                {
                    var tmpContext = (HttpListenerContext)tmpData;
                    try
                    {
                        // 获取请求处理对象
                        var url = tmpContext.Request.Url.AbsolutePath;
                        if (mApiData.ContainsKey(url) == false)
                        {
                            Log.Error("访问不存在的页面：IP:{0} Url:{1}", tmpContext.Request.UserHostAddress, tmpContext.Request.Url.ToString());

                            context.Response.StatusCode = 404;
                            context.Response.Write("Page Not Found!!");

                            return;
                        }

                        // 创建请求处理对象
                        var apiItem = (BaseApi)Activator.CreateInstance(mApiData[url]);
                        apiItem.context = tmpContext;

                        // 请求处理
                        apiItem.Process();
                    }
                    catch (Exception e1)
                    {
                        context.Response.StatusCode = 500;
                        context.Response.Write("There have been some inner error!!");
                        Log.Error("请求处理异常，错误信息：{0}", e1.ToString());
                    }
                    finally
                    {
                        context.Response.Close();
                    }
                }, context);
            }
        }

        #endregion

        #region 共有方法

        /// <summary>
        /// 开启Http监听
        /// </summary>
        /// <param name="url">需要监听的url</param>
        public static void Start(String url)
        {
            if (mHttpListner != null)
            {
                throw new Exception("已经存在http监听");
            }

            // 开启url监听
            mHttpListner = new HttpListener();
            mHttpListner.AuthenticationSchemes = AuthenticationSchemes.Anonymous;
            mHttpListner.Prefixes.Add(HandleUrl(url));

            // 开启监听
            mHttpListner.Start();

            // 开启接受处理请求的线程
            mAcceptThread = new Thread(Accept);
            mAcceptThread.IsBackground = true;
            mAcceptThread.Start();

            // 初始化接口
            RegisterApi();
        }

        /// <summary>
        /// 停止服务
        /// </summary>
        public static void Stop()
        {
            if (mHttpListner == null)
            {
                return;
            }

            mIfStop = true;
            try
            {
                mAcceptThread.Abort();
                mAcceptThread = null;
            }
            catch (Exception)
            {
                // ignored
            }

            mHttpListner.Close();
        }

        #endregion
    }
}
