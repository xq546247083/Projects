/************************************************************************
* 标题: 客户端处理
* 描述: 客户端处理
* 作者：肖强
* 日期：2017-5-3 10:43:36
* 版本：V1
*************************************************************************/
using System;
using System.Web;
using Newtonsoft.Json.Serialization;
using WebServer.BLL;

namespace WebSite.API
{
    using WebServer.Model;
    using Tool.Common;
    using Tool.Extension;

    /// <summary>
    /// Handler 的摘要说明
    /// </summary>
    public class ClientHandler : BaseAPI, IHttpHandler
    {
        /// <summary>
        /// 程序集名称
        /// </summary>
        private const String mAssemblyName = "WebServer.BLL";

        /// <summary>
        /// 类添加
        /// </summary>
        private const String mClassFlag = "BLL";

        /// <summary>
        /// 方法添加
        /// </summary>
        private const String mMethodFlag = "I_";

        /// <summary>
        /// 处理数据
        /// </summary>
        /// <param name="context">请求</param>
        public void ProcessRequest(HttpContext context)
        {
            ResponseDataObject responseDataObject = new ResponseDataObject() { ResultStatus = ResultStatus.Fail };
            try
            {
                //获取请求
                RequestDataObject requestDataObject = GetRequestDataObject(context);

                //处理请求
                responseDataObject = ReflectionTool.CallStaticMethod(mAssemblyName, String.Format("{0}.{1}{2}", mAssemblyName, requestDataObject.ClassName, mClassFlag), String.Format("{0}{1}", mMethodFlag, requestDataObject.MethodName), requestDataObject.Data) as ResponseDataObject;
                //如果有用户名，如果过期，则直接返回过期，如果没有，则更新过期时间
                if (!String.IsNullOrEmpty(requestDataObject.UserName) && requestDataObject.UserName != "null")
                {
                    if (requestDataObject.MethodName != "Login"
                        && requestDataObject.MethodName != "Register"
                        && requestDataObject.MethodName != "Identify"
                        && requestDataObject.MethodName != "Retrieve")
                    {
                        if (SysUserBLL.CheckPwdExpiredTime(requestDataObject.UserName) && responseDataObject != null)
                        {
                            responseDataObject.ResultStatus = ResultStatus.LoginIsOverTime;
                        }
                        else if (responseDataObject != null)
                        {
                            SysUserBLL.UpdatePwdExpiredTime(requestDataObject.UserName);
                            //返回过期时间
                            var sysUser = SysUserBLL.GetItemByUserNameOrEmail(requestDataObject.UserName);
                            if (sysUser != null)
                            {
                                responseDataObject.PwdExpiredTime = DateTimeTool.GetUnixTime(sysUser.PwdExpiredTime);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (responseDataObject != null)
                {
                    //自定义错误处理
                    if (ex is SelfDefinedException)
                    {
                        SelfDefinedException innerEx = ex as SelfDefinedException;
                        responseDataObject.ResultStatus = innerEx.ResultStatus;
                        if (innerEx.IfNeedLog)
                        {
                            Log.Write(ex.ToMessage(), LogType.Error);
                        }
                    }
                    else
                    {
                        responseDataObject.ResultStatus = ResultStatus.Exception;
                        Log.Write(ex.ToMessage(), LogType.Error);
                    }

                    responseDataObject.Value = ex.ToMessage();
                }
                else
                {
                    responseDataObject = new ResponseDataObject() { ResultStatus = ResultStatus.Fail };
                    responseDataObject.ResultStatus = ResultStatus.Exception;
                }
            }
            finally
            {
                //返回请求
                context.Response.Headers.Add("Access-Control-Allow-Origin", "*");//添加跨服返回的值
                context.Response.Write(GetResponseData(responseDataObject));
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}