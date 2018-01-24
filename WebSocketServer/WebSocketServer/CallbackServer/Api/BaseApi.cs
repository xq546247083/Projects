/************************************************************************
* 描述:回调处理基类
*************************************************************************/
using System;
using System.Net;
using System.Text;

namespace CallbackServer.Api
{
    using Tool.Common;
    using System.Collections.Specialized;

    /// <summary>
    /// 回调处理基类
    /// </summary>
    public abstract class BaseApi
    {
        #region 属性

        /// <summary>
        /// 表单数据
        /// </summary>
        private NameValueCollection formData;

        /// <summary>
        /// Url参数数据
        /// </summary>
        private NameValueCollection queryString;

        /// <summary>
        /// 所有请求数据
        /// </summary>
        private NameValueCollection requestData;

        /// <summary>
        /// 上下文对象
        /// </summary>
        public HttpListenerContext Context;

        #endregion

        #region 继承要实现的方法

        /// <summary>
        /// 请求处理
        /// </summary>
        public abstract void Process();

        /// <summary>
        /// 类名
        /// </summary>
        /// <returns>类名</returns>
        public abstract String GetClassName();

        #endregion

        #region 基础方法

        /// <summary>
        /// 获取通过POST方式传递的请求数据
        /// </summary>
        /// <returns>请求的数据</returns>
        protected String GetRequestString()
        {
            // 获取POST过来的二进制数组
            Byte[] byteArray = this.Request.ReadToEnd();
            if (byteArray == null || byteArray.Length <= 0)
            {
                return String.Empty;
            }

            // 转化为字符串
            return Encoding.UTF8.GetString(byteArray);
        }

        /// <summary>
        /// 请求对象
        /// </summary>
        public HttpListenerRequest Request
        {
            get { return this.Context.Request; }
        }

        /// <summary>
        /// 应答对象
        /// </summary>
        public HttpListenerResponse Response
        {
            get { return this.Context.Response; }
        }

        /// <summary>
        /// 表单数据(没有进行UrlDecode操作)
        /// </summary>
        public NameValueCollection FormData
        {
            get
            {
                if (formData == null)
                {
                    formData = RequestTool.ConverToNameValueCollection(GetRequestString(), false, Request.ContentEncoding);
                }

                return formData;
            }
        }

        /// <summary>
        /// 请求数据(通过url请求)
        /// </summary>
        public NameValueCollection QueryString
        {
            get
            {
                if (this.queryString == null)
                {
                    this.queryString = this.Request.QueryString;
                }

                return this.queryString;
            }
        }

        /// <summary>
        /// 请求数据
        /// （包含FormData QueryString）
        /// </summary>
        public NameValueCollection RequestData
        {
            get
            {
                if (this.requestData == null)
                {
                    this.requestData = new NameValueCollection { QueryString, FormData };
                }

                return this.requestData;
            }
        }

        #endregion

        #region 扩展方法

        /// <summary>
        /// Ifs the ip allowed.
        /// </summary>
        /// <param name="ips">允许的ip列表</param>
        /// <returns></returns>
        public Boolean IfIpAllowed(String[] ips)
        {
#if DEBUG

            return true;

#else
// 如果为空，直接返回
            if (ips == null || ips.Length == 0)
            {
                return false;
            }

            String ip = this.Request.RemoteEndPoint.Address.ToString();
            if (!ips.Contains(ip))
            {
                //记录日志
                Log.Error("Class:{0} 本次请求的IP不允许访问该接口，地址为：{1}", GetClassName(), ip);

                return false;
            }

            return true;
#endif
        }

        #endregion
    }
}
