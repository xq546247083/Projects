/************************************************************************
* 登录页面
*************************************************************************/
using System;
using System.Collections.Generic;
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
            // 开启客户端连接
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

            WebSocketClient.Send(ClientCmdEnum.Login, request);
        }
    }
}
