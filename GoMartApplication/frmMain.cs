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
            AboutBox1 about = new AboutBox1();
            about.Show();
        }
    }
}
