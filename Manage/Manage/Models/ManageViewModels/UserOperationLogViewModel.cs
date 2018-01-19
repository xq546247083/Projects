/************************************************************************
* 描述:用户操作日志视图
*************************************************************************/
using System;
using System.ComponentModel.DataAnnotations;

namespace Manage
{

    /// <summary>
    /// 用户日志操作页面模型
    /// </summary>
    public class UserOperationLogViewModel
    {
        /// <summary>
        /// 日志唯一标识
        /// </summary>
        [Display(Name = "日志标识")]
        public Int32 ID { get; set; }

        /// <summary>
        /// 操作用户唯一标识
        /// </summary>      
        [Display(Name = "用户标识")]
        public Int32 UserID { get; set; }

        /// <summary>
        /// 操作用户名称
        /// </summary>
        [Display(Name = "操作用户")]
        public String UserName { get; set; }

        /// <summary>
        /// 操作说明
        /// </summary>
        [Display(Name = "操作说明")]
        public String OperationName { get; set; }

        /// <summary>
        /// 操作方法
        /// </summary>
        [Display(Name = "操作方法")]
        public String OperationMothod { get; set; }

        /// <summary>
        /// 操作数据内容
        /// </summary>
        [Display(Name = "操作数据")]
        public String OperationData { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        [Display(Name = "操作时间")]
        public DateTime Crdate { get; set; }
    }
}