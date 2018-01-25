//***********************************************************************************
// 调用方法管理类
//***********************************************************************************
using System;
using System.Collections.Generic;
using System.Reflection;

namespace WebSocketServer
{
    using SocketServer.BLL;
    using SocketServer.Model;
    using Tool.CustomAttribute;

    /// <summary>
    /// 调用方法管理类
    /// </summary>
    public static class MethodManager
    {
        #region 属性

        /// <summary>
        /// 反射得到的API
        /// </summary>
        private static Dictionary<String, MethodReflectInfo> clientApiData = new Dictionary<String, MethodReflectInfo>();

        /// <summary>
        /// 需要反射的类名前缀
        /// </summary>
        public static String ClassStuffix = "BLL";

        /// <summary>
        /// 方法名前缀
        /// </summary>
        public static String MethodPrefix = "C_";

        #endregion

        #region 私有方法

        /// <summary>
        /// 获取API的全名
        /// </summary>
        /// <param name="className">类名</param>
        /// <param name="methodName">方法名</param>
        /// <returns></returns>
        private static String GetApiFullName(String className, String methodName)
        {
            return String.Format("{0}{1}", className, methodName);
        }

        /// <summary>
        /// 获取需要调用的函数
        /// </summary>
        /// <param name="apiName">Api名</param>
        /// <returns>方法反射信息</returns>
        private static MethodReflectInfo GetApi(String apiName)
        {
            if (clientApiData.ContainsKey(apiName) == false)
            {
                return null;
            }

            return clientApiData[apiName];
        }

        /// <summary>
        /// 检查方法是否符合要求
        /// </summary>
        /// <param name="classType">类型信息</param>
        /// <param name="methodInfo">方法信息</param>
        private static void CheckMethod(Type classType, MethodInfo methodInfo)
        {
            var paramArray = methodInfo.GetParameters();
            if (paramArray == null || paramArray.Length <= 0)
            {
                throw new Exception(String.Format("ClientApi 参数列表不正确,应该至少包含1个参数 Class:{0} MethodName:{1}", classType.FullName, methodInfo.Name));
            }

            // 参数列表检查
            if (paramArray[0].ParameterType != typeof(Context))
            {
                throw new Exception(String.Format("ClientApi 参数列表不正确,第一个参数必须为:Context Class:{0} MethodName:{1}", classType.FullName, methodInfo.Name));
            }

            // 返回值类型检查
            if (methodInfo.ReturnType != typeof(ReturnObject))
            {
                throw new Exception(String.Format("ClientApi 返回值不正确,必须为类型:ReturnObject Class:{0} MethodName:{1}", classType.FullName, methodInfo.Name));
            }
        }

        #endregion

        #region 公有方法

        /// <summary>
        /// 加载所有需要的函数
        /// </summary>
        /// <param name="assemblyName">程序集名称</param>
        public static void Load()
        {
            var result = new Dictionary<String, MethodReflectInfo>();
            var assemblyItem = Assembly.GetAssembly(typeof(SysUserBLL));
            var attrType = typeof(InvokeMethodAttribute);

            // 遍历所有class
            var allType = assemblyItem.GetExportedTypes();
            foreach (var classType in allType)
            {
                if (classType.IsClass == false)
                {
                    // 只反射class
                    continue;
                }

                if (classType.Name.ToLower().EndsWith(ClassStuffix.ToLower()) == false)
                {
                    // 只反射bll结尾的类
                    continue;
                }

                var className = classType.Name.Substring(0, classType.Name.Length - ClassStuffix.Length);

                // 提取此类下面的所有Client Api
                var methodList = classType.GetMethods(BindingFlags.Public | BindingFlags.Static);
                foreach (var methodItem in methodList)
                {
                    // 方法前缀检查
                    if (methodItem.Name.StartsWith(MethodPrefix) == false)
                    {
                        continue;
                    }

                    // 是否有定义所需标记
                    var attrItem = methodItem.GetCustomAttribute(attrType);
                    if (attrItem == null)
                    {
                        continue;
                    }

                    // 获取方法名
                    var methodName = methodItem.Name.Substring(MethodPrefix.Length, methodItem.Name.Length - MethodPrefix.Length);
                    var methodFullName = GetApiFullName(className, methodName);

                    // 检查方法
                    CheckMethod(classType, methodItem);

                    if (result.ContainsKey(methodFullName))
                    {
                        throw new Exception(String.Format("存在重复的Client API：{0}", methodFullName));
                    }
                    result[methodFullName] = new MethodReflectInfo(classType.Name, methodItem);
                }
            }

            // 保存到全局
            clientApiData = result;
        }

        /// <summary>
        /// 进行函数调用
        /// </summary>
        /// <param name="context">上下文对象</param>>
        /// <returns>结果实体对象</returns>
        public static ReturnObject Call(Context context)
        {
            var result = new ReturnObject() { Code = -1 };

            // 获取方法
            var apiFullName = context.Request["Api"];
            if (apiFullName == null)
            {
                result.Message = "找不到Api方法!";
                return result;
            }

            context.Request.Remove("Api");
            var methodInfo = GetApi(apiFullName);
            if (methodInfo == null)
            {
                result.Message = "找不到Api方法!";
                return result;
            }

            // 判断参数数量是否正确
            if (context.Request.Count + 1 != methodInfo.ParamTypes.Length)
            {
                result.Message = "参数数量不对";
                return result;
            }

            // 只有上下文的方法
            if (methodInfo.ParamTypes.Length == 1)
            {
                // 无参调用
                return (ReturnObject)methodInfo.TargetMethod.Invoke(null, new Object[] { context });
            }

            // 组装请求参数
            var paramObj = new Object[methodInfo.ParamTypes.Length];
            paramObj[0] = context;

            // 循环组装参数
            var i = 1;
            foreach (var item in context.Request)
            {
                paramObj[i] = Convert.ChangeType(item, methodInfo.ParamTypes[i]);
                i++;
            }

            return (ReturnObject)methodInfo.TargetMethod.Invoke(null, paramObj);
        }

        #endregion
    }
}
