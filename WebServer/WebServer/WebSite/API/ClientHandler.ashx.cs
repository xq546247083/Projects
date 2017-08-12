/************************************************************************
* 标题: 客户端处理
* 描述: 客户端处理
* 作者：肖强
* 日期：2017-5-3 10:43:36
* 版本：V1
*************************************************************************/
using System;
using System.Web;

namespace WebSite.API
{
    using WebServer.Model;
    using Tool.Common;
    using Tool.Log;
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
            }
            catch (Exception ex)
            {
                if (responseDataObject != null)
                {
                    //自定义错误处理
                    if (ex.InnerException is SelfDefinedException)
                    {
                        SelfDefinedException innerEx = ex.InnerException as SelfDefinedException;
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
            }
            finally
            {
                //返回请求
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