using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Timers;

namespace SoftAutoUpdate
{
    /// <summary>
    /// Ftp工具
    /// </summary>
    public partial class FtpUtil
    {
        #region 下载

        /// <summary>
        /// 服务器Ip
        /// </summary>
        private String ftpServerIp = "";

        /// <summary>
        /// 用户名
        /// </summary>
        private String userName = "";

        /// <summary>
        /// 用户密码
        /// </summary>
        private String userPwd = "";

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="ftpServerIp">服务器Ip</param>
        /// <param name="userName">用户名</param>
        /// <param name="userPwd">用户密码</param>
        public FtpUtil(String ftpServerIp, String userName, String userPwd)
        {
            this.ftpServerIp = ftpServerIp;
            this.userName = userName;
            this.userPwd = userPwd;
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="LocalFilePath">本地文件地址</param>
        /// <param name="ServerFilePath">服务器文件地址</param>
        /// <returns>下载信息</returns>
        public String DownloadFile(String LocalFilePath, String ServerFilePath)
        {
            // 读取服务器的传输数据协议
            var serverFileUri = $"ftp://{ftpServerIp}/{ServerFilePath}";
            FtpWebRequest ftpWebRequest = (FtpWebRequest)FtpWebRequest.Create(new Uri(serverFileUri));

            // 定义传输数据的协议
            ftpWebRequest.Method = WebRequestMethods.Ftp.DownloadFile;
            ftpWebRequest.Credentials = new NetworkCredential(userName, userPwd);
            ftpWebRequest.UseBinary = true;
            ftpWebRequest.UsePassive = true;

            // 本地写文件的流
            FileStream localFileStream = null;
            FtpWebResponse ftpWebResponse = null;

            try
            {
                // 获取ftp文件流
                ftpWebResponse = (FtpWebResponse)ftpWebRequest.GetResponse();
                var responseStream = ftpWebResponse.GetResponseStream();

                // 本地文件写入流
                localFileStream = new FileStream(LocalFilePath, FileMode.Create);

                // 读取ftp文件流到本地文件
                byte[] buff = new byte[2048];
                var content = responseStream.Read(buff, 0, 2048);
                while (content != 0)
                {
                    localFileStream.Write(buff, 0, content);
                    content = responseStream.Read(buff, 0, 2048);

                    downSumByte += 2.048;
                }

                return "Success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                if (ftpWebResponse != null) ftpWebResponse.Close();
                if (localFileStream != null) localFileStream.Close();
            }
        }

        #endregion

        #region 速度计算

        /// <summary>
        /// 上一秒下载总字节
        /// </summary>
        private static Double preSumByte = 0;

        /// <summary>
        /// 当前下载总字节
        /// </summary>
        private static Double downSumByte = 0;

        /// <summary>
        /// 更新下载速度的定时器(Timer不要声明成局部变量，否则会被GC回收)
        /// </summary>
        private static Timer updateDownSpeedTimer;

        /// <summary>
        /// 更新下载速度（每秒触发一次）
        /// </summary>
        public static event EventHandler<UpdateDownSpeedEventArgs> UpdateDownSpeedEvent;

        /// <summary>
        /// 静态构造方法
        /// </summary>
        static FtpUtil()
        {
            // 初始化timer
            updateDownSpeedTimer = new Timer();
            updateDownSpeedTimer.Interval = 1000;
            updateDownSpeedTimer.AutoReset = true;
            updateDownSpeedTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);

            updateDownSpeedTimer.Enabled = true;
        }

        /// <summary>
        /// timer事件触发
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="e">e</param>
        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            // 计算下载速度
            var downSpeed = Math.Round((downSumByte - preSumByte), 2);

            UpdateDownSpeedEvent?.Invoke(null, new UpdateDownSpeedEventArgs(downSpeed));
            preSumByte = downSumByte;
        }

        /// <summary>
        /// 更新下载速度事件参数
        /// </summary>
        public class UpdateDownSpeedEventArgs : EventArgs
        {
            /// <summary>
            /// 速度
            /// </summary>
            public Double DownSpeed { get; private set; }

            /// <summary>
            /// 构造函数
            /// </summary>
            /// <param name="downSpeed">下载速度</param>
            public UpdateDownSpeedEventArgs(Double downSpeed)
            {
                this.DownSpeed = downSpeed;
            }
        }

        #endregion
    }
}
