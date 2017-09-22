using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Gui.CompletionWindow;
using ICSharpCode.TextEditor.Document;
using ICSharpCode.TextEditor.Actions;

namespace HaoCodeBuilder
{
    public partial class Form_Code_Area : Form_Base
    {
        public Form_Code_Area(string text, string title)
        {
            InitializeComponent();
            this.Text = title;
            this.textEditorControl1.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("C#");
            this.textEditorControl1.Encoding = System.Text.Encoding.Default;
            this.textEditorControl1.Text = text;
        }

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainForm.SaveFile(this.ParentForm as MainForm);
        }

        private void 全部保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainForm.SaveAllFile(this.ParentForm as MainForm);
        }

        private void 全部关闭CToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var mainForm = this.ParentForm as MainForm;

            mainForm.dockPanel1.Contents.ToList().ForEach((content) =>
            {
                var tempItem = content.DockHandler.Form as Form_Code_Area;
                if (tempItem != null && tempItem != this)
                {
                    tempItem.Close();
                }
            });
        }
    }
}
