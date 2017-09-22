﻿namespace HaoCodeBuilder
{
    partial class Form_SetCodeBuilder
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBox_add = new System.Windows.Forms.CheckBox();
            this.checkBox_delete = new System.Windows.Forms.CheckBox();
            this.checkBox_update = new System.Windows.Forms.CheckBox();
            this.checkBox_getall = new System.Windows.Forms.CheckBox();
            this.checkBox_getbykey = new System.Windows.Forms.CheckBox();
            this.checkBox_exists = new System.Windows.Forms.CheckBox();
            this.checkBox_count = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(31, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "生成模式：";
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(148, 42);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(95, 16);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.Text = "普通三层架构";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(258, 42);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(119, 16);
            this.radioButton3.TabIndex = 2;
            this.radioButton3.Text = "工厂模式三层架构";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(33, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "生成方法：";
            // 
            // checkBox_add
            // 
            this.checkBox_add.AutoSize = true;
            this.checkBox_add.Checked = true;
            this.checkBox_add.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_add.Location = new System.Drawing.Point(55, 101);
            this.checkBox_add.Name = "checkBox_add";
            this.checkBox_add.Size = new System.Drawing.Size(48, 16);
            this.checkBox_add.TabIndex = 4;
            this.checkBox_add.Text = "新增";
            this.checkBox_add.UseVisualStyleBackColor = true;
            // 
            // checkBox_delete
            // 
            this.checkBox_delete.AutoSize = true;
            this.checkBox_delete.Checked = true;
            this.checkBox_delete.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_delete.Location = new System.Drawing.Point(119, 101);
            this.checkBox_delete.Name = "checkBox_delete";
            this.checkBox_delete.Size = new System.Drawing.Size(48, 16);
            this.checkBox_delete.TabIndex = 5;
            this.checkBox_delete.Text = "删除";
            this.checkBox_delete.UseVisualStyleBackColor = true;
            // 
            // checkBox_update
            // 
            this.checkBox_update.AutoSize = true;
            this.checkBox_update.Checked = true;
            this.checkBox_update.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_update.Location = new System.Drawing.Point(187, 101);
            this.checkBox_update.Name = "checkBox_update";
            this.checkBox_update.Size = new System.Drawing.Size(48, 16);
            this.checkBox_update.TabIndex = 6;
            this.checkBox_update.Text = "修改";
            this.checkBox_update.UseVisualStyleBackColor = true;
            // 
            // checkBox_getall
            // 
            this.checkBox_getall.AutoSize = true;
            this.checkBox_getall.Checked = true;
            this.checkBox_getall.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_getall.Location = new System.Drawing.Point(55, 134);
            this.checkBox_getall.Name = "checkBox_getall";
            this.checkBox_getall.Size = new System.Drawing.Size(96, 16);
            this.checkBox_getall.TabIndex = 7;
            this.checkBox_getall.Text = "查询所有记录";
            this.checkBox_getall.UseVisualStyleBackColor = true;
            // 
            // checkBox_getbykey
            // 
            this.checkBox_getbykey.AutoSize = true;
            this.checkBox_getbykey.Checked = true;
            this.checkBox_getbykey.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_getbykey.Location = new System.Drawing.Point(168, 134);
            this.checkBox_getbykey.Name = "checkBox_getbykey";
            this.checkBox_getbykey.Size = new System.Drawing.Size(96, 16);
            this.checkBox_getbykey.TabIndex = 8;
            this.checkBox_getbykey.Text = "查询主键记录";
            this.checkBox_getbykey.UseVisualStyleBackColor = true;
            // 
            // checkBox_exists
            // 
            this.checkBox_exists.AutoSize = true;
            this.checkBox_exists.Checked = true;
            this.checkBox_exists.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_exists.Location = new System.Drawing.Point(258, 101);
            this.checkBox_exists.Name = "checkBox_exists";
            this.checkBox_exists.Size = new System.Drawing.Size(120, 16);
            this.checkBox_exists.TabIndex = 9;
            this.checkBox_exists.Text = "判断记录是否存在";
            this.checkBox_exists.UseVisualStyleBackColor = true;
            // 
            // checkBox_count
            // 
            this.checkBox_count.AutoSize = true;
            this.checkBox_count.Checked = true;
            this.checkBox_count.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_count.Location = new System.Drawing.Point(282, 134);
            this.checkBox_count.Name = "checkBox_count";
            this.checkBox_count.Size = new System.Drawing.Size(96, 16);
            this.checkBox_count.TabIndex = 10;
            this.checkBox_count.Text = "查询记录条数";
            this.checkBox_count.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(132, 186);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(50, 42);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(83, 16);
            this.radioButton1.TabIndex = 21;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "自定义模式";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // Form_SetCodeBuilder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(423, 229);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.checkBox_count);
            this.Controls.Add(this.checkBox_exists);
            this.Controls.Add(this.checkBox_getbykey);
            this.Controls.Add(this.checkBox_getall);
            this.Controls.Add(this.checkBox_update);
            this.Controls.Add(this.checkBox_delete);
            this.Controls.Add(this.checkBox_add);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.radioButton3);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_SetCodeBuilder";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "设置代码生成模式";
            this.Load += new System.EventHandler(this.Form_SetCodeBuilder_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBox_add;
        private System.Windows.Forms.CheckBox checkBox_delete;
        private System.Windows.Forms.CheckBox checkBox_update;
        private System.Windows.Forms.CheckBox checkBox_getall;
        private System.Windows.Forms.CheckBox checkBox_getbykey;
        private System.Windows.Forms.CheckBox checkBox_exists;
        private System.Windows.Forms.CheckBox checkBox_count;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RadioButton radioButton1;
    }
}