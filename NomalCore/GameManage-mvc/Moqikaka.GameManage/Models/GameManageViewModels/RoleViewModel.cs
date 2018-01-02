/************************************************************************
* 描述:角色视图
*************************************************************************/
using System;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Moqikaka.GameManage
{
    /// <summary>
    /// 角色视图
    /// </summary>
    public class RoleViewModel
    {
        #region 属性

        /// <summary>
        /// 角色Id
        /// </summary>
        [Display(Name = "角色Id")]
        public Int32 Id { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>   
        [Display(Name = "角色名称")]
        [Required(ErrorMessage = "请填写角色名称")]
        [Remote("Exist", "Role", ErrorMessage = "该角色名称已存在", AdditionalFields = "Id")]
        public String RolesName { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [Display(Name = "描述")]
        [Required(ErrorMessage = "请填写描述")]
        public String Remark { get; set; }

        /// <summary>
        /// 页面权限
        /// </summary>
        [Display(Name = "页面权限")]
        public String Page { get; set; }

        #endregion
    }
}