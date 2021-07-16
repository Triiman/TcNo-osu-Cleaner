
namespace osu_cleaner.Properties
{
	partial class SelectUser
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectUser));
			this.lblUsername = new DarkUI.Controls.DarkLabel();
			this.btnUsernameOK = new DarkUI.Controls.DarkButton();
			this.listAccounts = new System.Windows.Forms.ListBox();
			this.SuspendLayout();
			// 
			// lblUsername
			// 
			this.lblUsername.AutoSize = true;
			this.lblUsername.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.lblUsername.Location = new System.Drawing.Point(12, 9);
			this.lblUsername.Name = "lblUsername";
			this.lblUsername.Size = new System.Drawing.Size(168, 13);
			this.lblUsername.TabIndex = 0;
			this.lblUsername.Text = "Please select your osu! username:";
			// 
			// btnUsernameOK
			// 
			this.btnUsernameOK.Location = new System.Drawing.Point(217, 126);
			this.btnUsernameOK.Name = "btnUsernameOK";
			this.btnUsernameOK.Padding = new System.Windows.Forms.Padding(5);
			this.btnUsernameOK.Size = new System.Drawing.Size(75, 23);
			this.btnUsernameOK.TabIndex = 2;
			this.btnUsernameOK.Text = "OK";
			this.btnUsernameOK.Click += new System.EventHandler(this.btnUsernameOK_Click);
			// 
			// listAccounts
			// 
			this.listAccounts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(33)))), ((int)(((byte)(44)))));
			this.listAccounts.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(242)))));
			this.listAccounts.FormattingEnabled = true;
			this.listAccounts.Location = new System.Drawing.Point(12, 25);
			this.listAccounts.Name = "listAccounts";
			this.listAccounts.Size = new System.Drawing.Size(280, 95);
			this.listAccounts.TabIndex = 3;
			// 
			// SelectUser
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(42)))), ((int)(((byte)(54)))));
			this.ClientSize = new System.Drawing.Size(304, 161);
			this.Controls.Add(this.listAccounts);
			this.Controls.Add(this.btnUsernameOK);
			this.Controls.Add(this.lblUsername);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(320, 200);
			this.Name = "SelectUser";
			this.Text = "Select osu! account";
			this.Load += new System.EventHandler(this.SelectUser_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DarkUI.Controls.DarkLabel lblUsername;
		private DarkUI.Controls.DarkButton btnUsernameOK;
		private System.Windows.Forms.ListBox listAccounts;
	}
}