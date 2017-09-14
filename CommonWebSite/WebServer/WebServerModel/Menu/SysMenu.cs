/************************************************************************
* 标题: sys_menu
* 描述: sys_menu
* 数据表:sys_menu
* 作者：xiaoqiang
* 日期：2017/9/8 20:41:09
* 版本：V1.0
*************************************************************************/

using System;
using System.Data;

namespace WebServer.Model
{
    using Tool.CustomAttribute;

    /// <summary>
    /// sys_menu
    /// </summary>
    [DataBaseTable("sys_menu")]
    public class SysMenu
    {
        #region 属性

        /// <summary>
        /// 菜单标识
        /// </summary>
        [PrimaryKeyAttribute]
        public Int32 MenuID { set; get; }

        /// <summary>
        /// 上级ID
        /// </summary>
        public Int32 ParentMenuID { set; get; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        public String MenuName { set; get; }

        /// <summary>
        /// 菜单地址
        /// </summary>
        public String MenuUrl { set; get; }

        /// <summary>
        /// 排序号
        /// </summary>
        public Int32 SortOrder { set; get; }

        /// <summary>
        /// 菜单图标路径（未用到）
        /// </summary>
        public String MenuIcon { set; get; }

        /// <summary>
        /// 常用菜单图标（未用到）
        /// </summary>
        public String BigMenuIcon { set; get; }

        /// <summary>
        /// 快捷键（未用到）
        /// </summary>
        public String ShortCut { set; get; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public Boolean IsShow { set; get; }

        #endregion
    }
}
