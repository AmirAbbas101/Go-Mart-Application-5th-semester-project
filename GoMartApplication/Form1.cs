using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GoMartApplication
{
    public partial class LoginFrom : Form
    {
        DBConnect dbCon = new DBConnect();
        public static string loginname, logintype;
        public LoginFrom()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void LoginFrom_Load(object sender, EventArgs e)
        {
            cmbRole.SelectedIndex = 1;
            txtUserName.Text = "Amir Abbas";
            txtPassword.Text = "12345";
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbRole.SelectedIndex>0)
                {
                    if(txtUserName.Text == String.Empty)
                    {
                        MessageBox.Show("Please Enter valid UserName.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtUserName.Focus();
                        return;
                    }
                    if(txtPassword.Text == String.Empty)
                    {
                        MessageBox.Show("Please Enter valid Password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtUserName.Focus();
                        return;
                    }
                    if(cmbRole.SelectedIndex > 0 && txtUserName.Text != String.Empty && txtPassword.Text != String.Empty)
                    {
                        // Login Code
                        if (cmbRole.Text == "Admin")
                        {
                            SqlCommand cmd = new SqlCommand("SELECT AdminID,APassword,FullName FROM tblAdmin WHERE FullName=@FullName AND APassword=@APassword",dbCon.GetCon());
                            cmd.Parameters.AddWithValue("@FullName", txtUserName.Text.Trim());
                            cmd.Parameters.AddWithValue("@APassword", txtPassword.Text.Trim());
                            dbCon.OpenCon();
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {
                                MessageBox.Show("Login Success Welcome to Home Page","Success",MessageBoxButtons.OK, MessageBoxIcon.Information);
                                loginname = txtUserName.Text;
                                logintype = cmbRole.Text;
                                clrValues();
                                this.Hide();
                                frmMain fm = new frmMain();
                                fm.Show();
                            }
                            else
                            {
                                MessageBox.Show("Invalid Login Please check userName and Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else if(cmbRole.Text == "Seller")
                        {
                                SqlCommand cmd = new SqlCommand("SELECT SellerName,SellerPass FROM tblSeller WHERE SellerName=@SellerName AND SellerPass=@SellerPass", dbCon.GetCon());
                                cmd.Parameters.AddWithValue("@SellerName", txtUserName.Text.Trim());
                                cmd.Parameters.AddWithValue("@SellerPass", txtPassword.Text.Trim());
                                dbCon.OpenCon();
                                SqlDataAdapter da = new SqlDataAdapter(cmd);
                                DataTable dt = new DataTable();
                                da.Fill(dt);
                                if (dt.Rows.Count > 0)
                                {
                                    MessageBox.Show("Login Success Welcome to Home Page", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    loginname = txtUserName.Text;
                                    logintype = cmbRole.Text;
                                    clrValues();
                                    this.Hide();
                                    frmMain fm = new frmMain();
                                    fm.Show();
                                }
                                else
                                {
                                    MessageBox.Show("Invalid Login Please check userName and Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                        }

                    }
                    else
                    {
                        MessageBox.Show("Please Enter UserName or Password","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                        clrValues();
                    }
                }
                else 
                {
                    MessageBox.Show("Please Select any role.","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    clrValues();
                }
            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void clrValues()
        {
            cmbRole.SelectedIndex = 0;
            txtUserName.Clear();
            txtPassword.Clear();
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            clrValues();
        }
    }
}
