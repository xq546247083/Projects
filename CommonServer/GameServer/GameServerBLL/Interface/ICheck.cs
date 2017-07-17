/************************************************************************
* 标题: 接口
* 描述: 检测数据
* 作者： 肖强
* 日期：2017-5-3 10:43:36
* 版本：V1
*************************************************************************/

using System;
using System.Collections.Generic;

namespace GameServer.BLL
{
    /// <summary>
    /// 检测数据
    ///</summary>
    public interface ICheck
    {
        /// <summary>
        /// 检测数据
        /// <returns>错误信息</returns>
        /// </summary>
        List<String> Check();
    }
}
