/************************************************************************
* 标题: 时间工具
* 描述: 时间工具
* 作者：肖强
* 日期：2017-5-3 10:43:36
* 版本：V1
*************************************************************************/
using System;

namespace Tool.Common
{
    /// <summary>
    /// 时间工具
    /// </summary>
    public static class DateTimeTool
    {
        /// <summary>
        /// 获取时间格式字符串(yyyy-MM-dd HH:mm:ss)
        /// </summary>
        /// <param name="time">需要转换的时间</param>
        /// <exception cref="T:System.FormatException">System.FormatException</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">System.ArgumentOutOfRangeException</exception>
        /// <returns>时间格式字符串</returns>
        public static String GetGreenWichTime(System.DateTime time)
        {
            return time.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 获取时间格式字符串(yyyy-MM-dd)
        /// </summary>
        /// <param name="time">需要转换的时间</param>
        /// <exception cref="T:System.FormatException">System.FormatException</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">System.ArgumentOutOfRangeException</exception>
        /// <returns>时间格式字符串</returns>
        public static String GetShortGreenWichTime(System.DateTime time)
        {
            return time.ToString("yyyy-MM-dd");
        }
    }
}
