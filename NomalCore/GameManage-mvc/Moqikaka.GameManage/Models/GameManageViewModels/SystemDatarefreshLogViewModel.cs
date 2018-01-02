/************************************************************************
* 描述:系统日志刷新视图
*************************************************************************/
using System;
using System.ComponentModel.DataAnnotations;

namespace Moqikaka.GameManage
{
    /// <summary>
    /// 系统日志刷新视图
    /// </summary>
    public class SystemDataRefreshLogViewModel
    {
        #region 属性

        /// <summary>
        /// 自增id
        /// </summary>       
        public Int32 Id { get; set; }

        /// <summary>
        /// 操作用户
        /// </summary>
        [Display(Name = "操作用户")]
        public String UserName { get; set; }

        /// <summary>
        /// 操作的服务器id
        /// </summary>
        [Display(Name = "操作的服务器")]
        public String ServerGroupIds { get; set; }

        /// <summary>
        /// 操作类型(1,游戏服务器 2,聊天服务器 3,中心服务器)
        /// </summary>
        [Display(Name = "操作类型")]
        public Byte OperationType { get; set; }

        /// <summary>
        /// 是否有失败
        /// </summary>
        [Display(Name = "是否有失败")]
        public Boolean HaveError { get; set; }

        /// <summary>
        /// 操作说明
        /// </summary>
        [Display(Name = "操作说明")]
        public String Remark { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        [Display(Name = "操作时间")]
        public DateTime Crdate { get; set; }

        #endregion

        #region 附加属性

        [Display(Name = "操作的服务器")]
        public String ServerGroupNames { get; set; }

        /// <summary>
        /// 操作类型(1,游戏服务器 2,聊天服务器 3,中心服务器)
        /// </summary>
        [Display(Name = "操作类型")]
        public RefreshTypeEnum OperationTypeEnum { get; set; }

        #endregion

    }
}