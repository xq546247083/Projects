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
        private string contentOne = "";
        private string contentTwo = "";

        public Form_Code_Area(string text, string title)
        {
            InitializeComponent();
            this.Text = title;
            this.textEditorControl1.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("C#");
            this.textEditorControl1.Encoding = System.Text.Encoding.Default;
            this.textEditorControl1.Text = text;
        }

        public Form_Code_Area(string text, string textTwo, string title)
        {
            InitializeComponent();
            this.Text = title;
            this.textEditorControl1.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("C#");
            this.textEditorControl1.Encoding = System.Text.Encoding.Default;
            contentOne = text;
            contentTwo = textTwo;

            this.textEditorControl1.Text = contentOne + Environment.NewLine + Environment.NewLine + Environment.NewLine + contentTwo;
        }

        public void SetText(String txtOne, String txtTwo)
        {
            contentOne += txtOne;
            contentTwo += txtTwo;
            var contentOneList = contentOne.Split(new String[] { Environment.NewLine }, StringSplitOptions.None).Distinct();
            var contentTwoList = contentOne.Split(new String[] { Environment.NewLine }, StringSplitOptions.None).Distinct();
            var resultOne = String.Empty;
            var resultTwo = String.Empty;

            foreach (var item in contentOneList)
            {
                resultOne += item + Environment.NewLine;
            }
            foreach (var item in contentTwoList)
            {
                resultTwo += item + Environment.NewLine;
            }

            this.textEditorControl1.Text = resultOne + Environment.NewLine + Environment.NewLine + Environment.NewLine + resultTwo;
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
