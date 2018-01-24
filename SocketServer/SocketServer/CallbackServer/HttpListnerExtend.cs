/************************************************************************
* 描述:Http的扩展处理
*************************************************************************/
using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace CallbackServer
{
    /// <summary>
    /// Http的扩展处理
    /// </summary>
    public static class HttpListnerExtend
    {
        /// <summary>
        /// 把数据写入到应答流
        /// </summary>
        /// <param name="response">应答对象</param>
        /// <param name="data">待写入的数据</param>
        /// <param name="offset">数据起始位置</param>
        /// <param name="length">写入的数据长度</param>
        public static void Write(this HttpListenerResponse response, Byte[] data, Int32 offset, Int32 length)
        {
            response.ContentLength64 = length;
            response.StatusCode = 200;
            response.OutputStream.Write(data, offset, length);
        }

        /// <summary>
        /// 把数据写入到应答流
        /// </summary>
        /// <param name="response">应答对象</param>
        /// <param name="data">待写入的数据</param>
        public static void Write(this HttpListenerResponse response, Byte[] data)
        {
            response.ContentLength64 = data.Length;
            response.StatusCode = 200;
            response.OutputStream.Write(data, 0, data.Length);
        }

        /// <summary>
        /// 把数据写入到应答流
        /// </summary>
        /// <param name="response">应答对象</param>
        /// <param name="data">待写入的数据</param>
        /// <param name="encoding">字符集</param>
        public static void Write(this HttpListenerResponse response, String data, String encoding = "UTF-8")
        {
            Write(response, Encoding.GetEncoding(encoding).GetBytes(data));
        }

        /// <summary>
        /// 把数据写入到应答流
        /// </summary>
        /// <param name="response">应答对象</param>
        /// <param name="data">待写入的数据</param>
        public static void Write(this HttpListenerResponse response, Stream data)
        {
            response.ContentLength64 = data.Length;
            response.StatusCode = 200;

            // 循环从stream中把数据写入到应答流
            var buffer = new Byte[10240];
            while (true)
            {
                Int32 readLen = data.Read(buffer, 0, buffer.Length);
                if (readLen <= 0)
                {
                    break;
                }

                response.OutputStream.Write(buffer, 0, readLen);
            }
        }

        /// <summary>
        /// 读取所有字节流
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <returns>读取到的字节流</returns>
        public static Byte[] ReadToEnd(this HttpListenerRequest request)
        {
            var result = new Byte[request.ContentLength64];

            request.InputStream.Read(result, 0, result.Length);

            return result;
        }
    }
}
