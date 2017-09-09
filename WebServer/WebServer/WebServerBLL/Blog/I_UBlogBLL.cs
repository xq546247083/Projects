/************************************************************************
* 标题: 博客类
* 描述: 博客类
* 作者： 肖强
* 日期：2017-5-3 10:43:36
* 版本：V1
*************************************************************************/
using System;

namespace WebServer.BLL
{
    using Tool.Common;
    using Tool.CustomAttribute;
    using WebServer.Model;

    /// <summary>
    /// 博客类
    /// </summary>
    [InvokeClassAttribute("博客类", "肖强", "2017-7-13 10:44:02")]
    public partial class UBlogBLL
    {
        /// <summary>
        /// 获取博客信息
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="blogType">博客类型</param>
        /// <param name="status">状态</param>
        /// <param name="tagInfo">标签</param>
        [MethodDescribe(
            "获取博客列表", "肖强", "2017-7-13 10:59:13",
@"{
    UserName:用户名
    blogType:博客类型
    status：状态
    tagInfo：标签
}           ",
@"
Value:博客列表信息
[
    {
        ID：博客Id
        Title：博客标题
        Content:博客内容
        Tag:博客tag
        ATUsers：艾特的用户
        BlogType:博客类型
        Status;状态
        CrDate：创建时间
        ReDate：更新时间
    }
]            ")]
        public static ResponseDataObject I_GetBlogList(String userName, Int32 blogType, Int32 status, String tagInfo)
        {
            ResponseDataObject result = new ResponseDataObject() { ResultStatus = ResultStatus.Fail };

            #region 检测请求

            SysUser sysUser = SysUserBLL.GetItemByUserNameOrEmail(userName);
            if (sysUser == null)
            {
                result.ResultStatus = ResultStatus.UserIsNotExist;
                return result;
            }

            #endregion

            #region 处理返回

            result.ResultStatus = ResultStatus.Success;
            result.Value = AssembleToClient(sysUser, blogType, status, tagInfo);

            return result;

            #endregion
        }
    }
}
