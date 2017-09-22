using System;
using System.Linq;
using System.Windows.Forms;
using HaoCodeBuilder.Model;

namespace HaoCodeBuilder
{
    public partial class Form_SetDirectory : Form
    {
        public Form_SetDirectory()
        {
            InitializeComponent();
        }

        private void Form_SetDirectory_Load(object sender, EventArgs e)
        {
            refresh();
        }

        private void refresh()
        {
            var list = new Common.Config_Directory().GetAll();
            if (list.Count > 0)
            {
                textBox1.Text = list.FirstOrDefault().Name;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var cd = new Common.Config_Directory();
            cd.DeleteAll();
            if (String.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("请选择路径!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            ConfigDirectory cdir=new ConfigDirectory();
            cdir.Name = textBox1.Text;
            cd.Add(cdir);

            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = folderBrowserDialog1.SelectedPath;
            }
        }
    }
}
