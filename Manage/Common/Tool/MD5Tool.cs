/************************************************************************
* 标题: MD5工具
*************************************************************************/
using System;

namespace Manage.Common
{
    /// <summary>
    /// MD5工具
    /// </summary>
    public static class MD5Tool
    {
        /// <summary>
		/// MD5加密
		/// </summary>
		/// <param name="str">需要加密的字符串</param>
		/// <param name="letterCase">大小写枚举</param>
		/// <exception cref="T:System.ArgumentNullException">System.ArgumentNullException</exception>
		/// <returns>加密后的字符串</returns>
		public static string MD5(String str, LetterCase letterCase = LetterCase.UpperCase)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                throw new System.ArgumentNullException("str", "str can't be empty.");
            }
            return MD5(System.Text.Encoding.UTF8.GetBytes(str), letterCase);
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="byteArray">需要加密的字节数组</param>
        /// <param name="letterCase">大小写枚举</param>
        /// <exception cref="T:System.ArgumentNullException">System.ArgumentNullException</exception>
        /// <returns>加密后的字符串</returns>
        public static string MD5(byte[] byteArray, LetterCase letterCase = LetterCase.UpperCase)
        {
            if (byteArray == null || byteArray.Length == 0)
            {
                throw new System.ArgumentNullException("Error", "str can't be empty.");
            }

            string result;
            using (System.Security.Cryptography.MD5CryptoServiceProvider mD5CryptoServiceProvider = new System.Security.Cryptography.MD5CryptoServiceProvider())
            {
                byte[] array = mD5CryptoServiceProvider.ComputeHash(byteArray);
                System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
                if (letterCase == LetterCase.LowerCase)
                {
                    for (int i = 0; i < array.Length; i++)
                    {
                        stringBuilder.Append(array[i].ToString("x2"));
                    }
                }
                else
                {
                    for (int j = 0; j < array.Length; j++)
                    {
                        stringBuilder.Append(array[j].ToString("X2"));
                    }
                }

                result = stringBuilder.ToString();
            }

            return result;
        }

        /// <summary>
        /// md5加密，并把结果进行Base64
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        /// <exception cref="T:System.ArgumentNullException">str;str can't be empty.</exception>
        public static string MD5WithBase64(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                throw new System.ArgumentNullException("str", "str can't be empty.");
            }

            return MD5WithBase64(System.Text.Encoding.UTF8.GetBytes(str));
        }

        /// <summary>
        /// md5加密，并把结果进行Base64
        /// </summary>
        /// <param name="byteArray">The byte array.</param>
        /// <returns></returns>
        public static string MD5WithBase64(byte[] byteArray)
        {
            if (byteArray == null || byteArray.Length == 0)
            {
                throw new ArgumentNullException("Error", "str can't be empty.");
            }

            string result;
            using (System.Security.Cryptography.MD5CryptoServiceProvider mD5CryptoServiceProvider = new System.Security.Cryptography.MD5CryptoServiceProvider())
            {
                byte[] inArray = mD5CryptoServiceProvider.ComputeHash(byteArray);
                result = Convert.ToBase64String(inArray);
            }
            return result;
        }
    }
}

/// <summary>
/// 字符的大小写枚举
/// </summary>
public enum LetterCase : byte
{
    /// <summary>
    /// 大写
    /// </summary>
    UpperCase,
    /// <summary>
    /// 小写
    /// </summary>
    LowerCase
}