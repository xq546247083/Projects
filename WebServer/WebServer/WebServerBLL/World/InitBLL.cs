/************************************************************************
* 标题: 初始化配置数据
* 描述: 初始化配置数据
* 作者： 肖强
* 日期：2017-5-3 10:43:36
* 版本：V1
*************************************************************************/
using System.Reflection;
using Tool.Common;

namespace WebServer.BLL
{
    /// <summary>
    /// 初始化继承IInit的类
    /// </summary>
    public static class InitBLL
    {
        /// <summary>
        /// 初始化配置数据
        /// </summary>
        public static void Start()
        {
            var typeList = ReflectionTool.GetTypeListOfIm(Assembly.GetAssembly(typeof(WorldBLL)), typeof(IInit));
            foreach (var type in typeList)
            {
                var typeConfig = type.Assembly.CreateInstance(type.FullName) as IInit;
                if (typeConfig != null)
                {
                    typeConfig.Init();
                }
            }
        }
    }
}
