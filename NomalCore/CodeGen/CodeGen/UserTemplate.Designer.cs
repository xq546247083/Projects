﻿namespace DockPanelSample
{
    partial class UserTemplate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserTemplate));
            this.txtInputArea = new System.Windows.Forms.TextBox();
            this.btnGetCode = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.txtDisplayArea = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtInputArea
            // 
            this.txtInputArea.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtInputArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtInputArea.Location = new System.Drawing.Point(3, 3);
            this.txtInputArea.Multiline = true;
            this.txtInputArea.Name = "txtInputArea";
            this.txtInputArea.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtInputArea.Size = new System.Drawing.Size(286, 40);
            this.txtInputArea.TabIndex = 4;
            this.txtInputArea.Text = resources.GetString("txtInputArea.Text");
            // 
            // btnGetCode
            // 
            this.btnGetCode.Location = new System.Drawing.Point(3, 49);
            this.btnGetCode.Name = "btnGetCode";
            this.btnGetCode.Size = new System.Drawing.Size(75, 23);
            this.btnGetCode.TabIndex = 5;
            this.btnGetCode.Text = "生成代码";
            this.btnGetCode.UseVisualStyleBackColor = true;
            this.btnGetCode.Click += new System.EventHandler(this.btnGetCode_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.txtDisplayArea, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtInputArea, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnGetCode, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(292, 273);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // txtDisplayArea
            // 
            this.txtDisplayArea.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDisplayArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDisplayArea.Location = new System.Drawing.Point(3, 89);
            this.txtDisplayArea.Multiline = true;
            this.txtDisplayArea.Name = "txtDisplayArea";
            this.txtDisplayArea.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDisplayArea.Size = new System.Drawing.Size(286, 181);
            this.txtDisplayArea.TabIndex = 6;
            // 
            // UserTemplate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "UserTemplate";
            this.Text = "自由模板";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtInputArea;
        private System.Windows.Forms.Button btnGetCode;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox txtDisplayArea;
    }
}