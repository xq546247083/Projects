/************************************************************************
* 描述: 字符串工具
*************************************************************************/
using System.Text.RegularExpressions;

namespace Manage.Common
{
    /// <summary>
    /// 字符串工具
    /// </summary>
    public static class StringTool
    {
        /// <summary>
        /// 是否为日期+时间型字符串
        /// </summary>
        /// <param name="strSource">字符串</param>
        /// <returns></returns>
        public static bool IsDateTime(string strSource)
        {
            return Regex.IsMatch(strSource, @"\d{4}/\d{1,2}/\d{1,2} \d{1,2}:\d{1,2}:\d{1,2}");
        }
    }
}
