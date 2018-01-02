/************************************************************************
* 描述: 服务器刷新记录
*************************************************************************/
using System;

namespace Moqikaka.GameManage.Model
{
    using Moqikaka.GameManage.Common;

    /// <summary>
    /// 服务器刷新记录
    /// </summary>
    [DataBaseTable("system_data_refresh_log")]
    public sealed class SystemDataRefreshLog
    {
        #region 属性

        /// <summary>
        /// 自增id
        /// </summary>
        [PrimaryKey]
        public Int32 ID { get; set; }

        /// <summary>
        /// 操作用户
        /// </summary>
        public String UserName { get; set; }

        /// <summary>
        /// 操作的服务器id
        /// </summary>
        public String ServerGroupIDs { get; set; }

        /// <summary>
        /// 操作类型(1,游戏服务器 2,聊天服务器 3,中心服务器)
        /// </summary>
        public Byte OperationType { get; set; }

        /// <summary>
        /// 是否有失败
        /// </summary>
        public Boolean HaveError { get; set; }

        /// <summary>
        /// 操作说明
        /// </summary>
        public String Remark { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime Crdate { get; set; }

        #endregion
    }
}