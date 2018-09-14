using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Specialized;

namespace Terraria_Options
{
    public partial class SettingsForm : Form
    {
        private Config config = new Config();
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            try
            {
                StringCollection col = config.SavePath; // new Config().SavePath;
                dataPathBox.Text = col[0];
                playerPathBox.Text = col[1];
                worldPathBox.Text = col[2];
            }
            catch { }
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    dataPathBox.Text = fbd.SelectedPath;
                    string[] files = System.IO.Directory.GetFiles(fbd.SelectedPath);

                    MessageBox.Show("Files found: " + files.Length.ToString(), "Message");
                }
            }
        }

        private void SettingsForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            /*StringCollection col = new StringCollection
            {
                dataPathBox.Text,
                playerPathBox.Text,
                worldPathBox.Text
            };*/
            //StringCollection col = new StringCollection();
            //col.AddRange(new string[] { dataPathBox.Text, playerPathBox.Text, worldPathBox.Text });
            config.SavePath = new StringCollection
            {
                dataPathBox.Text,
                playerPathBox.Text,
                worldPathBox.Text
            };
            config.Save();
            this.DialogResult = DialogResult.OK;
            Close();
        }
    }
}
