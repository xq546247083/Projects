/************************************************************************
* 描述: 操作日志
*************************************************************************/
using System;

namespace Moqikaka.GameManage.Model
{
    using Moqikaka.GameManage.Common;

    /// <summary>
    /// 操作日志
    /// </summary>
    [DataBaseTable("user_operation_log")]
    public sealed class UserOperationLog
    {
        #region 属性

        /// <summary>
        /// 日志唯一标识
        /// </summary>
        public Int32 ID { get; set; }

        /// <summary>
        /// 操作用户唯一标识
        /// </summary>
        public Int32 UserID { get; set; }

        /// <summary>
        /// 操作用户名称
        /// </summary>
        public String UserName { get; set; }

        /// <summary>
        /// 操作说明
        /// </summary>
        public String OperationName { get; set; }

        /// <summary>
        /// 操作方法
        /// </summary>
        public String OperationMothod { get; set; }

        /// <summary>
        /// 操作数据内容
        /// </summary>
        public String OperationData { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime Crdate { get; set; }

        #endregion
    }
}