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
    public partial class Form_SetCodeBuilder : Form
    {
        private TreeNode node = null;
        public Form_SetCodeBuilder()
        {
            InitializeComponent();
        }

        private void Form_SetCodeBuilder_Load(object sender, EventArgs e)
        {
            var builderType = new Common.Config_CodeBuilder().GetDefalutBuildType();
            this.radioButton1.Checked = builderType == Model.BuilderType.Custom.ToString();
            this.radioButton2.Checked = builderType == Model.BuilderType.Default.ToString();
            this.radioButton3.Checked = builderType == Model.BuilderType.Factory.ToString();
            GetConfig();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveConfig();
            this.Close();
        }

        private void SaveConfig()
        {
            var ccb = new Common.Config_CodeBuilder();
            var builderType = this.radioButton1.Checked ? Model.BuilderType.Custom : (this.radioButton2.Checked ? Model.BuilderType.Default : Model.BuilderType.Factory);
            ccb.UpdateDefalutBuildType(builderType.ToString());

            var model = ccb.GetDefault(builderType.ToString());
            model.Add = checkBox_add.Checked;
            model.Delete = checkBox_delete.Checked;
            model.Update = checkBox_update.Checked;
            model.Exist = checkBox_exists.Checked;
            model.Get = checkBox_getbykey.Checked;
            model.GetAll = checkBox_getall.Checked;
            model.GetCount = checkBox_count.Checked;

            ccb.Save(model);
        }

        private void GetConfig()
        {
            var builderType = this.radioButton1.Checked ? Model.BuilderType.Custom : (this.radioButton2.Checked ? Model.BuilderType.Default : Model.BuilderType.Factory);
            var model = new Common.Config_CodeBuilder().Get(builderType.ToString());
            checkBox_add.Checked = model.Add;
            checkBox_delete.Checked = model.Delete;
            checkBox_update.Checked = model.Update;
            checkBox_exists.Checked = model.Exist;
            checkBox_getbykey.Checked = model.Get;
            checkBox_getall.Checked = model.GetAll;
            checkBox_count.Checked = model.GetCount;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            GetConfig();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            GetConfig();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            GetConfig();
        }
    }
}
