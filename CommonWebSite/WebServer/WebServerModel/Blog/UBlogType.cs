/************************************************************************
* 标题: u_blog_type
* 描述: u_blog_type
* 数据表:u_blog_type
* 作者：xiaoqiang
* 日期：2017/9/9 22:19:04
* 版本：V1.0
*************************************************************************/

using System;
using System.Data;

namespace WebServer.Model
{
    using Tool.CustomAttribute;

    /// <summary>
    /// u_blog_type
    /// </summary>
    [DataBaseTable("u_blog_type")]
    public class UBlogType
    {
        #region 属性

        /// <summary>
        /// 主键
        /// </summary>
        [PrimaryKeyAttribute]
        public Int32 ID { set; get; }

        /// <summary>
        /// 类型名
        /// </summary>
        public String Name { set; get; }

        /// <summary>
        /// 图标
        /// </summary>
        public String Icon { set; get; }

        /// <summary>
        /// 是否展示
        /// </summary>
        public Boolean IsPublic { set; get; }

        #endregion
    }
}
