/************************************************************************
* 主窗体
*************************************************************************/
using System;
using System.Threading;
using System.Windows;

namespace ChatClient
{
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
            }
        }
    }
}
