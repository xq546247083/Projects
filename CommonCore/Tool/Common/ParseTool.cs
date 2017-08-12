/************************************************************************
* 标题: 转换工具
* 描述: 转换工具
* 作者：肖强
* 日期：2017-5-3 10:43:36
* 版本：V1
*************************************************************************/

namespace Tool.Common
{
    /// <summary>
    /// 转换工具
    /// </summary>
    public static class ParseTool
    {
        /// <summary>
		///  将对象转换为Byte类型
		/// </summary>
		/// <param name="obj">要转换的对象</param>
		/// <returns>转换后的数据</returns>
		public static byte? ParseNullableToByte(object obj)
        {
            if (obj != null)
            {
                byte value = 0;
                if (byte.TryParse(obj.ToString(), out value))
                {
                    return new byte?(value);
                }
            }
            return null;
        }

        /// <summary>
        /// 将对象转换为SByte类型
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <returns>转换后的数据</returns>
        public static sbyte? ParseNullableToSByte(object obj)
        {
            if (obj != null)
            {
                sbyte value = 0;
                if (sbyte.TryParse(obj.ToString(), out value))
                {
                    return new sbyte?(value);
                }
            }
            return null;
        }

        /// <summary>
        /// 将对象转换为DateTime类型
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <returns>转换后的数据</returns>
        public static System.DateTime? ParseNullableToDateTime(object obj)
        {
            if (obj != null)
            {
                System.DateTime value = default(System.DateTime);
                if (System.DateTime.TryParse(obj.ToString(), out value))
                {
                    return new System.DateTime?(value);
                }
            }
            return null;
        }

        /// <summary>
        /// 将对象转换为guid
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <returns>转换后的数据</returns>
        public static System.Guid? ParseNullableToGuid(object obj)
        {
            if (obj != null)
            {
                System.Guid empty = System.Guid.Empty;
                if (System.Guid.TryParse(obj.ToString(), out empty))
                {
                    return new System.Guid?(empty);
                }
            }
            return null;
        }

        /// <summary>
        /// 将对象转换为Boolean类型
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <returns>转换后的数据</returns>
        public static bool? ParseNullableToBoolean(object obj)
        {
            if (obj != null)
            {
                bool value = false;
                if (bool.TryParse(obj.ToString(), out value))
                {
                    return new bool?(value);
                }
            }
            return null;
        }

        /// <summary>
        /// 将对象转换为Char类型
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <returns>转换后的数据</returns>
        public static char? ParseNullableToChar(object obj)
        {
            if (obj != null)
            {
                char value = '\0';
                if (char.TryParse(obj.ToString(), out value))
                {
                    return new char?(value);
                }
            }
            return null;
        }

        /// <summary>
        /// 将对象转换为Decimal类型
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <returns>转换后的数据</returns>
        public static decimal? ParseNullableToDecimal(object obj)
        {
            if (obj != null)
            {
                decimal value = 0m;
                if (decimal.TryParse(obj.ToString(), out value))
                {
                    return new decimal?(value);
                }
            }
            return null;
        }

        /// <summary>
        /// 将对象转换为Double类型
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <returns>转换后的数据</returns>
        public static double? ParseNullableToDouble(object obj)
        {
            if (obj != null)
            {
                double value = 0.0;
                if (double.TryParse(obj.ToString(), out value))
                {
                    return new double?(value);
                }
            }
            return null;
        }

        /// <summary>
        /// 将对象转换为Int16类型
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <returns>转换后的数据</returns>
        public static short? ParseNullableToInt16(object obj)
        {
            if (obj != null)
            {
                short value = 0;
                if (short.TryParse(obj.ToString(), out value))
                {
                    return new short?(value);
                }
            }
            return null;
        }

        /// <summary>
        /// 将对象转换为int32类型
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <returns>转换后的数据</returns>
        public static int? ParseNullableToInt32(object obj)
        {
            if (obj != null)
            {
                int value = 0;
                if (int.TryParse(obj.ToString(), out value))
                {
                    return new int?(value);
                }
            }
            return null;
        }

        /// <summary>
        /// 将对象转换为Int64类型
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <returns>转换后的数据</returns>
        public static long? ParseNullableToInt64(object obj)
        {
            if (obj != null)
            {
                long value = 0L;
                if (long.TryParse(obj.ToString(), out value))
                {
                    return new long?(value);
                }
            }
            return null;
        }

        /// <summary>
        /// 将对象转换为Single类型
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <returns>转换后的数据</returns>
        public static float? ParseNullableToSingle(object obj)
        {
            if (obj != null)
            {
                float value = 0f;
                if (float.TryParse(obj.ToString(), out value))
                {
                    return new float?(value);
                }
            }
            return null;
        }

        /// <summary>
        /// 将对象转换为UInt16类型
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <returns>转换后的数据</returns>
        public static ushort? ParseNullableToUInt16(object obj)
        {
            if (obj != null)
            {
                ushort value = 0;
                if (ushort.TryParse(obj.ToString(), out value))
                {
                    return new ushort?(value);
                }
            }
            return null;
        }

        /// <summary>
        /// 将对象转换为UInt32类型
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <returns>转换后的数据</returns>
        public static uint? ParseNullableToUInt32(object obj)
        {
            if (obj != null)
            {
                uint value = 0u;
                if (uint.TryParse(obj.ToString(), out value))
                {
                    return new uint?(value);
                }
            }
            return null;
        }

        /// <summary>
        /// 将对象转换为UInt64类型
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <returns>转换后的数据</returns>
        public static ulong? ParseNullableToUInt64(object obj)
        {
            if (obj != null)
            {
                ulong value = 0uL;
                if (ulong.TryParse(obj.ToString(), out value))
                {
                    return new ulong?(value);
                }
            }
            return null;
        }
    }
}

