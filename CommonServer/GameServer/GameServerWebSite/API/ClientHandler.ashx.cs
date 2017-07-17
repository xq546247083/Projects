using System;
using System.Web;
using Tool.Log;

namespace GameServerWebSite.API
{
    using GameServer.Model;
    using Tool.Common;

    /// <summary>
    /// Handler 的摘要说明
    /// </summary>
    public class ClientHandler : BaseAPI, IHttpHandler
    {
        /// <summary>
        /// 程序集名称
        /// </summary>
        private const String mAssemblyName = "GameServer.BLL";

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
                responseDataObject = ReflectionTool.CallStaticMethod(mAssemblyName, String.Format("{0}{1}", requestDataObject.ClassName, mClassFlag), String.Format("{0}{1}", mMethodFlag, requestDataObject.MethodName), requestDataObject.Data) as ResponseDataObject;
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
                            Log.Write(ExHandler.Handle(ex), LogType.Error);
                        }
                    }
                    else
                    {
                        responseDataObject.ResultStatus = ResultStatus.Exception;
                        Log.Write(ExHandler.Handle(ex), LogType.Error);
                    }

                    responseDataObject.Value = ExHandler.Handle(ex);
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