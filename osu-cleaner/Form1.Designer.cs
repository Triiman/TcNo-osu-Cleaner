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
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.deleteButton = new DarkUI.Controls.DarkButton();
            this.elementList = new System.Windows.Forms.CheckedListBox();
            this.panel3 = new System.Windows.Forms.Panel();
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
            this.panel4 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.pnlFindCancel = new System.Windows.Forms.Panel();
            this.findButton = new DarkUI.Controls.DarkButton();
            this.cancelButton = new DarkUI.Controls.DarkButton();
            this.logoBox = new System.Windows.Forms.PictureBox();
            this.FindProgressBar = new osu_cleaner.DarkProgressBar();
            this.stripInfo = new System.Windows.Forms.StatusStrip();
            this.lblHenntix = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblTechNobo = new System.Windows.Forms.ToolStripStatusLabel();
            this.progressBarBackground = new System.Windows.Forms.Panel();
            this.tbLFooter.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tbLDirectory.SuspendLayout();
            this.panel4.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.pnlFindCancel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoBox)).BeginInit();
            this.stripInfo.SuspendLayout();
            this.progressBarBackground.SuspendLayout();
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
            this.selectAllButton.Click += new System.EventHandler(this.selectAllButton_Click);
            // 
            // deselectAllButton
            // 
            this.deselectAllButton.Location = new System.Drawing.Point(84, 3);
            this.deselectAllButton.Name = "deselectAllButton";
            this.deselectAllButton.Padding = new System.Windows.Forms.Padding(5);
            this.deselectAllButton.Size = new System.Drawing.Size(75, 23);
            this.deselectAllButton.TabIndex = 11;
            this.deselectAllButton.Text = "Unselect all";
            this.deselectAllButton.Click += new System.EventHandler(this.deselectAllButton_Click);
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
            this.tbLFooter.Location = new System.Drawing.Point(3, 642);
            this.tbLFooter.Name = "tbLFooter";
            this.tbLFooter.RowCount = 1;
            this.tbLFooter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbLFooter.Size = new System.Drawing.Size(824, 89);
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
            this.panel2.Size = new System.Drawing.Size(646, 83);
            this.panel2.TabIndex = 22;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.selectAllButton);
            this.panel1.Controls.Add(this.deselectAllButton);
            this.panel1.Controls.Add(this.deleteButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(652, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(172, 89);
            this.panel1.TabIndex = 22;
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(3, 32);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Padding = new System.Windows.Forms.Padding(5);
            this.deleteButton.Size = new System.Drawing.Size(75, 23);
            this.deleteButton.TabIndex = 2;
            this.deleteButton.Text = "Delete";
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // elementList
            // 
            this.elementList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(42)))), ((int)(((byte)(54)))));
            this.elementList.CheckOnClick = true;
            this.elementList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.elementList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(242)))));
            this.elementList.FormattingEnabled = true;
            this.elementList.Location = new System.Drawing.Point(3, 209);
            this.elementList.Name = "elementList";
            this.elementList.Size = new System.Drawing.Size(824, 398);
            this.elementList.TabIndex = 25;
            this.elementList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.elementList_MouseDown);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.allUncommon);
            this.panel3.Controls.Add(this.bloatExtraDeleteBox);
            this.panel3.Controls.Add(this.hitSoundsDeleteCheckbox);
            this.panel3.Controls.Add(this.backgroundDeleteCheckbox);
            this.panel3.Controls.Add(this.sbDeleteCheckbox);
            this.panel3.Controls.Add(this.skinDeleteCheckbox);
            this.panel3.Controls.Add(this.videoDeleteCheckbox);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.ForeColor = System.Drawing.Color.Purple;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(656, 159);
            this.panel3.TabIndex = 25;
            // 
            // allUncommon
            // 
            this.allUncommon.Location = new System.Drawing.Point(6, 141);
            this.allUncommon.Name = "allUncommon";
            this.allUncommon.Size = new System.Drawing.Size(260, 17);
            this.allUncommon.TabIndex = 27;
            this.allUncommon.Text = "All uncommon files (CHECK RESULTS FIRST)";
            this.allUncommon.CheckedChanged += new System.EventHandler(this.allUncommon_CheckedChanged);
            // 
            // bloatExtraDeleteBox
            // 
            this.bloatExtraDeleteBox.Location = new System.Drawing.Point(6, 118);
            this.bloatExtraDeleteBox.Name = "bloatExtraDeleteBox";
            this.bloatExtraDeleteBox.Size = new System.Drawing.Size(220, 17);
            this.bloatExtraDeleteBox.TabIndex = 26;
            this.bloatExtraDeleteBox.Text = "Delete thumbs.db && desktop.ini files";
            // 
            // hitSoundsDeleteCheckbox
            // 
            this.hitSoundsDeleteCheckbox.AutoSize = true;
            this.hitSoundsDeleteCheckbox.Location = new System.Drawing.Point(6, 95);
            this.hitSoundsDeleteCheckbox.Name = "hitSoundsDeleteCheckbox";
            this.hitSoundsDeleteCheckbox.Size = new System.Drawing.Size(105, 17);
            this.hitSoundsDeleteCheckbox.TabIndex = 25;
            this.hitSoundsDeleteCheckbox.Text = "Delete hitsounds";
            // 
            // backgroundDeleteCheckbox
            // 
            this.backgroundDeleteCheckbox.AutoSize = true;
            this.backgroundDeleteCheckbox.Location = new System.Drawing.Point(6, 72);
            this.backgroundDeleteCheckbox.Name = "backgroundDeleteCheckbox";
            this.backgroundDeleteCheckbox.Size = new System.Drawing.Size(122, 17);
            this.backgroundDeleteCheckbox.TabIndex = 24;
            this.backgroundDeleteCheckbox.Text = "Delete backgrounds";
            // 
            // sbDeleteCheckbox
            // 
            this.sbDeleteCheckbox.Location = new System.Drawing.Point(6, 49);
            this.sbDeleteCheckbox.Name = "sbDeleteCheckbox";
            this.sbDeleteCheckbox.Size = new System.Drawing.Size(157, 17);
            this.sbDeleteCheckbox.TabIndex = 21;
            this.sbDeleteCheckbox.Text = "Delete storyboard elements";
            // 
            // skinDeleteCheckbox
            // 
            this.skinDeleteCheckbox.Location = new System.Drawing.Point(6, 26);
            this.skinDeleteCheckbox.Name = "skinDeleteCheckbox";
            this.skinDeleteCheckbox.Size = new System.Drawing.Size(212, 17);
            this.skinDeleteCheckbox.TabIndex = 22;
            this.skinDeleteCheckbox.Text = "Delete skin elements";
            // 
            // videoDeleteCheckbox
            // 
            this.videoDeleteCheckbox.Location = new System.Drawing.Point(6, 3);
            this.videoDeleteCheckbox.Name = "videoDeleteCheckbox";
            this.videoDeleteCheckbox.Size = new System.Drawing.Size(133, 17);
            this.videoDeleteCheckbox.TabIndex = 23;
            this.videoDeleteCheckbox.Text = "Delete video";
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
            // 
            // directorySelectButton
            // 
            this.directorySelectButton.Location = new System.Drawing.Point(746, 3);
            this.directorySelectButton.Name = "directorySelectButton";
            this.directorySelectButton.Padding = new System.Windows.Forms.Padding(5);
            this.directorySelectButton.Size = new System.Drawing.Size(75, 23);
            this.directorySelectButton.TabIndex = 28;
            this.directorySelectButton.Text = "Browse";
            this.directorySelectButton.Click += new System.EventHandler(this.directorySelectButton_Click);
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
            this.tbLDirectory.Controls.Add(this.panel4, 1, 0);
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
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(71)))), ((int)(((byte)(90)))));
            this.panel4.Controls.Add(this.directoryPath);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(109, 3);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.panel4.Size = new System.Drawing.Size(631, 23);
            this.panel4.TabIndex = 28;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.progressBarBackground, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbLDirectory, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbLFooter, 0, 4);
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(830, 754);
            this.tableLayoutPanel1.TabIndex = 33;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 162F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel3, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 38);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(824, 165);
            this.tableLayoutPanel2.TabIndex = 34;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.pnlFindCancel, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.logoBox, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(665, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(156, 159);
            this.tableLayoutPanel3.TabIndex = 26;
            // 
            // pnlFindCancel
            // 
            this.pnlFindCancel.Controls.Add(this.findButton);
            this.pnlFindCancel.Controls.Add(this.cancelButton);
            this.pnlFindCancel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFindCancel.Location = new System.Drawing.Point(0, 130);
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
            this.findButton.Click += new System.EventHandler(this.findButton_Click);
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
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // logoBox
            // 
            this.logoBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.logoBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logoBox.Image = global::osu_cleaner.Properties.Resources.osu_cleaner_logo_256;
            this.logoBox.ImageLocation = "";
            this.logoBox.Location = new System.Drawing.Point(3, 3);
            this.logoBox.Name = "logoBox";
            this.logoBox.Size = new System.Drawing.Size(150, 124);
            this.logoBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.logoBox.TabIndex = 28;
            this.logoBox.TabStop = false;
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
            this.FindProgressBar.Click += new System.EventHandler(this.FindProgressBar_Click);
            // 
            // stripInfo
            // 
            this.stripInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(42)))), ((int)(((byte)(54)))));
            this.stripInfo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblHenntix,
            this.lblTechNobo});
            this.stripInfo.Location = new System.Drawing.Point(0, 734);
            this.stripInfo.Name = "stripInfo";
            this.stripInfo.Size = new System.Drawing.Size(830, 20);
            this.stripInfo.TabIndex = 35;
            this.stripInfo.Text = "statusStrip1";
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
            this.lblHenntix.Click += new System.EventHandler(this.lblHenntix_Click);
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
            this.lblTechNobo.Click += new System.EventHandler(this.lblTechNobo_Click);
            // 
            // progressBarBackground
            // 
            this.progressBarBackground.Controls.Add(this.FindProgressBar);
            this.progressBarBackground.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressBarBackground.Location = new System.Drawing.Point(3, 613);
            this.progressBarBackground.Name = "progressBarBackground";
            this.progressBarBackground.BackColor = Color.FromArgb(68, 71, 90);
            this.progressBarBackground.Size = new System.Drawing.Size(824, 23);
            this.progressBarBackground.TabIndex = 28;
            // 
            // MainApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(42)))), ((int)(((byte)(54)))));
            this.ClientSize = new System.Drawing.Size(830, 754);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
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
            this.tbLDirectory.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.pnlFindCancel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.logoBox)).EndInit();
            this.stripInfo.ResumeLayout(false);
            this.stripInfo.PerformLayout();
            this.progressBarBackground.ResumeLayout(false);
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
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private DarkUI.Controls.DarkButton deleteButton;
        private osu_cleaner.DarkProgressBar FindProgressBar;
        private System.Windows.Forms.CheckedListBox elementList;
        private System.Windows.Forms.Panel panel3;
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
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.StatusStrip stripInfo;
        private System.Windows.Forms.ToolStripStatusLabel lblHenntix;
        private System.Windows.Forms.ToolStripStatusLabel lblTechNobo;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Panel pnlFindCancel;
        private DarkUI.Controls.DarkButton findButton;
        private DarkUI.Controls.DarkButton cancelButton;
        private System.Windows.Forms.PictureBox logoBox;
        private System.Windows.Forms.Panel panel4;
        private Panel progressBarBackground;
    }
}

