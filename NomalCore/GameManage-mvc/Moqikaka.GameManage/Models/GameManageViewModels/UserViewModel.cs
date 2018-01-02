/************************************************************************
* 描述:用户视图
*************************************************************************/
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Moqikaka.GameManage
{
    /// <summary>
    /// 用户视图
    /// </summary>
    public class UserViewModel
    {
        #region 属性

        /// <summary>
        /// 用户唯一标识
        /// </summary>
        [Display(Name = "id")]
        public Int32 UserID { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        [Display(Name = "登录名")]
        [Required(ErrorMessage = "请填写登录名")]
        [Remote("Exist", "User", ErrorMessage = "该登录名已存在")]
        public String UserName { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        [Display(Name = "用户密码")]
        [Required(ErrorMessage = "请填写登录密码")]
        public String UserPwd { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        [Display(Name = "添加时间")]
        public DateTime Crdate { get; set; }

        /// <summary>
        /// 上次登陆时间
        /// </summary>
        [Display(Name = "上次登陆时间")]
        public DateTime LastLoginTime { get; set; }

        /// <summary>
        /// 上次登陆IP
        /// </summary>
        [Display(Name = "上次登陆IP")]
        public String LastLoginIP { get; set; }

        /// <summary>
        /// 是否超级用户
        /// </summary>
        [Display(Name = "是否超级用户")]
        public Boolean IfSuper { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        [Display(Name = "角色ID")]
        public Int32 UserRole { get; set; }

        /// <summary>
        /// 状态(0-正常，-1-锁定)
        /// </summary>
        [Display(Name = "帐号状态")]
        public Int32 Status { get; set; }

        #endregion

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "当前密码")]
        public String OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "新密码")]
        public String NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "确认新密码")]
        [System.ComponentModel.DataAnnotations.Compare("NewPassword", ErrorMessage = "新密码和确认密码不匹配。")]
        public String ConfirmPassword { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        [Display(Name = "角色名称")]
        [Remote("Exist", "User", ErrorMessage = "该用户名称已存在")]
        public String UserRoleName { get; set; }

        /// <summary>
        /// 用户UI菜单
        /// </summary>
        [Display(Name = "用户ui菜单")]
        [Required(ErrorMessage = "请为用户分配菜单")]
        public String MenuId { get; set; }

        /// <summary>
        /// 页面提示信息
        /// </summary>
        public String Message { get; set; }

        /// <summary>
        /// 进入的url
        /// </summary>
        public String ReturnUrl { get; set; }
    }
}