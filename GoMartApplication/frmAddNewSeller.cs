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
    public partial class frmAddNewSeller : Form
    {
        DBConnect dbCon = new DBConnect();
        public frmAddNewSeller()
        {
            InitializeComponent();
        }

        private void frmAddNewSeller_Load(object sender, EventArgs e)
        {
            lblSellerID.Visible = false;
            btnDelete.Visible = false;
            btnUpdate.Visible = false;
            btnAdd.Visible = true;
            BindSeller();
        }
        private void txtClear()
        {
            txtSellerName.Clear();
            txtSellerAge.Clear();
            txtPhone.Clear();
            txtSellerPass.Clear();
        }
        private void BindSeller()
        {
            SqlCommand cmd = new SqlCommand("SELECT SellerID AS [Seller ID], SellerName, SellerAge, SellerPhone, SellerPass AS [Seller Password] FROM tblSeller;", dbCon.GetCon());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dbCon.OpenCon();
        }

        private void dataGridView1_Click_1(object sender, EventArgs e)
        {
            btnDelete.Visible = true;
            btnUpdate.Visible = true;
            btnAdd.Visible = false;
            lblSellerID.Visible = true;

            lblSellerID.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            txtSellerName.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            txtSellerAge.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            txtPhone.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            txtSellerPass.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtSellerName.Text == String.Empty)
            {
                MessageBox.Show("Please Enter Seller Name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSellerName.Focus();
                return;
            }
            else if (txtSellerPass.Text == String.Empty)
            {
                MessageBox.Show("Please Enter Seller Password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSellerPass.Focus();
                return;
            }
            else
            {
                SqlCommand cmd = new SqlCommand("SELECT SellerName FROM tblSeller WHERE SellerName=@SellerName", dbCon.GetCon());
                cmd.Parameters.AddWithValue("@SellerName", txtSellerName.Text);
                dbCon.OpenCon();
                var result = cmd.ExecuteScalar();
                if (result != null)
                {
                    MessageBox.Show(String.Format("Seller already exist!"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtClear();
                }
                else
                {
                    try
                    {
                        cmd = new SqlCommand("spSellerInsert", dbCon.GetCon());
                        cmd.Parameters.AddWithValue("@SellerName", txtSellerName.Text);
                        cmd.Parameters.AddWithValue("@SellerAge", txtSellerAge.Text);
                        cmd.Parameters.AddWithValue("@SellerPhone", txtPhone.Text);
                        cmd.Parameters.AddWithValue("@SellerPass", txtSellerPass.Text);
                        cmd.CommandType = CommandType.StoredProcedure;
                        int i = cmd.ExecuteNonQuery();
                        dbCon.ClosCon();
                        if (i > 0)
                        {
                            MessageBox.Show("Seller Inserted Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtClear();
                            BindSeller();
                        }
                    }
                    catch(Exception ex)
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
                if (lblSellerID.Text == String.Empty)
                {
                    MessageBox.Show("Please Select SellerID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSellerName.Focus();
                    return;
                }
                if (txtSellerName.Text == String.Empty)
                {
                    MessageBox.Show("Please Enter Seller Name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSellerName.Focus();
                    return;
                }
                else if (txtSellerPass.Text == String.Empty)
                {
                    MessageBox.Show("Please Enter Seller Password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSellerPass.Focus();
                    return;
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("SELECT SellerName FROM tblSeller WHERE SellerName=@SellerName", dbCon.GetCon());
                    cmd.Parameters.AddWithValue("@SellerName", txtSellerName.Text);
                    dbCon.OpenCon();
                    var result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        MessageBox.Show(String.Format("Seller already exist."), "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtClear();
                    }
                    else
                    {
                        cmd = new SqlCommand("spSellerUpdate", dbCon.GetCon());
                        cmd.Parameters.AddWithValue("@SellerID", Convert.ToInt32(lblSellerID.Text));
                        cmd.Parameters.AddWithValue("@SellerName", txtSellerName.Text);
                        cmd.Parameters.AddWithValue("@SellerAge", Convert.ToInt32(txtSellerAge.Text));
                        cmd.Parameters.AddWithValue("@SellerPhone", txtPhone.Text);
                        cmd.Parameters.AddWithValue("@SellerPass", txtSellerPass.Text);
                        cmd.CommandType = CommandType.StoredProcedure;
                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            MessageBox.Show("Seller Updated Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtClear();
                            BindSeller();
                            lblSellerID.Visible = false;
                            btnDelete.Visible = false;
                            btnUpdate.Visible = false;
                            btnAdd.Visible = true;
                        }
                        else
                        {
                            MessageBox.Show("Update Fail...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtClear();
                            BindSeller();
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
                if (lblSellerID.Text == String.Empty)
                {
                    MessageBox.Show("Please Select SellerID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (lblSellerID.Text != String.Empty)
                {
                    if (DialogResult.Yes == MessageBox.Show("Do you want to delete?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                    {
                        SqlCommand cmd = new SqlCommand("spSellerDelete", dbCon.GetCon());
                        cmd.Parameters.AddWithValue("@SellerID", Convert.ToInt32(lblSellerID.Text));
                        dbCon.OpenCon();
                        cmd.CommandType = CommandType.StoredProcedure;
                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            MessageBox.Show("Seller Deleted Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtClear();
                            BindSeller();
                            btnUpdate.Visible = false;
                            btnDelete.Visible = false;
                            lblSellerID.Visible = false;
                            btnAdd.Visible = true;
                        }
                        else
                        {
                            MessageBox.Show("Delete Fail...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtClear();
                            BindSeller();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
