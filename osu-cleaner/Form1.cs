// cln! TcNo-osu-Cleaner - A small tool, written in C#, to clean unused elements from beatmaps.
// Copyright (C) 2024 TroubleChute (Wesley Pyburn)
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.

// A fork from henntix's osu-cleaner
// https://github.com/henntix/osu-cleaner
// Originally licensed under The MIT License (MIT)

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DarkUI.Forms;
using Microsoft.VisualBasic.FileIO;
using Microsoft.Win32;
using Monitor.Core.Utilities;
using osu_cleaner.Properties;
using SymbolicLinkSupport;

namespace osu_cleaner
{
    public partial class MainApp : DarkForm
    {
	    private const string _versionNumber = "2.5";
	    private readonly ContextMenuStrip _collectionRoundMenuStrip = new ContextMenuStrip();
        private long _filesSize;
        private long _forRemovalSize;
        private readonly List<string> _foundElements = new List<string>();

        // Context menu
        private string _selectedMenuItem;
        private BackgroundWorker _worker, _delWorker, _listUpdater, _pixelWorker;

        public MainApp()
        {
            InitializeComponent();
        }

        private void MainApp_Load(object sender, EventArgs e)
        {
            Text = $"cln! (osu!Cleaner by TroubleChute) v{_versionNumber}";

            directoryPath.Text = GetOsuPath();

            if (directoryPath.Text == "")
            {
                MessageBox.Show("Please locate osu!'s folder (contains osu!.exe)", "Could not find osu!.exe",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                FindInstallDir();
                directoryPath.Text = GetOsuPath();
            }

            _worker = new BackgroundWorker()
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };
            _worker.DoWork += FindElements;
            _worker.ProgressChanged += ProgressBar;
            _worker.RunWorkerCompleted += FindComplete;

            _delWorker = new BackgroundWorker()
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };
            _delWorker.DoWork += DeleteElements;
            _delWorker.ProgressChanged += ProgressBar;
            _delWorker.RunWorkerCompleted += DeleteComplete;

            _listUpdater = new BackgroundWorker()
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };
            _listUpdater.DoWork += ListboxUpdater;
            _listUpdater.RunWorkerCompleted += ListboxUpdateFinal;

