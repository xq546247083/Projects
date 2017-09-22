using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HaoCodeBuilder
{
    public partial class Form_SetNameSpaceClass : Form
    {
        public Form_SetNameSpaceClass()
        {
            InitializeComponent();
        }
        private Common.Config_NameSpaceClass CNSC = new Common.Config_NameSpaceClass();
        private void Form_SetNameSpaceClass_Load(object sender, EventArgs e)
        {
            var config = CNSC.GetDefault();
            this.textBox_model.Text = config.Model;
            this.textBox_data.Text = config.Data;
            this.textBox_business.Text = config.Business;
            this.textBox_interface.Text = config.Interface;
            this.textBox_factory.Text = config.Factory;
            this.textBox1.Text = config.UserName;
        }

        private Model.ConfigNameSpaceClass GetModel()
        {
            string model = this.textBox_model.Text;
            string data = this.textBox_data.Text;
            string business = this.textBox_business.Text;
            string interface1 = this.textBox_interface.Text;
            string factory = this.textBox_factory.Text;
            string userName = this.textBox1.Text;

            if (model.IsNullOrEmpty())
            {
                MessageBox.Show("实体层命名不能为空!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }
            if (data.IsNullOrEmpty())
            {
                MessageBox.Show("数据层命名不能为空!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }
            if (business.IsNullOrEmpty())
            {
                MessageBox.Show("业务层命名不能为空!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }

            Model.ConfigNameSpaceClass cnsc = new Model.ConfigNameSpaceClass();
            cnsc.Model = model.Trim();
            cnsc.Data = data.Trim();
            cnsc.Business = business.Trim();
            cnsc.Interface = interface1.Trim();
            cnsc.Factory = factory.Trim();
            cnsc.UserName = userName.Trim();
            return cnsc;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Model.ConfigNameSpaceClass cnsc = GetModel();
            if (cnsc != null)
            {
                if (CNSC.Save(cnsc))
                {
                    MessageBox.Show("保存成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("保存失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
