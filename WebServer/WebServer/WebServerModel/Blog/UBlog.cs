/************************************************************************
* 标题: u_blog
* 描述: u_blog
* 数据表:u_blog
* 作者：xiaoqiang
* 日期：2017/9/10 0:25:41
* 版本：V1.0
*************************************************************************/

using System;
using System.Data;

namespace WebServer.Model
{
    using Tool.CustomAttribute;

    /// <summary>
    /// u_blog
    /// </summary>
    [DataBaseTable("u_blog")]
    public class UBlog
    {
        #region 属性

        /// <summary>
        /// 主键
        /// </summary>
        [PrimaryKeyAttribute]
        public Guid ID { set; get; }

        /// <summary>
        /// 用户id
        /// </summary>
        public Guid UserId { set; get; }

        /// <summary>
        /// 标题
        /// </summary>
        public String Title { set; get; }

        /// <summary>
        /// 内容
        /// </summary>
        public String Content { set; get; }

        /// <summary>
        /// 标签（用，号隔开）
        /// </summary>
        public String Tag { set; get; }

        /// <summary>
        /// @的用户
        /// </summary>
        public String ATUsers { set; get; }

        /// <summary>
        /// 博客类型
        /// </summary>
        public Int32 BlogType { set; get; }

        /// <summary>
        /// 状态【0：草稿，1：正常，2：删除，3：彻底删除】
        /// </summary>
        public Int32 Status { set; get; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CrDate { set; get; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime ReDate { set; get; }

        #endregion
    }
}
