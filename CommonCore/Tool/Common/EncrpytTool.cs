/************************************************************************
* 标题: 加密工具
* 描述: 加密工具
* 作者：肖强
* 日期：2017-8-13 15:25:13
* 版本：V1
*************************************************************************/

using System;
using System.Security.Cryptography;
using System.Text;

namespace Tool.Common
{
    /// <summary>
    /// 密码加密助手类
    /// </summary>
    public static class EncrpytTool
    {
        internal static byte[] GetHash(String key)
        {
            using (MD5CryptoServiceProvider hashMD5 = new MD5CryptoServiceProvider())
            {
                return hashMD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(key));
            }
        }

        /// <summary>
        /// key
        /// </summary>
        private static byte[] _DefaultHashKey;
        internal static byte[] DefaultHashKey
        {
            get
            {
                if (_DefaultHashKey == null)
                {
                    _DefaultHashKey = GetHash("!1@2#3$4%5^6");
                }
                return _DefaultHashKey;
            }
        }
        /// <summary>
        /// 预留的二次加密
        /// </summary>
        internal static String EncrpytKey
        {
            get
            {
                return "suibianlaiyige";
            }
        }

        #region 方法

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="isStrict">是否二次加密</param>
        /// <returns>返回密文</returns>
        public static String Encrypt(String text, Boolean isStrict = false)
        {
            String result = Encrypt(text, DefaultHashKey);
            if (EncrpytKey != "")
            {
                result = Encrypt(result, GetHash(EncrpytKey)) + "=2";//设置二级加密标识
            }
            return result;
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="text">要解密的字符串</param>
        /// <param name="isStrict">是否二次加密</param>
        /// <returns>解密后的字符串</returns>
        internal static String Decrypt(String text, Boolean isStrict = false)
        {
            if (String.IsNullOrEmpty(text))
            {
                return String.Empty;
            }
            else
            {
                text = text.Trim().Replace(' ', '+');//处理Request的+号变空格问题。
                if (EncrpytKey != "" && (isStrict || text.EndsWith("=2")))
                {
                    text = Decrypt(text.Substring(0, text.Length - 2), GetHash(EncrpytKey));//先解一次Key
                }
                return Decrypt(text, DefaultHashKey);
            }
        }

        /// <summary>
        /// 3des加密字符串
        /// </summary>
        /// <param name="text">要加密的字符串</param>
        /// <param name="hashKey">密钥</param>
        /// <returns>加密后并经base64编码的字符串</returns>
        /// <remarks>静态方法，采用默认ascii编码</remarks>
        private static String Encrypt(String text, byte[] hashKey)
        {
            String result = String.Empty;
            using (TripleDESCryptoServiceProvider DES = new TripleDESCryptoServiceProvider())
            {
                DES.Key = DefaultHashKey;
                DES.Mode = CipherMode.ECB;
                ICryptoTransform DESEncrypt = DES.CreateEncryptor();

                byte[] Buffer = ASCIIEncoding.UTF8.GetBytes(text);
                String pass = Convert.ToBase64String(DESEncrypt.TransformFinalBlock(Buffer, 0, Buffer.Length));
                result = pass.Replace('=', '#');
            }

            return result;
        }

        /// <summary>
        /// 3des解密字符串
        /// </summary>
        /// <param name="text">要解密的字符串</param>
        /// <param name="key">密钥</param>
        /// <returns>解密后的字符串</returns>
        /// <exception cref="">密钥错误</exception>
        /// <remarks>静态方法，采用默认ascii编码</remarks>
        private static String Decrypt(String text, byte[] hashKey)
        {
            String result = "";
            text = text.Replace('#', '=');
            using (TripleDESCryptoServiceProvider DES = new TripleDESCryptoServiceProvider())
            {
                DES.Key = hashKey;
                DES.Mode = CipherMode.ECB;

                ICryptoTransform DESDecrypt = DES.CreateDecryptor();
                try
                {
                    byte[] Buffer = Convert.FromBase64String(text);
                    result = ASCIIEncoding.UTF8.GetString(DESDecrypt.TransformFinalBlock(Buffer, 0, Buffer.Length));
                }
                catch
                {
                    return text;
                }
            }
            return result;
        }

        #endregion
    }
}
