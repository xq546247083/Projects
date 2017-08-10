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

    /// <summary>
    /// 玩家类
    /// </summary>
    [InvokeClassAttribute("玩家类", "肖强", "2017-7-13 10:44:02")]
    public partial class PlayerBLL
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="serverId">serverId</param>
        /// <param name="userId">userId</param>
        /// <param name="userPwd">userPwd</param>
        /// <param name="inputEncryptedString">inputEncryptedString</param>
        /// <param name="random">random</param>
        [MethodDescribe(
            "登录", "肖强", "2017-7-13 10:59:13",
@"{
    serverId:服务器Id
    userId:用户Id
    userPwd:用户密码
    inputEncryptedString：加密字符串
    random:随机数
}           ",
@"[
    IsSuccess:是否成功登陆
]            ")]
        public static ResponseDataObject I_Login(String serverId, String userId, String userPwd, String inputEncryptedString, String random)
        {
            ResponseDataObject result = new ResponseDataObject() { ResultStatus = ResultStatus.Fail };

            #region 检测请求

            Player player = PlayerBLL.GetItem(userId);

            if (player.UserPwd != userPwd)
            {
                result.ResultStatus = ResultStatus.PwdError;
                return result;
            }

            //检测密码
            //String loginStr = String.Format("{0}{1}{2}{3}", serverId, userId, userPwd, random);
            //var md5 = new MD5CryptoServiceProvider();
            //string loginStrMd = BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(loginStr)));
            //if (inputEncryptedString != loginStrMd)
            //{
            //    result.ResultStatus = ResultStatus.PwdError;
            //    return result;
            //}

            #endregion

            #region 处理请求

            TransactionHandler.Handle(() =>
            {
                player.IsOnline = true;
                player.OnlieTime = DateTime.Now;
                Update(player);
            }, null);

            #endregion

            #region 处理返回

            result.ResultStatus = ResultStatus.Success;
            result.Value = AssembleToClient(player);

            return result;

            #endregion
        }
    }
}
