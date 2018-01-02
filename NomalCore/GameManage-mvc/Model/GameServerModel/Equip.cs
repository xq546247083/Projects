/************************************************************************
* 描述: 装备模型
*************************************************************************/
using System;

namespace Moqikaka.GameManage.Model
{
    using Moqikaka.GameManage.Common;

    /// <summary>
    /// 装备模型
    /// </summary>
    [DataBaseTable("b_equip_model")]
    public class Equip
    {
        #region 属性

        /// <summary>
        /// #装备ID
        /// </summary>
        [PrimaryKey]
        public Int32 ID { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 资源类型ID
        /// </summary>
        public Int32 TypeID { get; set; }

        /// <summary>
        /// 品质
        /// </summary>
        public Int32 ColorLv { get; set; }

        /// <summary>
        /// 属性(属性ID,数值|属性ID,数值
        /// </summary>
        public string Attr { get; set; }

        /// <summary>
        /// 洗练属性数
        /// </summary>
        public Int32 ClearNum { get; set; }

        /// <summary>
        /// 是否有额外洗炼属性
        /// </summary>
        public Boolean IfAddAttr { get; set; }

        /// <summary>
        /// 分解产出ID
        /// </summary>
        public Int32 BreakID { get; set; }

        #endregion
    }
}
