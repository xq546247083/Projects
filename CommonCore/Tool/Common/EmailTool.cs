/************************************************************************
* 标题: 邮箱助手
* 描述: 邮箱助手
* 作者：肖强
* 日期：2017-5-3 10:43:36
* 版本：V1
*************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using Tool.Extension;

namespace Tool.Common
{
    /// <summary>
    /// 邮箱助手
    /// </summary>
    public static class EmailTool
    {
        private static string mMailHost = string.Empty;

        private static string mSenderAddress = string.Empty;

        private static string mSenderPassword = string.Empty;

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="mailTo">收件人</param>
        /// <param name="subject">邮件主题</param>
        /// <param name="body">邮件内容</param>
        /// <param name="attachFiles">附件路径</param>
        /// <param name="isBodyHtml">内容是否为Html</param>
        /// <exception cref="T:System.ArgumentException"></exception>
        /// <exception cref="T:System.ArgumentNullException"></exception>
        /// <exception cref="T:System.FormatException"></exception>
        /// <exception cref="T:System.InvalidOperationException"></exception>
        /// <exception cref="T:System.ObjectDisposedException"></exception>
        /// <exception cref="T:System.Net.Mail.SmtpException"></exception>
        /// <exception cref="T:System.Net.Mail.SmtpFailedRecipientsException"></exception>        
        private static Boolean Send(IEnumerable<string> mailTo, string subject, string body, IEnumerable<string> attachFiles, bool isBodyHtml)
        {
            MailMessage mailMessage = new MailMessage();
            try
            {
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.From = new MailAddress(mSenderAddress);
                mailMessage.IsBodyHtml = isBodyHtml;
                foreach (string current in mailTo)
                {
                    mailMessage.To.Add(current);
                }
                if (attachFiles != null && attachFiles.Count<string>() > 0)
                {
                    foreach (string current2 in attachFiles)
                    {
                        mailMessage.Attachments.Add(new Attachment(current2));
                    }
                }
                SmtpClient smtpClient = new SmtpClient(mMailHost)
                {
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(mSenderAddress, mSenderPassword)
                };
                smtpClient.Send(mailMessage);
            }
            catch (System.Exception ex)
            {
                Log.Write(ex.ToMessage(), LogType.Warn);
                return false;
            }
            finally
            {
                if (mailMessage.Attachments != null)
                {
                    mailMessage.Attachments.Dispose();
                }
            }

            return true;
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="state">相关参数</param>
        private static void Send(object state)
        {
            Dictionary<string, object> dictionary = (Dictionary<string, object>)state;
            IEnumerable<string> mailTo = (IEnumerable<string>)dictionary["MailTo"];
            string subject = dictionary["Subject"].ToString();
            string body = dictionary["Body"].ToString();
            bool isBodyHtml = System.Convert.ToBoolean(dictionary["IsBodyHtml"]);
            IEnumerable<string> attachFiles = null;
            if (dictionary.ContainsKey("AttachFiles"))
            {
                attachFiles = (IEnumerable<string>)dictionary["AttachFiles"];
            }
            try
            {
                Send(mailTo, subject, body, attachFiles, isBodyHtml);
            }
            catch (System.Exception ex)
            {
                Log.Write(ex.ToMessage(), LogType.Error);
            }
        }

        /// <summary>
        /// 设置发送者信息（一次设定，永久生效）
        /// </summary>
        /// <param name="mailHost">邮箱主机地址</param>
        /// <param name="address">发送者邮件地址</param>
        /// <param name="password">发送者密码</param>
        public static void SetSenderInfo(string mailHost, string address, string password)
        {
            mMailHost = mailHost;
            mSenderAddress = address;
            mSenderPassword = password;
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="mailTo">收件人地址</param>
        /// <param name="subject">邮件主题</param>
        /// <param name="body">邮件内容</param>
        /// <param name="sendPattern">发送模式</param>
        /// <param name="isBodyHtml">内容是否为Html</param>
        /// <exception cref="T:System.ArgumentException"></exception>
        /// <exception cref="T:System.ArgumentNullException"></exception>
        /// <exception cref="T:System.FormatException"></exception>
        /// <exception cref="T:System.InvalidOperationException"></exception>
        /// <exception cref="T:System.ObjectDisposedException"></exception>
        /// <exception cref="T:System.Net.Mail.SmtpException"></exception>
        /// <exception cref="T:System.Net.Mail.SmtpFailedRecipientsException"></exception>
        public static Boolean SendMail(string[] mailTo, string subject, string body, SendPattern sendPattern, bool isBodyHtml = false)
        {
            if (mailTo == null || mailTo.Length == 0)
            {
                throw new ArgumentNullException("mailTo", "mailTo can't be null or empty");
            }
            if (string.IsNullOrEmpty(subject))
            {
                throw new ArgumentNullException("subject", "subject can't be null or empty");
            }
            if (string.IsNullOrEmpty(body))
            {
                throw new ArgumentNullException("body", "body can't be null or empty");
            }
            if (sendPattern == SendPattern.Synchronous)
            {
                return Send(mailTo, subject, body, null, isBodyHtml);
            }
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary["MailTo"] = mailTo;
            dictionary["Subject"] = subject;
            dictionary["Body"] = body;
            dictionary["IsBodyHtml"] = isBodyHtml;
            ThreadPool.QueueUserWorkItem(new WaitCallback(Send), dictionary);

            //采用异步方式发送邮件，直接默认为发送成功
            return true;
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="mailTo">收件人</param>
        /// <param name="subject">邮件主题</param>
        /// <param name="body">邮件内容</param>
        /// <param name="attachFiles">附件</param>
        /// <param name="sendPattern">发送模式</param>
        /// <param name="isBodyHtml">内容是否为Html</param>
        public static Boolean SendMail(IEnumerable<string> mailTo, string subject, string body, IEnumerable<string> attachFiles, SendPattern sendPattern, bool isBodyHtml = false)
        {
            if (mailTo == null || mailTo.Count<string>() == 0)
            {
                throw new System.ArgumentNullException("mailTo", "mailTo can't be null or empty");
            }
            if (string.IsNullOrEmpty(subject))
            {
                throw new System.ArgumentNullException("subject", "subject can't be null or empty");
            }
            if (string.IsNullOrEmpty(body))
            {
                throw new System.ArgumentNullException("body", "body can't be null or empty");
            }
            if (sendPattern == SendPattern.Synchronous)
            {
                return Send(mailTo, subject, body, attachFiles, isBodyHtml);
            }
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary["MailTo"] = mailTo;
            dictionary["Subject"] = subject;
            dictionary["Body"] = body;
            dictionary["IsBodyHtml"] = isBodyHtml;
            if (attachFiles != null && attachFiles.Count<string>() > 0)
            {
                dictionary["AttachFiles"] = attachFiles;
            }
            ThreadPool.QueueUserWorkItem(new WaitCallback(Send), dictionary);

            //采用异步方式发送邮件，直接默认为发送成功
            return true;
        }
    }

    /// <summary>
	/// 发送模式
	/// </summary>
	public enum SendPattern : byte
    {
        /// <summary>
        /// 同步模式
        /// </summary>
        Synchronous,
        /// <summary>
        /// 异步模式
        /// </summary>
        Asynchronous
    }
}