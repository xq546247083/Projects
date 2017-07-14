/************************************************************************
* 标题: 玩家类
* 描述: 玩家类
* 作者： 肖强
* 日期：2017-5-3 10:43:36
* 版本：V1
*************************************************************************/
using System;
using GameServer.Model;


namespace GameServer.BLL
{
    using Tool.CustomAttribute;

    /// <summary>
    /// 玩家类
    /// </summary>
    [InvokeClassAttribute("测试所用类", "肖强", "2017-7-13 10:44:02")]
    public static partial class TestLBLL
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
            "测试方法", "肖强", "2017-7-13 10:59:13",
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
        public static ResponseDataObject I_Test(String serverId, String userId, String userPwd, String inputEncryptedString, String random)
        {
            ResponseDataObject responseDataObject = new ResponseDataObject() { ResultStatus = ResultStatus.Fail };

            return responseDataObject;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="serverId">serverId</param>
        /// <param name="userId">userId</param>
        /// <param name="userPwd">userPwd</param>
        /// <param name="inputEncryptedString">inputEncryptedString</param>
        /// <param name="random">random</param>
        [MethodDescribe(
            "测试方法2", "肖强", "2017-7-13 10:59:13",
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
        public static ResponseDataObject I_ATest2(String serverId, String userId, String userPwd, String inputEncryptedString, String random)
        {
            ResponseDataObject responseDataObject=new ResponseDataObject(){ResultStatus = ResultStatus.Fail};

            return responseDataObject;
        }
    }
}
