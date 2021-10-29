using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace osu_cleaner
{
    public partial class ManageReplays : Form
    {
        private readonly string _replayDir;
        Dictionary<string, List<string>> _replaysDictionary = new Dictionary<string, List<string>>();
        Dictionary<string, double> _sizesDictionary = new Dictionary<string, double>();
        private BackgroundWorker _worker, _delWorker;
        private string _selectedUser = "";


        public ManageReplays(string replayDir)
        {
            InitializeComponent();
            _replayDir = Path.Combine(replayDir, "Data\\r\\");

            if (Directory.Exists(_replayDir)) return;

            MessageBox.Show("No replay directory", "Can not find replay directory", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            Close();
        }

        private void ManageReplays_Load(object sender, EventArgs e)
        {
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
            _delWorker.DoWork += DelElements;
            _delWorker.ProgressChanged += ProgressBar;
            _delWorker.RunWorkerCompleted += DelComplete;
        }

        private int numFiles, curFile;
        private void ProgressBar(object sender, ProgressChangedEventArgs e)
        {
            pbScan.Value = curFile;
            lblNumFiles.Text = curFile + " / " + numFiles;
        }

        #region Delete files
        private void btnDeleteReplays_Click(object sender, EventArgs e)
        {
            if (listAccounts.SelectedItem == null) return;

            var user = (string)listAccounts.SelectedItem;
            user = user.Substring(0, user.IndexOf(" - ", StringComparison.Ordinal));
            numFiles = _replaysDictionary[user].Count;
            pbScan.Maximum = numFiles;
            _selectedUser = user;

            btnDeleteReplays.Visible = false;
            pbScan.Visible = true;
            pbScan.Value = 0;
            lblNumFiles.Visible = true;
            listAccounts.Visible = false;
            _delWorker.RunWorkerAsync();
        }

        private void DelElements(object sender, DoWorkEventArgs e)
        {
            for (var i = 0; i < _replaysDictionary[_selectedUser].Count; i++)
            {
                curFile = i;

                var replayFile = _replaysDictionary[_selectedUser][i];
                if (File.Exists(replayFile)) File.Delete(replayFile);
                var osg = replayFile.Replace(".osr", ".osg");
                if (File.Exists(osg)) File.Delete(osg);

                if (_worker.CancellationPending) return;
                _delWorker.ReportProgress(0);
            }

            _delWorker.ReportProgress(100);
        }

        private void DelComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            pbScan.Visible = false;
            lblNumFiles.Visible = false;
            listAccounts.Visible = true;
            btnDeleteReplays.Visible = true;

            for (var i = 0; i < listAccounts.Items.Count; i++)
            {
                var curString = (string)listAccounts.Items[i];
                if (!curString.StartsWith(_selectedUser + " - ")) continue;
                listAccounts.Items.RemoveAt(i);
                break;
            }
        }

        #endregion

        #region Finding files
        private void FindComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            pbScan.Visible = false;
            lblNumFiles.Visible = false;

            listAccounts.Visible = true;
            lblStatus.Text = "2. Select an account and then an action below:";

            foreach (var k in _replaysDictionary.Keys)
            {
                listAccounts.Items.Add(k + " - " + _replaysDictionary[k].Count + " - " + Math.Round((double)_sizesDictionary[k] / 1048576, 4) + " MB");
            }

            btnDeleteReplays.Visible = true;
            btnDeleteReplays.Enabled = true;
        }

        private void FindElements(object sender, DoWorkEventArgs e)
        {
            var replayFiles = Directory.GetFiles(_replayDir, "*.osr");
            var replayFilesLen = replayFiles.Length;
            //pbScan.Maximum = replayFilesLen;

            for (var i = 0; i < replayFilesLen - 1; i++)
            {
                var user = GetReplayUsername(replayFiles[i]);

                Console.WriteLine(i + " | " + replayFiles[i] + " - " + user);

                if (!_replaysDictionary.ContainsKey(user))
                    _replaysDictionary[user] = new List<string>();
                if (!_sizesDictionary.ContainsKey(user))
                    _sizesDictionary[user] = 0;

                _replaysDictionary[user].Add(replayFiles[i]);

                var sizeInfo = new FileInfo(replayFiles[i]);
                _sizesDictionary[user] += sizeInfo.Length;

                curFile = i;

                if (_worker.CancellationPending) return;
                _worker.ReportProgress(0);
            }
            _worker.ReportProgress(100);
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            if (_worker.IsBusy) return;
            btnScan.Visible = false;
            pbScan.Visible = true;

            numFiles = Directory.GetFiles(_replayDir, "*.osr").Length;
            pbScan.Maximum = numFiles;

            _worker.RunWorkerAsync();
        }

        public static string GetReplayUsername(string fileName)
        {
            byte[] buff = null;
            var fs = new FileStream(fileName,
                FileMode.Open,
                FileAccess.Read);
            var br = new BinaryReader(fs);
            var numBytes = new FileInfo(fileName).Length;

            var replayType = br.ReadByte();
            var replayVersion = br.ReadInt32();
            var beatMapHash = ReadOsuString(ref br);
            var replayName = ReadOsuString(ref br);

            //buff = br.ReadBytes((int)numBytes);
            return replayName;
        }

        static string ReadOsuString(ref BinaryReader br)
        {
            // Info from https://osu.ppy.sh/wiki/en/osu%21_File_Formats/Osr_%28file_format%29
            var type = br.ReadByte();
            if (type == 0x00) return "";
            if (type != 0x0B) throw new Exception("String byte does not start with 0x00 or 0x0B"); // Else it is 0x0b

            // Read ULEB128 (To get length)
            // - See https://stackoverflow.com/a/3564685/5165437
            var more = true;
            var length = 0;
            var shift = 0;
            while (more)
            {
                var lower7bits = br.ReadByte();
                more = (lower7bits & 128) != 0;
                length |= (lower7bits & 0x7f) << shift;
                shift += 7;
            }

            var bytes = br.ReadBytes(length);
            var str = Encoding.UTF8.GetString(bytes);

            return str;
        }
        #endregion
    }
}
