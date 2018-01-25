﻿//***********************************************************************************
// 需要初始化接口
//***********************************************************************************

namespace SocketServer.Model
{
    /// <summary>
    /// 需要初始化接口
    /// </summary>
    public interface INeedInit
    {
        /// <summary>
        /// 初始化
        /// </summary>
        void Init();
    }
}