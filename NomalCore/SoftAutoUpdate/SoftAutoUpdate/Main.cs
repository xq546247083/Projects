using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace SoftAutoUpdate
{
    /// <summary>
    /// 主要思路是，主程序检测是否有更新，如果有，则开启这个专门用来更新的程序，杀掉原来的程序。获取需要更新的文件
    /// App.config用来配置服务器和客户端的数据，DownFileList用来保存需要下载的文件,FtpSample为Ftp下载类
    /// </summary>
    public partial class Main : Form
    {
        /// <summary>
        /// 强制退出标志
        /// </summary>
        private Boolean exitFlag = false;

        /// <summary>
        /// 更新线程
        /// </summary>
        private Thread updateThread;

        /// <summary>
        /// 构造函数
        /// </summary>
        public Main()
        {
            InitializeComponent();
            Init();
            UpdateSoftThread();
        }

        #region 方法

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            FtpUtil.UpdateDownSpeedEvent += FtpUtil_UpdateDownSpeedEvent;
        }

        /// <summary>
        /// 启用更新软件线程
        /// </summary>
        private void UpdateSoftThread()
        {
            updateThread = new Thread(new ThreadStart(UpdateSoft));
            updateThread.IsBackground = true;
            updateThread.Start();
        }

        /// <summary>
        /// 更新程序
        /// </summary>
        private void UpdateSoft()
        {
            KillMainProcess();
            DownFile();
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        private void DownFile()
        {
            this.Invoke(new Action(() => { this.Text = "正在更新......请稍候"; }));

            // 如果程序文件夹不存在，则创建文件夹
            var localSoftLocation = CommonUtil.GetConfigValue("LocalSoftLocation");
            if (!Directory.Exists(localSoftLocation))
            {
                Directory.CreateDirectory(localSoftLocation);
            }

            // 获取下载配置，并初始化
            var serverUserName = CommonUtil.GetConfigValue("ServerUserName");
            var serverUserPwd = CommonUtil.GetConfigValue("ServerUserPwd");
            var ftpServerIp = CommonUtil.GetConfigValue("ServerFtpIp");
            var fs = new FtpUtil(ftpServerIp, serverUserName, serverUserPwd);

            // 设置进度条属性
            Int32 downProgress = 0;
            var fileNameList = GetUpdateFileList();
            pbUpdate.Invoke(new Action(() =>
            {
                pbUpdate.Minimum = 0;
                pbUpdate.Maximum = fileNameList.Count;
                pbUpdate.Value = downProgress;
            }));

            lbFilesSum.Invoke(new Action(() =>
            {
                lbFilesSum.Text = "文件总数：" + fileNameList.Count.ToString();
            }));

            // 按照列表直接更新所有文件到程序文件夹，如果存在，则删除后下载
            foreach (var path in fileNameList)
            {
                lbCurrentFile.Invoke(new Action(() =>
                {
                    lbCurrentFile.Text = "当前正在下载第" + (downProgress + 1).ToString() + "个文件";
                }));

                var localPath = $"{localSoftLocation}\\{path}";

                //判断file的文件夹是否存在，不存在，则创建
                var fileInfo = new FileInfo(localPath);
                if (!CommonUtil.CreateDir(fileInfo))
                {
                    MessageBox.Show("创建文件夹失败，请检查程序权限是否足够！", "提示");
                    this.Invoke(new Action(() => { this.Text = "更新失败!"; }));
                    return;
                }

                // 下载文件
                var serverFilePath = $"{ CommonUtil.GetConfigValue("ServerSoftDirName")}/{path}";
                var downMessage = fs.DownloadFile(localPath, serverFilePath);
                if (downMessage != "Success")
                {
                    MessageBox.Show($"请检查服务器配置或者网络！错误信息：{downMessage}", "提示");
                    this.Invoke(new Action(() => { this.Text = "更新失败!"; }));
                    return;
                }

                // 更新进度
                downProgress++;
                pbUpdate.Invoke(new Action(() =>
                {
                    pbUpdate.Value = downProgress;
                }));
            }

            this.Invoke(new Action(() => { this.Text = "更新完成!"; }));
        }

        /// <summary>
        /// 获取更新文件列表
        /// </summary>
        /// <returns>更新文件列表</returns>
        private List<String> GetUpdateFileList()
        {
            // 结果
            List<String> result = new List<String>();

            // 加载xml文件
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load($"{Application.StartupPath}\\DownFileList.xml");

                XmlNode xn = doc.SelectSingleNode("Files");
                foreach (XmlNode xnTemp in xn.ChildNodes)
                {
                    var xnStr = xnTemp.InnerText.ToString().Trim();
                    if (xnStr != "" && xnStr != null)
                    {
                        result.Add(xnStr);
                    }
                }
            }
            catch
            {
                // 一般要记录错误日志
            }

            return result;
        }

        /// <summary>
        /// 杀掉主线程
        /// </summary>
        private void KillMainProcess()
        {
            // 如果不存在，直接返回
            var mainProcessValue = CommonUtil.GetConfigValue("MainProcessValue");
            if (!CommonUtil.ExistProcess(mainProcessValue))
            {
                return;
            }

            DialogResult dr = MessageBox.Show($"更新需要关闭{mainProcessValue}进程，是否现在关闭？", "提示", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                CommonUtil.KillProcess(mainProcessValue);
            }
            else
            {
                exitFlag = true;
                Application.Exit();
            }
        }

        #endregion

        #region 事件

        /// <summary>
        /// 下载工具速度更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FtpUtil_UpdateDownSpeedEvent(object sender, FtpUtil.UpdateDownSpeedEventArgs e)
        {
            lbDownSpeed.Invoke(new Action(() =>
            {
                lbDownSpeed.Text = "下载的速度为：" + e.DownSpeed + "kb/s";
            }));
        }

        /// <summary>
        /// 关闭更新程序，启动主程序
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnSure_Click(object sender, EventArgs e)
        {
            if (!updateThread.IsAlive)
            {
                String exeLocation = $"{CommonUtil.GetConfigValue("LocalSoftLocation")}\\{ CommonUtil.GetConfigValue("MainProcessValue")}";
                if (File.Exists(exeLocation)) Process.Start(exeLocation);
                Application.Exit();
            }
            else
            {
                MessageBox.Show("请耐心等待更新完成", "提示");
            }
        }

        /// <summary>
        /// 窗体关闭事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (exitFlag)
            {
                return;
            }

            // 如果正在更新
            if (updateThread.IsAlive)
            {
                DialogResult dr = MessageBox.Show("正在下载主程序，强制关闭可能会导致主程序无法使用，确定关闭？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr != DialogResult.Yes)
                {
                    e.Cancel = true;
                }
            }
        }

        #endregion
    }
}
