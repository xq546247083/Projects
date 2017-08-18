/************************************************************************
* 标题: 响应数据对象
* 描述: 响应数据对象
* 作者：肖强
* 日期：2017-5-3 10:43:36
* 版本：V1
*************************************************************************/
using System;


namespace WebServer.Model
{
    using Newtonsoft.Json;
    using Tool.Extension;

    /// <summary>
    /// 响应数据对象
    /// </summary>
    public class ResponseDataObject
    {
        [JsonIgnore]
        public ResultStatus ResultStatus { get; set; }

        /// <summary>
        /// 结果状态码
        /// </summary>
        public Int32 Status
        {
            get
            {
                return (Int32)ResultStatus;
            }
        }

        /// <summary>
        /// 状态文本
        /// </summary>
        public String StatusValue
        {
            get
            {
                return ResultStatus.ToDescription();
            }
        }

        /// <summary>
        /// 当前时间
        /// </summary>
        private DateTime ResponseTime
        {
            get { return DateTime.Now; }
        }

        /// <summary>
        /// 密码过期时间
        /// </summary>
        public long PwdExpiredTime
        {
            get;
            set;
        }

        /// <summary>
        /// 返回值数据
        /// </summary>
        public Object Value { get; set; }
    }
}