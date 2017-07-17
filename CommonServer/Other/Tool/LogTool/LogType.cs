/************************************************************************
* 标题: 日志类型
* 描述: 日志类型
* 作者：肖强
* 日期：2017-5-3 10:43:36
* 版本：V1
*************************************************************************/

namespace Tool.Log
{
    /// <summary>
    /// 日志类型
    /// </summary>
    public enum LogType : byte
    {
        /// <summary>
        /// Info
        /// </summary>
        Info = 0,

        /// <summary>
        /// Debug
        /// </summary>
        Debug = 2,

        /// <summary>
        /// Warn
        /// </summary>
        Warn = 1,

        /// <summary>
        /// Error
        /// </summary>
        Error = 3,
    }
}
