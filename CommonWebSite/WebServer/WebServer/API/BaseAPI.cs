/************************************************************************
* 标题: 基础API
* 描述: 基础API
* 作者：肖强
* 日期：2017-5-3 10:43:36
* 版本：V1
*************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Web;


namespace WebSite.API
{
    using Newtonsoft.Json;
    using Tool.Common;
    using WebServer.BLL;
    using WebServer.Model;

    /// <summary>
    /// 基础API
    /// </summary>
    public class BaseApi
    {
        /// <summary>
        /// 获取请求数据
        /// </summary>
        /// <param name="httpContext">请求的httpContext</param>
        /// <returns>请求结果</returns>
        public RequestDataObject GetRequestDataObject(HttpContext httpContext)
        {
            //获取POST过来的二进制数组,并转换为string
            Byte[] byteArray = httpContext.Request.BinaryRead(httpContext.Request.TotalBytes);
            String value = Encoding.UTF8.GetString(byteArray);

            //将客户端请求的数据反序列化
            var result = JsonConvert.DeserializeObject<RequestDataObject>(value);
            result.Data = MethodInfoBLL.ConvertParameters(result.Data, result.ClassName, result.MethodName);

            Log.Write("服务器收到请求:" + value, LogType.Debug);

            return result;
        }

        /// <summary>
        /// 返回结果
        /// </summary>
        /// <param name="responseDataObject">返回的结果</param>
        /// <returns>请求结果</returns>
        public String GetResponseData(ResponseDataObject responseDataObject)
        {
            if (responseDataObject == null)
            {
                responseDataObject = new ResponseDataObject() { ResultStatus = ResultStatus.Fail };
            }
            
            Dictionary<String, Object> responseObj = new Dictionary<String, Object>();
            responseObj["Status"] = responseDataObject.Status;
            responseObj["StatusValue"] = responseDataObject.StatusValue;
            responseObj["PwdExpiredTime"] = responseDataObject.PwdExpiredTime;
            responseObj["Value"] = responseDataObject.Value;

            //将结果序列化并返回
            var returnValueStr = JsonConvert.SerializeObject(responseObj);

            Log.Write("服务器返回数据:" + returnValueStr, LogType.Debug);
            return returnValueStr;
        }
    }
}