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
		string _installDir = "";

		public SelectUser(string installDir)
		{
			InitializeComponent();
			_installDir = installDir;
		}

		private void SelectUser_Load(object sender, EventArgs e)
		{
			foreach (var f in new DirectoryInfo(_installDir).GetFiles("osu!*.cfg"))
			{
				if (f.Name == "osu!.cfg") continue;
				var user = f.Name.Substring(5, f.Name.Length - 9);
				listAccounts.Items.Add(user);
			}

		}

		private void btnUsernameOK_Click(object sender, EventArgs e)
		{
			ReturnedUsername = "osu!." + (string)listAccounts.SelectedItem + ".cfg";
			DialogResult = DialogResult.OK;
			Close();
		}
	}
}
