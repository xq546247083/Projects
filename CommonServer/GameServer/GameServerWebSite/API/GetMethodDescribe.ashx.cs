using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using GameServer.BLL;
using GameServer.DAL;
using GameServer.Model.Const;
using Newtonsoft.Json;
using Tool.Common;
using Tool.CustomAttribute;

namespace GameServerWebSite.API
{
    /// <summary>
    /// GetMethodDescribe 的摘要说明
    /// </summary>
    public class GetMethodDescribe : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            String operationStr = context.Request.Form["Operation"];
            if (operationStr.Equals("GetInvokeClassDescribe"))
            {
                context.Response.Write(GetInvokeClassDescribe());
            }
            else
            {
                String className = context.Request.Form["ClassName"];
                context.Response.Write(GetInvokeMethodDescribe(className));
            }
        }

        /// <summary>
        /// 获取调用类的描述
        /// </summary>
        /// <returns>描述</returns>
        private String GetInvokeClassDescribe()
        {
            //返回结果
            List<Dictionary<String, String>> result = new List<Dictionary<String, String>>();

            var typeList = ReflectionTool.GetTypeListOfAttribute(typeof(WorldBLL).Assembly, typeof(InvokeClassAttribute));
            foreach (var type in typeList)
            {
                //获取类型的描述
                var customTypeAttribute = type.GetCustomAttribute(typeof(InvokeClassAttribute), false) as InvokeClassAttribute;
                if (customTypeAttribute != null)
                {
                    Dictionary<String, String> typeDescribe = new Dictionary<String, String>();
                    typeDescribe[CommonConst.Name] = type.Name.Substring(0, type.Name.Length - 3);
                    typeDescribe[CommonConst.Creator] = customTypeAttribute.Creator;
                    typeDescribe[CommonConst.CreateDate] = customTypeAttribute.CreateDate;
                    typeDescribe[CommonConst.Describe] = customTypeAttribute.Describe;

                    result.Add(typeDescribe);
                }
            }

            return JsonConvert.SerializeObject(result.OrderBy(r => r[CommonConst.Name]));
        }

        /// <summary>
        /// 获取调用类的方法描述
        /// </summary>
        /// <param name="typeName">类型名称</param>
        /// <returns>描述</returns>
        private String GetInvokeMethodDescribe(String typeName)
        {
            //返回结果
            List<Dictionary<String, String>> result = new List<Dictionary<String, String>>();

            //获取type
            var typeList = ReflectionTool.GetTypeListOfAttribute(typeof(WorldBLL).Assembly, typeof(InvokeClassAttribute));
            var type = typeList.FirstOrDefault(r => r.Name == String.Format("{0}BLL", typeName));

            if (type != null)
            {
                //获取类型的方法
                foreach (var method in type.GetMethods())
                {
                    //获取方法的属性
                    var customMethodAttribute = method.GetCustomAttribute(typeof(MethodDescribeAttribute), false) as MethodDescribeAttribute;
                    if (customMethodAttribute != null)
                    {
                        Dictionary<String, String> methodDescribe = new Dictionary<String, String>();
                        methodDescribe[CommonConst.Name] = method.Name.Substring(2, method.Name.Length - 2);
                        methodDescribe[CommonConst.Describe] = customMethodAttribute.Describe;
                        methodDescribe[CommonConst.Creator] = customMethodAttribute.Creator;
                        methodDescribe[CommonConst.CreateDate] = customMethodAttribute.CreateDate;
                        methodDescribe[CommonConst.ParameterDescribe] = customMethodAttribute.ParameterDescribe;
                        methodDescribe[CommonConst.ReturnDescribe] = customMethodAttribute.ReturnDescribe;

                        result.Add(methodDescribe);
                    }
                }
            }

            return JsonConvert.SerializeObject(result.OrderBy(r => r[CommonConst.Name]));
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