namespace osu_cleaner
{
    partial class MainApp
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
            this.DeletePermanentlyCheckbox = new System.Windows.Forms.CheckBox();
            this.selectAllButton = new System.Windows.Forms.Button();
            this.deselectAllButton = new System.Windows.Forms.Button();
            this.filesSizeLabel = new System.Windows.Forms.Label();
            this.forRemovalSizeLabel = new System.Windows.Forms.Label();
            this.moveCheckBox = new System.Windows.Forms.CheckBox();
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.tbLFooter = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.deleteButton = new System.Windows.Forms.Button();
            this.FindProgressBar = new System.Windows.Forms.ProgressBar();
            this.elementList = new System.Windows.Forms.CheckedListBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.allUncommon = new System.Windows.Forms.CheckBox();
            this.bloatExtraDeleteBox = new System.Windows.Forms.CheckBox();
            this.hitSoundsDeleteCheckbox = new System.Windows.Forms.CheckBox();
            this.backgroundDeleteCheckbox = new System.Windows.Forms.CheckBox();
            this.sbDeleteCheckbox = new System.Windows.Forms.CheckBox();
            this.skinDeleteCheckbox = new System.Windows.Forms.CheckBox();
            this.videoDeleteCheckbox = new System.Windows.Forms.CheckBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.findButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.directoryPath = new System.Windows.Forms.TextBox();
            this.directorySelectButton = new System.Windows.Forms.Button();
            this.directoryLabel = new System.Windows.Forms.Label();
            this.tbLDirectory = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.stripInfo = new System.Windows.Forms.StatusStrip();
            this.lblHenntix = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblTechNobo = new System.Windows.Forms.ToolStripStatusLabel();
            this.tbLFooter.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.tbLDirectory.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.stripInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // DeletePermanentlyCheckbox
            // 
            this.DeletePermanentlyCheckbox.Checked = true;
            this.DeletePermanentlyCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.DeletePermanentlyCheckbox.Location = new System.Drawing.Point(3, 29);
            this.DeletePermanentlyCheckbox.Name = "DeletePermanentlyCheckbox";
            this.DeletePermanentlyCheckbox.Size = new System.Drawing.Size(281, 24);
            this.DeletePermanentlyCheckbox.TabIndex = 0;
            this.DeletePermanentlyCheckbox.Text = "Delete permanently instead of moving to Recycle Bin";
            this.DeletePermanentlyCheckbox.UseVisualStyleBackColor = true;
            this.DeletePermanentlyCheckbox.CheckedChanged += new System.EventHandler(this.DeletePermanentlyCheckbox_CheckedChanged);
            // 
            // selectAllButton
            // 
            this.selectAllButton.Location = new System.Drawing.Point(3, 3);
            this.selectAllButton.Name = "selectAllButton";
            this.selectAllButton.Size = new System.Drawing.Size(75, 23);
            this.selectAllButton.TabIndex = 10;
            this.selectAllButton.Text = "Select all";
            this.selectAllButton.UseVisualStyleBackColor = true;
            this.selectAllButton.Click += new System.EventHandler(this.selectAllButton_Click);
            // 
            // deselectAllButton
            // 
            this.deselectAllButton.Location = new System.Drawing.Point(84, 3);
            this.deselectAllButton.Name = "deselectAllButton";
            this.deselectAllButton.Size = new System.Drawing.Size(75, 23);
            this.deselectAllButton.TabIndex = 11;
            this.deselectAllButton.Text = "Unselect all";
            this.deselectAllButton.UseVisualStyleBackColor = true;
            this.deselectAllButton.Click += new System.EventHandler(this.deselectAllButton_Click);
            // 
            // filesSizeLabel
            // 
            this.filesSizeLabel.AutoSize = true;
            this.filesSizeLabel.Location = new System.Drawing.Point(3, 0);
            this.filesSizeLabel.Name = "filesSizeLabel";
            this.filesSizeLabel.Size = new System.Drawing.Size(68, 13);
            this.filesSizeLabel.TabIndex = 12;
            this.filesSizeLabel.Text = "Found: 0 MB";
            // 
            // forRemovalSizeLabel
            // 
            this.forRemovalSizeLabel.AutoSize = true;
            this.forRemovalSizeLabel.Location = new System.Drawing.Point(3, 13);
            this.forRemovalSizeLabel.Name = "forRemovalSizeLabel";
            this.forRemovalSizeLabel.Size = new System.Drawing.Size(132, 13);
            this.forRemovalSizeLabel.TabIndex = 13;
            this.forRemovalSizeLabel.Text = "Selected for removal: 0MB";
            // 
            // moveCheckBox
            // 
            this.moveCheckBox.AutoSize = true;
            this.moveCheckBox.Location = new System.Drawing.Point(3, 54);
            this.moveCheckBox.Name = "moveCheckBox";
            this.moveCheckBox.Size = new System.Drawing.Size(206, 17);
            this.moveCheckBox.TabIndex = 15;
            this.moveCheckBox.Text = "Move to \'Cleaned\' instead of removing";
            this.moveCheckBox.UseVisualStyleBackColor = true;
            this.moveCheckBox.CheckedChanged += new System.EventHandler(this.moveCheckBox_CheckedChanged);
            // 
            // tbLFooter
            // 
            this.tbLFooter.ColumnCount = 2;
            this.tbLFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbLFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 172F));
            this.tbLFooter.Controls.Add(this.panel2, 0, 0);
            this.tbLFooter.Controls.Add(this.panel1, 1, 0);
            this.tbLFooter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbLFooter.Location = new System.Drawing.Point(3, 651);
            this.tbLFooter.Name = "tbLFooter";
            this.tbLFooter.RowCount = 1;
            this.tbLFooter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbLFooter.Size = new System.Drawing.Size(956, 89);
            this.tbLFooter.TabIndex = 21;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.filesSizeLabel);
            this.panel2.Controls.Add(this.forRemovalSizeLabel);
            this.panel2.Controls.Add(this.DeletePermanentlyCheckbox);
            this.panel2.Controls.Add(this.moveCheckBox);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(778, 83);
            this.panel2.TabIndex = 22;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.selectAllButton);
            this.panel1.Controls.Add(this.deselectAllButton);
            this.panel1.Controls.Add(this.deleteButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(784, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(172, 89);
            this.panel1.TabIndex = 22;
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(3, 32);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(75, 23);
            this.deleteButton.TabIndex = 2;
            this.deleteButton.Text = "Delete";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // FindProgressBar
            // 
            this.FindProgressBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FindProgressBar.Location = new System.Drawing.Point(3, 622);
            this.FindProgressBar.Name = "FindProgressBar";
            this.FindProgressBar.Size = new System.Drawing.Size(956, 23);
            this.FindProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.FindProgressBar.TabIndex = 22;
            // 
            // elementList
            // 
            this.elementList.CheckOnClick = true;
            this.elementList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.elementList.FormattingEnabled = true;
            this.elementList.Location = new System.Drawing.Point(3, 209);
            this.elementList.Name = "elementList";
            this.elementList.Size = new System.Drawing.Size(956, 407);
            this.elementList.TabIndex = 25;
            // 
            // panel3
            // 
            this.panel3.AutoSize = true;
            this.panel3.Controls.Add(this.allUncommon);
            this.panel3.Controls.Add(this.bloatExtraDeleteBox);
            this.panel3.Controls.Add(this.hitSoundsDeleteCheckbox);
            this.panel3.Controls.Add(this.backgroundDeleteCheckbox);
            this.panel3.Controls.Add(this.sbDeleteCheckbox);
            this.panel3.Controls.Add(this.skinDeleteCheckbox);
            this.panel3.Controls.Add(this.videoDeleteCheckbox);
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(254, 159);
            this.panel3.TabIndex = 25;
            // 
            // allUncommon
            // 
            this.allUncommon.AutoSize = true;
            this.allUncommon.Location = new System.Drawing.Point(6, 141);
            this.allUncommon.Name = "allUncommon";
            this.allUncommon.Size = new System.Drawing.Size(245, 17);
            this.allUncommon.TabIndex = 27;
            this.allUncommon.Text = "All uncommon files (CHECK RESULTS FIRST)";
            this.allUncommon.UseVisualStyleBackColor = true;
            this.allUncommon.CheckedChanged += new System.EventHandler(this.allUncommon_CheckedChanged);
            // 
            // bloatExtraDeleteBox
            // 
            this.bloatExtraDeleteBox.AutoSize = true;
            this.bloatExtraDeleteBox.Location = new System.Drawing.Point(6, 118);
            this.bloatExtraDeleteBox.Name = "bloatExtraDeleteBox";
            this.bloatExtraDeleteBox.Size = new System.Drawing.Size(193, 17);
            this.bloatExtraDeleteBox.TabIndex = 26;
            this.bloatExtraDeleteBox.Text = "Delete thumbs.db && desktop.ini files";
            this.bloatExtraDeleteBox.UseVisualStyleBackColor = true;
            // 
            // hitSoundsDeleteCheckbox
            // 
            this.hitSoundsDeleteCheckbox.AutoSize = true;
            this.hitSoundsDeleteCheckbox.Location = new System.Drawing.Point(6, 95);
            this.hitSoundsDeleteCheckbox.Name = "hitSoundsDeleteCheckbox";
            this.hitSoundsDeleteCheckbox.Size = new System.Drawing.Size(105, 17);
            this.hitSoundsDeleteCheckbox.TabIndex = 25;
            this.hitSoundsDeleteCheckbox.Text = "Delete hitsounds";
            this.hitSoundsDeleteCheckbox.UseVisualStyleBackColor = true;
            // 
            // backgroundDeleteCheckbox
            // 
            this.backgroundDeleteCheckbox.AutoSize = true;
            this.backgroundDeleteCheckbox.Location = new System.Drawing.Point(6, 72);
            this.backgroundDeleteCheckbox.Name = "backgroundDeleteCheckbox";
            this.backgroundDeleteCheckbox.Size = new System.Drawing.Size(122, 17);
            this.backgroundDeleteCheckbox.TabIndex = 24;
            this.backgroundDeleteCheckbox.Text = "Delete backgrounds";
            this.backgroundDeleteCheckbox.UseVisualStyleBackColor = true;
            // 
            // sbDeleteCheckbox
            // 
            this.sbDeleteCheckbox.Location = new System.Drawing.Point(6, 49);
            this.sbDeleteCheckbox.Name = "sbDeleteCheckbox";
            this.sbDeleteCheckbox.Size = new System.Drawing.Size(157, 17);
            this.sbDeleteCheckbox.TabIndex = 21;
            this.sbDeleteCheckbox.Text = "Delete storyboard elements";
            this.sbDeleteCheckbox.UseVisualStyleBackColor = true;
            // 
            // skinDeleteCheckbox
            // 
            this.skinDeleteCheckbox.Location = new System.Drawing.Point(6, 26);
            this.skinDeleteCheckbox.Name = "skinDeleteCheckbox";
            this.skinDeleteCheckbox.Size = new System.Drawing.Size(212, 17);
            this.skinDeleteCheckbox.TabIndex = 22;
            this.skinDeleteCheckbox.Text = "Delete skin elements";
            this.skinDeleteCheckbox.UseVisualStyleBackColor = true;
            // 
            // videoDeleteCheckbox
            // 
            this.videoDeleteCheckbox.Location = new System.Drawing.Point(6, 3);
            this.videoDeleteCheckbox.Name = "videoDeleteCheckbox";
            this.videoDeleteCheckbox.Size = new System.Drawing.Size(133, 17);
            this.videoDeleteCheckbox.TabIndex = 23;
            this.videoDeleteCheckbox.Text = "Delete video";
            this.videoDeleteCheckbox.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.findButton);
            this.panel4.Controls.Add(this.cancelButton);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(794, 136);
            this.panel4.Margin = new System.Windows.Forms.Padding(0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(162, 29);
            this.panel4.TabIndex = 25;
            // 
            // findButton
            // 
            this.findButton.Location = new System.Drawing.Point(3, 3);
            this.findButton.Name = "findButton";
            this.findButton.Size = new System.Drawing.Size(75, 23);
            this.findButton.TabIndex = 1;
            this.findButton.Text = "Find";
            this.findButton.UseVisualStyleBackColor = true;
            this.findButton.Click += new System.EventHandler(this.findButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(84, 3);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 18;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Visible = false;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // directoryPath
            // 
            this.directoryPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.directoryPath.Location = new System.Drawing.Point(109, 3);
            this.directoryPath.Name = "directoryPath";
            this.directoryPath.Size = new System.Drawing.Size(763, 20);
            this.directoryPath.TabIndex = 29;
            // 
            // directorySelectButton
            // 
            this.directorySelectButton.Location = new System.Drawing.Point(878, 3);
            this.directorySelectButton.Name = "directorySelectButton";
            this.directorySelectButton.Size = new System.Drawing.Size(75, 23);
            this.directorySelectButton.TabIndex = 28;
            this.directorySelectButton.Text = "Browse";
            this.directorySelectButton.UseVisualStyleBackColor = true;
            // 
            // directoryLabel
            // 
            this.directoryLabel.Location = new System.Drawing.Point(3, 0);
            this.directoryLabel.Name = "directoryLabel";
            this.directoryLabel.Size = new System.Drawing.Size(100, 23);
            this.directoryLabel.TabIndex = 30;
            this.directoryLabel.Text = "osu! directory path:";
            // 
            // tbLDirectory
            // 
            this.tbLDirectory.ColumnCount = 3;
            this.tbLDirectory.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 106F));
            this.tbLDirectory.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbLDirectory.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 81F));
            this.tbLDirectory.Controls.Add(this.directoryLabel, 0, 0);
            this.tbLDirectory.Controls.Add(this.directorySelectButton, 2, 0);
            this.tbLDirectory.Controls.Add(this.directoryPath, 1, 0);
            this.tbLDirectory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbLDirectory.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tbLDirectory.Location = new System.Drawing.Point(3, 3);
            this.tbLDirectory.Name = "tbLDirectory";
            this.tbLDirectory.RowCount = 1;
            this.tbLDirectory.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbLDirectory.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tbLDirectory.Size = new System.Drawing.Size(956, 29);
            this.tbLDirectory.TabIndex = 31;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbLDirectory, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbLFooter, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.FindProgressBar, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.elementList, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.stripInfo, 0, 5);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 171F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 95F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(962, 763);
            this.tableLayoutPanel1.TabIndex = 33;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 162F));
            this.tableLayoutPanel2.Controls.Add(this.panel4, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel3, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 38);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(956, 165);
            this.tableLayoutPanel2.TabIndex = 34;
            // 
            // stripInfo
            // 
            this.stripInfo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblHenntix,
            this.lblTechNobo});
            this.stripInfo.Location = new System.Drawing.Point(0, 743);
            this.stripInfo.Name = "stripInfo";
            this.stripInfo.Size = new System.Drawing.Size(962, 20);
            this.stripInfo.TabIndex = 35;
            this.stripInfo.Text = "statusStrip1";
            // 
            // lblHenntix
            // 
            this.lblHenntix.IsLink = true;
            this.lblHenntix.Name = "lblHenntix";
            this.lblHenntix.Size = new System.Drawing.Size(135, 15);
            this.lblHenntix.Text = "Original project: henntix";
            this.lblHenntix.Click += new System.EventHandler(this.lblHenntix_Click);
            // 
            // lblTechNobo
            // 
            this.lblTechNobo.IsLink = true;
            this.lblTechNobo.Name = "lblTechNobo";
            this.lblTechNobo.Size = new System.Drawing.Size(125, 15);
            this.lblTechNobo.Text = "Updated by TechNobo";
            this.lblTechNobo.Click += new System.EventHandler(this.lblTechNobo_Click);
            // 
            // MainApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(962, 763);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MinimumSize = new System.Drawing.Size(846, 442);
            this.Name = "MainApp";
            this.Text = "osu!Cleaner v2.0 (TechNobo)";
            this.Load += new System.EventHandler(this.MainApp_Load);
            this.tbLFooter.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.tbLDirectory.ResumeLayout(false);
            this.tbLDirectory.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.stripInfo.ResumeLayout(false);
            this.stripInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.CheckBox DeletePermanentlyCheckbox;
        private System.Windows.Forms.Button selectAllButton;
        private System.Windows.Forms.Button deselectAllButton;
        private System.Windows.Forms.Label filesSizeLabel;
        private System.Windows.Forms.Label forRemovalSizeLabel;
        private System.Windows.Forms.CheckBox moveCheckBox;
        private System.ComponentModel.BackgroundWorker bgWorker;
        private System.Windows.Forms.TableLayoutPanel tbLFooter;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.ProgressBar FindProgressBar;
        private System.Windows.Forms.CheckedListBox elementList;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.CheckBox allUncommon;
        private System.Windows.Forms.CheckBox bloatExtraDeleteBox;
        private System.Windows.Forms.CheckBox hitSoundsDeleteCheckbox;
        private System.Windows.Forms.CheckBox backgroundDeleteCheckbox;
        private System.Windows.Forms.CheckBox sbDeleteCheckbox;
        private System.Windows.Forms.CheckBox skinDeleteCheckbox;
        private System.Windows.Forms.CheckBox videoDeleteCheckbox;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button findButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.TextBox directoryPath;
        private System.Windows.Forms.Button directorySelectButton;
        private System.Windows.Forms.Label directoryLabel;
        private System.Windows.Forms.TableLayoutPanel tbLDirectory;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.StatusStrip stripInfo;
        private System.Windows.Forms.ToolStripStatusLabel lblHenntix;
        private System.Windows.Forms.ToolStripStatusLabel lblTechNobo;
    }
}

