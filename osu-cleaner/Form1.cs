/**
* TcNo-osu-cleaner
* Version: 2.1.1
* Original project: henntix
* Updated & Styling: TechNobo (https://tcno.co)
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using DarkUI.Forms;
using Microsoft.VisualBasic.FileIO;
using Microsoft.Win32;
using osu_cleaner;
using osu_cleaner.Properties;

namespace osu_cleaner
{
    public partial class MainApp : DarkForm
    {
        private readonly string versionNumber = "2.1.1";
        private readonly ContextMenuStrip _collectionRoundMenuStrip = new ContextMenuStrip();
        private long _filesSize;
        private long _forRemovalSize;
        private readonly List<string> _foundElements = new List<string>();

        // Context menu
        private string _selectedMenuItem;
        private BackgroundWorker _worker, _delWorker, _listUpdater;

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
            var folder = new FolderBrowserDialog();
            folder.ShowNewFolderButton = false;
            folder.RootFolder = Environment.SpecialFolder.MyComputer;
            folder.Description = "Select an osu! root directory:";
            folder.SelectedPath = directoryPath.Text;
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
                throw;
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

        private string GetOsuPath()
        {
            using (var osureg = Registry.ClassesRoot.OpenSubKey("osu\\DefaultIcon"))
            {
                if (osureg != null)
                {
                    var osukey = osureg.GetValue(null).ToString();
                    var osupath = osukey.Remove(0, 1);
                    osupath = osupath.Remove(osupath.Length - 11);
                    return osupath;
                }

                return "";
            }
        }

        private string GetBgPath(string path)
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
                                var tmp = "\\" + items[2].Replace("\"", string.Empty);
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

        private List<string> GetSbElements(string file)
        {
            var sbElements = new List<string>();
            using (var sbFile = File.OpenText(file))
            {
                string line;
                while ((line = sbFile.ReadLine()) != null)
                {
                    var items = line.Split(',');
                    if (items[0] == "Sprite")
                    {
                        var tmp = "\\" + items[3].Replace("\"", string.Empty);
                        tmp = tmp.Replace("/", "\\");
                        if (!sbElements.Contains(tmp))
                            sbElements.Add(tmp);
                    }
                }
            }

            return sbElements;
        }

        private long GetFileSize(string path)
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
            Process.Start("explorer.exe", string.Format("/select,\"{0}\"", fileFullPath));
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

        private List<string> queueRemovedItems = new List<string>();
        private readonly Mutex m = new Mutex();

        private void QueueRemoveString(string toRemove)
        {
            m.WaitOne();
            try
            {
                queueRemovedItems.Add(toRemove);
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

        List<string> currentQueue = new List<string>();
        private void UpdateListbox()
        {
            m.WaitOne();
            try
            {
                if (queueRemovedItems != null && queueRemovedItems.Count > 0)
                {
                    currentQueue = queueRemovedItems.ToList();
                    queueRemovedItems.Clear();
                }
            }
            finally
            {
                m.ReleaseMutex();
            }

            if (currentQueue != null && currentQueue.Count > 0)
            {
                elementList.Invoke(() => { elementList.BeginUpdate(); });
                foreach (string s in currentQueue)
                {
                    elementList.Invoke(() => { elementList.Items.Remove(s); });
                }

                elementList.Invoke(() => { elementList.EndUpdate(); });

                Thread.Sleep(240);
            }
        }


        private void openMoved_Click(object sender, EventArgs e)
        {
            Process.Start(Path.Combine(directoryPath.Text, "Cleaned"));
        }

        private void lblTTCNOWeb_Click(object sender, EventArgs e)
        {
            Process.Start("https://tcno.co");
        }

        private void tblLogoButtons_Paint(object sender, PaintEventArgs e)
        {

        }

        private void logoBox_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/TcNobo/TcNo-osu-Cleaner");
        }

        private void logoBox_MouseLeave(object sender, EventArgs e)
        {
            logoBox.Image = Resources.osu_cleaner_logo_256;
        }

        private void logoBox_MouseDown(object sender, MouseEventArgs e)
        {
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
