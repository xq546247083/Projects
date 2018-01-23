/************************************************************************
* 标题: 字符串助手
* 描述: 字符串助手
* 作者：肖强
* 日期：2017-5-3 10:43:36
* 版本：V1
*************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;

namespace Tool.Common
{
    using Tool.Extension;

    /// <summary>
    /// 字符串助手
    /// </summary>
    public static class StringTool
    {
        /// <summary>
        /// 拆分字符串到list
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="str">拆分的字符串</param>
        /// <returns>拆分的结果</returns>
        public static List<String> SplitToStrList(String str)
        {
            List<String> result = new List<String>();
            if (String.IsNullOrEmpty(str))
            {
                return result;
            }

            var strArray = str.Split(',');
            foreach (var val in strArray)
            {
                result.Add(val);
            }

            return result;
        }

        /// <summary>
        /// 拆分字符串到list
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="str">拆分的字符串</param>
        /// <returns>拆分的结果</returns>
        public static List<Int32> SplitToIntList(String str)
        {
            List<Int32> result = new List<Int32>();
            if (String.IsNullOrEmpty(str))
            {
                return result;
            }

            var strArray = str.Split(',');
            foreach (var val in strArray)
            {
                result.Add(Int32.Parse(val));
            }

            return result;
        }
    }
}