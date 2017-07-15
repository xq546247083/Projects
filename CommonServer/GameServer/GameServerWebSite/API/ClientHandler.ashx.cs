using System;
using System.Web;

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
        private const String mMethodFlag = "i_";

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
                    responseDataObject.ResultStatus = ResultStatus.Exception;
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