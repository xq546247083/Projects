/************************************************************************
* 标题: 随机工具
* 描述: 随机工具
* 作者：肖强
* 日期：2017-5-3 10:43:36
* 版本：V1
*************************************************************************/
using System;

namespace Tool.Common
{
    /// <summary>
    /// 随机工具
    /// </summary>
    public static class RandomTool
    {
        /// <summary>
        /// 获取一个随机字符串（会重复）
        /// </summary>
        /// <param name="length">长度</param>
        /// <returns></returns>
        public static String GetRandomStr(Int32 length)
        {
            Random rd = new Random();
            string str = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string result = "";
            for (int i = 0; i < length; i++)
            {
                result += str[rd.Next(str.Length)];
            }

            return result;
        }
    }
}
