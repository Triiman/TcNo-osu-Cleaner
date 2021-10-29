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

namespace osu_cleaner.Properties
{
	public partial class SelectUser : Form
	{
		public string ReturnedUsername { get; set;}
		public string ReturnedFilename { get; set;}

		private readonly string _installDir;

		public SelectUser(string installDir)
		{
			InitializeComponent();
			_installDir = installDir;
		}

		private void SelectUser_Load(object sender, EventArgs e)
		{
			if (listAccounts.Items.Count != 0) return;
			foreach (var f in new DirectoryInfo(_installDir).GetFiles("osu!*.cfg").OrderBy(f => f.Name).ToArray())
			{
				if (f.Name == "osu!.cfg") continue;
				var user = f.Name.Substring(5, f.Name.Length - 9);
				listAccounts.Items.Add(user);
			}

		}

		private void BtnUsernameOK_Click(object sender, EventArgs e)
		{
			if ((string)listAccounts.SelectedItem == null) return;

			ReturnedUsername = (string)listAccounts.SelectedItem;
			ReturnedFilename = "osu!." + (string)listAccounts.SelectedItem + ".cfg";
			DialogResult = DialogResult.OK;
			Close();
		}

		private void BtnWhyUsername_Click(object sender, EventArgs e)
		{
			this.Height = this.Height != 306 ? 306 : 200;
		}
	}
}
