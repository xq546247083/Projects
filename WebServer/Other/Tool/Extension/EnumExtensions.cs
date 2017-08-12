/************************************************************************
* 标题: 枚举扩展
* 描述: 枚举扩展
* 作者：肖强
* 日期：2017-8-12 10:42:53
* 版本：V1
*************************************************************************/

using System;
using System.Reflection;

namespace Tool.Extension
{
    /// <summary>
    ///     枚举扩展方法类
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// 获取枚举项的Description特性的描述文字
        /// </summary>
        /// <param name="enumeration"> </param>
        /// <returns> </returns>
        public static String ToDescription(this Enum enumeration)
        {
            Type type = enumeration.GetType();
            MemberInfo[] members = type.GetMember(enumeration.CastTo<String>());
            if (members.Length > 0)
            {
                return members[0].ToDescription();
            }
            return enumeration.CastTo<String>();
        }
    }
}