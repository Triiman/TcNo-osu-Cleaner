using System.Drawing;
using System.Windows.Forms;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainApp));
            this.DeletePermanentlyCheckbox = new DarkUI.Controls.DarkCheckBox();
            this.selectAllButton = new DarkUI.Controls.DarkButton();
            this.deselectAllButton = new DarkUI.Controls.DarkButton();
            this.filesSizeLabel = new DarkUI.Controls.DarkLabel();
            this.forRemovalSizeLabel = new DarkUI.Controls.DarkLabel();
            this.moveCheckBox = new DarkUI.Controls.DarkCheckBox();
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.tbLFooter = new System.Windows.Forms.TableLayoutPanel();
            this.pnlBottomInfo = new System.Windows.Forms.Panel();
            this.pnlBottomButtons = new System.Windows.Forms.Panel();
            this.symlinkButton = new DarkUI.Controls.DarkButton();
            this.openMoved = new DarkUI.Controls.DarkButton();
            this.deleteButton = new DarkUI.Controls.DarkButton();
            this.pnlCheckboxes = new System.Windows.Forms.Panel();
            this.btnManageReplays = new DarkUI.Controls.DarkButton();
            this.btnReplaceMissing = new DarkUI.Controls.DarkButton();
            this.lblPrompt = new DarkUI.Controls.DarkLabel();
            this.allUncommon = new DarkUI.Controls.DarkCheckBox();
            this.bloatExtraDeleteBox = new DarkUI.Controls.DarkCheckBox();
            this.hitSoundsDeleteCheckbox = new DarkUI.Controls.DarkCheckBox();
            this.backgroundDeleteCheckbox = new DarkUI.Controls.DarkCheckBox();
            this.sbDeleteCheckbox = new DarkUI.Controls.DarkCheckBox();
            this.skinDeleteCheckbox = new DarkUI.Controls.DarkCheckBox();
            this.videoDeleteCheckbox = new DarkUI.Controls.DarkCheckBox();
            this.directoryPath = new System.Windows.Forms.TextBox();
            this.directorySelectButton = new DarkUI.Controls.DarkButton();
            this.directoryLabel = new DarkUI.Controls.DarkLabel();
            this.tbLDirectory = new System.Windows.Forms.TableLayoutPanel();
            this.pnlDirBar = new System.Windows.Forms.Panel();
            this.tblEntireForm = new System.Windows.Forms.TableLayoutPanel();
            this.progressBarBackground = new System.Windows.Forms.Panel();
            this.FindProgressBar = new osu_cleaner.DarkProgressBar();
            this.tblTopSection = new System.Windows.Forms.TableLayoutPanel();
            this.tblLogoButtons = new System.Windows.Forms.TableLayoutPanel();
            this.pnlFindCancel = new System.Windows.Forms.Panel();
            this.findButton = new DarkUI.Controls.DarkButton();
            this.cancelButton = new DarkUI.Controls.DarkButton();
            this.logoBox = new System.Windows.Forms.PictureBox();
            this.elementList = new osu_cleaner.DarkCheckedListBox();
            this.stripInfo = new System.Windows.Forms.StatusStrip();
            this.lblCurrentAccount = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblHenntix = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblTechNobo = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblTCNOWeb = new System.Windows.Forms.ToolStripStatusLabel();
            this.tbLFooter.SuspendLayout();
            this.pnlBottomInfo.SuspendLayout();
            this.pnlBottomButtons.SuspendLayout();
            this.pnlCheckboxes.SuspendLayout();
            this.tbLDirectory.SuspendLayout();
            this.pnlDirBar.SuspendLayout();
            this.tblEntireForm.SuspendLayout();
            this.progressBarBackground.SuspendLayout();
            this.tblTopSection.SuspendLayout();
            this.tblLogoButtons.SuspendLayout();
            this.pnlFindCancel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoBox)).BeginInit();
            this.stripInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // DeletePermanentlyCheckbox
            // 
            this.DeletePermanentlyCheckbox.Checked = true;
            this.DeletePermanentlyCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.DeletePermanentlyCheckbox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.DeletePermanentlyCheckbox.Location = new System.Drawing.Point(0, 36);
            this.DeletePermanentlyCheckbox.Name = "DeletePermanentlyCheckbox";
            this.DeletePermanentlyCheckbox.Size = new System.Drawing.Size(646, 24);
            this.DeletePermanentlyCheckbox.TabIndex = 0;
            this.DeletePermanentlyCheckbox.Text = "Delete permanently instead of moving to Recycle Bin";
            this.DeletePermanentlyCheckbox.CheckedChanged += new System.EventHandler(this.DeletePermanentlyCheckbox_CheckedChanged);
            // 
            // selectAllButton
            // 
            this.selectAllButton.Location = new System.Drawing.Point(3, 3);
            this.selectAllButton.Name = "selectAllButton";
            this.selectAllButton.Padding = new System.Windows.Forms.Padding(5);
            this.selectAllButton.Size = new System.Drawing.Size(75, 23);
            this.selectAllButton.TabIndex = 10;
            this.selectAllButton.Text = "Select all";
            this.selectAllButton.Click += new System.EventHandler(this.SelectAllButton_Click);
            // 
            // deselectAllButton
            // 
            this.deselectAllButton.Location = new System.Drawing.Point(84, 3);
            this.deselectAllButton.Name = "deselectAllButton";
            this.deselectAllButton.Padding = new System.Windows.Forms.Padding(5);
            this.deselectAllButton.Size = new System.Drawing.Size(75, 23);
            this.deselectAllButton.TabIndex = 11;
            this.deselectAllButton.Text = "Unselect all";
            this.deselectAllButton.Click += new System.EventHandler(this.DeselectAllButton_Click);
            // 
            // filesSizeLabel
            // 
            this.filesSizeLabel.AutoSize = true;
            this.filesSizeLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.filesSizeLabel.Location = new System.Drawing.Point(3, 0);
            this.filesSizeLabel.Name = "filesSizeLabel";
            this.filesSizeLabel.Size = new System.Drawing.Size(68, 13);
            this.filesSizeLabel.TabIndex = 12;
            this.filesSizeLabel.Text = "Found: 0 MB";
            // 
            // forRemovalSizeLabel
            // 
            this.forRemovalSizeLabel.AutoSize = true;
            this.forRemovalSizeLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.forRemovalSizeLabel.Location = new System.Drawing.Point(3, 19);
            this.forRemovalSizeLabel.Name = "forRemovalSizeLabel";
            this.forRemovalSizeLabel.Size = new System.Drawing.Size(132, 13);
            this.forRemovalSizeLabel.TabIndex = 13;
            this.forRemovalSizeLabel.Text = "Selected for removal: 0MB";
            // 
            // moveCheckBox
            // 
            this.moveCheckBox.AutoSize = true;
            this.moveCheckBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.moveCheckBox.Location = new System.Drawing.Point(0, 60);
            this.moveCheckBox.Name = "moveCheckBox";
            this.moveCheckBox.Padding = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.moveCheckBox.Size = new System.Drawing.Size(646, 23);
            this.moveCheckBox.TabIndex = 15;
            this.moveCheckBox.Text = "Move to \'Cleaned\' instead of removing";
            this.moveCheckBox.CheckedChanged += new System.EventHandler(this.MoveCheckBox_CheckedChanged);
            // 
            // tbLFooter
            // 
            this.tbLFooter.ColumnCount = 2;
            this.tbLFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbLFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 172F));
            this.tbLFooter.Controls.Add(this.pnlBottomInfo, 0, 0);
            this.tbLFooter.Controls.Add(this.pnlBottomButtons, 1, 0);
            this.tbLFooter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbLFooter.Location = new System.Drawing.Point(3, 642);
            this.tbLFooter.Name = "tbLFooter";
            this.tbLFooter.RowCount = 1;
            this.tbLFooter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbLFooter.Size = new System.Drawing.Size(824, 89);
            this.tbLFooter.TabIndex = 21;
            // 
            // pnlBottomInfo
            // 
            this.pnlBottomInfo.Controls.Add(this.filesSizeLabel);
            this.pnlBottomInfo.Controls.Add(this.forRemovalSizeLabel);
            this.pnlBottomInfo.Controls.Add(this.DeletePermanentlyCheckbox);
            this.pnlBottomInfo.Controls.Add(this.moveCheckBox);
            this.pnlBottomInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBottomInfo.Location = new System.Drawing.Point(3, 3);
            this.pnlBottomInfo.Name = "pnlBottomInfo";
            this.pnlBottomInfo.Size = new System.Drawing.Size(646, 83);
            this.pnlBottomInfo.TabIndex = 22;
            // 
            // pnlBottomButtons
            // 
            this.pnlBottomButtons.Controls.Add(this.symlinkButton);
            this.pnlBottomButtons.Controls.Add(this.openMoved);
            this.pnlBottomButtons.Controls.Add(this.selectAllButton);
            this.pnlBottomButtons.Controls.Add(this.deselectAllButton);
            this.pnlBottomButtons.Controls.Add(this.deleteButton);
            this.pnlBottomButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBottomButtons.Location = new System.Drawing.Point(652, 0);
            this.pnlBottomButtons.Margin = new System.Windows.Forms.Padding(0);
            this.pnlBottomButtons.Name = "pnlBottomButtons";
            this.pnlBottomButtons.Size = new System.Drawing.Size(172, 89);
            this.pnlBottomButtons.TabIndex = 22;
            // 
            // symlinkButton
            // 
            this.symlinkButton.Location = new System.Drawing.Point(3, 61);
            this.symlinkButton.Name = "symlinkButton";
            this.symlinkButton.Padding = new System.Windows.Forms.Padding(5);
            this.symlinkButton.Size = new System.Drawing.Size(156, 23);
            this.symlinkButton.TabIndex = 13;
            this.symlinkButton.Text = "Move song files (Symlink)";
            this.symlinkButton.Click += new System.EventHandler(this.SymlinkButton_Click);
            // 
            // openMoved
            // 
            this.openMoved.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.openMoved.ImagePadding = 0;
            this.openMoved.Location = new System.Drawing.Point(84, 32);
            this.openMoved.Name = "openMoved";
            this.openMoved.Padding = new System.Windows.Forms.Padding(5);
            this.openMoved.Size = new System.Drawing.Size(75, 23);
            this.openMoved.TabIndex = 12;
            this.openMoved.Text = "See Files";
            this.openMoved.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.openMoved.Visible = false;
            this.openMoved.Click += new System.EventHandler(this.OpenMoved_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(3, 32);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Padding = new System.Windows.Forms.Padding(5);
            this.deleteButton.Size = new System.Drawing.Size(75, 23);
            this.deleteButton.TabIndex = 2;
            this.deleteButton.Text = "Delete";
            this.deleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // pnlCheckboxes
            // 
            this.pnlCheckboxes.Controls.Add(this.btnManageReplays);
            this.pnlCheckboxes.Controls.Add(this.btnReplaceMissing);
            this.pnlCheckboxes.Controls.Add(this.lblPrompt);
            this.pnlCheckboxes.Controls.Add(this.allUncommon);
            this.pnlCheckboxes.Controls.Add(this.bloatExtraDeleteBox);
            this.pnlCheckboxes.Controls.Add(this.hitSoundsDeleteCheckbox);
            this.pnlCheckboxes.Controls.Add(this.backgroundDeleteCheckbox);
            this.pnlCheckboxes.Controls.Add(this.sbDeleteCheckbox);
            this.pnlCheckboxes.Controls.Add(this.skinDeleteCheckbox);
            this.pnlCheckboxes.Controls.Add(this.videoDeleteCheckbox);
            this.pnlCheckboxes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCheckboxes.ForeColor = System.Drawing.Color.Purple;
            this.pnlCheckboxes.Location = new System.Drawing.Point(3, 3);
            this.pnlCheckboxes.Name = "pnlCheckboxes";
            this.pnlCheckboxes.Size = new System.Drawing.Size(656, 179);
            this.pnlCheckboxes.TabIndex = 25;
            // 
            // btnManageReplays
            // 
            this.btnManageReplays.Location = new System.Drawing.Point(509, 124);
            this.btnManageReplays.Name = "btnManageReplays";
            this.btnManageReplays.Padding = new System.Windows.Forms.Padding(5);
            this.btnManageReplays.Size = new System.Drawing.Size(144, 23);
            this.btnManageReplays.TabIndex = 30;
            this.btnManageReplays.Text = "Manage Replays";
            this.btnManageReplays.Click += new System.EventHandler(this.btnManageReplays_Click);
            // 
            // btnReplaceMissing
            // 
            this.btnReplaceMissing.Location = new System.Drawing.Point(370, 153);
            this.btnReplaceMissing.Name = "btnReplaceMissing";
            this.btnReplaceMissing.Padding = new System.Windows.Forms.Padding(5);
            this.btnReplaceMissing.Size = new System.Drawing.Size(283, 23);
            this.btnReplaceMissing.TabIndex = 29;
            this.btnReplaceMissing.Text = "Replace missing images with black images";
            this.btnReplaceMissing.Click += new System.EventHandler(this.BtnReplaceMissing_Click);
            // 
            // lblPrompt
            // 
            this.lblPrompt.AutoSize = true;
            this.lblPrompt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lblPrompt.Location = new System.Drawing.Point(3, 0);
            this.lblPrompt.Margin = new System.Windows.Forms.Padding(0);
            this.lblPrompt.Name = "lblPrompt";
            this.lblPrompt.Size = new System.Drawing.Size(139, 13);
            this.lblPrompt.TabIndex = 28;
            this.lblPrompt.Text = "Select files to Move/Delete:";
            // 
            // allUncommon
            // 
            this.allUncommon.Location = new System.Drawing.Point(6, 157);
            this.allUncommon.Name = "allUncommon";
            this.allUncommon.Size = new System.Drawing.Size(260, 17);
            this.allUncommon.TabIndex = 27;
            this.allUncommon.Text = "All uncommon files (CHECK RESULTS FIRST)";
            this.allUncommon.CheckedChanged += new System.EventHandler(this.AllUncommon_CheckedChanged);
            // 
            // bloatExtraDeleteBox
            // 
            this.bloatExtraDeleteBox.Location = new System.Drawing.Point(6, 134);
            this.bloatExtraDeleteBox.Name = "bloatExtraDeleteBox";
            this.bloatExtraDeleteBox.Size = new System.Drawing.Size(300, 17);
            this.bloatExtraDeleteBox.TabIndex = 26;
            this.bloatExtraDeleteBox.Text = "Leftover files (thumbs.db, desktop.ini, *.DS_Store)";
            // 
            // hitSoundsDeleteCheckbox
            // 
            this.hitSoundsDeleteCheckbox.AutoSize = true;
            this.hitSoundsDeleteCheckbox.Location = new System.Drawing.Point(6, 111);
            this.hitSoundsDeleteCheckbox.Name = "hitSoundsDeleteCheckbox";
            this.hitSoundsDeleteCheckbox.Size = new System.Drawing.Size(73, 17);
            this.hitSoundsDeleteCheckbox.TabIndex = 25;
            this.hitSoundsDeleteCheckbox.Text = "Hitsounds";
            // 
            // backgroundDeleteCheckbox
            // 
            this.backgroundDeleteCheckbox.AutoSize = true;
            this.backgroundDeleteCheckbox.Location = new System.Drawing.Point(6, 88);
            this.backgroundDeleteCheckbox.Name = "backgroundDeleteCheckbox";
            this.backgroundDeleteCheckbox.Size = new System.Drawing.Size(89, 17);
            this.backgroundDeleteCheckbox.TabIndex = 24;
            this.backgroundDeleteCheckbox.Text = "Backgrounds";
            // 
            // sbDeleteCheckbox
            // 
            this.sbDeleteCheckbox.Location = new System.Drawing.Point(6, 65);
            this.sbDeleteCheckbox.Name = "sbDeleteCheckbox";
            this.sbDeleteCheckbox.Size = new System.Drawing.Size(157, 17);
            this.sbDeleteCheckbox.TabIndex = 21;
            this.sbDeleteCheckbox.Text = "Storyboard elements";
            // 
            // skinDeleteCheckbox
            // 
            this.skinDeleteCheckbox.Location = new System.Drawing.Point(6, 42);
            this.skinDeleteCheckbox.Name = "skinDeleteCheckbox";
            this.skinDeleteCheckbox.Size = new System.Drawing.Size(212, 17);
            this.skinDeleteCheckbox.TabIndex = 22;
            this.skinDeleteCheckbox.Text = "Skin elements";
            // 
            // videoDeleteCheckbox
            // 
            this.videoDeleteCheckbox.Location = new System.Drawing.Point(6, 19);
            this.videoDeleteCheckbox.Name = "videoDeleteCheckbox";
            this.videoDeleteCheckbox.Size = new System.Drawing.Size(133, 17);
            this.videoDeleteCheckbox.TabIndex = 23;
            this.videoDeleteCheckbox.Text = "Videos";
            // 
            // directoryPath
            // 
            this.directoryPath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(71)))), ((int)(((byte)(90)))));
            this.directoryPath.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.directoryPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.directoryPath.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(242)))));
            this.directoryPath.Location = new System.Drawing.Point(3, 5);
            this.directoryPath.Name = "directoryPath";
            this.directoryPath.Size = new System.Drawing.Size(625, 13);
            this.directoryPath.TabIndex = 29;
            this.directoryPath.TextChanged += new System.EventHandler(this.DirectoryPath_TextChanged);
            // 
            // directorySelectButton
            // 
            this.directorySelectButton.Location = new System.Drawing.Point(746, 3);
            this.directorySelectButton.Name = "directorySelectButton";
            this.directorySelectButton.Padding = new System.Windows.Forms.Padding(5);
            this.directorySelectButton.Size = new System.Drawing.Size(75, 23);
            this.directorySelectButton.TabIndex = 28;
            this.directorySelectButton.Text = "Browse";
            this.directorySelectButton.Click += new System.EventHandler(this.DirectorySelectButton_Click);
            // 
            // directoryLabel
            // 
            this.directoryLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.directoryLabel.Location = new System.Drawing.Point(3, 2);
            this.directoryLabel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 0);
            this.directoryLabel.Name = "directoryLabel";
            this.directoryLabel.Size = new System.Drawing.Size(100, 23);
            this.directoryLabel.TabIndex = 30;
            this.directoryLabel.Text = "osu! directory path:";
            this.directoryLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbLDirectory
            // 
            this.tbLDirectory.ColumnCount = 3;
            this.tbLDirectory.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 106F));
            this.tbLDirectory.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbLDirectory.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 81F));
            this.tbLDirectory.Controls.Add(this.pnlDirBar, 1, 0);
            this.tbLDirectory.Controls.Add(this.directorySelectButton, 2, 0);
            this.tbLDirectory.Controls.Add(this.directoryLabel, 0, 0);
            this.tbLDirectory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbLDirectory.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tbLDirectory.Location = new System.Drawing.Point(3, 3);
            this.tbLDirectory.Name = "tbLDirectory";
            this.tbLDirectory.RowCount = 1;
            this.tbLDirectory.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbLDirectory.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tbLDirectory.Size = new System.Drawing.Size(824, 29);
            this.tbLDirectory.TabIndex = 31;
            // 
            // pnlDirBar
            // 
            this.pnlDirBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(71)))), ((int)(((byte)(90)))));
            this.pnlDirBar.Controls.Add(this.directoryPath);
            this.pnlDirBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDirBar.Location = new System.Drawing.Point(109, 3);
            this.pnlDirBar.Name = "pnlDirBar";
            this.pnlDirBar.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.pnlDirBar.Size = new System.Drawing.Size(631, 23);
            this.pnlDirBar.TabIndex = 28;
            // 
            // tblEntireForm
            // 
            this.tblEntireForm.ColumnCount = 1;
            this.tblEntireForm.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblEntireForm.Controls.Add(this.progressBarBackground, 0, 3);
            this.tblEntireForm.Controls.Add(this.tblTopSection, 0, 1);
            this.tblEntireForm.Controls.Add(this.tbLDirectory, 0, 0);
            this.tblEntireForm.Controls.Add(this.tbLFooter, 0, 4);
            this.tblEntireForm.Controls.Add(this.elementList, 0, 2);
            this.tblEntireForm.Controls.Add(this.stripInfo, 0, 5);
            this.tblEntireForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblEntireForm.Location = new System.Drawing.Point(0, 0);
            this.tblEntireForm.Name = "tblEntireForm";
            this.tblEntireForm.RowCount = 6;
            this.tblEntireForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tblEntireForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 191F));
            this.tblEntireForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblEntireForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tblEntireForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 95F));
            this.tblEntireForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblEntireForm.Size = new System.Drawing.Size(830, 754);
            this.tblEntireForm.TabIndex = 33;
            // 
            // progressBarBackground
            // 
            this.progressBarBackground.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(71)))), ((int)(((byte)(90)))));
            this.progressBarBackground.Controls.Add(this.FindProgressBar);
            this.progressBarBackground.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressBarBackground.Location = new System.Drawing.Point(3, 613);
            this.progressBarBackground.Name = "progressBarBackground";
            this.progressBarBackground.Size = new System.Drawing.Size(824, 23);
            this.progressBarBackground.TabIndex = 28;
            // 
            // FindProgressBar
            // 
            this.FindProgressBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FindProgressBar.Location = new System.Drawing.Point(0, 0);
            this.FindProgressBar.Name = "FindProgressBar";
            this.FindProgressBar.Size = new System.Drawing.Size(824, 23);
            this.FindProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.FindProgressBar.TabIndex = 22;
            this.FindProgressBar.Visible = false;
            // 
            // tblTopSection
            // 
            this.tblTopSection.ColumnCount = 2;
            this.tblTopSection.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblTopSection.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 162F));
            this.tblTopSection.Controls.Add(this.tblLogoButtons, 1, 0);
            this.tblTopSection.Controls.Add(this.pnlCheckboxes, 0, 0);
            this.tblTopSection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblTopSection.Location = new System.Drawing.Point(3, 38);
            this.tblTopSection.Name = "tblTopSection";
            this.tblTopSection.RowCount = 1;
            this.tblTopSection.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblTopSection.Size = new System.Drawing.Size(824, 185);
            this.tblTopSection.TabIndex = 34;
            // 
            // tblLogoButtons
            // 
            this.tblLogoButtons.ColumnCount = 1;
            this.tblLogoButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLogoButtons.Controls.Add(this.pnlFindCancel, 0, 1);
            this.tblLogoButtons.Controls.Add(this.logoBox, 0, 0);
            this.tblLogoButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLogoButtons.Location = new System.Drawing.Point(665, 3);
            this.tblLogoButtons.Name = "tblLogoButtons";
            this.tblLogoButtons.RowCount = 2;
            this.tblLogoButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLogoButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tblLogoButtons.Size = new System.Drawing.Size(156, 179);
            this.tblLogoButtons.TabIndex = 26;
            // 
            // pnlFindCancel
            // 
            this.pnlFindCancel.Controls.Add(this.findButton);
            this.pnlFindCancel.Controls.Add(this.cancelButton);
            this.pnlFindCancel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFindCancel.Location = new System.Drawing.Point(0, 150);
            this.pnlFindCancel.Margin = new System.Windows.Forms.Padding(0);
            this.pnlFindCancel.Name = "pnlFindCancel";
            this.pnlFindCancel.Size = new System.Drawing.Size(156, 29);
            this.pnlFindCancel.TabIndex = 27;
            // 
            // findButton
            // 
            this.findButton.Location = new System.Drawing.Point(3, 3);
            this.findButton.Name = "findButton";
            this.findButton.Padding = new System.Windows.Forms.Padding(5);
            this.findButton.Size = new System.Drawing.Size(75, 23);
            this.findButton.TabIndex = 1;
            this.findButton.Text = "Find";
            this.findButton.Click += new System.EventHandler(this.FindButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(84, 3);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Padding = new System.Windows.Forms.Padding(5);
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 18;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.Visible = false;
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // logoBox
            // 
            this.logoBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.logoBox.Dock = System.Windows.Forms.DockStyle.Fill;
            /* this.logoBox.Image = global::osu_cleaner.Properties.Resources.osu_cleaner_logo_256; */
            this.logoBox.ImageLocation = "";
            this.logoBox.Location = new System.Drawing.Point(3, 3);
            this.logoBox.Name = "logoBox";
            this.logoBox.Size = new System.Drawing.Size(150, 144);
            this.logoBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.logoBox.TabIndex = 28;
            this.logoBox.TabStop = false;
            this.logoBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LogoBox_MouseDown);
            this.logoBox.MouseEnter += new System.EventHandler(this.LogoBox_HoverImage);
            this.logoBox.MouseLeave += new System.EventHandler(this.LogoBox_MouseLeave);
            this.logoBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.LogoBox_HoverImage);
            // 
            // elementList
            // 
            this.elementList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(33)))), ((int)(((byte)(44)))));
            this.elementList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.elementList.CheckOnClick = true;
            this.elementList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.elementList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(242)))));
            this.elementList.FormattingEnabled = true;
            this.elementList.Items.AddRange(new object[] {
            "Sample line"});
            this.elementList.Location = new System.Drawing.Point(3, 229);
            this.elementList.Name = "elementList";
            this.elementList.Size = new System.Drawing.Size(824, 378);
            this.elementList.TabIndex = 25;
            this.elementList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ElementList_MouseDown);
            // 
            // stripInfo
            // 
            this.stripInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(42)))), ((int)(((byte)(54)))));
            this.stripInfo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblCurrentAccount,
            this.lblHenntix,
            this.lblTechNobo,
            this.lblTCNOWeb});
            this.stripInfo.Location = new System.Drawing.Point(0, 734);
            this.stripInfo.Name = "stripInfo";
            this.stripInfo.Size = new System.Drawing.Size(830, 20);
            this.stripInfo.TabIndex = 35;
            this.stripInfo.Text = "statusStrip1";
            // 
            // lblCurrentAccount
            // 
            this.lblCurrentAccount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(100)))), ((int)(((byte)(158)))));
            this.lblCurrentAccount.Name = "lblCurrentAccount";
            this.lblCurrentAccount.Size = new System.Drawing.Size(128, 15);
            this.lblCurrentAccount.Text = "Current account: None";
            this.lblCurrentAccount.Click += new System.EventHandler(this.LblCurrentAccount_Click);
            this.lblCurrentAccount.MouseEnter += new System.EventHandler(this.LinkLabel_MouseEnter);
            this.lblCurrentAccount.MouseLeave += new System.EventHandler(this.LinkLabel_MouseLeave);
            // 
            // lblHenntix
            // 
            this.lblHenntix.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(42)))), ((int)(((byte)(54)))));
            this.lblHenntix.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lblHenntix.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(242)))));
            this.lblHenntix.IsLink = true;
            this.lblHenntix.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(242)))));
            this.lblHenntix.Name = "lblHenntix";
            this.lblHenntix.Size = new System.Drawing.Size(117, 15);
            this.lblHenntix.Text = "Original project: henntix";
            this.lblHenntix.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LblHenntix_MouseDown);
            this.lblHenntix.MouseEnter += new System.EventHandler(this.LinkLabel_MouseEnter);
            this.lblHenntix.MouseLeave += new System.EventHandler(this.LinkLabel_MouseLeave);
            // 
            // lblTechNobo
            // 
            this.lblTechNobo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(42)))), ((int)(((byte)(54)))));
            this.lblTechNobo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lblTechNobo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(242)))));
            this.lblTechNobo.IsLink = true;
            this.lblTechNobo.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(242)))));
            this.lblTechNobo.Name = "lblTechNobo";
            this.lblTechNobo.Size = new System.Drawing.Size(157, 15);
            this.lblTechNobo.Text = "Updated && Styled by TechNobo";
            this.lblTechNobo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LblTechNobo_MouseDown);
            this.lblTechNobo.MouseEnter += new System.EventHandler(this.LinkLabel_MouseEnter);
            this.lblTechNobo.MouseLeave += new System.EventHandler(this.LinkLabel_MouseLeave);
            // 
            // lblTCNOWeb
            // 
            this.lblTCNOWeb.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(42)))), ((int)(((byte)(54)))));
            this.lblTCNOWeb.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lblTCNOWeb.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(242)))));
            this.lblTCNOWeb.IsLink = true;
            this.lblTCNOWeb.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(242)))));
            this.lblTCNOWeb.Name = "lblTCNOWeb";
            this.lblTCNOWeb.Size = new System.Drawing.Size(91, 15);
            this.lblTCNOWeb.Text = "tcno.co (Website)";
            this.lblTCNOWeb.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LblTCNOWeb_MouseDown);
            this.lblTCNOWeb.MouseEnter += new System.EventHandler(this.LinkLabel_MouseEnter);
            this.lblTCNOWeb.MouseLeave += new System.EventHandler(this.LinkLabel_MouseLeave);
            // 
            // MainApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(42)))), ((int)(((byte)(54)))));
            this.ClientSize = new System.Drawing.Size(830, 754);
            this.Controls.Add(this.tblEntireForm);
            /* this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon"))); */
            this.MinimumSize = new System.Drawing.Size(846, 442);
            this.Name = "MainApp";
            this.Text = "cln! (osu!Cleaner by TechNobo)";
            this.Load += new System.EventHandler(this.MainApp_Load);
            this.tbLFooter.ResumeLayout(false);
            this.pnlBottomInfo.ResumeLayout(false);
            this.pnlBottomInfo.PerformLayout();
            this.pnlBottomButtons.ResumeLayout(false);
            this.pnlCheckboxes.ResumeLayout(false);
            this.pnlCheckboxes.PerformLayout();
            this.tbLDirectory.ResumeLayout(false);
            this.pnlDirBar.ResumeLayout(false);
            this.pnlDirBar.PerformLayout();
            this.tblEntireForm.ResumeLayout(false);
            this.tblEntireForm.PerformLayout();
            this.progressBarBackground.ResumeLayout(false);
            this.tblTopSection.ResumeLayout(false);
            this.tblLogoButtons.ResumeLayout(false);
            this.pnlFindCancel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.logoBox)).EndInit();
            this.stripInfo.ResumeLayout(false);
            this.stripInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private DarkUI.Controls.DarkCheckBox DeletePermanentlyCheckbox;
        private DarkUI.Controls.DarkButton selectAllButton;
        private DarkUI.Controls.DarkButton deselectAllButton;
        private DarkUI.Controls.DarkLabel filesSizeLabel;
        private DarkUI.Controls.DarkLabel forRemovalSizeLabel;
        private DarkUI.Controls.DarkCheckBox moveCheckBox;
        private System.ComponentModel.BackgroundWorker bgWorker;
        private System.Windows.Forms.TableLayoutPanel tbLFooter;
        private System.Windows.Forms.Panel pnlBottomInfo;
        private System.Windows.Forms.Panel pnlBottomButtons;
        private DarkUI.Controls.DarkButton deleteButton;
        private osu_cleaner.DarkProgressBar FindProgressBar;
        private DarkCheckedListBox elementList;
        private System.Windows.Forms.Panel pnlCheckboxes;
        private DarkUI.Controls.DarkCheckBox allUncommon;
        private DarkUI.Controls.DarkCheckBox bloatExtraDeleteBox;
        private DarkUI.Controls.DarkCheckBox hitSoundsDeleteCheckbox;
        private DarkUI.Controls.DarkCheckBox backgroundDeleteCheckbox;
        private DarkUI.Controls.DarkCheckBox sbDeleteCheckbox;
        private DarkUI.Controls.DarkCheckBox skinDeleteCheckbox;
        private DarkUI.Controls.DarkCheckBox videoDeleteCheckbox;
        private System.Windows.Forms.TextBox directoryPath;
        private DarkUI.Controls.DarkButton directorySelectButton;
        private DarkUI.Controls.DarkLabel directoryLabel;
        private System.Windows.Forms.TableLayoutPanel tbLDirectory;
        private System.Windows.Forms.TableLayoutPanel tblEntireForm;
        private System.Windows.Forms.TableLayoutPanel tblTopSection;
        private System.Windows.Forms.StatusStrip stripInfo;
        private System.Windows.Forms.ToolStripStatusLabel lblHenntix;
        private System.Windows.Forms.ToolStripStatusLabel lblTCNOWeb;
        private System.Windows.Forms.TableLayoutPanel tblLogoButtons;
        private System.Windows.Forms.Panel pnlFindCancel;
        private DarkUI.Controls.DarkButton findButton;
        private DarkUI.Controls.DarkButton cancelButton;
        private System.Windows.Forms.PictureBox logoBox;
        private System.Windows.Forms.Panel pnlDirBar;
        private Panel progressBarBackground;
        private DarkUI.Controls.DarkButton openMoved;
        private ToolStripStatusLabel lblTechNobo;
        private DarkUI.Controls.DarkLabel lblPrompt;
        private DarkUI.Controls.DarkButton symlinkButton;
        private DarkUI.Controls.DarkButton btnReplaceMissing;
        private ToolStripStatusLabel lblCurrentAccount;
        private DarkUI.Controls.DarkButton btnManageReplays;
    }
}

