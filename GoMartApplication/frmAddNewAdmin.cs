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
    public partial class frmAddNewAdmin : Form
    {
        DBConnect dbCon = new DBConnect();
        public frmAddNewAdmin()
        {
            InitializeComponent();
        }

        private void frmAddNewAdmin_Load(object sender, EventArgs e)
        {
            btnAdd.Visible = true;
            btnUpdate.Visible = false;
            btnDelete.Visible = false;
            BindAdmin();
        }

        private void txtClear()
        {
            txtAdminID.Clear();
            txtAdminName.Clear();
            txtAdminPass.Clear();
        }
        private void BindAdmin()
        {
            SqlCommand cmd = new SqlCommand("SELECT AdminID AS [Admin ID], FullName AS [Admin Name], APassword AS Password FROM tblAdmin;", dbCon.GetCon());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dbCon.OpenCon();
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtAdminID.Text == String.Empty)
            {
                MessageBox.Show("Please Enter AdminID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtAdminID.Focus();
                return;
            }
            else if (txtAdminName.Text == String.Empty)
            {
                MessageBox.Show("Please Enter Admin Name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtAdminName.Focus();
                return;
            }
            else if (txtAdminPass.Text == String.Empty)
            {
                MessageBox.Show("Please Enter Admin Password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtAdminPass.Focus();
                return;
            }
            else
            {
                SqlCommand cmd = new SqlCommand("SELECT FullName FROM tblAdmin WHERE FullName=@FullName", dbCon.GetCon());
                cmd.Parameters.AddWithValue("@FullName", txtAdminName.Text);
                dbCon.OpenCon();
                var result = cmd.ExecuteScalar();
                if (result != null)
                {
                    MessageBox.Show(String.Format("Admin already exist!"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtClear();
                }
                else
                {
                    try
                    {
                        cmd = new SqlCommand("spAdminInsert", dbCon.GetCon());
                        cmd.Parameters.AddWithValue("@AdminID", txtAdminID.Text);
                        cmd.Parameters.AddWithValue("@APassword", txtAdminPass.Text);
                        cmd.Parameters.AddWithValue("@FullName", txtAdminName.Text);
                        cmd.CommandType = CommandType.StoredProcedure;
                        int i = cmd.ExecuteNonQuery();
                        dbCon.ClosCon();
                        if (i > 0)
                        {
                            MessageBox.Show("Admin Inserted Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtClear();
                            BindAdmin();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                dbCon.ClosCon();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtAdminID.Text == String.Empty)
                {
                    MessageBox.Show("Please AdminID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtAdminName.Focus();
                    return;
                }
                else if (txtAdminName.Text == String.Empty)
                {
                    MessageBox.Show("Please Enter Seller Name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtAdminName.Focus();
                    return;
                }
                else if (txtAdminPass.Text == String.Empty)
                {
                    MessageBox.Show("Please Enter Admin Password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtAdminPass.Focus();
                    return;
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("SELECT FullName FROM tblAdmin WHERE FullName=@FullName", dbCon.GetCon());
                    cmd.Parameters.AddWithValue("@FullName", txtAdminName.Text);
                    dbCon.OpenCon();
                    var result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        MessageBox.Show(String.Format("Admin Name already exist."), "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtClear();
                    }
                    else
                    {
                        cmd = new SqlCommand("spAdminUpdate", dbCon.GetCon());
                        cmd.Parameters.AddWithValue("@AdminID", txtAdminID.Text);
                        cmd.Parameters.AddWithValue("@FullName", txtAdminName.Text);
                        cmd.Parameters.AddWithValue("@APassword", txtAdminPass.Text);
                        cmd.CommandType = CommandType.StoredProcedure;
                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            MessageBox.Show("Admin Updated Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtClear();
                            BindAdmin();
                            btnDelete.Visible = false;
                            btnUpdate.Visible = false;
                            btnAdd.Visible = true;
                        }
                        else
                        {
                            MessageBox.Show("Update Fail...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtClear();
                            BindAdmin();
                        }

                    }
                    dbCon.ClosCon();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtAdminID.Text == String.Empty)
                {
                    MessageBox.Show("Please Select SellerID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (txtAdminID.Text != String.Empty)
                {
                    if (DialogResult.Yes == MessageBox.Show("Do you want to delete?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                    {
                        SqlCommand cmd = new SqlCommand("spAdminDelete", dbCon.GetCon());
                        cmd.Parameters.AddWithValue("@AdminID", txtAdminID.Text);
                        dbCon.OpenCon();
                        cmd.CommandType = CommandType.StoredProcedure;
                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            MessageBox.Show("Admin Deleted Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtClear();
                            BindAdmin();
                            btnUpdate.Visible = false;
                            btnDelete.Visible = false;
                            btnAdd.Visible = true;
                        }
                        else
                        {
                            MessageBox.Show("Delete Fail...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtClear();
                            BindAdmin();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            btnDelete.Visible = true;
            btnUpdate.Visible = true;
            btnAdd.Visible = false;

            txtAdminID.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            txtAdminName.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            txtAdminPass.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
        }
    }
}
