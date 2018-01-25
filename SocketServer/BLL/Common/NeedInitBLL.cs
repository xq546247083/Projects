/************************************************************************
* 初始化继承INeddInit的类
*************************************************************************/
using System.Reflection;

namespace SocketServer.BLL
{
    using SocketServer.Model;
    using Tool.Common;

    /// <summary>
    /// 初始化继承INeddInit的类
    /// </summary>
    public static class NeedInitBLL
    {
        /// <summary>
        /// 初始化配置数据
        /// </summary>
        public static void Start()
        {
            var typeList = ReflectionTool.GetTypeListOfIm(Assembly.GetAssembly(typeof(GlobalBLL)), typeof(INeedInit));
            foreach (var type in typeList)
            {
                if (type.Assembly.CreateInstance(type.FullName) is INeedInit needInit)
                {
                    needInit.Init();
                }
            }
        }
    }
}
