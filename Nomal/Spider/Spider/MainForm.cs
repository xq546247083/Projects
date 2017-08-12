using System;
using System.Configuration;
using System.Windows.Forms;

namespace Spider
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 接受消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void spider_PushInfo(object sender, Event.OnPushInfoEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(() =>
                {
                    if (e.Type == 1)
                    {
                        txtInfo.Text += e.Info + Environment.NewLine;
                    }
                    else if (e.Type == 2)
                    {
                        txtUrlInfo.Text += e.Info + Environment.NewLine;
                    }
                    else if (e.Type == 3)
                    {
                        txtUrlInfo.Text = txtImgInfo.Text.Replace(e.Info + Environment.NewLine, "");
                    }
                    else if (e.Type == 4)
                    {
                        txtImgInfo.Text += e.Info + Environment.NewLine;
                    }
                    else if (e.Type == 5)
                    {
                        txtImgInfo.Text = txtImgInfo.Text.Replace(e.Info + Environment.NewLine, "");
                    }

                }));
            }
            else
            {
                txtInfo.Text += e.Info;
            }
        }

        private void btnStart_Click(object sender, System.EventArgs e)
        {
            BLL.Spider spider = new BLL.Spider();
            spider.PushInfo += spider_PushInfo;
            spider.Start(ConfigurationManager.AppSettings["url"]);
        }
    }
}
