using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace CodeGen
{
	/// <summary>
	/// PersonalInfo ��ժҪ˵����
	/// </summary>
	public class PersonalInfo:System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox userName;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox structureName;
		private System.Windows.Forms.TextBox DALName;
		private System.Windows.Forms.TextBox BLLName;
		private System.Windows.Forms.Button save;
		private System.Windows.Forms.Button cancel;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox VIEWName;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox SQLName;
        private TextBox eMail;
        private Label label6;
        private TextBox corpName;
        private Label label1;

        /// <summary>
        /// ����������������
        /// </summary>
        private System.ComponentModel.Container components = null;

		public PersonalInfo()
		{
			//
			// Windows ���������֧���������
			//
			InitializeComponent();

			//
			// TODO:�� InitializeComponent ���ú�����κι��캯������
			//
		}

		/// <summary>
		/// ������������ʹ�õ���Դ��
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows ������������ɵĴ���
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PersonalInfo));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.userName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.structureName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.VIEWName = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.SQLName = new System.Windows.Forms.TextBox();
            this.BLLName = new System.Windows.Forms.TextBox();
            this.DALName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.save = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.eMail = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.corpName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.userName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Location = new System.Drawing.Point(8, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(234, 52);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ʹ������Ϣ";
            // 
            // userName
            // 
            this.userName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.userName.Location = new System.Drawing.Point(57, 20);
            this.userName.Name = "userName";
            this.userName.Size = new System.Drawing.Size(160, 21);
            this.userName.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 23);
            this.label2.TabIndex = 3;
            this.label2.Text = "���ƣ�";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.structureName);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.VIEWName);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.SQLName);
            this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox2.Location = new System.Drawing.Point(8, 168);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(280, 108);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "�����ռ���Ϣ";
            this.groupBox2.Visible = false;
            // 
            // structureName
            // 
            this.structureName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.structureName.Location = new System.Drawing.Point(96, 16);
            this.structureName.Name = "structureName";
            this.structureName.Size = new System.Drawing.Size(176, 21);
            this.structureName.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(10, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 23);
            this.label3.TabIndex = 0;
            this.label3.Text = "�ṹ�㣺";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(8, 80);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 23);
            this.label7.TabIndex = 2;
            this.label7.Text = "���ֲ㣺";
            // 
            // VIEWName
            // 
            this.VIEWName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.VIEWName.Location = new System.Drawing.Point(96, 80);
            this.VIEWName.Name = "VIEWName";
            this.VIEWName.Size = new System.Drawing.Size(176, 21);
            this.VIEWName.TabIndex = 5;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(8, 48);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 23);
            this.label8.TabIndex = 2;
            this.label8.Text = "SQL�㣺";
            // 
            // SQLName
            // 
            this.SQLName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SQLName.Location = new System.Drawing.Point(96, 48);
            this.SQLName.Name = "SQLName";
            this.SQLName.Size = new System.Drawing.Size(176, 21);
            this.SQLName.TabIndex = 5;
            // 
            // BLLName
            // 
            this.BLLName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BLLName.Location = new System.Drawing.Point(96, 303);
            this.BLLName.Name = "BLLName";
            this.BLLName.Size = new System.Drawing.Size(176, 21);
            this.BLLName.TabIndex = 5;
            this.BLLName.Visible = false;
            // 
            // DALName
            // 
            this.DALName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DALName.Location = new System.Drawing.Point(96, 279);
            this.DALName.Name = "DALName";
            this.DALName.Size = new System.Drawing.Size(176, 21);
            this.DALName.TabIndex = 4;
            this.DALName.Visible = false;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(16, 303);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 23);
            this.label5.TabIndex = 2;
            this.label5.Text = "�߼��㣺";
            this.label5.Visible = false;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(16, 279);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 23);
            this.label4.TabIndex = 1;
            this.label4.Text = "���ݷ��ʲ㣺";
            this.label4.Visible = false;
            // 
            // save
            // 
            this.save.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.save.Location = new System.Drawing.Point(81, 70);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(75, 25);
            this.save.TabIndex = 2;
            this.save.Text = "����(&S)";
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // cancel
            // 
            this.cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancel.Location = new System.Drawing.Point(162, 70);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(75, 25);
            this.cancel.TabIndex = 3;
            this.cancel.Text = "ȡ��(&C)";
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // eMail
            // 
            this.eMail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.eMail.Location = new System.Drawing.Point(8, 77);
            this.eMail.Name = "eMail";
            this.eMail.Size = new System.Drawing.Size(160, 21);
            this.eMail.TabIndex = 11;
            this.eMail.Visible = false;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(11, 79);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 19);
            this.label6.TabIndex = 10;
            this.label6.Text = "EMail��";
            this.label6.Visible = false;
            // 
            // corpName
            // 
            this.corpName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.corpName.Location = new System.Drawing.Point(8, 63);
            this.corpName.Name = "corpName";
            this.corpName.Size = new System.Drawing.Size(160, 21);
            this.corpName.TabIndex = 9;
            this.corpName.Visible = false;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(11, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 19);
            this.label1.TabIndex = 8;
            this.label1.Text = "��˾���ƣ�";
            this.label1.Visible = false;
            // 
            // PersonalInfo
            // 
            this.AcceptButton = this.save;
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(256, 106);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.save);
            this.Controls.Add(this.eMail);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.corpName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.DALName);
            this.Controls.Add(this.BLLName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "PersonalInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ʹ������Ϣ";
            this.Load += new System.EventHandler(this.PersonalInfo_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion


		private void PersonalInfo_Load(object sender, System.EventArgs e)
		{
			this.FillForm();
		}

		private void cancel_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		/// <summary>
		/// ��ע����ȡ��Ϣ ���細����
		/// </summary>
		private void FillForm()
		{
			this.corpName.Text= RegisterAccess.ReadKey("corpName");
			this.userName.Text= RegisterAccess.ReadKey("userName");
			this.eMail.Text= RegisterAccess.ReadKey("email");
		}

		private void save_Click(object sender, System.EventArgs e)
		{
			RegisterAccess.WriteKey("corpName",this.corpName.Text);
			RegisterAccess.WriteKey("structureName",this.structureName.Text);
			RegisterAccess.WriteKey("userName",this.userName.Text);
			RegisterAccess.WriteKey("email",this.eMail.Text);

			this.Close();
		}
	}
}
