/************************************************************************
* 标题: 玩家类
* 描述: 玩家类
* 作者： 肖强
* 日期：2017-5-3 10:43:36
* 版本：V1
*************************************************************************/
using System;

namespace WebServer.BLL
{
    using WebServer.Model;
    using Tool.CustomAttribute;
    using Tool.Common;

    /// <summary>
    /// 玩家类
    /// </summary>
    [InvokeClassAttribute("玩家类", "肖强", "2017-7-13 10:44:02")]
    public partial class SysUserBLL
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userName">userName</param>
        /// <param name="userPwd">userPwd</param>
        [MethodDescribe(
            "登录", "肖强", "2017-7-13 10:59:13",
@"{
    UserName:用户名
    UserPwd:用户密码
}           ",
@"[
    UserName:是否成功登陆
    FullName:用户名
    Sex:性别
    Phone：电话
    Email：邮箱
    LastLoginTime:最后登录名
    LastLoginIP：最近登录ip
    LoginCount:登录次数
    Status：状态
    CreateTime:创建时间
]            ")]
        public static ResponseDataObject I_Login(String userName, String userPwd)
        {
            ResponseDataObject result = new ResponseDataObject() { ResultStatus = ResultStatus.Fail };

            #region 检测请求

            SysUser sysUser = GetItemByUserName(userName);

            if (sysUser == null)
            {
                result.ResultStatus = ResultStatus.UserIsNotExist;
                return result;
            }

            if (sysUser.Password != EncrpytTool.Encrypt(userPwd))
            {
                result.ResultStatus = ResultStatus.PwdError;
                return result;
            }

            #endregion

            #region 处理请求

            TransactionHandler.Handle(() =>
            {
                sysUser.LastLoginTime = DateTime.Now;
                sysUser.LoginCount += 1;

                Update(sysUser);
            }, null);

            #endregion

            #region 处理返回

            result.ResultStatus = ResultStatus.Success;
            result.Value = AssembleToClient(sysUser);

            return result;

            #endregion
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="userName">userName</param>
        /// <param name="userPwd">userPwd</param>
        [MethodDescribe(
            "注册", "肖强", "2017-8-13 15:35:14",
@"{
    UserName:用户名
    UserPwd:密码
    FullName:用户名称
    Sex：性别
    Phone：电话
    Email：邮箱
}           ",
@"[
    UserName:是否成功登陆
    FullName:用户名
    Sex:性别
    Phone：电话
    Email：邮箱
    LastLoginTime:最后登录名
    LastLoginIP：最近登录ip
    LoginCount:登录次数
    Status：状态
    CreateTime:创建时间
]            ")]
        public static ResponseDataObject I_Register(String userName, String userPwd, String fullName, Int32 sex, String phone, String email)
        {
            ResponseDataObject result = new ResponseDataObject() { ResultStatus = ResultStatus.Fail };

            #region 检测请求

            SysUser sysUser = GetItemByUserName(userName);

            if (sysUser != null)
            {
                result.ResultStatus = ResultStatus.UserNameIsExist;
                return result;
            }
            var sexFlag = true;
            Boolean.TryParse(sex.ToString(), out sexFlag);

            sysUser = new SysUser()
            {
                UserID = Guid.NewGuid(),
                UserName = userName,
                Password = EncrpytTool.Encrypt(userPwd),
                FullName = fullName,
                Sex = sexFlag,
                Phone = phone,
                Email = email,
                Status = 1,
                LoginCount = 0,
                CreateTime = DateTime.Now
            };

            #endregion

            #region 处理请求

            TransactionHandler.Handle(() =>
            {
                Insert(sysUser);
            }, null);

            #endregion

            #region 处理返回

            result.ResultStatus = ResultStatus.Success;
            result.Value = AssembleToClient(sysUser);

            return result;

            #endregion
        }
    }
}
