/************************************************************************
* 描述: 系统配置
*************************************************************************/
using System;

namespace Moqikaka.GameManage.Model
{
    using Moqikaka.GameManage.Common;

    /// <summary>
    /// 系统配置
    /// </summary>
    [DataBaseTable("system_config")]
    public sealed class SystemConfig
    {
        #region 属性

        /// <summary>
        /// 配置的键
        /// </summary>
        [PrimaryKey]
        public String ConfigKey { get; set; }

        /// <summary>
        /// 配置的值
        /// </summary>
        public String ConfigValue { get; set; }

        /// <summary>
        /// 配置描述信息
        /// </summary>
        public String ConfigDesc { get; set; }

        #endregion
    }
}
