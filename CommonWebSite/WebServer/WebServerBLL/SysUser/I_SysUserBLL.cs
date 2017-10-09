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
    UserName:用户账号
    FullName:用户名
    Sex:性别
    Phone：电话
    Email：邮箱
    LastLoginTime:最后登录名
    LastLoginIP：最近登录ip
    LoginCount:登录次数
    Status：状态
    CreateTime:创建时间
    PwdExpiredTime：密码过期时间
]            ")]
        public static ResponseDataObject I_Login(String userName, String userPwd)
        {
            ResponseDataObject result = new ResponseDataObject() { ResultStatus = ResultStatus.Fail };

            #region 检测请求

            SysUser sysUser = GetItemByUserNameOrEmail(userName);
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
                sysUser.PwdExpiredTime = DateTime.Now.AddHours(WebConfig.PwdExpiredTime);
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
        /// 退出
        /// </summary>
        /// <param name="userName">userName</param>
        [MethodDescribe(
            "退出", "肖强", "2017-7-13 10:59:13",
@"{
    UserName:用户名
}           ",
@"[
    UserName:用户账号
    FullName:用户名
    Sex:性别
    Phone：电话
    Email：邮箱
    LastLoginTime:最后登录名
    LastLoginIP：最近登录ip
    LoginCount:登录次数
    Status：状态
    CreateTime:创建时间
    PwdExpiredTime：密码过期时间
]            ")]
        public static ResponseDataObject I_LoginOut(String userName)
        {
            ResponseDataObject result = new ResponseDataObject() { ResultStatus = ResultStatus.Fail };

            #region 检测请求

            SysUser sysUser = GetItemByUserNameOrEmail(userName);
            if (sysUser == null)
            {
                result.ResultStatus = ResultStatus.UserIsNotExist;
                return result;
            }

            #endregion

            #region 处理请求

            TransactionHandler.Handle(() =>
            {
                sysUser.PwdExpiredTime = DateTime.Now;

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
        /// <param name="email">email</param>
        /// <param name="identifyCode">验证码</param>
        [MethodDescribe(
            "注册", "肖强", "2017-8-13 15:35:14",
@"{
    UserName:用户名
    UserPwd:密码
    FullName:用户名称
    Sex：性别
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
    PwdExpiredTime：密码过期时间
]            ")]
        public static ResponseDataObject I_Register(String userName, String userPwd, String fullName, Int32 sex, String email, String identifyCode)
        {
            ResponseDataObject result = new ResponseDataObject() { ResultStatus = ResultStatus.Fail };

            #region 检测请求

            if (String.IsNullOrEmpty(userName))
            {
                result.ResultStatus = ResultStatus.UserNameCantBeEmpty;
                return result;
            }

            if (String.IsNullOrEmpty(fullName))
            {
                result.ResultStatus = ResultStatus.UserNameCantBeEmpty;
                return result;
            }

            SysUser sysUser = GetItemByUserName(userName);
            if (sysUser != null)
            {
                result.ResultStatus = ResultStatus.UserNameIsExist;
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

            if (mEmailData[email].Item1 != identifyCode.ToUpper())
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

            //获取注册用户默认角色
            string roleIds = "";
            var roleList = SysRoleBLL.GetData().Where(r => r.Value.IsDefault).ToList();
            foreach (var item in roleList)
            {
                roleIds += item.Value.RoleID + ",";
            }

            if (roleIds.Length > 0)
            {
                roleIds = roleIds.Substring(0, roleIds.Length - 1);
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
                Phone = String.Empty,
                Email = email,
                Status = 1,
                LoginCount = 0,
                RoleIDs = roleIds,
                CreateTime = DateTime.Now,
                PwdExpiredTime = DateTime.Now.AddHours(WebConfig.PwdExpiredTime)
            };

            #endregion

            #region 处理请求

            //先插入数据到数据库，如果插入成功，则更新内存
            TransactionHandler.Handle(() =>
            {
                Insert(sysUser);
            }, () =>
            {
                //更新内存
                allUser[sysUser.UserID] = sysUser;
                if (mEmailData.ContainsKey(email))
                {
                    mEmailData.Remove(email);
                }
            });

            #endregion

            #region 处理返回

            result.ResultStatus = ResultStatus.Success;
            result.Value = AssembleToClient(sysUser);

            return result;

            #endregion
        }

        /// <summary>
        /// 找回密码
        /// </summary>
        /// <param name="userPwd">userPwd</param>
        /// <param name="email">email</param>
        /// <param name="identifyCode">验证码</param>
        [MethodDescribe(
            "找回密码", "肖强", "2017-8-13 15:35:14",
@"{
    UserPwd:密码
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
    PwdExpiredTime：密码过期时间
]            ")]
        public static ResponseDataObject I_Retrieve(String userPwd, String email, String identifyCode)
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

            SysUser sysUser = GetItemByEmail(email);
            if (sysUser == null)
            {
                result.ResultStatus = ResultStatus.UserIsNotExist;
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

            if (mEmailData[email].Item1 != identifyCode.ToUpper())
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

            #region 处理数据

            sysUser.Password = EncrpytTool.Encrypt(userPwd);
            sysUser.PwdExpiredTime = DateTime.Now.AddHours(WebConfig.PwdExpiredTime);

            #endregion

            #region 处理请求

            TransactionHandler.Handle(() =>
            {
                Update(sysUser);
                if (mEmailData.ContainsKey(email))
                {
                    mEmailData.Remove(email);
                }
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
        /// <param name="style">style</param>
        [MethodDescribe(
            "验证邮箱", "肖强", "2017-8-13 15:35:14",
@"{
    Email：邮箱
    style:验证方式，0是登录页面，1是找回密码页面
}           ", @"[]")]
        public static ResponseDataObject I_Identify(String email, Int32 style)
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

            //注册时，判断邮箱是否未注册
            var allUser = GetData();
            if (allUser.Any(r => r.Value.Email == email) && style == 0)
            {
                result.ResultStatus = ResultStatus.EmailAlreadyExist;
                return result;
            }

            //找回时，判断邮箱是否已注册
            if (allUser.Count(r => r.Value.Email == email) == 0 && style == 1)
            {
                result.ResultStatus = ResultStatus.EmailIsNotRegister;
                return result;
            }

            if (mEmailData.ContainsKey(email) && mEmailData[email].Item2.AddMinutes(1) > DateTime.Now)
            {
                result.ResultStatus = ResultStatus.SendEmailIsFast;
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

            mEmailData[email] = new Tuple<String, DateTime>(keyStr, DateTime.Now);

            #endregion

            #region 处理返回

            result.ResultStatus = ResultStatus.Success;

            return result;

            #endregion
        }
    }
}
