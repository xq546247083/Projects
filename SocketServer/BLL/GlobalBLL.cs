/************************************************************************
*  bll全局类
*************************************************************************/
namespace SocketServer.BLL
{
    /// <summary>
    /// bll全局类
    /// </summary>
    public static class GlobalBLL
    {
        /// <summary>
        /// 开始初始化
        /// </summary>
        public static void Start()
        {
            #region 加载数据

            NeedInitBLL.Start();

            #endregion
        }
    }
}
