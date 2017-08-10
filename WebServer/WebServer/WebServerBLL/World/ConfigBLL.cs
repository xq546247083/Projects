/************************************************************************
* 标题: 初始化配置数据
* 描述: 初始化配置数据
* 作者： 肖强
* 日期：2017-5-3 10:43:36
* 版本：V1
*************************************************************************/

using System;
using System.Collections.Generic;
using System.Reflection;
using Tool.Common;

namespace WebServer.BLL
{
    /// <summary>
    /// 初始化继承iconfig的类
    /// </summary>
    public static class ConfigBLL
    {
        /// <summary>
        /// 初始化配置数据
        /// </summary>
        public static void Start()
        {
            var typeList = ReflectionTool.GetTypeListOfIm(Assembly.GetAssembly(typeof(WorldBLL)), typeof(IConfig));
            foreach (var type in typeList)
            {
                var typeConfig = type.Assembly.CreateInstance(type.FullName) as IConfig;
                if (typeConfig != null)
                {
                    typeConfig.Init();
                }
            }
        }

        /// <summary>
        /// 检测数据
        /// </summary>
        public static void Check()
        {
            List<String> errorList = new List<string>();

            //获取错误列表
            var typeList = ReflectionTool.GetTypeListOfIm(Assembly.GetAssembly(typeof(WorldBLL)), typeof(ICheck), typeof(IConfig));
            foreach (var type in typeList)
            {
                var typeCheck = type.Assembly.CreateInstance(type.FullName) as ICheck;
                if (typeCheck != null)
                {
                    try
                    {
                        var result = typeCheck.Check();
                        if (result != null && result.Count != 0)
                        {
                            errorList.AddRange(result);
                        }
                    }
                    catch (Exception ex)
                    {
                        errorList.Add(ExHandler.Handle(ex));
                    }
                }
            }

            //抛出错误
            if (errorList.Count > 0)
            {
                throw new Exception(String.Join(Environment.NewLine, errorList));
            }

        }
    }
}
