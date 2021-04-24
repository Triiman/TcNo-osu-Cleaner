// cln! TcNo-osu-Cleaner - A small tool, written in C#, to clean unused elements from beatmaps.
// Copyright (C) 2021 TechNobo (Wesley Pyburn)
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
        private readonly string versionNumber = "2.2";
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
            this.Text = "cln! (osu!Cleaner by TechNobo) v" + versionNumber;

            directoryPath.Text = GetOsuPath();
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
            tsOpenFile.Click += tsOpenFile_Click;
            var tsOpenFolder = new ToolStripMenuItem {Text = "Open Folder"};
            tsOpenFolder.Click += tsOpenFolder_Click;
            var tsCopyFilePath = new ToolStripMenuItem {Text = "Copy file path"};
            tsCopyFilePath.Click += tsCopyFilePath_Click;
            _collectionRoundMenuStrip.Items.AddRange(new ToolStripItem[] {tsOpenFile, tsOpenFolder, tsCopyFilePath});

            openMoved.Visible = Directory.Exists(Path.Combine(directoryPath.Text, "Cleaned"));
            elementList.Items.Clear();
        }

        private void directorySelectButton_Click(object sender, EventArgs e)
        {
            var folder = new FolderBrowserDialog
            {
                ShowNewFolderButton = false,
                RootFolder = Environment.SpecialFolder.MyComputer,
                Description = "Select an osu! root directory:",
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

                    directorySelectButton_Click(sender, e);
                    return;
                }

            directoryPath.Text = folder.SelectedPath;
            openMoved.Visible = Directory.Exists(Path.Combine(directoryPath.Text, "Cleaned"));
        }

        private void findButton_Click(object sender, EventArgs e)
        {
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

        private void cancelButton_Click(object sender, EventArgs e)
        {
            if (_worker.IsBusy)
                _worker.CancelAsync();
            if (_delWorker.IsBusy)
                _delWorker.CancelAsync();
            if (_pixelWorker.IsBusy)
                _pixelWorker.CancelAsync();
        }

        private void selectAllButton_Click(object sender, EventArgs e)
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

        private void deselectAllButton_Click(object sender, EventArgs e)
        {
            for (var i = 0; i < elementList.Items.Count; i++)
                elementList.SetItemChecked(i, false);
            _forRemovalSize = 0;
            forRemovalSizeLabel.Text =
                "Selected for removal: " + Math.Round((double) _forRemovalSize / 1048576, 4) + " MB";
        }

        private void elementList_SelectedIndexChanged(object sender, EventArgs e)
        {
            _forRemovalSize = 0;
            foreach (string file in elementList.CheckedItems)
            {
                var sizeInfo = new FileInfo(file);
                _forRemovalSize += sizeInfo.Length;
            }

            forRemovalSizeLabel.Text =
                "Selected for removal: " + Math.Round((double) _forRemovalSize / 1048576, 4) + " MB";
        }

        private void DeletePermanentlyCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (DeletePermanentlyCheckbox.Checked) moveCheckBox.Checked = false;
        }

        private void moveCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (moveCheckBox.Checked) DeletePermanentlyCheckbox.Checked = false;
            if (moveCheckBox.Checked) deleteButton.Text = "Move";
            else deleteButton.Text = "Delete";
        }

        private bool RegexMatch(string str, string regex)
        {
            var r = new Regex(regex, RegexOptions.IgnoreCase);
            return r.IsMatch(str);
        }

        private void FindElements(object sender, DoWorkEventArgs e)
        {
            int folderCount;
            try
            {
                folderCount = Directory.GetDirectories(Path.Combine(directoryPath.Text, "Songs")).Length;
            }
            catch (DirectoryNotFoundException)
            {
                MessageBox.Show("Can not find Songs directory in osu! folder.", "Fatal error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            Console.WriteLine(folderCount);
            var current = 0;
            foreach (var d in Directory.GetDirectories(Path.Combine(directoryPath.Text, "Songs")))
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
                                    if (size != 0)
                                    {
                                        _foundElements.Add(d + sbElement);
                                        _filesSize += size;
                                    }
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

                    if (!allUncommon.Checked && backgroundDeleteCheckbox.Checked)
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

                    if (allUncommon.Checked)
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

                    if (videoDeleteCheckbox.Checked)
                        if (RegexMatch(fileName, "(avi|wmv|flv|mp4|mpg|mov|mkv|m4v|mpeg|3gp|mkv|webm)$"))
                        {
                            _foundElements.Add(file);
                            _filesSize += GetFileSize(file);
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

                    if (bloatExtraDeleteBox.Checked)
                        if (RegexMatch(fileName, "(thumbs.db|desktop.ini|ds_store)$"))
                        {
                            _foundElements.Add(file);
                            _filesSize += GetFileSize(file);
                        }
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
                return osuPath;

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
                            var items = line.Split(',');
                            if (items[0] == "0")
                            {
                                var tmp = (precedeWithSlash ? "\\" : "") + items[2].Replace("\"", string.Empty);
                                return tmp;
                            }
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

        private void allUncommon_CheckedChanged(object sender, EventArgs e)
        {
            var bChecked = allUncommon.Checked;
            bloatExtraDeleteBox.Enabled = !bChecked;
            hitSoundsDeleteCheckbox.Enabled = !bChecked;
            backgroundDeleteCheckbox.Enabled = !bChecked;
            sbDeleteCheckbox.Enabled = !bChecked;
            skinDeleteCheckbox.Enabled = !bChecked;
            videoDeleteCheckbox.Enabled = !bChecked;
        }

        private void lblTechNobo_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/TcNobo/TcNo-osu-Cleaner");
        }

        private void lblHenntix_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/henntix/osu-Cleaner");
        }

        // Context menu
        private void tsCopyFilePath_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(_selectedMenuItem);
        }

        private void tsOpenFolder_Click(object sender, EventArgs e)
        {
            if (!File.Exists(_selectedMenuItem)) return;
            var fileFullPath = Path.GetFullPath(_selectedMenuItem);
            Process.Start("explorer.exe", $"/select,\"{fileFullPath}\"");
        }

        private void tsOpenFile_Click(object sender, EventArgs e)
        {
            if (!File.Exists(_selectedMenuItem)) return;
            Process.Start(_selectedMenuItem);
        }

        private void elementList_MouseDown(object sender, MouseEventArgs e)
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

        private void deleteButton_Click(object sender, EventArgs e)
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

            foreach (var file in delete)
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
                                Path.Combine(directoryPath.Text, "Songs"), Path.Combine(directoryPath.Text, "Cleaned"));
                            Directory.CreateDirectory(relativePath);
                            File.Move(file, Path.Combine(relativePath, fileInfo.Name));
                        }
                    }
                    else
                    {
                        FileSystem.DeleteFile(file, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
                    }

                    current++;
                }
                catch (FileNotFoundException)
                {
                }
                catch (NotSupportedException)
                {
                }

                // Prevent cross-thread errors
                //elementList.Invoke(() => { elementList.Items.Remove(file); }); // This flickers A LOT!
                //elementList.Invoke(() => { elementList.BeginUpdate(); elementList.Items.Remove(file); elementList.EndUpdate(); }); // This flickers A LOT!
                QueueRemoveString(file);

                filesSizeLabel.Invoke(() =>
                {
                    filesSizeLabel.Text = "Found: " + Math.Round((double) _filesSize / 1048576, 4) + " MB";
                });

                _delWorker.ReportProgress((int) ((double) current / totalToDelete * 100));
            }

            _delWorker.ReportProgress(100);
            progressBarBackground.Invoke(() => { progressBarBackground.ForeColor = Color.FromArgb(80, 250, 123); });
            if (_listUpdater.IsBusy) _listUpdater.CancelAsync();
        }

        private readonly List<string> _queueRemovedItems = new List<string>();
        private readonly Mutex m = new Mutex();

        private void QueueRemoveString(string toRemove)
        {
            m.WaitOne();
            try
            {
                _queueRemovedItems.Add(toRemove);
            }
            finally {
                m.ReleaseMutex();
            }
        }

        // Loop to update the list only once a second while deleting
        private void ListboxUpdater(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = (BackgroundWorker)sender;
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

        List<string> _currentQueue = new List<string>();
        private void UpdateListbox()
        {
            m.WaitOne();
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
                m.ReleaseMutex();
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


        private void openMoved_Click(object sender, EventArgs e)
        {
            Process.Start(Path.Combine(directoryPath.Text, "Cleaned"));
        }

        private void lblTCNOWeb_MouseDown(object sender, MouseEventArgs e)
        {
            Process.Start("https://tcno.co");
        }
        private void lblTechNobo_MouseDown(object sender, MouseEventArgs e)
        {
            Process.Start("https://github.com/TcNobo/TcNo-osu-Cleaner");
        }

        private void lblHenntix_MouseDown(object sender, MouseEventArgs e)
        {
            Process.Start("https://github.com/henntix/osu-Cleaner");
        }

        private void logoBox_MouseLeave(object sender, EventArgs e)
        {
            logoBox.Image = Resources.osu_cleaner_logo_256;
        }

        private void logoBox_MouseDown(object sender, MouseEventArgs e)
        {
            Process.Start("https://github.com/TcNobo/TcNo-osu-Cleaner");
            logoBox.Image = Resources.osu_cleaner_logo_256_click;
        }

        private void logoBox_HoverImage(object sender, EventArgs e)
        {
            logoBox.Image = Resources.osu_cleaner_logo_256_hover;
        }

        private void linkLabel_MouseEnter(object sender, EventArgs e)
        {
            var tssl = (ToolStripStatusLabel)sender;
            tssl.LinkColor = Color.FromArgb(138, 255, 128);
        }

        private void linkLabel_MouseLeave(object sender, EventArgs e)
        {
            var tssl = (ToolStripStatusLabel)sender;
            tssl.LinkColor = Color.FromArgb(248, 248, 242);
        }

        private void DeleteComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            cancelButton.Visible = false;
            FindProgressBar.Value = 0;
            FindProgressBar.Hide();
            if (moveCheckBox.Checked) openMoved.Visible = true;
        }



        private void symlinkButton_Click(object sender, EventArgs e)
        {
            var songsFolder = Path.Combine(directoryPath.Text, "Songs");
            var isSymlink = new DirectoryInfo(songsFolder).IsSymbolicLink();
            var isJunction = JunctionPoint.Exists(songsFolder);
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
            if (path == DialogResult.OK)
            {
                var fo = new InteropSHFileOperation
                {
                    wFunc = InteropSHFileOperation.FO_Func.FO_MOVE,
                    fFlags =
                    {
                        FOF_ALLOWUNDO = false,
                        FOF_NOCONFIRMATION = true,
                        FOF_NOERRORUI = false,
                        FOF_SILENT = false
                    },
                    pFrom = songsFolder,
                    pTo = Path.Combine(folder.SelectedPath, "Songs")
                };
                if (fo.Execute())
                {
                    // Success
                    JunctionPoint.Create(songsFolder, Path.Combine(folder.SelectedPath, "Songs"), true);
                    MessageBox.Show(this, "Moved files, and created symlink junction correctly!", "Success!",
                        MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
                else
                {
                    MessageBox.Show(this, "Failed to copy files", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1);
                }
            }
        }

        private void btnReplaceMissing_Click(object sender, EventArgs e)
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
                folderCount = Directory.GetDirectories(Path.Combine(directoryPath.Text, "Songs")).Length;
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
            foreach (var d in Directory.GetDirectories(Path.Combine(directoryPath.Text, "Songs")))
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

public class InteropSHFileOperation
{
    // http://pinvoke.net/default.aspx/shell32/SHFileOperation.html
    public enum FO_Func : uint
    {
        FO_MOVE = 0x0001,
        FO_COPY = 0x0002,
        FO_DELETE = 0x0003,
        FO_RENAME = 0x0004,
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 2)]
    struct SHFILEOPSTRUCT
    {
        public IntPtr hwnd;
        public FO_Func wFunc;
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
    static extern int SHFileOperation([In, Out] ref SHFILEOPSTRUCT lpFileOp);

    private SHFILEOPSTRUCT _ShFile;
    public FILEOP_FLAGS fFlags;

    public IntPtr hwnd
    {
        set => this._ShFile.hwnd = value;
    }
    public FO_Func wFunc
    {
        set => this._ShFile.wFunc = value;
    }

    public string pFrom
    {
        set => this._ShFile.pFrom = value + '\0' + '\0';
    }
    public string pTo
    {
        set => this._ShFile.pTo = value + '\0' + '\0';
    }

    public bool fAnyOperationsAborted
    {
        set => this._ShFile.fAnyOperationsAborted = value;
    }
    public IntPtr hNameMappings
    {
        set => this._ShFile.hNameMappings = value;
    }
    public string lpszProgressTitle
    {
        set => this._ShFile.lpszProgressTitle = value + '\0';
    }

    public InteropSHFileOperation()
    {

        this.fFlags = new FILEOP_FLAGS();
        this._ShFile = new SHFILEOPSTRUCT();
        this._ShFile.hwnd = IntPtr.Zero;
        this._ShFile.wFunc = FO_Func.FO_COPY;
        this._ShFile.pFrom = "";
        this._ShFile.pTo = "";
        this._ShFile.fAnyOperationsAborted = false;
        this._ShFile.hNameMappings = IntPtr.Zero;
        this._ShFile.lpszProgressTitle = "";

    }

    public bool Execute()
    {
        this._ShFile.fFlags = this.fFlags.Flag;
        return SHFileOperation(ref this._ShFile) == 0;//true if no errors
    }

    public class FILEOP_FLAGS
    {
        [Flags]
        private enum FILEOP_FLAGS_ENUM : ushort
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

        public bool FOF_MULTIDESTFILES = false;
        public bool FOF_CONFIRMMOUSE = false;
        public bool FOF_SILENT = false;
        public bool FOF_RENAMEONCOLLISION = false;
        public bool FOF_NOCONFIRMATION = false;
        public bool FOF_WANTMAPPINGHANDLE = false;
        public bool FOF_ALLOWUNDO = false;
        public bool FOF_FILESONLY = false;
        public bool FOF_SIMPLEPROGRESS = false;
        public bool FOF_NOCONFIRMMKDIR = false;
        public bool FOF_NOERRORUI = false;
        public bool FOF_NOCOPYSECURITYATTRIBS = false;
        public bool FOF_NORECURSION = false;
        public bool FOF_NO_CONNECTED_ELEMENTS = false;
        public bool FOF_WANTNUKEWARNING = false;
        public bool FOF_NORECURSEREPARSE = false;

        public ushort Flag
        {
            get
            {
                ushort returnValue = 0;

                if (this.FOF_MULTIDESTFILES)
                    returnValue |= (ushort)FILEOP_FLAGS_ENUM.FOF_MULTIDESTFILES;
                if (this.FOF_CONFIRMMOUSE)
                    returnValue |= (ushort)FILEOP_FLAGS_ENUM.FOF_CONFIRMMOUSE;
                if (this.FOF_SILENT)
                    returnValue |= (ushort)FILEOP_FLAGS_ENUM.FOF_SILENT;
                if (this.FOF_RENAMEONCOLLISION)
                    returnValue |= (ushort)FILEOP_FLAGS_ENUM.FOF_RENAMEONCOLLISION;
                if (this.FOF_NOCONFIRMATION)
                    returnValue |= (ushort)FILEOP_FLAGS_ENUM.FOF_NOCONFIRMATION;
                if (this.FOF_WANTMAPPINGHANDLE)
                    returnValue |= (ushort)FILEOP_FLAGS_ENUM.FOF_WANTMAPPINGHANDLE;
                if (this.FOF_ALLOWUNDO)
                    returnValue |= (ushort)FILEOP_FLAGS_ENUM.FOF_ALLOWUNDO;
                if (this.FOF_FILESONLY)
                    returnValue |= (ushort)FILEOP_FLAGS_ENUM.FOF_FILESONLY;
                if (this.FOF_SIMPLEPROGRESS)
                    returnValue |= (ushort)FILEOP_FLAGS_ENUM.FOF_SIMPLEPROGRESS;
                if (this.FOF_NOCONFIRMMKDIR)
                    returnValue |= (ushort)FILEOP_FLAGS_ENUM.FOF_NOCONFIRMMKDIR;
                if (this.FOF_NOERRORUI)
                    returnValue |= (ushort)FILEOP_FLAGS_ENUM.FOF_NOERRORUI;
                if (this.FOF_NOCOPYSECURITYATTRIBS)
                    returnValue |= (ushort)FILEOP_FLAGS_ENUM.FOF_NOCOPYSECURITYATTRIBS;
                if (this.FOF_NORECURSION)
                    returnValue |= (ushort)FILEOP_FLAGS_ENUM.FOF_NORECURSION;
                if (this.FOF_NO_CONNECTED_ELEMENTS)
                    returnValue |= (ushort)FILEOP_FLAGS_ENUM.FOF_NO_CONNECTED_ELEMENTS;
                if (this.FOF_WANTNUKEWARNING)
                    returnValue |= (ushort)FILEOP_FLAGS_ENUM.FOF_WANTNUKEWARNING;
                if (this.FOF_NORECURSEREPARSE)
                    returnValue |= (ushort)FILEOP_FLAGS_ENUM.FOF_NORECURSEREPARSE;

                return returnValue;
            }
        }
    }

}
