namespace Spider
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.txtInfo = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtUrlInfo = new System.Windows.Forms.TextBox();
            this.txtImgInfo = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtInfo
            // 
            this.txtInfo.Dock = System.Windows.Forms.DockStyle.Right;
            this.txtInfo.Location = new System.Drawing.Point(521, 0);
            this.txtInfo.Multiline = true;
            this.txtInfo.Name = "txtInfo";
            this.txtInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtInfo.Size = new System.Drawing.Size(309, 517);
            this.txtInfo.TabIndex = 0;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(26, 3);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "开始";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtImgInfo);
            this.panel1.Controls.Add(this.txtUrlInfo);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(521, 517);
            this.panel1.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnStart);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(521, 36);
            this.panel2.TabIndex = 2;
            // 
            // txtUrlInfo
            // 
            this.txtUrlInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtUrlInfo.Location = new System.Drawing.Point(0, 36);
            this.txtUrlInfo.Multiline = true;
            this.txtUrlInfo.Name = "txtUrlInfo";
            this.txtUrlInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtUrlInfo.Size = new System.Drawing.Size(521, 481);
            this.txtUrlInfo.TabIndex = 3;
            // 
            // txtImgInfo
            // 
            this.txtImgInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtImgInfo.Location = new System.Drawing.Point(0, 282);
            this.txtImgInfo.Multiline = true;
            this.txtImgInfo.Name = "txtImgInfo";
            this.txtImgInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtImgInfo.Size = new System.Drawing.Size(521, 235);
            this.txtImgInfo.TabIndex = 4;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(830, 517);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtInfo);
            this.Name = "MainForm";
            this.Text = "爬虫";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtInfo;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtImgInfo;
        private System.Windows.Forms.TextBox txtUrlInfo;
    }
}

