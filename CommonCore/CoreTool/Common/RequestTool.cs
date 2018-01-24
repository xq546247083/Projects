/************************************************************************
* 标题: 请求工具
* 描述: 请求工具
* 作者：肖强
* 日期：2017-5-3 10:43:36
* 版本：V1
*************************************************************************/
using System;
using System.Collections.Specialized;
using System.Text;
using System.Web;

namespace Tool.Common
{
    /// <summary>
    /// 请求工具
    /// </summary>
    public static class RequestTool
    {
        /// <summary>
        /// 从字符串中读取key value的值
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="urlencoded">是否经过了url编码</param>
        /// <param name="encoding">编码方式</param>
        public static NameValueCollection ConverToNameValueCollection(String data, Boolean urlencoded, Encoding encoding)
        {
            NameValueCollection nvc = new NameValueCollection();

            // 如果为空直接返回
            if (String.IsNullOrWhiteSpace(data))
            {
                return nvc;
            }

            // 获取？后面的为参数
            int num = data.Length;
            for (int i = (((data.Length > 0) && (data[0] == '?')) ? 1 : 0); i < num; i++)
            {
                int startIndex = i;
                int num4 = -1;
                while (i < num)
                {
                    char ch = data[i];
                    if (ch == '=')
                    {
                        if (num4 < 0)
                        {
                            num4 = i;
                        }
                    }
                    else if (ch == '&')
                    {
                        break;
                    }
                    i++;
                }
                string str = null;
                string str2 = null;
                if (num4 >= 0)
                {
                    str = data.Substring(startIndex, num4 - startIndex);
                    str2 = data.Substring(num4 + 1, (i - num4) - 1);
                }
                else
                {
                    str2 = data.Substring(startIndex, i - startIndex);
                }
                if (urlencoded)
                {

                    nvc.Add((str == null) ? null : HttpUtility.UrlDecode(str, encoding), HttpUtility.UrlDecode(str2, encoding));
                }
                else
                {
                    nvc.Add(str, str2);
                }
                if ((i == (num - 1)) && (data[i] == '&'))
                {
                    nvc.Add(null, "");
                }
            }

            return nvc;
        }
    }
}
