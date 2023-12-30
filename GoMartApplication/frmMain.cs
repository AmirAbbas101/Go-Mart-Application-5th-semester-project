using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GoMartApplication
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            if(LoginFrom.loginname != null)
            {
                toolStripStatusLabel2.Text = LoginFrom.loginname;
            }
            if(LoginFrom.logintype != null && LoginFrom.logintype == "Seller")
            {
                categoryToolStripMenuItem.Enabled = false;
                productToolStripMenuItem.Enabled = false;
                addUserToolStripMenuItem.Enabled = false;   
            }
        }

        private void categoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCategory fcat = new frmCategory();
            fcat.Show();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.Show();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Do you really want to close?","Close",MessageBoxButtons.YesNo,MessageBoxIcon.Stop);
            if(dialog==DialogResult.No) 
            {
                e.Cancel = true;
            }
            else
            {
                Application.Exit();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Do you really want to close?", "Close", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
            if (dialog == DialogResult.No)
            {
                Application.Exit();
            }
        }

        private void sellerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddNewSeller fseller = new frmAddNewSeller();
            fseller.ShowDialog();
        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddNewAdmin fadmin = new frmAddNewAdmin();
            fadmin.ShowDialog();
        }
    }
}
