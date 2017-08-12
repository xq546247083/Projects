//***********************************************************************************
//文件名称：MethodInfoBLL.cs
//功能描述：
//数据表：Nothing
//作者：byron
//日期：2014/8/27 15:40:37
//修改记录：
//***********************************************************************************

using System;
using System.Reflection;
using System.Collections.Generic;
using WebServer.Model;
using Newtonsoft.Json;
using Tool.Common;
using Tool.CustomAttribute;

namespace WebServer.BLL
{
    /// <summary>
    /// 方法参数获取类
    /// </summary>
    public class MethodInfoBLL : IInit
    {
        #region 字段

        //类名称，用于错误标识
        private const String mClassName = "MethodInfoBLL";

        /// <summary>
        /// 表示
        /// </summary>
        private const String mStarPrefix = "I_";

        /// <summary>
        /// 客户端调用方法信息
        /// </summary>
        private static readonly Dictionary<String, Type[]> cParamsInfos = new Dictionary<String, Type[]>();

        /// <summary>
        /// 后台调用方法信息
        /// </summary>
        private static readonly Dictionary<String, Type[]> mParamsInfos = new Dictionary<String, Type[]>();

        /// <summary>
        /// 前端方法备注类
        /// </summary>
        private static readonly Dictionary<String, String> cMethodDesc = new Dictionary<String, String>();

        #endregion

        #region 初始化方法

