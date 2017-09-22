namespace HaoCodeBuilder
{
    partial class Form_Code_Area
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
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.全部保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.全部关闭CToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textEditorControl1 = new ICSharpCode.TextEditor.TextEditorControl();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.保存ToolStripMenuItem,
            this.全部保存ToolStripMenuItem,
            this.全部关闭CToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(216, 70);
            // 
            // 保存ToolStripMenuItem
            // 
            this.保存ToolStripMenuItem.Name = "保存ToolStripMenuItem";
            this.保存ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.保存ToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.保存ToolStripMenuItem.Text = "保存(&S)";
            this.保存ToolStripMenuItem.Click += new System.EventHandler(this.保存ToolStripMenuItem_Click);
            // 
            // 全部保存ToolStripMenuItem
            // 
            this.全部保存ToolStripMenuItem.Name = "全部保存ToolStripMenuItem";
            this.全部保存ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.S)));
            this.全部保存ToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.全部保存ToolStripMenuItem.Text = "全部保存(&AS)";
            this.全部保存ToolStripMenuItem.Click += new System.EventHandler(this.全部保存ToolStripMenuItem_Click);
            // 
            // 全部关闭CToolStripMenuItem
            // 
            this.全部关闭CToolStripMenuItem.Name = "全部关闭CToolStripMenuItem";
            this.全部关闭CToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.全部关闭CToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.全部关闭CToolStripMenuItem.Text = "关闭其他(&C)";
            this.全部关闭CToolStripMenuItem.Click += new System.EventHandler(this.全部关闭CToolStripMenuItem_Click);
            // 
            // textEditorControl1
            // 
            this.textEditorControl1.ContextMenuStrip = this.contextMenuStrip1;
            this.textEditorControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textEditorControl1.IsReadOnly = false;
            this.textEditorControl1.Location = new System.Drawing.Point(0, 0);
            this.textEditorControl1.Name = "textEditorControl1";
            this.textEditorControl1.Size = new System.Drawing.Size(1079, 641);
            this.textEditorControl1.TabIndex = 0;
            // 
            // Form_Code_Area
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1079, 641);
            this.Controls.Add(this.textEditorControl1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.KeyPreview = true;
            this.Name = "Form_Code_Area";
            this.ShowInTaskbar = false;
            this.Text = "Form_Code_Area";
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public ICSharpCode.TextEditor.TextEditorControl textEditorControl1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 保存ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 全部保存ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 全部关闭CToolStripMenuItem;
    }
}