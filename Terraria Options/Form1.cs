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
using System.IO;

namespace Terraria_Options
{
    public partial class Form1 : Form
    {
        private bool nolaunch = false;
        public Form1()
        {
            GenRoamingFiles();
            InitializeComponent();
            StringCollection col = new Config().SavePath;
            if (col == null || col.Count < 2)
            {
                col = new StringCollection
                {
                    Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\My Games\\Terraria",
                    "Players",
                    "Worlds"
                };
                Config c = new Config
                {
                    SavePath = col
                };
                c.Save();
            }
            //Load worlds & players
            LoadWorlds();
            LoadPlayers();
        }
        public Form1(string arg)
        {
            GenRoamingFiles();
            InitializeComponent();
            StringCollection col = new Config().SavePath;
            if (col == null || col.Count < 2)
            {
                col = new StringCollection
                {
                    Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\My Games\\Terraria",
                    "Players",
                    "Worlds"
                };
                Config c = new Config
                {
                    SavePath = col
                };
                c.Save();
            }
            //Load worlds & players
            LoadWorlds();
            LoadPlayers();
            if (arg.Trim().ToLower() == "nolaunch")
            {
                this.nolaunch = true;
                launchButton.Enabled = false;
                launchButton.Visible = false;
            }
        }
        private void LoadWorlds()
        {
            worldComboBox.Items.Clear();
            StringCollection col = new Config().SavePath;
            string[] files = Directory.GetFiles(col[0] + "\\" + col[2], "*.wld", SearchOption.TopDirectoryOnly);
            List<string> worlds = new List<string>();
            foreach (string s in files)
                worlds.Add(s.Split('\\')[s.Split('\\').Length - 1]);
            worldComboBox.Items.AddRange(worlds.ToArray<string>());
        }
        private void LoadPlayers()
        {
            playerComboBox.Items.Clear();
            StringCollection col = new Config().SavePath;
            string[] files = Directory.GetFiles(col[0] + "\\" + col[1], "*.plr", SearchOption.TopDirectoryOnly);
            List<string> players = new List<string>();
            foreach (string s in files)
                players.Add(s.Split('\\')[s.Split('\\').Length - 1]);
            playerComboBox.Items.AddRange(players.ToArray<string>());
        }
        //Creates the files in the user's Roaming folder for my Death Game Launcher to utilize
        private void GenRoamingFiles()
        {
            try
            {
                //Variables for the paths to each file
                string hdFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "HoodedDeath");
                string folder = Path.Combine(hdFolder, "Terraria Options");
                string toFile = Path.Combine(folder, "Terraria Options.txt");
                string readmeFile = Path.Combine(folder, "readme.txt");
                //Makes sure the folder "HoodedDeath" exists in the roaming folder
                if (!Directory.Exists(hdFolder))
                    Directory.CreateDirectory(hdFolder);
                //Makes sure the folder "Terraria Options" exists in the "HoodedDeath" folder
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);
                //Writes the executable path into the "TO.txt" file by removing the file if it exists, then creating/re-creating the file, to avoid overwrite issues
                if (File.Exists(toFile))
                    File.Delete(toFile);
                //File.Create(toFile);
                StreamWriter sw = new StreamWriter(File.OpenWrite(toFile));
                sw.Write(Application.ExecutablePath);
                sw.Close();
                sw.Dispose();
                //Writes the warning file "readme.txt" to let the user know why the "TO.txt" file is needed
                if (!File.Exists(readmeFile))
                {
                    //File.Create(readmeFile);
                    sw = null; sw = new StreamWriter(File.OpenWrite(readmeFile));
                    sw.Write("Please do NOT delete these files, they are used with the corresponding Death Game Launcher.\r\n\r\nTerraria Options.txt is used to tell the game launcher where to find the Terraria Options application.\r\n\r\nIf Death Game Launcher is not able to find the executable for the Terraria Options application, launch Terraria Options and it should fix the path issue.");
                    sw.Close();
                    sw.Dispose();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void SettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Open SettingsForm
            using (var form = new SettingsForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    worldComboBox.SelectedItem = null;
                    playerComboBox.SelectedItem = null;
                    LoadWorlds();
                    LoadPlayers();
                }
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Show About/Help pop-up
            new AboutForm().ShowDialog();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.nolaunch)
                e.Cancel = false;
            else if (MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo) == DialogResult.Yes)
                e.Cancel = false;
            else
                e.Cancel = true;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Application.Exit();
        }

        private void LaunchButton_Click(object sender, EventArgs e)
        {
            System.Collections.Specialized.StringCollection t = new Config().SavePath;
            //System.Diagnostics.Process.Start("explorer.exe", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)); //"steam://rungameid/105600");
            /*MessageBox.Show(t[0]);
            MessageBox.Show(t[0]+t[1]);
            MessageBox.Show(t[0]+t[2]);
            textBox1.Text = t[0] + t[1];
            return;*/
            System.Diagnostics.Process.Start("explorer.exe", t[0]);
            System.Diagnostics.Process.Start("explorer.exe", t[0] + "\\" + t[1]);
            System.Diagnostics.Process.Start("explorer.exe", t[0] + "\\" + t[2]);
        }

        private void WorldBackupButton_Click(object sender, EventArgs e)
        {
            string item = worldComboBox.SelectedItem.ToString();
            if (item != null && item != "")
            {
                StringCollection sc = new Config().SavePath;
                File.Copy(sc[0] + "\\" + sc[2] + "\\" + item, sc[0] + "\\" + sc[2] + "\\" + item + ".BACKUP", true);
                MessageBox.Show("Backup of " + item + " completed.");
            }
            else
                MessageBox.Show("No item selected.");
        }

        private void PlayerBackupButton_Click(object sender, EventArgs e)
        {
            string item = playerComboBox.SelectedItem.ToString();
            if (item != null && item != "")
            {
                StringCollection sc = new Config().SavePath;
                File.Copy(sc[0] + "\\" + sc[1] + "\\" + item, sc[0] + "\\" + sc[1] + "\\" + item + ".BACKUP", true);
                MessageBox.Show("Backup of " + item + " completed.");
            }
            else
                MessageBox.Show("No item selected.");
        }

        private void PlayerEdit_Click(object sender, EventArgs e)
        {

        }
    }
}
