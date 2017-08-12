/************************************************************************
* 标题: 反射工具
* 描述: 反射工具
* 作者：肖强
* 日期：2017-5-3 10:43:36
* 版本：V1
*************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Tool.Common
{
    using Tool.Extension;

    /// <summary>
    /// 反射工具
    /// </summary>
    public static class ReflectionTool
    {
        /// <summary>
        /// 方法缓存
        /// key:方法字符串
        /// value:方法信息
        /// </summary>
        private static Dictionary<String, MethodInfo> mMethodData = new Dictionary<String, MethodInfo>();

        /// <summary>
        /// 类型缓存
        /// key:方法字符串
        /// value:方法信息
        /// </summary>
        private static Dictionary<String, Type> mTypeData = new Dictionary<String, Type>();

        #region 获取类型列表

        /// <summary>
        /// 获取实现该接口的所有类
        /// </summary>
        /// <param name="assembly">要搜索的程序集</param>
        /// <param name="imType">接口类型</param>
        /// <returns>类列表</returns>
        public static List<Type> GetTypeListOfIm(Assembly assembly, params  Type[] imType)
        {
            List<Type> result = new List<Type>();

            var types = assembly.GetTypes();
            foreach (var type in types)
            {
                var imTypes = type.GetInterfaces();

                if (imType.All(r => imTypes.Contains(r)))
                {
                    result.Add(type);
                }
            }

            return result;
        }

        /// <summary>
        /// 获取有该属性的的所有类
        /// </summary>
        /// <param name="assembly">要搜索的程序集</param>
        /// <param name="attribute">属性</param>
        /// <returns>类列表</returns>
        public static List<Type> GetTypeListOfAttribute(Assembly assembly, Type attribute)
        {
            List<Type> result = new List<Type>();

            var types = assembly.GetTypes();
            foreach (var type in types)
            {
                var typeAttribute = type.GetCustomAttribute(attribute, false);
                if (typeAttribute != null)
                {
                    result.Add(type);
                }
            }

            return result;
        }

        #endregion

        #region 调用方法

        /// <summary>
        /// 调用类中的静态方法
        /// </summary>
        /// <param name="assemblyName">要反射的程序集名称</param>
        /// <param name="className">静态方法所在的类</param>
        /// <param name="methodName">方法名称</param>
        /// <param name="param">方法所需要的参数</param>
        /// <returns>调用方法所返回的值</returns>
        public static object CallStaticMethod(string assemblyName, string className, string methodName, params object[] param)
        {
            MethodInfo method = null;
            try
            {
                method = GetMethod(assemblyName, className, methodName);
            }
            catch (Exception ex)
            {
                throw new AmbiguousMatchException(string.Format("{0}.{1}方法名重复。ex:{2}", className, methodName, ex.ToMessage()));
            }

            if (method == null)
            {
                throw new AmbiguousMatchException(string.Format("未能在类{0}中发现{1}方法。", className, methodName));
            }

            return method.Invoke(null, param);
        }

        /// <summary>
        /// 返回指定的方法类型对象
        /// </summary>
        /// <param name="assemblyName">要反射的程序集名称</param>
        /// <param name="className">要获取对象类型的类名称</param>
        /// <param name="methodName">方法名称</param>
        /// <returns>该类所对应的类型</returns>
        private static MethodInfo GetMethod(string assemblyName, string className, string methodName)
        {
            string key = string.Join("_", new string[]
			{
				assemblyName,
				className,
				methodName
			});
            //如果没有获取method，则反射method
            if (!mMethodData.ContainsKey(key))
            {
                Type classType = GetClassType(assemblyName, className);
                mMethodData[key] = classType.GetMethod(methodName);
            }

            return mMethodData[key];
        }

        /// <summary>
        /// 返回指定类型的类型对象
        /// </summary>
        /// <param name="assemblyName">要反射的程序集名称</param>
        /// <param name="className">要获取对象类型的类名称</param>
        /// <returns>该类所对应的类型</returns>
        private static Type GetClassType(string assemblyName, string className)
        {
            string key = string.Join("_", new string[]
			{
				assemblyName,
				className
			});
            //如果没有获取type，则反射type
            if (!mTypeData.ContainsKey(key))
            {
                mTypeData[key] = AssemblyType(assemblyName, className);
            }

            return mTypeData[key];
        }

        /// <summary>
        /// 反射指定类型
        /// </summary>
        /// <param name="assemblyName">要反射的程序集名称</param>
        /// <param name="className">要反射的类名称</param>
        /// <returns>该类所对应的类型</returns>
        private static Type AssemblyType(string assemblyName, string className)
        {
            Assembly assembly = Assembly.Load(assemblyName);
            return assembly.GetType(className);
        }

        #endregion
    }
}
