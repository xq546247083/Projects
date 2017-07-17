/************************************************************************
* 标题: 自定义异常
* 描述: 自定义异常
* 作者： 肖强
* 日期：2017-5-3 10:43:36
* 版本：V1
*************************************************************************/
using System;

namespace GameServer.Model
{
    /// <summary>
    /// 自定义异常
    /// </summary>
    public class SelfDefinedException : Exception
    {
        /// <summary>
        /// 返回值枚举
        /// </summary>
        public ResultStatus ResultStatus { get; set; }

        /// <summary>
        /// 是否需要记录日志
        /// </summary>
        public Boolean IfNeedLog { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="status">返回值枚举</param>
        /// <param name="message">错误消息</param>
        /// <param name="ifNeedLog">是否要写日志</param>
        public SelfDefinedException(ResultStatus status,String message, Boolean ifNeedLog = true)
            :base(message)
        {
            this.ResultStatus = status;
            this.IfNeedLog = ifNeedLog;
        }
    }
}
