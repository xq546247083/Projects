/************************************************************************
* 登录页面
*************************************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows;

namespace ChatClient
{
    using Tool.Common;

    /// <summary>
    /// LoginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();

            Init();
            InitData();
        }

        /// <summary>
        /// 程序启动初始化
        /// </summary>
        private void Init()
        {
            // 初始化配置
            ChatClientConfig.Init();

            // 设置日志位置
            Log.Set(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log"), true, true, true, true);

            // 注册消息接受事件
            WebSocketClient.HandleMessage += WebSocketClient_HandleMessage;

            // 开始链接
            WebSocketClient.Start(ChatClientConfig.WebSocketServerUrl);
        }

        /// <summary>
        /// 初始化界面数据
        /// </summary>
        private void InitData()
        {
            if (!String.IsNullOrEmpty(ChatClientConfig.UserID))
            {
                txtUserID.Text = ChatClientConfig.UserID;
            }

            if (!String.IsNullOrEmpty(ChatClientConfig.NickName))
            {
                txtNickName.Text = ChatClientConfig.NickName;
            }
        }

        /// <summary>
        /// 点击登录
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            var userID = txtUserID.Text;
            var nickName = txtNickName.Text;

            if (String.IsNullOrEmpty(userID))
            {
                MessageBox.Show("用户ID不能为空！");
                return;
            }

            if (String.IsNullOrEmpty(nickName))
            {
                MessageBox.Show("昵称不能为空！");
                return;
            }

            var request = new Dictionary<String, Object>();
            request["UserID"] = userID;
            request["NickName"] = nickName;

            try
            {
                WebSocketClient.Send(ClientCmdEnum.SysUserLogin, request);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 处理接受到的消息
        /// </summary>
        /// <param name="returnObject">返回的结果</param>
        private void WebSocketClient_HandleMessage(ReturnObject returnObject)
        {
            if (Dispatcher.Thread == Thread.CurrentThread)
            {
                HandleMessage(returnObject);
                return;
            }

            this.Dispatcher.Invoke(() => { HandleMessage(returnObject); });
        }

        /// <summary>
        /// 处理接受到的消息
        /// </summary>
        /// <param name="returnObject">返回值</param>
        private void HandleMessage(ReturnObject returnObject)
        {
            // 处理连接命令
            if (returnObject.Cmd == ClientCmdEnum.Connect)
            {
                // 如果返回错误，直接提示
                if (returnObject.Code != 0)
                {
                    MessageBox.Show(returnObject.Message);
                }
            }

            // 处理登录命令
            if (returnObject.Cmd == ClientCmdEnum.SysUserLogin)
            {
                // 如果返回错误，直接提示
                if (returnObject.Code != 0)
                {
                    this.DialogResult = false;
                    MessageBox.Show(returnObject.Message);
                }

                this.DialogResult = true;
                this.Close();
            }
        }
    }
}
