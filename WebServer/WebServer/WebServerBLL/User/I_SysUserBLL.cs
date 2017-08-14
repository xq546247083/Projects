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
    using System.Linq;
    using Tool.Extension;

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

            SysUser sysUser = null;
            if (userName.IsValidEmail())
            {
                sysUser = GetItemByEmail(userName);
            }
            else
            {
                sysUser = GetItemByUserName(userName);
            }

            if (sysUser == null)
            {
                result.ResultStatus = ResultStatus.UserIsNotExist;
                return result;
            }

            //判断密码是否为空密码
            if (userPwd == "d41d8cd98f00b204e9800998ecf8427e")
            {
                result.ResultStatus = ResultStatus.PlsEnterPassword;
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
        /// <param name="fullName">fullName</param>
        /// <param name="sex">sex</param>
        /// <param name="phone">phone</param>
        /// <param name="email">email</param>
        /// <param name="identifyCode">验证码</param>
        [MethodDescribe(
            "注册", "肖强", "2017-8-13 15:35:14",
@"{
    UserName:用户名
    UserPwd:密码
    FullName:用户名称
    Sex：性别
    Phone：电话
    Email：邮箱
    identifyCode:验证码
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
        public static ResponseDataObject I_Register(String userName, String userPwd, String fullName, Int32 sex, String phone, String email, String identifyCode)
        {
            ResponseDataObject result = new ResponseDataObject() { ResultStatus = ResultStatus.Fail };

            #region 检测请求

            SysUser sysUser = GetItemByUserName(userName);

            if (sysUser != null)
            {
                result.ResultStatus = ResultStatus.UserNameIsExist;
                return result;
            }

            if (String.IsNullOrEmpty(userName))
            {
                result.ResultStatus = ResultStatus.UserNameCantBeEmpty;
                return result;
            }

            if (!char.IsLetter(userName[0]))
            {
                result.ResultStatus = ResultStatus.UserNameMustBeginWithLetter;
                return result;
            }

            if (userName.ToCharArray().Any(r => !char.IsLetterOrDigit(r)))
            {
                result.ResultStatus = ResultStatus.UserNameMustBeLetterOrNum;
                return result;
            }

            if (!phone.IsValidPhone())
            {
                result.ResultStatus = ResultStatus.PhoneStyleIsError;
                return result;
            }

            if (String.IsNullOrEmpty(email))
            {
                result.ResultStatus = ResultStatus.EmailCanBeNotEmpty;
                return result;
            }

            if (!email.IsValidEmail())
            {
                result.ResultStatus = ResultStatus.EmailStyleIsError;
                return result;
            }

            //判断邮箱是否已注册
            var allUser = GetData();
            if (allUser.Any(r => r.Value.Email == email))
            {
                result.ResultStatus = ResultStatus.EmailAlreadyExist;
                return result;
            }

            if (String.IsNullOrEmpty(identifyCode))
            {
                result.ResultStatus = ResultStatus.PlsEnterIdentifyCode;
                return result;
            }

            if (!mEmailData.ContainsKey(email))
            {
                result.ResultStatus = ResultStatus.IdentifyCodeNoThisEmail;
                return result;
            }

            if (mEmailData[email] != identifyCode.ToUpper())
            {
                result.ResultStatus = ResultStatus.IdentifyCodeIsError;
                return result;
            }

            //判断密码是否为空密码
            if (userPwd == "d41d8cd98f00b204e9800998ecf8427e")
            {
                result.ResultStatus = ResultStatus.UserPasswordCanBeNotEmpty;
                return result;
            }

            #endregion

            #region 构造玩家数据

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

        /// <summary>
        /// 验证邮箱
        /// </summary>
        /// <param name="email">email</param>
        [MethodDescribe(
            "验证邮箱", "肖强", "2017-8-13 15:35:14",
@"{
    Email：邮箱
}           ", @"[]")]
        public static ResponseDataObject I_Identify(String email)
        {
            ResponseDataObject result = new ResponseDataObject() { ResultStatus = ResultStatus.Fail };

            #region 检测请求

            if (String.IsNullOrEmpty(email))
            {
                result.ResultStatus = ResultStatus.EmailCanBeNotEmpty;
                return result;
            }

            if (!email.IsValidEmail())
            {
                result.ResultStatus = ResultStatus.EmailStyleIsError;
                return result;
            }

            //判断邮箱是否已注册
            var allUser = GetData();
            if (allUser.Any(r => r.Value.Email == email))
            {
                result.ResultStatus = ResultStatus.EmailAlreadyExist;
                return result;
            }

            #endregion

            #region 处理请求

            var keyStr = RandomTool.GetRandomStr(6);

            var flag = EmailTool.SendMail(new String[] { email }, "注册验证码", String.Format("验证码:{0}", keyStr), SendPattern.Synchronous);
            if (!flag)
            {
                result.ResultStatus = ResultStatus.SendEmailFail;
                return result;
            }

            mEmailData[email] = keyStr;

            #endregion

            #region 处理返回

            result.ResultStatus = ResultStatus.Success;

            return result;

            #endregion
        }
    }
}
