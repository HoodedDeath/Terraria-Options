using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Terraria_Options
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
            richTextBox1.Text = "This application is made to help with Terraria worlds and players.\r\n\r\nSelecting a player or a world and clicking \"Backup\" under the drop-down box. Terraria does make its own backups, but on a rare occasion, there is a chance the world will become corrupted and Terraria may overwrite the previous backup with a backup of the corrupted version.\r\n\r\nDue to that, this application will save a backup by adding \".BACKUP\" to the end of the file name.\r\n\r\nIn the future, this application my implement a player/world editor.";
        }

        private void AboutForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }
    }
}
