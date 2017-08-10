﻿/************************************************************************
* 标题: 接口
* 描述: 表示需要初始化的类，在所有初始化后面
* 作者： 肖强
* 日期：2017-5-3 10:43:36
* 版本：V1
*************************************************************************/

namespace WebServer.BLL
{
    /// <summary>
    /// 表示需要初始化的类，在所有初始化后面
    ///</summary>
    public interface IInit
    {
        /// <summary>
        /// 初始化数据
        /// </summary>
        void Init();
    }
}