        /// <summary>
        /// 初始化对象
        /// </summary>
        public void Init()
        {
            String className = String.Empty;
            String methodName = String.Empty;
            try
            {
                Type[] types = Assembly.GetExecutingAssembly().GetTypes();
                foreach (var t in types)
                {
                    className = t.Name.Substring(0, t.Name.Length - 3);
                    MethodInfo[] methods = t.GetMethods(BindingFlags.Public | BindingFlags.Static);
                    if (methods != null)
                    {
                        foreach (var method in methods)
                        {
                            methodName = method.Name.Substring(2, method.Name.Length - 2);
                            String key = GetConfigKey(className, methodName);
                            cParamsInfos[key] = GetParamTypes(method);

                            // 读取方法备注
                            InvokeClassAttribute cratt = method.GetCustomAttribute<InvokeClassAttribute>();
                            MethodDescribeAttribute mra = method.GetCustomAttribute<MethodDescribeAttribute>();
                            if (cratt != null && mra != null)
                                cMethodDesc[key] = String.Format("{0}|{1}", cratt.Describe, mra.Describe);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }

        #endregion

        #region 内部或其它类调用的方法

        /// <summary>
        /// 将参数转换为对应的参数
        /// </summary>
        /// <param name="objs">输入参数</param>
        /// <param name="moduleName">模块名称</param>
        /// <param name="methodName">方法名称</param>
        /// <returns>转换后的参数</returns>
        public static Object[] ConvertParameters(Object[] objs, String moduleName, String methedName)
        {
            String key = GetConfigKey(moduleName, methedName);
            if (cParamsInfos.ContainsKey(key) == false)
            {
                throw new SelfDefinedException(ResultStatus.ClientParamTypeError, String.Format("不存在key={0}的方法", key));
            }

            return ParseParameterValue(objs, cParamsInfos[key]);
        }

        /// <summary>
        /// 获取参数类型
        /// </summary>
        /// <param name="moduleName">模块名称</param>
        /// <param name="methodename">方法名称</param>
        /// <returns>方法需要的参数类型</returns>
        public static Type[] GetParameterTypes(String moduleName, String methedName)
        {
            String key = GetConfigKey(moduleName, methedName);
            if (cParamsInfos.ContainsKey(key) == false)
            {
                throw new SelfDefinedException(ResultStatus.ClientDataError, String.Format("不存在key={0}的方法", key));
            }

            return cParamsInfos[key];
        }

        /// <summary>
        /// 获取方法备注
        /// </summary>
        /// <param name="moduleName">模块名称</param>
        /// <param name="methodName">方法名称</param>
        /// <returns>如果存在对应的方法备注，则返回对应的备注；否则返回String.Empty；</returns>
        public static String GetMethodDesc(String moduleName, String methodName)
        {
            String key = GetConfigKey(moduleName, methodName);
            if (cMethodDesc.ContainsKey(key))
            {
                return cMethodDesc[key];
            }
            return String.Empty;
        }

        #region private methods

        /// <summary>
        /// 转换参数值
        /// </summary>
        /// <param name="objs">输入参数</param>
        /// <param name="types">参数类型</param>
        /// <returns>转换后的参数</returns>
        private static Object[] ParseParameterValue(Object[] objs, Type[] types)
        {
            if (objs.Length < types.Length)
            {
                throw new SelfDefinedException(ResultStatus.ClientParamCountNoEnough, "方法传递的参数数量不足。");
            }

            Object[] result = new Object[types.Length];
            for (Int32 i = 0; i < types.Length; i++)
            {
                if (objs[i] == null)
                {
                    throw new SelfDefinedException(ResultStatus.NullParameter, "不支持传递null参数");
                }

                Type tempT = objs[i].GetType();
                if (tempT == types[i])
                {
                    result[i] = objs[i];
                }
                else
                {
                    #region 强行转换

                    switch (types[i].ToString())
                    {
                        case "System.Boolean":
                            result[i] = Convert.ToBoolean(objs[i]);
                            break;
                        case "System.Byte":
                            result[i] = Byte.Parse(objs[i].ToString());
                            break;
                        case "System.Char":
                            result[i] = Char.Parse(objs[i].ToString());
                            break;
                        case "System.DateTime":
                            result[i] = DateTime.Parse(objs[i].ToString());
                            break;
                        case "System.Decimal":
                            result[i] = Decimal.Parse(objs[i].ToString());
                            break;
                        case "System.Double":
                            result[i] = Double.Parse(objs[i].ToString());
                            break;
                        case "System.Guid":
                            result[i] = Guid.Parse(objs[i].ToString());
                            break;
                        case "System.Int16":
                            Int16 tempInt16 = Int16.Parse(objs[i].ToString());
                            if (tempInt16 < 0)
                                throw new SelfDefinedException(ResultStatus.ClientDataError, "System.Int16不允许为负数");

                            result[i] = tempInt16;
                            break;
                        case "System.Int32":
                            Int32 tempInt32 = Int32.Parse(objs[i].ToString());
                            if (tempInt32 < 0)
                                throw new SelfDefinedException(ResultStatus.ClientDataError, "System.Int32不允许为负数");

                            result[i] = tempInt32;
                            break;
                        case "System.Int64":
                            Int64 tempInt64 = Int64.Parse(objs[i].ToString());
                            if (tempInt64 < 0)
                                throw new SelfDefinedException(ResultStatus.ClientDataError, "System.Int64不允许为负数");

                            result[i] = tempInt64;
                            break;
                        case "System.SByte":
                            result[i] = SByte.Parse(objs[i].ToString());
                            break;
                        case "System.Single":
                            result[i] = Single.Parse(objs[i].ToString());
                            break;
                        case "System.String":
                            result[i] = objs[i].ToString();
                            break;
                        case "System.UInt16":
                            UInt16 tempUInt16 = UInt16.Parse(objs[i].ToString());
                            if (tempUInt16 < 0)
                                throw new SelfDefinedException(ResultStatus.ClientDataError, "System.UInt16不允许为负数");

                            result[i] = UInt16.Parse(objs[i].ToString());
                            break;
                        case "System.UInt32":
                            UInt32 tempUInt32 = UInt32.Parse(objs[i].ToString());
                            if (tempUInt32 < 0)
                                throw new SelfDefinedException(ResultStatus.ClientDataError, "System.UInt32不允许为负数");

                            result[i] = tempUInt32;
                            break;
                        case "System.UInt64":
                            UInt64 tempUInt64 = UInt64.Parse(objs[i].ToString());
                            if (tempUInt64 < 0)
                                throw new SelfDefinedException(ResultStatus.ClientDataError, "System.tempUInt64不允许为负数");

                            result[i] = tempUInt64;
                            break;
                        case "System.Boolean[]":
                            result[i] = JsonConvert.DeserializeObject<Boolean[]>(objs[i].ToString());
                            break;
                        case "System.Byte[]":
                            result[i] = JsonConvert.DeserializeObject<Byte[]>(objs[i].ToString());
                            break;
                        case "System.Char[]":
                            result[i] = JsonConvert.DeserializeObject<Char[]>(objs[i].ToString());
                            break;
                        case "System.DateTime[]":
                            result[i] = JsonConvert.DeserializeObject<DateTime[]>(objs[i].ToString());
                            break;
                        case "System.Decimal[]":
                            result[i] = JsonConvert.DeserializeObject<Decimal[]>(objs[i].ToString());
                            break;
                        case "System.Double[]":
                            result[i] = JsonConvert.DeserializeObject<Double[]>(objs[i].ToString());
                            break;
                        case "System.Guid[]":
                            result[i] = JsonConvert.DeserializeObject<Guid[]>(objs[i].ToString());
                            break;
                        case "System.Int16[]":
                            result[i] = JsonConvert.DeserializeObject<Int16[]>(objs[i].ToString());
                            break;
                        case "System.Int32[]":
                            result[i] = JsonConvert.DeserializeObject<Int32[]>(objs[i].ToString());
                            break;
                        case "System.Int64[]":
                            result[i] = JsonConvert.DeserializeObject<Int64[]>(objs[i].ToString());
                            break;
                        case "System.SByte[]":
                            result[i] = JsonConvert.DeserializeObject<SByte[]>(objs[i].ToString());
                            break;
                        case "System.Single[]":
                            result[i] = JsonConvert.DeserializeObject<Single[]>(objs[i].ToString());
                            break;
                        case "System.String[]":
                            result[i] = JsonConvert.DeserializeObject<String[]>(objs[i].ToString());
                            break;
                        case "System.UInt16[]":
                            result[i] = JsonConvert.DeserializeObject<UInt16[]>(objs[i].ToString());
                            break;
                        case "System.UInt32[]":
                            result[i] = JsonConvert.DeserializeObject<UInt32[]>(objs[i].ToString());
                            break;
                        case "System.UInt64[]":
                            result[i] = JsonConvert.DeserializeObject<UInt64[]>(objs[i].ToString());
                            break;
                        case "System.Collections.Generic.List`1[System.Boolean]":
                            result[i] = JsonConvert.DeserializeObject<List<Boolean>>(objs[i].ToString());
                            break;
                        case "System.Collections.Generic.List`1[System.Byte]":
                            result[i] = JsonConvert.DeserializeObject<List<Byte>>(objs[i].ToString());
                            break;
                        case "System.Collections.Generic.List`1[System.Char]":
                            result[i] = JsonConvert.DeserializeObject<List<Char>>(objs[i].ToString());
                            break;
                        case "System.Collections.Generic.List`1[System.DateTime]":
                            result[i] = JsonConvert.DeserializeObject<List<DateTime>>(objs[i].ToString());
                            break;
                        case "System.Collections.Generic.List`1[System.Decimal]":
                            result[i] = JsonConvert.DeserializeObject<List<Decimal>>(objs[i].ToString());
                            break;
                        case "System.Collections.Generic.List`1[System.Double]":
                            result[i] = JsonConvert.DeserializeObject<List<Double>>(objs[i].ToString());
                            break;
                        case "System.Collections.Generic.List`1[System.Guid]":
                            result[i] = JsonConvert.DeserializeObject<List<Guid>>(objs[i].ToString());
                            break;
                        case "System.Collections.Generic.List`1[System.Int16]":
                            result[i] = JsonConvert.DeserializeObject<List<Int16>>(objs[i].ToString());
                            break;
                        case "System.Collections.Generic.List`1[System.Int32]":
                            result[i] = JsonConvert.DeserializeObject<List<Int32>>(objs[i].ToString());
                            break;
                        case "System.Collections.Generic.List`1[System.Int64]":
                            result[i] = JsonConvert.DeserializeObject<List<Int64>>(objs[i].ToString());
                            break;
                        case "System.Collections.Generic.List`1[System.SByte]":
                            result[i] = JsonConvert.DeserializeObject<List<SByte>>(objs[i].ToString());
                            break;
                        case "System.Collections.Generic.List`1[System.Single]":
                            result[i] = JsonConvert.DeserializeObject<List<Single>>(objs[i].ToString());
                            break;
                        case "System.Collections.Generic.List`1[System.String]":
                            result[i] = JsonConvert.DeserializeObject<List<String>>(objs[i].ToString());
                            break;
                        case "System.Collections.Generic.List`1[System.UInt16]":
                            result[i] = JsonConvert.DeserializeObject<List<UInt16>>(objs[i].ToString());
                            break;
                        case "System.Collections.Generic.List`1[System.UInt32]":
                            result[i] = JsonConvert.DeserializeObject<List<UInt32>>(objs[i].ToString());
                            break;
                        case "System.Collections.Generic.List`1[System.UInt64]":
                            result[i] = JsonConvert.DeserializeObject<List<UInt64>>(objs[i].ToString());
                            break;
                        case "System.Collections.Generic.Dictionary`2[System.String,System.Object]":
                            result[i] = JsonConvert.DeserializeObject<Dictionary<String, Object>>(objs[i].ToString());
                            break;
                        case "System.Collections.Generic.Dictionary`2[System.Int32,System.Int32]":
                            result[i] = JsonConvert.DeserializeObject<Dictionary<Int32, Int32>>(objs[i].ToString());
                            break;
                        case "System.Collections.Generic.Dictionary`2[System.String,System.Int32]":
                            result[i] = JsonConvert.DeserializeObject<Dictionary<String, Int32>>(objs[i].ToString());
                            break;
                        case "System.Nullable`1[System.Guid]":
                            result[i] = ParseTool.ParseNullableToGuid(objs[i]);
                            break;
                        default:
                            result[i] = objs[i];
                            break;
                    }

                    #endregion
                }
            }

            return result;
        }

        /// <summary>
        /// 拼接Key
        /// </summary>
        /// <param name="className">模块名称</param>
        /// <param name="methedName">子模块名称</param>
        /// <returns>拼接之后的Key</returns>
        private static String GetConfigKey(String className, String methedName)
        {
            return String.Format("{0}_{1}", className, methedName);
        }

        /// <summary>
        /// 获取方法的参数信息
        /// </summary>
        /// <param name="method">方法对象</param>
        /// <returns>方法所对应的参数</returns>
        private static Type[] GetParamTypes(MethodInfo method)
        {
            var parms = method.GetParameters();
            Type[] result = new Type[parms.Length];

            for (Int32 i = 0; i < parms.Length; i++)
                result[i] = parms[i].ParameterType;

            return result;
        }

        #endregion

        #endregion
    }
}
