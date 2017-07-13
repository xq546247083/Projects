using System;
using System.Collections.Generic;
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
            context.Response.Write(GetInvokeClassMethodDescribe());
        }

        /// <summary>
        /// 获取调用类的方法描述
        /// </summary>
        /// <returns>描述</returns>
        private String GetInvokeClassMethodDescribe()
        {
            //返回结果
            Dictionary<String, Dictionary<String, Dictionary<String, String>>> result = new Dictionary<String, Dictionary<String, Dictionary<String, String>>>();

            var typeList = ReflectionTool.GetTypeListOfAttribute(typeof(WorldBLL).Assembly, typeof(InvokeClassAttribute));
            foreach (var type in typeList)
            {
                //获取类型的描述
                String typeDescribeStr = String.Empty;
                var customTypeAttribute = type.GetCustomAttribute(typeof(InvokeClassAttribute), false) as InvokeClassAttribute;
                if (customTypeAttribute != null)
                {
                    Dictionary<String, String> typeDescribe = new Dictionary<String, String>();
                    typeDescribe[CommonConst.Name] = type.Name.Substring(0, type.Name.Length - 3);
                    typeDescribe[CommonConst.Creator] = customTypeAttribute.Creator;
                    typeDescribe[CommonConst.CreateDate] = customTypeAttribute.CreateDate;
                    typeDescribe[CommonConst.CreateDate] = customTypeAttribute.Describe;

                    typeDescribeStr = JsonConvert.SerializeObject(typeDescribe);
                }

                //如果没有，直接下一个
                if (String.IsNullOrEmpty(typeDescribeStr))
                {
                    continue;
                }

                if (!result.ContainsKey(typeDescribeStr))
                {
                    result[typeDescribeStr] = new Dictionary<String, Dictionary<String, String>>();
                }

                //获取类型的方法
                foreach (var method in type.GetMethods())
                {
                    String methodName = method.Name.Substring(2, method.Name.Length - 2);
                    //获取方法的属性
                    var customMethodAttribute = method.GetCustomAttribute(typeof(MethodDescribeAttribute), false) as MethodDescribeAttribute;
                    if (customMethodAttribute != null)
                    {
                        if (!result[typeDescribeStr].ContainsKey(methodName))
                        {
                            result[typeDescribeStr][methodName] = new Dictionary<String, String>();
                        }

                        result[typeDescribeStr][methodName][CommonConst.Describe] = customMethodAttribute.Describe;
                        result[typeDescribeStr][methodName][CommonConst.Creator] = customMethodAttribute.Creator;
                        result[typeDescribeStr][methodName][CommonConst.CreateDate] = customMethodAttribute.CreateDate;
                        result[typeDescribeStr][methodName][CommonConst.ParameterDescribe] = customMethodAttribute.ParameterDescribe == null ? String.Empty : customMethodAttribute.ParameterDescribe.Replace(Environment.NewLine, ""); ;
                        result[typeDescribeStr][methodName][CommonConst.ReturnDescribe] = customMethodAttribute.ReturnDescribe == null ? String.Empty : customMethodAttribute.ReturnDescribe.Replace(Environment.NewLine, ""); ;
                    }

                }
            }

            return JsonConvert.SerializeObject(result);
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