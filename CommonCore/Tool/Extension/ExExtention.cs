/************************************************************************
* 标题: 错误扩展
* 描述: 错误扩展
* 作者：肖强
* 日期：2017-8-12 10:43:04
* 版本：V1
*************************************************************************/
using System;
using System.Text;

namespace Tool.Extension
{
    /// <summary>
    /// 错误扩展
    /// </summary>
    public static class ExExtention
    {
        /// <summary>
        /// 处理错误，返回字符串
        /// </summary>
        /// <param name="ex">错误</param>
        /// <param name="objects">要输出的消息</param>
        /// <returns>错误字符串</returns>
        public static String ToMessage(this Exception ex, params object[] objects)
        {
            Int32 i = 1;
            StringBuilder sb = new StringBuilder();
            foreach (var obj in objects)
            {
                sb.Append(String.Format("消息{0}:{1}{2}", i, obj, Environment.NewLine));
                i++;
            }

            return String.Format("{0}时间：{1}{2}错误消息：{3}{4}{5}{6}", i > 1 ? String.Format("输出的消息:{0}", sb) : String.Empty, DateTime.Now, Environment.NewLine, ex.Message, Environment.NewLine, ex.StackTrace, Environment.NewLine);
        }
    }
}
