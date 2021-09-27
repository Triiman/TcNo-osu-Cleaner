
namespace osu_cleaner
{
    partial class ManageReplays
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManageReplays));
            this.listAccounts = new System.Windows.Forms.ListBox();
            this.btnDeleteReplays = new DarkUI.Controls.DarkButton();
            this.lblStatus = new DarkUI.Controls.DarkLabel();
            this.btnScan = new DarkUI.Controls.DarkButton();
            this.pbScan = new osu_cleaner.DarkProgressBar();
            this.lblNumFiles = new DarkUI.Controls.DarkLabel();
            this.SuspendLayout();
            // 
            // listAccounts
            // 
            this.listAccounts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(33)))), ((int)(((byte)(44)))));
            this.listAccounts.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(242)))));
            this.listAccounts.FormattingEnabled = true;
            this.listAccounts.Location = new System.Drawing.Point(12, 33);
            this.listAccounts.Name = "listAccounts";
            this.listAccounts.Size = new System.Drawing.Size(280, 95);
            this.listAccounts.TabIndex = 8;
            this.listAccounts.Visible = false;
            // 
            // btnDeleteReplays
            // 
            this.btnDeleteReplays.Enabled = false;
            this.btnDeleteReplays.Location = new System.Drawing.Point(126, 134);
            this.btnDeleteReplays.Name = "btnDeleteReplays";
            this.btnDeleteReplays.Padding = new System.Windows.Forms.Padding(5);
            this.btnDeleteReplays.Size = new System.Drawing.Size(166, 23);
            this.btnDeleteReplays.TabIndex = 7;
            this.btnDeleteReplays.Text = "Delete replays for this account";
            this.btnDeleteReplays.Visible = false;
            this.btnDeleteReplays.Click += new System.EventHandler(this.btnDeleteReplays_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lblStatus.Location = new System.Drawing.Point(12, 9);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(188, 13);
            this.lblStatus.TabIndex = 6;
            this.lblStatus.Text = "1. Scan to collect usernames from files";
            // 
            // btnScan
            // 
            this.btnScan.Location = new System.Drawing.Point(12, 33);
            this.btnScan.Name = "btnScan";
            this.btnScan.Padding = new System.Windows.Forms.Padding(5);
            this.btnScan.Size = new System.Drawing.Size(125, 23);
            this.btnScan.TabIndex = 10;
            this.btnScan.Text = "Scan Replay files";
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // pbScan
            // 
            this.pbScan.Location = new System.Drawing.Point(12, 33);
            this.pbScan.Name = "pbScan";
            this.pbScan.Size = new System.Drawing.Size(635, 23);
            this.pbScan.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pbScan.TabIndex = 23;
            this.pbScan.Visible = false;
            // 
            // lblNumFiles
            // 
            this.lblNumFiles.AutoSize = true;
            this.lblNumFiles.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lblNumFiles.Location = new System.Drawing.Point(12, 59);
            this.lblNumFiles.Name = "lblNumFiles";
            this.lblNumFiles.Size = new System.Drawing.Size(30, 13);
            this.lblNumFiles.TabIndex = 24;
            this.lblNumFiles.Text = "0 / 0";
            this.lblNumFiles.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ManageReplays
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(42)))), ((int)(((byte)(54)))));
            this.ClientSize = new System.Drawing.Size(659, 175);
            this.Controls.Add(this.lblNumFiles);
            this.Controls.Add(this.pbScan);
            this.Controls.Add(this.btnScan);
            this.Controls.Add(this.listAccounts);
            this.Controls.Add(this.btnDeleteReplays);
            this.Controls.Add(this.lblStatus);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ManageReplays";
            this.Text = "ManageReplays";
            this.Load += new System.EventHandler(this.ManageReplays_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListBox listAccounts;
        private DarkUI.Controls.DarkButton btnDeleteReplays;
        private DarkUI.Controls.DarkLabel lblStatus;
        private DarkUI.Controls.DarkButton btnScan;
        private DarkProgressBar pbScan;
        private DarkUI.Controls.DarkLabel lblNumFiles;
    }
}