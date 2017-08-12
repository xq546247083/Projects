/************************************************************************
* 标题: 字符串扩展类
* 描述: 字符串扩展类
* 作者：肖强
* 日期：2017-8-12 10:42:53
* 版本：V1
*************************************************************************/

using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;

namespace Tool.Extension
{
    /// <summary>
    /// 字符串扩展类
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// 用于判断是否为空字符
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsBlank(this String s)
        {
            return s == null || (s.Trim().Length == 0);
        }

        /// <summary>
        /// 用于判断是否不为空字符
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsNotBlank(this String s)
        {
            return !s.IsBlank();
        }
        /// <summary>
        /// 判断是否为有效的Email地址
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsValidEmail(this String s)
        {
            if (!s.IsBlank())
            {
                const String pattern = @"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$";
                return Regex.IsMatch(s, pattern);
            }
            return false;
        }

        /// <summary>
        /// 验证是否是合法的电话号码
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsValidPhone(this String s)
        {
            if (!s.IsBlank())
            {
                return Regex.IsMatch(s, @"^\+?((\d{2,4}(-)?)|(\(\d{2,4}\)))*(\d{0,16})*$");
            }
            return true;
        }

        /// <summary>
        /// 验证是否是合法的手机号码
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsValidMobile(this String s)
        {
            if (!s.IsBlank())
            {
                return Regex.IsMatch(s, @"^\+?\d{0,4}?[1][3-8]\d{9}$");
            }
            return false;
        }

        /// <summary>
        /// 验证是否是合法的邮编
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsValidZipCode(this String s)
        {
            if (!s.IsBlank())
            {
                return Regex.IsMatch(s, @"[1-9]\d{5}(?!\d)");
            }
            return true;
        }

        /// <summary>
        /// 验证是否是合法的传真
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsValidFax(this String s)
        {
            if (!s.IsBlank())
            {
                return Regex.IsMatch(s, @"(^[0-9]{3,4}\-[0-9]{7,8}$)|(^[0-9]{7,8}$)|(^\([0-9]{3,4}\)[0-9]{3,8}$)|(^0{0,1}13[0-9]{9}$)");
            }
            return true;
        }

        /// <summary>
        /// 检查字符串是否为有效的INT32数字
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool IsInt32(this String val)
        {
            if (IsBlank(val))
                return false;
            Int32 k;
            return Int32.TryParse(val, out k);
        }

        /// <summary>
        /// 检查字符串是否为有效的INT64数字
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool IsInt64(this String val)
        {
            if (IsBlank(val))
                return false;
            Int64 k;
            return Int64.TryParse(val, out k);
        }

        /// <summary>
        /// 检查字符串是否为有效的Decimal
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool IsDecimal(this String val)
        {
            if (IsBlank(val))
                return false;
            decimal d;
            return Decimal.TryParse(val, out d);
        }

        /// <summary>
        /// 将字符串转换成MD5加密字符串
        /// </summary>
        /// <param name="orgStr"></param>
        /// <returns></returns>
        public static String ToMD5(this String orgStr)
        {
            MD5 md5Hasher = MD5.Create();

            // Convert the input String to a byte array and compute the hash.
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(orgStr));

            // Create a new Stringbuilder to collect the bytes
            // and create a String.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal String.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal String.
            return sBuilder.ToString();
        }

        /// <summary>
        /// 将对象序列化成XML
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static String ToXML<T>(this T obj) where T : class
        {
            return ToXML(obj, Encoding.Default.BodyName);
        }

        public static String ToXML<T>(this T obj, String encodeName) where T : class
        {
            if (obj == null) throw new ArgumentNullException("obj", "obj is null.");

            if (obj is String) throw new ApplicationException("obj can't be String object.");

            Encoding en = Encoding.GetEncoding(encodeName);
            XmlSerializer serial = new XmlSerializer(typeof(T));
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            MemoryStream ms = new MemoryStream();
            XmlTextWriter xt = new XmlTextWriter(ms, en);
            serial.Serialize(xt, obj, ns);
            xt.Close();
            ms.Close();
            return en.GetString(ms.ToArray());
        }

        /// <summary>
        /// 将XML字符串反序列化成对象实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="s"></param>
        /// <returns></returns>
        public static T Deserial<T>(this String s) where T : class
        {
            return Deserial<T>(s, Encoding.Default.BodyName);
        }

        public static T Deserial<T>(this String s, String encodeName) where T : class
        {
            if (s.IsBlank())
            {
                throw new ApplicationException("xml String is null or empty.");
            }
            XmlSerializer serial = new XmlSerializer(typeof(T));
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");

            return (T)serial.Deserialize(new StringReader(s));
        }

        /// <summary>
        /// 获取扩展名
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static String GetExt(this String s)
        {
            String ret = String.Empty;
            if (s.Contains('.'))
            {
                String[] temp = s.Split('.');
                ret = temp[temp.Length - 1];
            }

            return ret;
        }

        /// <summary>
        /// 验证QQ格式
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsValidQQ(this String s)
        {
            if (!s.IsBlank())
            {
                return Regex.IsMatch(s, @"^[1-9]\d{4,15}$");
            }
            return false;
        }

        /// <summary>
        /// 字符串转成Int
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int ToInt(this String s)
        {
            int res;
            int.TryParse(s, out res);
            return res;
        }
    }
}
