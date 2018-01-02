/************************************************************************
* 描述: 发放月卡记录
*************************************************************************/
using System;

namespace Moqikaka.GameManage.Model
{
    using Moqikaka.GameManage.Common;

    /// <summary>
    /// 发放月卡记录
    /// </summary>
    [DataBaseTable("system_month_card_log")]
    public sealed class SystemMonthCardLog
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
        public Int32 ServerGroupID { get; set; }

        /// <summary>
        /// 玩家名称
        /// </summary>
        public String PlayerNames { get; set; }

        /// <summary>
        /// 月卡类型(1,20月卡 2,50月卡)
        /// </summary>
        public Byte MonthCardType { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime Crdate { get; set; }

        #endregion
    }
}