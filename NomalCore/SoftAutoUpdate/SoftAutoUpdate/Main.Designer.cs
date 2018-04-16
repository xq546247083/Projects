namespace SoftAutoUpdate
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.pbUpdate = new System.Windows.Forms.ProgressBar();
            this.btnSure = new System.Windows.Forms.Button();
            this.lbDownSpeed = new System.Windows.Forms.Label();
            this.lbCurrentFile = new System.Windows.Forms.Label();
            this.lbFilesSum = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // pbUpdate
            // 
            this.pbUpdate.Location = new System.Drawing.Point(21, 21);
            this.pbUpdate.Name = "pbUpdate";
            this.pbUpdate.Size = new System.Drawing.Size(426, 23);
            this.pbUpdate.TabIndex = 0;
            // 
            // btnSure
            // 
            this.btnSure.Location = new System.Drawing.Point(471, 21);
            this.btnSure.Name = "btnSure";
            this.btnSure.Size = new System.Drawing.Size(75, 23);
            this.btnSure.TabIndex = 1;
            this.btnSure.Text = "启动主程序";
            this.btnSure.UseVisualStyleBackColor = true;
            this.btnSure.Click += new System.EventHandler(this.btnSure_Click);
            // 
            // lbDownSpeed
            // 
            this.lbDownSpeed.AutoSize = true;
            this.lbDownSpeed.Location = new System.Drawing.Point(182, 59);
            this.lbDownSpeed.Name = "lbDownSpeed";
            this.lbDownSpeed.Size = new System.Drawing.Size(119, 12);
            this.lbDownSpeed.TabIndex = 2;
            this.lbDownSpeed.Text = "下载的速度为：1kb/s";
            // 
            // lbCurrentFile
            // 
            this.lbCurrentFile.AutoSize = true;
            this.lbCurrentFile.Location = new System.Drawing.Point(19, 59);
            this.lbCurrentFile.Name = "lbCurrentFile";
            this.lbCurrentFile.Size = new System.Drawing.Size(131, 12);
            this.lbCurrentFile.TabIndex = 3;
            this.lbCurrentFile.Text = "当前正在下载第1个文件";
            // 
            // lbFilesSum
            // 
            this.lbFilesSum.AutoSize = true;
            this.lbFilesSum.Location = new System.Drawing.Point(411, 59);
            this.lbFilesSum.Name = "lbFilesSum";
            this.lbFilesSum.Size = new System.Drawing.Size(65, 12);
            this.lbFilesSum.TabIndex = 4;
            this.lbFilesSum.Text = "文件总数：";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(568, 80);
            this.Controls.Add(this.lbFilesSum);
            this.Controls.Add(this.lbCurrentFile);
            this.Controls.Add(this.lbDownSpeed);
            this.Controls.Add(this.btnSure);
            this.Controls.Add(this.pbUpdate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Main";
            this.Text = "更新";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar pbUpdate;
        private System.Windows.Forms.Button btnSure;
        private System.Windows.Forms.Label lbDownSpeed;
        private System.Windows.Forms.Label lbCurrentFile;
        private System.Windows.Forms.Label lbFilesSum;

    }
}

