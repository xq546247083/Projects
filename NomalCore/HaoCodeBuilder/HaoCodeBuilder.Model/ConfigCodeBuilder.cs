using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HaoCodeBuilder.Model
{
    public  class ConfigCodeBuilder
    {
        /// <summary>
        /// 模式
        /// </summary>
        public String Modle { get; set; }

        /// <summary>
        /// 新增
        /// </summary>
        public Boolean Add { get; set; }

        /// <summary>
        /// 删除
        /// </summary>
        public Boolean Delete { get; set; }

        /// <summary>
        /// 修改
        /// </summary>
        public Boolean Update { get; set; }

        /// <summary>
        /// 是否存在
        /// </summary>
        public Boolean Exist { get; set; }

        /// <summary>
        /// 获取所有数据
        /// </summary>
        public Boolean GetAll { get; set; }

        /// <summary>
        /// 获取主键数据
        /// </summary>
        public Boolean Get { get; set; }

        /// <summary>
        /// 查询数量
        /// </summary>
        public Boolean GetCount { get; set; }
    }
}