            _pixelWorker = new BackgroundWorker()
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };
            _pixelWorker.DoWork += PixelWorkerWork;
            _pixelWorker.ProgressChanged += BackgroundReplacerProgressBar;
            _pixelWorker.RunWorkerCompleted += PixelWorkerWorkComplete;

            // Context menu
            var tsOpenFile = new ToolStripMenuItem {Text = "Open file"};
            tsOpenFile.Click += TsOpenFile_Click;
            var tsOpenFolder = new ToolStripMenuItem {Text = "Open Folder"};
            tsOpenFolder.Click += TsOpenFolder_Click;
            var tsCopyFilePath = new ToolStripMenuItem {Text = "Copy file path"};
            tsCopyFilePath.Click += TsCopyFilePath_Click;
            _collectionRoundMenuStrip.Items.AddRange(new ToolStripItem[] {tsOpenFile, tsOpenFolder, tsCopyFilePath});

            openMoved.Visible = Directory.Exists(Path.Combine(directoryPath.Text, "Cleaned"));
            elementList.Items.Clear();
        }

        private void DirectorySelectButton_Click(object sender, EventArgs e) => FindInstallDir();

        private void FindInstallDir()
        {
            // TODO: Swap this for a locate osu!.exe browser. Easier to select a folder that way.
            var folder = new FolderBrowserDialog
            {
                ShowNewFolderButton = false,
                RootFolder = Environment.SpecialFolder.MyComputer,
                Description = "Select an osu! root directory (contains osu!.exe):",
                SelectedPath = directoryPath.Text
            };
            var path = folder.ShowDialog();
            if (path == DialogResult.OK)
                //check if osu!.exe is present
                if (!File.Exists(folder.SelectedPath + "\\osu!.exe"))
                {
                    using (var dlg = new DarkMessageBox("Not a valid osu! directory!", "Error!",
                        DarkMessageBoxIcon.Error, DarkDialogButton.Ok))
                    {
                        dlg.ShowDialog();
                    }

                    FindInstallDir();
                    return;
                }

            directoryPath.Text = folder.SelectedPath;
            openMoved.Visible = Directory.Exists(Path.Combine(directoryPath.Text, "Cleaned"));
        }

        private void FindButton_Click(object sender, EventArgs e)
        {
	        if (_worker.IsBusy) return;
            cancelButton.Visible = true;
            elementList.Items.Clear();
            _filesSize = 0;
            filesSizeLabel.Text = "Found: " + Math.Round((double) _filesSize / 1048576, 4) + " MB";
            _forRemovalSize = 0;
            forRemovalSizeLabel.Text =
                "Selected for removal: " + Math.Round((double) _forRemovalSize / 1048576, 4) + " MB";

            FindProgressBar.Show();
            _worker.RunWorkerAsync();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            if (_worker.IsBusy)
                _worker.CancelAsync();
            if (_delWorker.IsBusy)
                _delWorker.CancelAsync();
            if (_pixelWorker.IsBusy)
                _pixelWorker.CancelAsync();
        }

        private void SelectAllButton_Click(object sender, EventArgs e)
        {
            for (var i = 0; i < elementList.Items.Count; i++)
                elementList.SetItemChecked(i, true);
            _forRemovalSize = 0;
            foreach (string file in elementList.CheckedItems)
            {
                var sizeInfo = new FileInfo(file);
                _forRemovalSize += sizeInfo.Length;
            }

            forRemovalSizeLabel.Text =
                "Selected for removal: " + Math.Round((double) _forRemovalSize / 1048576, 4) + " MB";
        }

        private void DeselectAllButton_Click(object sender, EventArgs e)
        {
            for (var i = 0; i < elementList.Items.Count; i++)
                elementList.SetItemChecked(i, false);
            _forRemovalSize = 0;
            forRemovalSizeLabel.Text =
                "Selected for removal: " + Math.Round((double) _forRemovalSize / 1048576, 4) + " MB";
        }

        private void DeletePermanentlyCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (DeletePermanentlyCheckbox.Checked) moveCheckBox.Checked = false;
        }

        private void MoveCheckBox_CheckedChanged(object sender, EventArgs e)
        {
	        if (moveCheckBox.Checked) DeletePermanentlyCheckbox.Checked = false;
	        deleteButton.Text = moveCheckBox.Checked ? "Move" : "Delete";
        }

        private bool RegexMatch(string str, string regex)
        {
            var r = new Regex(regex, RegexOptions.IgnoreCase);
            return r.IsMatch(str);
        }

        private string SongsFolder = "";

        private bool FileContainsString(string filePath, string searchString)
        {
            try
            {
                using (var reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.Contains(searchString))
                        {
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading file {filePath}: {ex.Message}");
            }
            return false;
        }
        private void FindElements(object sender, DoWorkEventArgs e)
        {
            int folderCount;
            try
            {
                folderCount = Directory.GetDirectories(SongsFolder).Length;
            }
            catch (DirectoryNotFoundException)
            {
                MessageBox.Show("Can not find Songs directory in osu! folder.", "Fatal error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            Console.WriteLine(folderCount);
            var current = 0;
            foreach (var d in Directory.GetDirectories(SongsFolder))
            {
                if (!allUncommon.Checked && sbDeleteCheckbox.Checked)
                {
                    //whitelisting BG from deletion (often BG files are used in SB)
                    var whitelist = new List<string>();
                    foreach (var file in Directory.GetFiles(d))
                        if (Regex.IsMatch(file, "osu$"))
                        {
                            var bg = GetBgPath(file);
                            if (bg != null && !whitelist.Contains(bg))
                                whitelist.Add(bg);
                        }

                    foreach (var file in Directory.GetFiles(d))
                        if (Regex.IsMatch(file, "osb$"))
                        {
                            var sbElements = GetSbElements(file);
                            foreach (var sbElement in sbElements)
                                if (!whitelist.Contains(sbElement))
                                {
                                    var size = GetFileSize(d + sbElement);
                                    if (size == 0) continue;
                                    _foundElements.Add(d + sbElement);
                                    _filesSize += size;
                                }
                        }
                }


                var bgElements = new List<string>();
                foreach (var file in Directory.GetFiles(d))
                {
                    string fileName;
                    try
                    {
                        fileName = new FileInfo(file).Name;
                    }
                    catch (PathTooLongException)
                    {
                        continue;
                    }

                    switch (allUncommon.Checked)
                    {
	                    case false when backgroundDeleteCheckbox.Checked:
	                    {
		                    if (Regex.IsMatch(fileName, "osu$"))
		                    {
			                    var bg = GetBgPath(file);
			                    if (bg != null && !bgElements.Contains(bg))
			                    {
				                    var size = GetFileSize(d + bg);
				                    if (size != 0)
				                    {
					                    bgElements.Add(bg);
					                    _foundElements.Add(d + bg);
					                    _filesSize += size;
				                    }
			                    }
		                    }

		                    break;
	                    }
	                    case true:
	                    {
		                    if (!RegexMatch(fileName,
			                    "(avi|wmv|flv|mp4|mpg|mov|mkv|m4v|mpeg|3gp|mkv|webm|osu|png|jpeg|jpg|png|bmp|osb|osu|mp3|aac|wav|ogg|txt)$")
		                    )
		                    {
			                    _foundElements.Add(file);
			                    _filesSize += GetFileSize(file);
		                    }

		                    continue;
	                    }
                    }

                    if (videoDeleteCheckbox.Checked)
                        if (RegexMatch(fileName, "(avi|wmv|flv|mp4|mpg|mov|mkv|m4v|mpeg|3gp|mkv|webm)$"))
                        {
                            _foundElements.Add(file);
                            _filesSize += GetFileSize(file);
                        }
                    if (standardModeCheckbox.Checked)
                    {
                        if (Regex.IsMatch(fileName, "osu$"))
                        {
                            if (FileContainsString(file, "Mode: 0"))
                            {
                                _foundElements.Add(file);
                                _filesSize += GetFileSize(file);
                            }
                        }
                    }
                    if (taikoModeCheckbox.Checked)
                    {
                        if (Regex.IsMatch(fileName, "osu$"))
                        {
                            if (FileContainsString(file, "Mode: 1"))
                            {
                                _foundElements.Add(file);
                                _filesSize += GetFileSize(file);
                            }
                        }
                    }
                    if (ctbModeCheckbox.Checked)
                    {
                        if (Regex.IsMatch(fileName, "osu$"))
                        {
                            if (FileContainsString(file, "Mode: 2"))
                            {
                                _foundElements.Add(file);
                                _filesSize += GetFileSize(file);
                            }
                        }
                    }
                    if (maniaModeCheckbox.Checked)
                    {
                        if (Regex.IsMatch(fileName, "osu$"))
                        {
                            if (FileContainsString(file, "Mode: 3"))
                            {
                                _foundElements.Add(file);
                                _filesSize += GetFileSize(file);
                            }
                        }
                    }
                    if (skinDeleteCheckbox.Checked)
                        if (RegexMatch(fileName, "^(applause|approachcircle|button-|combobreak|comboburst|count1|" +
                                                 "count2|count3|count|cursor|default-|failsound|fail-background|followpoint|" +
                                                 "fruit-|go.png|go@2x.png|gos.png|gos@2x.png|hit0|hit100|hit300|hit50|" +
                                                 "hitcircle|inputoverlay-|lighting|lighting@2x.png|mania-|menu.|menu-|" +
                                                 "menu-back|particle100|particle300|particle50|pause-|pippidon|play-|" +
                                                 "ranking-|ready|reversearrow|score-|scorebar-|sectionfail|sectionpass|" +
                                                 "section-|selection-|sliderb|sliderfollowcircle|sliderscorepoint|spinnerbonus|" +
                                                 "spinner-|spinnerspin|star.png|star@2x.png|star2.png|" +
                                                 "star2@2x.png|taiko-|taikobigcircle|taikohitcircle)"))
                        {
                            _foundElements.Add(file);
                            _filesSize += GetFileSize(file);
                        }

                    if (hitSoundsDeleteCheckbox.Checked)
                        if (RegexMatch(fileName, "^(drum-|normal-|soft-)"))
                        {
                            _foundElements.Add(file);
                            _filesSize += GetFileSize(file);
                        }

                    if (!bloatExtraDeleteBox.Checked || !RegexMatch(fileName, "(thumbs.db|desktop.ini|ds_store)$")) continue;
                    _foundElements.Add(file);
                    _filesSize += GetFileSize(file);
                }

                if (_worker.CancellationPending) return;
                current++;
                _worker.ReportProgress((int) ((double) current / folderCount * 100));
            }

            _worker.ReportProgress(100);
            progressBarBackground.Invoke(() => { progressBarBackground.ForeColor = Color.FromArgb(80, 250, 123); });
        }

        private void ProgressBar(object sender, ProgressChangedEventArgs e)
        {
            FindProgressBar.Value = e.ProgressPercentage;
            filesSizeLabel.Text = "Found: " + Math.Round((double) _filesSize / 1048576, 4) + " MB";
        }

        private void BackgroundReplacerProgressBar(object sender, ProgressChangedEventArgs e)
        {
            FindProgressBar.Value = e.ProgressPercentage;
        }

        private void FindComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBarBackground.ForeColor = Color.FromArgb(80, 250, 123);
            foreach (var file in _foundElements)
                elementList.Items.Add(file);
            filesSizeLabel.Text = "Found: " + Math.Round((double) _filesSize / 1048576, 4) + " MB";
            _foundElements.Clear();
            cancelButton.Visible = false;
            FindProgressBar.Value = 0;
            FindProgressBar.Hide();
        }

        private static string GetOsuPath()
        {
            using (var osuReg = Registry.ClassesRoot.OpenSubKey("osu\\DefaultIcon"))
            {
                if (osuReg == null) return "";
                var osuKey = osuReg.GetValue(null).ToString();
                var osuPath = osuKey.Remove(0, 1);
                osuPath = osuPath.Remove(osuPath.Length - 11);
                return Directory.Exists(osuPath) ? osuPath : "";
            }
        }

        private static string GetBgPath(string path, bool precedeWithSlash = true)
        {
            try
            {
                using (var file = File.OpenText(path))
                {
                    string line;
                    while ((line = file.ReadLine()) != null)
                        if (Regex.IsMatch(line, "^//Background and Video events"))
                        {
                            line = file.ReadLine();
                            if (line == null) continue;
                            var items = line.Split(',');
                            if (items[0] != "0") continue;
                            var tmp = (precedeWithSlash ? "\\" : "") + items[2].Replace("\"", string.Empty);
                            return tmp;
                        }

                    return null;
                }
            }
            catch (PathTooLongException)
            {
                return null;
            }
        }

        private static IEnumerable<string> GetSbElements(string file)
        {
            var sbElements = new List<string>();
            using (var sbFile = File.OpenText(file))
            {
                string line;
                while ((line = sbFile.ReadLine()) != null)
                {
                    var items = line.Split(',');
                    if (items[0] != "Sprite") continue;
                    var tmp = "\\" + items[3].Replace("\"", string.Empty);
                    tmp = tmp.Replace("/", "\\");
                    if (!sbElements.Contains(tmp))
                        sbElements.Add(tmp);
                }
            }

            return sbElements;
        }

        private static long GetFileSize(string path)
        {
            try
            {
                var sizeInfo = new FileInfo(path);
                return sizeInfo.Length;
            }
            catch (FileNotFoundException)
            {
                return 0;
            }
            catch (NotSupportedException)
            {
                return 0;
            }
            catch (PathTooLongException)
            {
                return 0;
            }
        }

        private void AllUncommon_CheckedChanged(object sender, EventArgs e)
        {
            var bChecked = allUncommon.Checked;
            bloatExtraDeleteBox.Enabled = !bChecked;
            hitSoundsDeleteCheckbox.Enabled = !bChecked;
            backgroundDeleteCheckbox.Enabled = !bChecked;
            sbDeleteCheckbox.Enabled = !bChecked;
            skinDeleteCheckbox.Enabled = !bChecked;
            videoDeleteCheckbox.Enabled = !bChecked;
            standardModeCheckbox.Enabled = !bChecked;
            taikoModeCheckbox.Enabled = !bChecked;
            ctbModeCheckbox.Enabled = !bChecked;
            maniaModeCheckbox.Enabled = !bChecked;
        }

        // Context menu
        private void TsCopyFilePath_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(_selectedMenuItem);
        }

        private void TsOpenFolder_Click(object sender, EventArgs e)
        {
            if (!File.Exists(_selectedMenuItem)) return;
            var fileFullPath = Path.GetFullPath(_selectedMenuItem);
            Process.Start("explorer.exe", $"/select,\"{fileFullPath}\"");
        }

        private void TsOpenFile_Click(object sender, EventArgs e)
        {
            if (!File.Exists(_selectedMenuItem)) return;
            Process.Start(_selectedMenuItem);
        }

        private void ElementList_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
            var index = elementList.IndexFromPoint(e.Location);
            if (index != ListBox.NoMatches)
            {
                elementList.SelectedIndex = index;
                _selectedMenuItem = elementList.Items[index].ToString();
                _collectionRoundMenuStrip.Show(Cursor.Position);
                _collectionRoundMenuStrip.Visible = true;
            }
            else
            {
                _collectionRoundMenuStrip.Visible = false;
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (moveCheckBox.Checked) Directory.CreateDirectory(Path.Combine(directoryPath.Text, "Cleaned"));
            FindProgressBar.Show();
            _delWorker.RunWorkerAsync();
        }

        private void DeleteElements(object sender, DoWorkEventArgs e)
        {
            _listUpdater.RunWorkerAsync();

            var delete = new List<string>();
            foreach (string file in elementList.CheckedItems
            ) //adding items to temporary collection to let me delete items from on-screen list
                delete.Add(file);

            var totalToDelete = delete.Count;
            var current = 0;

            Parallel.ForEach(delete, file =>
            {
                try
                {
                    _filesSize -= GetFileSize(file);
                    if (DeletePermanentlyCheckbox.Checked)
                    {
                        FileSystem.DeleteFile(file, UIOption.OnlyErrorDialogs, RecycleOption.DeletePermanently);
                    }
                    else if (moveCheckBox.Checked)
                    {
                        var fileInfo = new FileInfo(file);
                        if (fileInfo.Directory != null)
                        {
                            var relativePath = fileInfo.Directory.FullName.Replace(
	                            SongsFolder, Path.Combine(directoryPath.Text, "Cleaned"));
                            Directory.CreateDirectory(relativePath);
                            File.Move(file, Path.Combine(relativePath, fileInfo.Name));
                        }
                    }
                    else
                    {
                        FileSystem.DeleteFile(file, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
                    }

                    Interlocked.Increment(ref current);
                }
                catch (FileNotFoundException)
                {
                }
                catch (NotSupportedException)
                {
                }

                // Prevent cross-thread errors
                QueueRemoveString(file);

                filesSizeLabel.Invoke(() =>
                {
                    filesSizeLabel.Text = "Found: " + Math.Round((double) _filesSize / 1048576, 4) + " MB";
                });

                _delWorker.ReportProgress((int)((double)current / totalToDelete * 100));
            });
        }
        private readonly List<string> _queueRemovedItems = new List<string>();
        private readonly Mutex _m = new Mutex();

        private void QueueRemoveString(string toRemove)
        {
            _m.WaitOne();
            try
            {
                _queueRemovedItems.Add(toRemove);
            }
            finally {
                _m.ReleaseMutex();
            }
        }

        // Loop to update the list only once a second while deleting
        private void ListboxUpdater(object sender, DoWorkEventArgs e)
        {
            var worker = (BackgroundWorker)sender;
            while (!worker.CancellationPending)
            {
                UpdateListbox();
                Thread.Sleep(10);
            }
        }
        // Final update for the listbox after it stops
        private void ListboxUpdateFinal(object sender, RunWorkerCompletedEventArgs e)
        {
            UpdateListbox();
        }

        private List<string> _currentQueue = new List<string>();
        private void UpdateListbox()
        {
            _m.WaitOne();
            try
            {
                if (_queueRemovedItems != null && _queueRemovedItems.Count > 0)
                {
                    _currentQueue = _queueRemovedItems.ToList();
                    _queueRemovedItems.Clear();
                }
            }
            finally
            {
                _m.ReleaseMutex();
            }

            if (_currentQueue == null || _currentQueue.Count <= 0) return;
            elementList.Invoke(() => { elementList.BeginUpdate(); });
            foreach (var s in _currentQueue)
            {
                elementList.Invoke(() => { elementList.Items.Remove(s); });
            }

            elementList.Invoke(() => { elementList.EndUpdate(); });

            Thread.Sleep(240);
        }


        private void OpenMoved_Click(object sender, EventArgs e)
        {
            Process.Start(Path.Combine(directoryPath.Text, "Cleaned"));
        }

        private void LblTCNOWeb_MouseDown(object sender, MouseEventArgs e)
        {
            Process.Start("https://tcno.co");
        }
        private void LblTechNobo_MouseDown(object sender, MouseEventArgs e)
        {
            Process.Start("https://github.com/TcNobo/TcNo-osu-Cleaner");
        }

        private void LblHenntix_MouseDown(object sender, MouseEventArgs e)
        {
            Process.Start("https://github.com/henntix/osu-Cleaner");
        }

        private void LogoBox_MouseLeave(object sender, EventArgs e)
        {
            logoBox.Image = Resources.osu_cleaner_logo_256;
        }

        private void LogoBox_MouseDown(object sender, MouseEventArgs e)
        {
            Process.Start("https://github.com/TcNobo/TcNo-osu-Cleaner");
            logoBox.Image = Resources.osu_cleaner_logo_256_click;
        }

        private void LogoBox_HoverImage(object sender, EventArgs e)
        {
            logoBox.Image = Resources.osu_cleaner_logo_256_hover;
        }

        private void LinkLabel_MouseEnter(object sender, EventArgs e)
        {
            var tssl = (ToolStripStatusLabel)sender;
            tssl.LinkColor = Color.FromArgb(138, 255, 128);
            tssl.ForeColor = Color.FromArgb(138, 255, 128);
            this.Cursor = Cursors.Hand;
        }

        private void LinkLabel_MouseLeave(object sender, EventArgs e)
        {
            var tssl = (ToolStripStatusLabel)sender;
            tssl.LinkColor = Color.FromArgb(248, 248, 242);
            tssl.ForeColor = Color.FromArgb(230, 100, 158); // For the username text
            this.Cursor = Cursors.Default;
        }

        private void DeleteComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            cancelButton.Visible = false;
            FindProgressBar.Value = 0;
            FindProgressBar.Hide();
            if (moveCheckBox.Checked) openMoved.Visible = true;
        }

        private void SymlinkButton_Click(object sender, EventArgs e)
        {
            var isSymlink = new DirectoryInfo(SongsFolder).IsSymbolicLink();
            var isJunction = JunctionPoint.Exists(SongsFolder);
            if (isSymlink || isJunction)
            {
                MessageBox.Show(this, "The Songs folder is already a symlink/junction!", "Notice!",
                    MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }

            var folder = new FolderBrowserDialog
            {
                ShowNewFolderButton = false,
                RootFolder = Environment.SpecialFolder.MyComputer,
                Description = "Select a folder to move the Songs folder to (Folder, not files within):",
                SelectedPath = directoryPath.Text
            };
            var path = folder.ShowDialog();
            if (path != DialogResult.OK) return;
            var fo = new InteropShFileOperation
            {
	            WFunc = InteropShFileOperation.FoFunc.FO_MOVE,
	            FFlags =
	            {
		            FofAllowundo = false,
		            FofNoconfirmation = true,
		            FofNoerrorui = false,
		            FofSilent = false
	            },
	            PFrom = SongsFolder,
	            PTo = folder.SelectedPath
            };
            if (fo.Execute())
            {
	            // Success
	            JunctionPoint.Create(SongsFolder, folder.SelectedPath, true);
	            MessageBox.Show(this, "Moved files, and created symlink junction correctly!", "Success!",
		            MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            else
            {
	            MessageBox.Show(this, "Failed to copy files", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error,
		            MessageBoxDefaultButton.Button1);
            }
        }

		private string _userCfgFile = "";

		private void FindAccount()
		{
			SelectUser su = null;


			var configFiles = new DirectoryInfo(directoryPath.Text).GetFiles("osu!*.cfg").OrderBy(f => f.Name).ToArray();
			if (configFiles.Length == 2)
			{
				_userCfgFile = configFiles[1].Name == "osu!.cfg" ? configFiles[0].Name : configFiles[1].Name;
				lblCurrentAccount.Text = "Current account: " + _userCfgFile.Substring(5, _userCfgFile.Length - 9);
                FindSongsFolder();
            }
			else if (configFiles.Length > 1)
            {
                int attempts = 0;
                while (true)
                {
                    attempts++;
                    // Notify user as well if auto picked what it will do.
                    if (su == null) su = new SelectUser(directoryPath.Text)
                    {
                        StartPosition = FormStartPosition.CenterScreen
                    };
                    var result = su.ShowDialog();

                    if (result != DialogResult.OK)
                    {
                        if (attempts >= 2)
                        {
                            MessageBox.Show(this, "You need to pick a username! Exiting.", "No username selected", MessageBoxButtons.OK,
                                MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                            Environment.Exit(0);
                        }

                        MessageBox.Show(this, "You need to pick a username!", "No username selected", MessageBoxButtons.OK,
                            MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                        continue;
                    }
                    _userCfgFile = su.ReturnedFilename;
                    lblCurrentAccount.Text = "Current account: " + su.ReturnedUsername;
                    break;
                }
                FindSongsFolder();
            }
            else if (!Directory.Exists(Path.Combine(directoryPath.Text, "Songs")))
            {
                MessageBox.Show("A user needs to be logged into osu! to continue. There is no 'osu!<user>.cfg' file!",
                    "Error!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }
            else
            {
                SongsFolder = Path.Combine(directoryPath.Text, "Songs");
            }
		}

        /// <summary>
        /// Finds the user's Songs folder, based on what's set in the config file.
        /// </summary>
		private void FindSongsFolder()
		{
			var file = new StreamReader(Path.Combine(directoryPath.Text, _userCfgFile));
			string line;
			while ((line = file.ReadLine()) != null)
			{
				if (line.StartsWith("BeatmapDirectory"))
				{
					var songsPath = line.Split('=')[1];
					songsPath = songsPath.Substring(1, songsPath.Length - 1);
                    // I can't get it to work with absolute paths, but this is here just incase.
					SongsFolder = songsPath.Contains(@":\\") ? songsPath.Replace("\"", "") : Path.Combine(directoryPath.Text, songsPath.Replace("\"", ""));
                    break;
				}
			}

			file.Close();
        }

        private void DirectoryPath_TextChanged(object sender, EventArgs e)
        {
	        FindAccount();
        }

		private void LblCurrentAccount_Click(object sender, EventArgs e)
		{
			FindAccount();
        }

        private void btnManageReplays_Click(object sender, EventArgs e)
        {
            var mr = new ManageReplays(directoryPath.Text)
            {
                StartPosition = FormStartPosition.CenterScreen
            };
            mr.ShowDialog();
        }

        private void BtnReplaceMissing_Click(object sender, EventArgs e)
        {
            FindProgressBar.Show();
            _pixelWorker.RunWorkerAsync();
        }

        // Loop to update the list only once a second while deleting
        private void PixelWorkerWork(object sender, DoWorkEventArgs e)
        {
            int folderCount;
            try
            {
                folderCount = Directory.GetDirectories(SongsFolder).Length;
            }
            catch (DirectoryNotFoundException)
            {
                MessageBox.Show("Can not find Songs directory in osu! folder.", "Fatal error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            Console.WriteLine(folderCount);
            var current = 0;
            var backgrounds = new List<string>();
            var missingBgCount = 0;
            foreach (var d in Directory.GetDirectories(SongsFolder))
            {
                // Collect all backgrounds
                foreach (var file in Directory.GetFiles(d))
                    if (Regex.IsMatch(file, "osu$"))
                    {
                        var bg = GetBgPath(file, false);
                        if (bg != null && !backgrounds.Contains(Path.Combine(d, bg)))
                        {
                            if (File.Exists(Path.Combine(d, bg))) continue;
                            backgrounds.Add(Path.Combine(d, bg));
                            missingBgCount++;
                        }
                    }
                if (_pixelWorker.CancellationPending) return;
                current++;
                _pixelWorker.ReportProgress((int)((double)current / folderCount * 100));
                filesSizeLabel.Invoke(() => { filesSizeLabel.Text =
                    $"(Step 1 of 2) Checked: {current} of {folderCount} folders. ({missingBgCount} missing BGs)"; });
            }

            _pixelWorker.ReportProgress(0);
            current = 0;
            foreach (var bg in backgrounds)
            {
                current++;
                _pixelWorker.ReportProgress((int)((double)current / missingBgCount * 100));
                filesSizeLabel.Invoke(() => { filesSizeLabel.Text = "(Step 2 of 2) Copied 1x1px images to: " + current + " of " + missingBgCount + " folders."; });

                Resources.pixel.Save(bg);
            }

            _pixelWorker.ReportProgress(100);
            progressBarBackground.Invoke(() => { progressBarBackground.ForeColor = Color.FromArgb(80, 250, 123); });
            MessageBox.Show(
                missingBgCount > 0
                    ? $"Finished copying in files. {missingBgCount} ~90 byte files created.\nSaving you tons of space, and no warnings in-game :)"
                    : "No missing backgrounds found", "Done", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
        // Final update for the listbox after it stops
        private void PixelWorkerWorkComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBarBackground.ForeColor = Color.FromArgb(80, 250, 123);
            foreach (var file in _foundElements)
                elementList.Items.Add(file);
            filesSizeLabel.Text = "Found: " + Math.Round((double)_filesSize / 1048576, 4) + " MB";
            _foundElements.Clear();
            cancelButton.Visible = false;
            FindProgressBar.Value = 0;
            FindProgressBar.Hide();
        }
    }
}

public static class ControlExtensions
{
    public static void Invoke(this Control control, Action action)
    {
        if (control.InvokeRequired)
            control.Invoke(new MethodInvoker(action), null);
        else
            action.Invoke();
    }
}

public class InteropShFileOperation
{
    // http://pinvoke.net/default.aspx/shell32/SHFileOperation.html
    public enum FoFunc : uint
    {
        FO_MOVE = 0x0001,
        FO_COPY = 0x0002,
        FO_DELETE = 0x0003,
        FO_RENAME = 0x0004,
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 2)]
    struct Shfileopstruct
    {
        public IntPtr hwnd;
        public FoFunc wFunc;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string pFrom;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string pTo;
        public ushort fFlags;
        [MarshalAs(UnmanagedType.Bool)]
        public bool fAnyOperationsAborted;
        public IntPtr hNameMappings;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string lpszProgressTitle;

    }

    [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
    static extern int SHFileOperation([In, Out] ref Shfileopstruct lpFileOp);

    private Shfileopstruct _shFile;
    public FileopFlags FFlags;

    public IntPtr Hwnd
    {
        set => _shFile.hwnd = value;
    }
    public FoFunc WFunc
    {
        set => _shFile.wFunc = value;
    }

    public string PFrom
    {
        set => _shFile.pFrom = value + '\0' + '\0';
    }
    public string PTo
    {
        set => _shFile.pTo = value + '\0' + '\0';
    }

    public bool FAnyOperationsAborted
    {
        set => _shFile.fAnyOperationsAborted = value;
    }
    public IntPtr HNameMappings
    {
        set => _shFile.hNameMappings = value;
    }
    public string LpszProgressTitle
    {
        set => _shFile.lpszProgressTitle = value + '\0';
    }

    public InteropShFileOperation()
    {

        FFlags = new FileopFlags();
        _shFile = new Shfileopstruct
        {
	        hwnd = IntPtr.Zero,
	        wFunc = FoFunc.FO_COPY,
	        pFrom = "",
	        pTo = "",
	        fAnyOperationsAborted = false,
	        hNameMappings = IntPtr.Zero,
	        lpszProgressTitle = ""
        };

    }

    public bool Execute()
    {
        _shFile.fFlags = FFlags.Flag;
        return SHFileOperation(ref _shFile) == 0;//true if no errors
    }

    public class FileopFlags
    {
        [Flags]
        private enum FileopFlagsEnum : ushort
        {
            FOF_MULTIDESTFILES = 0x0001,
            FOF_CONFIRMMOUSE = 0x0002,
            FOF_SILENT = 0x0004,  // don't create progress/report
            FOF_RENAMEONCOLLISION = 0x0008,
            FOF_NOCONFIRMATION = 0x0010,  // Don't prompt the user.
            FOF_WANTMAPPINGHANDLE = 0x0020,  // Fill in SHFILEOPSTRUCT.hNameMappings
            // Must be freed using SHFreeNameMappings
            FOF_ALLOWUNDO = 0x0040,
            FOF_FILESONLY = 0x0080,  // on *.*, do only files
            FOF_SIMPLEPROGRESS = 0x0100,  // means don't show names of files
            FOF_NOCONFIRMMKDIR = 0x0200,  // don't confirm making any needed dirs
            FOF_NOERRORUI = 0x0400,  // don't put up error UI
            FOF_NOCOPYSECURITYATTRIBS = 0x0800,  // dont copy NT file Security Attributes
            FOF_NORECURSION = 0x1000,  // don't recurse into directories.
            FOF_NO_CONNECTED_ELEMENTS = 0x2000,  // don't operate on connected elements.
            FOF_WANTNUKEWARNING = 0x4000,  // during delete operation, warn if nuking instead of recycling (partially overrides FOF_NOCONFIRMATION)
            FOF_NORECURSEREPARSE = 0x8000,  // treat reparse points as objects, not containers
        }

        public bool FofMultidestfiles = false;
        public bool FofConfirmmouse = false;
        public bool FofSilent = false;
        public bool FofRenameoncollision = false;
        public bool FofNoconfirmation = false;
        public bool FofWantmappinghandle = false;
        public bool FofAllowundo = false;
        public bool FofFilesonly = false;
        public bool FofSimpleprogress = false;
        public bool FofNoconfirmmkdir = false;
        public bool FofNoerrorui = false;
        public bool FofNocopysecurityattribs = false;
        public bool FofNorecursion = false;
        public bool FofNoConnectedElements = false;
        public bool FofWantnukewarning = false;
        public bool FofNorecursereparse = false;

        public ushort Flag
        {
            get
            {
                ushort returnValue = 0;

                if (FofMultidestfiles)
                    returnValue |= (ushort)FileopFlagsEnum.FOF_MULTIDESTFILES;
                if (FofConfirmmouse)
                    returnValue |= (ushort)FileopFlagsEnum.FOF_CONFIRMMOUSE;
                if (FofSilent)
                    returnValue |= (ushort)FileopFlagsEnum.FOF_SILENT;
                if (FofRenameoncollision)
                    returnValue |= (ushort)FileopFlagsEnum.FOF_RENAMEONCOLLISION;
                if (FofNoconfirmation)
                    returnValue |= (ushort)FileopFlagsEnum.FOF_NOCONFIRMATION;
                if (FofWantmappinghandle)
                    returnValue |= (ushort)FileopFlagsEnum.FOF_WANTMAPPINGHANDLE;
                if (FofAllowundo)
                    returnValue |= (ushort)FileopFlagsEnum.FOF_ALLOWUNDO;
                if (FofFilesonly)
                    returnValue |= (ushort)FileopFlagsEnum.FOF_FILESONLY;
                if (FofSimpleprogress)
                    returnValue |= (ushort)FileopFlagsEnum.FOF_SIMPLEPROGRESS;
                if (FofNoconfirmmkdir)
                    returnValue |= (ushort)FileopFlagsEnum.FOF_NOCONFIRMMKDIR;
                if (FofNoerrorui)
                    returnValue |= (ushort)FileopFlagsEnum.FOF_NOERRORUI;
                if (FofNocopysecurityattribs)
                    returnValue |= (ushort)FileopFlagsEnum.FOF_NOCOPYSECURITYATTRIBS;
                if (FofNorecursion)
                    returnValue |= (ushort)FileopFlagsEnum.FOF_NORECURSION;
                if (FofNoConnectedElements)
                    returnValue |= (ushort)FileopFlagsEnum.FOF_NO_CONNECTED_ELEMENTS;
                if (FofWantnukewarning)
                    returnValue |= (ushort)FileopFlagsEnum.FOF_WANTNUKEWARNING;
                if (FofNorecursereparse)
                    returnValue |= (ushort)FileopFlagsEnum.FOF_NORECURSEREPARSE;

                return returnValue;
            }
        }
    }

}
