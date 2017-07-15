/************************************************************************
* 标题: 返回枚举
* 描述: 返回枚举
* 作者：肖强
* 日期：2017-5-3 10:43:36
* 版本：V1
*************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Model
{
    /// <summary>
    /// 返回枚举
    /// </summary>
    public enum ResultStatus
    {
        /// <summary>
        /// 成功
        /// </summary>
        Success = 0,

        /// <summary>
        /// 出现异常
        /// </summary>
        Exception=1,

        /// <summary>
        /// 失败
        /// </summary>
        Fail=2,
    }
}
