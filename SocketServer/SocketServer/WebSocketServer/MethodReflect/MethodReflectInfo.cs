//***********************************************************************************
// 方法的反射信息
//***********************************************************************************
using System;
using System.Reflection;

namespace WebSocketServer
{
    /// <summary>
    /// 方法反射信息
    /// </summary>
    public class MethodReflectInfo
    {
        /// <summary>
        /// 对应类名
        /// </summary>
        public String ClassName { get; private set; }

        /// <summary>
        /// 对应的方法
        /// </summary>
        public MethodInfo TargetMethod { get; private set; }

        /// <summary>
        /// 参数列表
        /// </summary>
        public Type[] ParamTypes { get; private set; }

        /// <summary>
        /// 返回值类型
        /// </summary>
        public Type ReturnType { get; private set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="className">方法名</param>
        /// <param name="method">方法名</param>
        public MethodReflectInfo(String className, MethodInfo method)
        {
            this.TargetMethod = method;
            this.ClassName = className;

            // 获取参数类型
            var paramArray = this.TargetMethod.GetParameters();
            this.ParamTypes = new Type[paramArray.Length];
            for (Int32 i = 0; i < paramArray.Length; i++)
            {
                this.ParamTypes[i] = paramArray[i].ParameterType;
            }

            // 获取返回值类型
            this.ReturnType = method.ReturnType;
        }
    }
}
