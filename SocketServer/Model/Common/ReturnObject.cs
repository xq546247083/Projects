//***********************************************************************************
// 返回对象
//***********************************************************************************
using System;
using SocketServer.Enum;

namespace SocketServer.Model
{
    /// <summary>
    /// 返回对象
    /// </summary>
    public class ReturnObject
    {
        /// <summary>
        /// 返回的状态值；0：成功；非0：失败
        /// </summary>
        public Int32 Code { get; set; }

        /// <summary>
        /// 客户端命令枚举
        /// </summary>
        public ClientCmdEnum Cmd { get; set; }

        /// <summary>
        /// 返回的描述信息
        /// </summary>
        public String Message { get; set; }

        /// <summary>
        /// 返回的数据
        /// </summary>
        public Object Data { get; set; }
    }
}
