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
    /// <summary>
    /// 反射工具
    /// </summary>
    public static class ReflectionTool
    {
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
    }
}
