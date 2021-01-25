/**
* osu-cleaner
* Version: 1.00
* Author: henntix
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.VisualBasic.FileIO;
using Microsoft.Win32;
namespace osu_cleaner
{
    public partial class MainApp : Form
    {
        BackgroundWorker _worker;
        private long _filesSize;
        private long _forRemovalSize;
        private List<string> _foundElements = new List<string>();

        // Context menu
        private string _selectedMenuItem;
        private readonly ContextMenuStrip _collectionRoundMenuStrip = new ContextMenuStrip();

        public MainApp()
        {
            InitializeComponent();
        }

        private void MainApp_Load(object sender, EventArgs e)
        {
            directoryPath.Text = GetOsuPath();
            _worker = new BackgroundWorker();
            _worker.DoWork += new DoWorkEventHandler(FindElements);
            _worker.ProgressChanged += new ProgressChangedEventHandler(ProgressBar);
            _worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(FindComplete);
            _worker.WorkerReportsProgress = true;
            _worker.WorkerSupportsCancellation = true;

            // Context menu
            var tsOpenFile = new ToolStripMenuItem { Text = "Open file" };
            tsOpenFile.Click += tsOpenFile_Click;
            var tsOpenFolder = new ToolStripMenuItem { Text = "Open Folder" };
            tsOpenFolder.Click += tsOpenFolder_Click;
            var tsCopyFilePath = new ToolStripMenuItem { Text = "Copy file path" };
            tsCopyFilePath.Click += tsCopyFilePath_Click;
            _collectionRoundMenuStrip.Items.AddRange(new ToolStripItem[] { tsOpenFile, tsOpenFolder, tsCopyFilePath });
        }

        private void directorySelectButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            folder.ShowNewFolderButton = false;
            folder.RootFolder = Environment.SpecialFolder.MyComputer;
            folder.Description = "Select an osu! root directory:";
            folder.SelectedPath = directoryPath.Text;
            DialogResult path = folder.ShowDialog();
            if (path == DialogResult.OK)
            {
                //check if osu!.exe is present
                if (!File.Exists(folder.SelectedPath + "\\osu!.exe"))
                {
                    MessageBox.Show("Not a valid osu! directory!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    directorySelectButton_Click(sender, e);
                    return;
                }
            }
            directoryPath.Text = folder.SelectedPath + "\\";
        }

        private void findButton_Click(object sender, EventArgs e)
        {
            cancelButton.Visible = true;
            elementList.Items.Clear();
            _filesSize = 0;
            filesSizeLabel.Text = "Found: " + Math.Round((double)(_filesSize) / 1048576, 4) + " MB";
            _forRemovalSize = 0;
            forRemovalSizeLabel.Text = "Selected for removal: " + Math.Round((double)(_forRemovalSize) / 1048576, 4) + " MB";
            _worker.RunWorkerAsync();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            if (_worker.IsBusy)
                _worker.CancelAsync();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            List<string> delete = new List<string>();
            foreach (string file in elementList.CheckedItems)//adding items to temporary collection to let me delete items from on-screen list
                delete.Add(file);
            if (moveCheckBox.Checked) Directory.CreateDirectory(directoryPath.Text + "Cleaned");

            foreach (string file in delete)
            {
                try
                {
                    _filesSize -= GetFileSize(file);
                    if (DeletePermanentlyCheckbox.Checked) FileSystem.DeleteFile(file, UIOption.OnlyErrorDialogs, RecycleOption.DeletePermanently);
                    else if (moveCheckBox.Checked)
                    {
                        FileInfo fileInfo = new FileInfo(file);
                        string relativePath = fileInfo.Directory.FullName.Replace(directoryPath.Text + "Songs", directoryPath.Text + "Cleaned\\");
                        if (!Directory.Exists(relativePath))
                            Directory.CreateDirectory(relativePath);
                        File.Move(file, relativePath + "\\" + fileInfo.Name);
                    }
                    else FileSystem.DeleteFile(file, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
                }
                catch (FileNotFoundException) { }
                catch (NotSupportedException) { }

                elementList.Items.Remove(file);
                filesSizeLabel.Text = "Found: " + Math.Round((double)(_filesSize) / 1048576, 4) + " MB";
            }
        }

        private void selectAllButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < elementList.Items.Count; i++)
                elementList.SetItemChecked(i, true);
            _forRemovalSize = 0;
            foreach (string file in elementList.CheckedItems)
            {
                FileInfo sizeInfo = new FileInfo(file);
                _forRemovalSize += sizeInfo.Length;
            }
            forRemovalSizeLabel.Text = "Selected for removal: " + Math.Round((double)(_forRemovalSize) / 1048576, 4) + " MB";
        }

        private void deselectAllButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < elementList.Items.Count; i++)
                elementList.SetItemChecked(i, false);
            _forRemovalSize = 0;
            forRemovalSizeLabel.Text = "Selected for removal: " + Math.Round((double)(_forRemovalSize) / 1048576, 4) + " MB";
        }

        private void elementList_SelectedIndexChanged(object sender, EventArgs e)
        {
            _forRemovalSize = 0;
            foreach (string file in elementList.CheckedItems)
            {
                FileInfo sizeInfo = new FileInfo(file);
                _forRemovalSize += sizeInfo.Length;
            }
            forRemovalSizeLabel.Text = "Selected for removal: " + Math.Round((double)(_forRemovalSize) / 1048576, 4) + " MB";
        }

        private void DeletePermanentlyCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if(DeletePermanentlyCheckbox.Checked) moveCheckBox.Checked = false;
        }

        private void moveCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if(moveCheckBox.Checked) DeletePermanentlyCheckbox.Checked = false;
            if (moveCheckBox.Checked) deleteButton.Text = "Move";
            else deleteButton.Text = "Delete";
        }

        private bool RegexMatch(string str, string regex)
        {
            Regex r = new Regex(regex, RegexOptions.IgnoreCase);
            return r.IsMatch(str);
        }

        private void FindElements(object sender, DoWorkEventArgs e)
        {
            int folderCount = Directory.GetDirectories(directoryPath.Text + "Songs").Length;
            Console.WriteLine(folderCount);
            int current = 0;
            foreach (string d in Directory.GetDirectories(directoryPath.Text + "Songs"))
            {
                if (!allUncommon.Checked && sbDeleteCheckbox.Checked)
                {
                    //whitelisting BG from deletion (often BG files are used in SB)
                    List<string> whitelist = new List<string>();
                    foreach (string file in Directory.GetFiles(d))
                        if (Regex.IsMatch(file, "osu$"))
                        {
                            string bg = GetBgPath(file);
                            if (bg != null && !whitelist.Contains(bg))
                                whitelist.Add(bg);
                        }

                    foreach (string file in Directory.GetFiles(d))
                    {
                        if (Regex.IsMatch(file, "osb$"))
                        {
                            List<string> sbElements = GetSbElements(file);
                            foreach (string sbElement in sbElements)
                            {
                                if (!whitelist.Contains(sbElement))
                                {
                                    long size = GetFileSize(d + sbElement);
                                    if (size != 0)
                                    {
                                        _foundElements.Add(d + sbElement);
                                        _filesSize += size;
                                    }
                                }
                            }
                        }
                    }
                }


                List<string> bgElements = new List<string>();
                foreach (string file in Directory.GetFiles(d))
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
                            string bg = GetBgPath(file);
                            if (bg != null && !bgElements.Contains(bg))
                            {
                                long size = GetFileSize(d + bg);
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
                        if (!RegexMatch(fileName, "(avi|wmv|flv|mp4|mpg|mov|mkv|m4v|mpeg|3gp|mkv|webm|osu|png|jpeg|jpg|png|bmp|osb|osu|mp3|aac|wav|ogg|txt)$"))
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
                        if (RegexMatch(fileName, "^(applause|approachcircle|button-|combobreak|comboburst|" +
                                                 "count|cursor|default-|failsound|followpoint|fruit-|go.png|" +
                                                 "go@2x.png|gos.png|gos@2x.png|hit0|hit100|hit300|hit50|" +
                                                 "hitcircle|inputoverlay-|lighting.png|lighting@2x.png|mania-|" +
                                                 "menu.|menu-back|particle100|particle300|particle50|pause-|" +
                                                 "pippidon|play-|ranking-|ready|reversearrow|score-|scorebar-|" +
                                                 "sectionfail|sectionpass|section-|selection-|sliderb|sliderfollowcircle|" +
                                                 "sliderscorepoint|spinnerbonus|spinner-|spinnerspin|star.png|" +
                                                 "star@2x.png|star2.png|star2@2x.png|taiko-|taikobigcircle|" +
                                                 "taikohitcircle)"))
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
                _worker.ReportProgress((int)((double)current / folderCount * 100));
            }
            _worker.ReportProgress(100);
        }

        private void ProgressBar(object sender, ProgressChangedEventArgs e)
        {
            FindProgressBar.Value = e.ProgressPercentage;
            filesSizeLabel.Text = "Found: " + Math.Round((double)(_filesSize) / 1048576, 4) + " MB";
        }

        private void FindComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            foreach (string file in _foundElements)
                    elementList.Items.Add(file);
            filesSizeLabel.Text = "Found: " + Math.Round((double)(_filesSize) / 1048576, 4) + " MB";
            _foundElements.Clear();
            cancelButton.Visible = false;
            FindProgressBar.Value = 0;
        }

        private string GetOsuPath()
        {
            using (RegistryKey osureg = Registry.ClassesRoot.OpenSubKey("osu\\DefaultIcon"))
            {
                if (osureg != null)
                {
                    string osukey = osureg.GetValue(null).ToString();
                    string osupath = osukey.Remove(0, 1);
                    osupath = osupath.Remove(osupath.Length - 11);
                    return osupath;
                }
                else return "";
            }
        }

        private string GetBgPath(string path)
        {
            try
            {
                using (StreamReader file = File.OpenText(path))
                {
                    string line;
                    while ((line = file.ReadLine()) != null)
                    {
                        if (Regex.IsMatch(line, "^//Background and Video events"))
                        {
                            line = file.ReadLine();
                            string[] items = line.Split(',');
                            if (items[0] == "0")
                            {
                                string tmp = "\\" + items[2].Replace("\"", string.Empty);
                                return tmp;
                            }
                        }
                    }
                    return null;
                }
            }
            catch (System.IO.PathTooLongException)
            {
                return null;
            }
        }

        private List<string> GetSbElements(string file)
        {
            List<string> sbElements = new List<string>();
            using (StreamReader sbFile = File.OpenText(file))
            {
                string line;
                while ((line = sbFile.ReadLine()) != null)
                {
                    string[] items = line.Split(',');
                    if (items[0] == "Sprite")
                    {
                        string tmp = "\\" + items[3].Replace("\"", string.Empty);
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
                FileInfo sizeInfo = new FileInfo(path);
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
            bool bChecked = allUncommon.Checked;
            bloatExtraDeleteBox.Enabled = !bChecked;
            hitSoundsDeleteCheckbox.Enabled = !bChecked;
            backgroundDeleteCheckbox.Enabled = !bChecked;
            sbDeleteCheckbox.Enabled = !bChecked;
            skinDeleteCheckbox.Enabled = !bChecked;
            videoDeleteCheckbox.Enabled = !bChecked;
        }

        private void lblTechNobo_Click(object sender, EventArgs e)
        {
            lblTechNobo.LinkVisited = true;
            System.Diagnostics.Process.Start("https://github.com/TcNobo/osu-cleaner");
        }

        private void lblHenntix_Click(object sender, EventArgs e)
        {
            lblHenntix.LinkVisited = true;
            System.Diagnostics.Process.Start("https://github.com/henntix/osu-cleaner");
        }

        // Context menu
        private void tsCopyFilePath_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(_selectedMenuItem);
        }

        private void tsOpenFolder_Click(object sender, EventArgs e)
        {
            if (!File.Exists(_selectedMenuItem)) return;
            string fileFullPath = Path.GetFullPath(_selectedMenuItem);
            System.Diagnostics.Process.Start("explorer.exe", string.Format("/select,\"{0}\"", fileFullPath));
        }

        private void tsOpenFile_Click(object sender, EventArgs e)
        {
            if (!File.Exists(_selectedMenuItem)) return;
            System.Diagnostics.Process.Start(_selectedMenuItem);
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
    }
}
