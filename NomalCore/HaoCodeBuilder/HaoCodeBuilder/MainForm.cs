using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace HaoCodeBuilder
{
    public partial class MainForm : DockContent
    {
        public static MainForm Instance = null;
        public static Form_Database form_Database = null;
        public static Form_Home form_Home = null;
        public static Form_TemplateTree form_TemplateTree = null;

        public MainForm()
        {
            InitializeComponent();
            Instance = this;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dockPanel1.DockLeftPortion = 300;
            form_Database = new Form_Database();
            form_Database.Show(dockPanel1, DockState.DockLeft);

            form_Home = new Form_Home();
            form_Home.Show(dockPanel1);
            form_Home.Activate();

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            ShowServerList();
        }

        /// <summary>
        /// 显示服务器资源管理器
        /// </summary>
        public void ShowServerList()
        {
            if (form_Database == null)
            {
                form_Database = new Form_Database();
                form_Database.Show(dockPanel1, DockState.DockLeft);
            }

            form_Database.Activate();
        }


        /// <summary>
        /// 显示模板管理器
        /// </summary>
        public void ShowTemplate()
        {
            if (form_TemplateTree == null)
            {
                form_TemplateTree = new Form_TemplateTree();
                form_TemplateTree.Show(dockPanel1, DockState.DockLeftAutoHide);
            }
            form_TemplateTree.Activate();
        }

        /// <summary>
        /// 显示起始页
        /// </summary>
        public void ShowHome()
        {
            if (form_Home == null)
            {
                form_Home = new Form_Home();
                form_Home.Show(dockPanel1);
            }

            form_Home.Activate();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ShowHome();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            form_Database.ShowCodeSet();
        }

        private void 添加数据库服务器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            form_Database.toolStripButton1_Click(sender, e);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Exit();
        }

        private void 注销数据库服务器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            form_Database.RemoveServer();
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            Exit();
        }

        /// <summary>
        /// 退出系统
        /// </summary>
        private void Exit()
        {
            Application.Exit();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Form_SetNameSpace fs = new Form_SetNameSpace();
            fs.ShowDialog();
        }

        private void 命名空间配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton3_Click(sender, e);
        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            Form_SetNameSpaceClass fssc = new Form_SetNameSpaceClass();
            fssc.ShowDialog();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            toolStripButton12_Click(sender, e);
        }

        private void 关于我们ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton10_Click(sender, e);
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            Form_About fa = new Form_About();
            fa.ShowDialog();
        }

        private void 数据库服务器配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton7_Click(sender, e);
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            Form_SetServers fss = new Form_SetServers();
            fss.ShowDialog();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            Form_SetDirectory fsd = new Form_SetDirectory();
            fsd.ShowDialog();
        }

        private void 保存目录配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton6_Click(sender, e);
        }

        private void 起始页ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowHome();
        }

        private void 数据库服务器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowServerList();
        }

        private void 代码生成配置toolStripMenuItem_Click(object sender, EventArgs e)
        {
            form_Database.ShowCodeSet();
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.X && e.Control)
            {
                var curForm = this.dockPanel1.ActiveDocument.DockHandler.Form as Form_Code_Area;
                this.dockPanel1.Contents.ToList().ForEach((content) =>
                {
                    var tempItem = content.DockHandler.Form as Form_Code_Area;
                    if (tempItem != null && tempItem != curForm)
                    {
                        tempItem.Close();
                    }
                });
            }
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            SaveFile(this);
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            SaveAllFile(this);
        }

        private void 起始页ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (form_Home == null)
            {
                form_Home = new Form_Home();
                form_Home.Show(dockPanel1);
            }

            form_Home.Activate();
        }

        public static void SaveFile(MainForm mainForm)
        {
            var curForm = mainForm.dockPanel1.ActiveDocument.DockHandler.Form as Form_Code_Area;
            if (curForm == null)
            {
                return;
            }

            String path = @"D;\\CodeBuildDiretory";
            var list = new Common.Config_Directory().GetAll();
            if (list.Count > 0)
            {
                path = list.FirstOrDefault().Name;
            }

            //生成实体类
            StreamWriter sw;
            var FileName = Common.Func.ExistsDirectory(string.Format("{0}\\{1}.cs", path, curForm.Text));
            sw = File.CreateText(FileName);
            sw.Write(curForm.textEditorControl1.Text);
            sw.Close();
            sw.Dispose();

            MessageBox.Show("保存完成!目录：" + path, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void SaveAllFile(MainForm mainForm)
        {
            String path = @"D;\\CodeBuildDiretory";
            var list = new Common.Config_Directory().GetAll();
            if (list.Count > 0)
            {
                path = list.FirstOrDefault().Name;
            }

            StreamWriter sw;
            string FileName = string.Empty;
            int count = 0;
            foreach (var content in mainForm.dockPanel1.Contents)
            {
                var item = content.DockHandler.Form as Form_Code_Area;
                if (item == null)
                {
                    continue;
                }

                count++;
                //生成实体类
                FileName = Common.Func.ExistsDirectory(string.Format("{0}\\{1}.cs", path, item.Text));
                sw = File.CreateText(FileName);
                sw.Write(item.textEditorControl1.Text);
                sw.Close();
                sw.Dispose();
            }

            if (count != 0)
            {
                MessageBox.Show("保存完成!目录：" + path, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
