/************************************************************************
* 标题: 博客状态枚举
* 描述: 博客状态枚举
* 作者：肖强
* 日期：2017-5-3 10:43:36
* 版本：V1
*************************************************************************/

using System.ComponentModel;

namespace WebServer.Model
{
    /// <summary>
    /// 博客状态枚举
    /// </summary>
    public enum BlogStatusEnum : int
    {
        /// <summary>
        /// 草稿
        /// </summary>
        [Description("草稿")]
        Draft = 0,

        /// <summary>
        /// 正常状态
        /// </summary>
        [Description("正常状态")]
        Common = 1,

        /// <summary>
        /// 删除
        /// </summary>
        [Description("删除")]
        Delete = 2,

        /// <summary>
        /// 彻底删除
        /// </summary>
        [Description("彻底删除")]
        Remove = 3
    }
}
