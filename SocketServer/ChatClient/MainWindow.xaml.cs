/************************************************************************
* 主窗体
*************************************************************************/
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Documents;

namespace ChatClient
{
    using Tool.Common;

    /// <summary>
    /// MainWindow1.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public MainWindow()
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
            // 注册消息接受事件
            WebSocketClient.HandleMessage += WebSocketClient_HandleMessage;
        }

        /// <summary>
        /// 初始化界面数据
        /// </summary>
        private void InitData()
        {
            this.Title = $"{ChatClientConfig.UserID}-【昵称:{ChatClientConfig.NickName}】";
            try
            {
                WebSocketClient.Send(ClientCmdEnum.SysUserGetList);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            ListBoxSysUser.ItemsSource = null;
            ListBoxSysUser.ItemsSource = SysUserManager.GetCopyData();
        }

        /// <summary>
        /// 刷新聊天窗口
        /// </summary>
        private void RefreshMsgBox()
        {
            // 目标用户
            var item = ListBoxSysUser.SelectedItem;
            if (item == null)
            {
                return;
            }

            var sysUser = ((SysUser)item);
            var msgList = MsgManager.GetCopyData(sysUser.UserID);

            Paragraph p = new Paragraph();
            foreach (var msg in msgList)
            {
                Run r = new Run($"{msg.Message}");
                p.Inlines.Add(r);
            }

            TxtReciveMsg.Document.Blocks.Add(p);
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ButtonMsg_Click(object sender, RoutedEventArgs e)
        {
            // 发送消息
            var txtMsgRange = new TextRange(TxtMsg.Document.ContentStart, TxtMsg.Document.ContentEnd);
            if (String.IsNullOrEmpty(txtMsgRange.Text))
            {
                MessageBox.Show("发送消息不能为空！");
                return;
            }

            // 目标用户
            var item = ListBoxSysUser.SelectedItem;
            if (item == null)
            {
                MessageBox.Show("请选中发送用户！");
                return;
            }

            var request = new Dictionary<String, Object>();
            request["ToUserID"] = ((SysUser)item).UserID;
            request["Msg"] = txtMsgRange.Text;

            try
            {
                WebSocketClient.Send(ClientCmdEnum.SysUserChat, request);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            TxtMsg.Document.Blocks.Clear();
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
            // 如果返回错误，直接提示
            if (returnObject.Code != 0)
            {
                MessageBox.Show(returnObject.Message);
                return;
            }

            // 处理上线返回的用户列表
            if (returnObject.Cmd == ClientCmdEnum.SysUserGetList)
            {
                // 如果返回错误，直接提示
                if (returnObject.Code != 0)
                {
                    MessageBox.Show(returnObject.Message);
                }

                // 加载列表
                if (!String.IsNullOrEmpty(returnObject.Data?.ToString()))
                {
                    // 添加上线的玩家
                    SysUserManager.AddOrUpdateUsers(JsonTool.Deserialize<List<SysUser>>(returnObject.Data.ToString()));
                }

                ListBoxSysUser.ItemsSource = null;
                ListBoxSysUser.ItemsSource = SysUserManager.GetCopyData();
            }

            // 处理登录命令
            if (returnObject.Cmd == ClientCmdEnum.Push_SysUserInfo)
            {
                // 加载列表
                if (!String.IsNullOrEmpty(returnObject.Data?.ToString()))
                {
                    var sysUser = JsonTool.Deserialize<SysUser>(returnObject.Data.ToString());
                    SysUserManager.AddOrUpdateUsers(new List<SysUser>() { sysUser });
                }

                ListBoxSysUser.ItemsSource = null;
                ListBoxSysUser.ItemsSource = SysUserManager.GetCopyData();
            }

            // 处理广播消息
            if (returnObject.Cmd == ClientCmdEnum.Push_Broadcast)
            {
                // 加载列表
                if (!String.IsNullOrEmpty(returnObject.Data?.ToString()))
                {
                    var msg = JsonTool.Deserialize<Msg>(returnObject.Data.ToString());
                    MsgManager.AddBrocastMsg(msg);

                    RefreshMsgBox();
                }
            }

            // 处理私聊消息
            if (returnObject.Cmd == ClientCmdEnum.Push_Chat)
            {
                // 加载列表
                if (!String.IsNullOrEmpty(returnObject.Data?.ToString()))
                {
                    var msg = JsonTool.Deserialize<Msg>(returnObject.Data.ToString());
                    MsgManager.AddMsg(msg);

                    RefreshMsgBox();
                }
            }
        }
    }
}
