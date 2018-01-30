/************************************************************************
* 主窗体
*************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using Tool.Common;

namespace ChatClient
{
    /// <summary>
    /// MainWindow1.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// 缓存用户列表
        /// </summary>
        private List<SysUser> sysUserList = new List<SysUser>();

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
            try
            {
                WebSocketClient.Send(ClientCmdEnum.SysUserGetList);
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
                    sysUserList = JsonTool.Deserialize<List<SysUser>>(returnObject.Data.ToString());
                }

                sysUserList.ForEach((r) => { r.Color = r.Status ? "LightBlue" : "Gray"; });

                ListBoxSysUser.ItemsSource = sysUserList;
            }

            // 处理登录命令
            if (returnObject.Cmd == ClientCmdEnum.Push_SysUserInfo)
            {
                var sysUser = new SysUser();
                // 加载列表
                if (!String.IsNullOrEmpty(returnObject.Data?.ToString()))
                {
                    sysUser = JsonTool.Deserialize<SysUser>(returnObject.Data.ToString());
                }

                if (String.IsNullOrEmpty(sysUser.UserID))
                {
                    return;
                }

                // 处理颜色
                sysUser.Color = sysUser.Status ? "LightBlue" : "Gray";

                // 更新数据
                var sysUserTemp = sysUserList.FirstOrDefault(r => r.UserID == sysUser.UserID);
                if (sysUserTemp == null)
                {
                    sysUserList.Add(sysUser);
                }
                else
                {
                    sysUserTemp.Copy(sysUser);
                }

                ListBoxSysUser.ItemsSource = sysUserList;
            }
        }
    }
}
