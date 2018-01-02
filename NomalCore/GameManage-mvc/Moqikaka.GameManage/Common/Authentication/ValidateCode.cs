/************************************************************************
* 描述:验证码
*************************************************************************/
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Web;

namespace Moqikaka.GameManage
{
    /// <summary>
    /// 验证码
    /// </summary>
    public class ValidateCode
    {
        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <param name="retInfo">验证码</param>
        /// <returns></returns>
        public string CreateValidateCode(out string retInfo)
        {
            #region 验证码 + - 计算

            string retcode = "";
            retInfo = "moqikakavcode";
            int seekSeek = unchecked((int)DateTime.Now.Ticks);
            Random rand = new Random(seekSeek);
            string stroper = "+*";
            string oper = stroper[rand.Next(0, stroper.Length)].ToString();
            string k1 = string.Empty;
            string k2 = string.Empty;

            switch (oper)
            {
                case "*":
                    {
                        k1 = rand.Next(1, 10).ToString();
                        k2 = rand.Next(1, 10).ToString();
                        retInfo = (int.Parse(k1) * int.Parse(k2)).ToString();
                        retcode = k1 + "* " + k2 + "=?";
                        break;
                    }
                case "+":
                    {
                        k1 = rand.Next(1, 50).ToString();
                        k2 = rand.Next(1, 50).ToString();
                        retInfo = (int.Parse(k1) + int.Parse(k2)).ToString();
                        retcode = k1 + "+ " + k2 + "=?";
                        break;
                    }
            }
            return retcode;

            #endregion
        }

        /// <summary>
        /// 创建验证码的图片
        /// </summary>
        /// <param name="validateCode">验证码</param>
        public void CreateValidateGraphic(string validateCode)
        {
            Bitmap image = new Bitmap(110, 34);
            Graphics g = Graphics.FromImage(image);
            try
            {
                //生成随机生成器
                Random random = new Random();
                //清空图片背景色
                g.Clear(Color.White);

                //画图片的干扰线
                for (int i = 0; i < 25; i++)
                {
                    int x1 = random.Next(image.Width);
                    int x2 = random.Next(image.Width);
                    int y1 = random.Next(image.Height);
                    int y2 = random.Next(image.Height);
                    g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
                }

                Font font = new Font("Arial", 16, (FontStyle.Bold | FontStyle.Italic));
                LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.Blue, Color.DarkRed, 1.2f, true);
                g.DrawString(validateCode, font, brush, 0, 3);

                //画图片的前景干扰点
                for (int i = 0; i < 100; i++)
                {
                    int x = random.Next(image.Width);
                    int y = random.Next(image.Height);
                    image.SetPixel(x, y, Color.FromArgb(random.Next()));
                }

                //画图片的边框线
                g.DrawRectangle(new Pen(Color.Silver, 4), 0, 0, image.Width - 1, image.Height - 1);
                //保存图片数据
                MemoryStream stream = new MemoryStream();
                image.Save(stream, ImageFormat.Jpeg);

                //输出图片流
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ContentType = "image/jpeg";
                HttpContext.Current.Response.BinaryWrite(stream.ToArray());
            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }
        }
    }
}