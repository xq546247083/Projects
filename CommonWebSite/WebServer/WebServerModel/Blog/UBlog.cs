/************************************************************************
* 标题: 博客表
* 描述: 博客表
* 数据表:u_blog
* 作者：xiaoqiang
* 日期：2017/9/23 2:06:23
* 版本：V1.0
*************************************************************************/

using System;
using System.Data;

namespace WebServer.Model
{
    using Tool.CustomAttribute;

    /// <summary>
    /// 博客表
    /// </summary>
    [DataBaseTable("u_blog")]
    public class UBlog
    {
        #region 属性

        /// <summary>
        /// 主键
        /// </summary>
        [PrimaryKey]
        public Guid ID { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public String Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public String Content { get; set; }

        /// <summary>
        /// 标签（用，号隔开）
        /// </summary>
        public String Tag { get; set; }

        /// <summary>
        /// @的用户
        /// </summary>
        public String ATUsers { get; set; }

        /// <summary>
        /// 博客类型
        /// </summary>
        public Int32 BlogType { get; set; }

        /// <summary>
        /// 状态【0：草稿，1：正常，2：删除，3：彻底删除】
        /// </summary>
        public Byte Status { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CrDate { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime ReDate { get; set; }

        #endregion
    }
}
