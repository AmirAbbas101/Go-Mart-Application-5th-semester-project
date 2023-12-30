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
    public partial class frmCategory : Form
    {
        DBConnect dbCon = new DBConnect();
        public frmCategory()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtCategoryName.Text == String.Empty)
            {
                MessageBox.Show("Please Enter Category Name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCategoryName.Focus();
                return;
            }
            else if (rtbCatDesc.Text == String.Empty)
            {
                MessageBox.Show("Please Enter Category Description.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                rtbCatDesc.Focus();
                return;
            }
            else
            {
                SqlCommand cmd = new SqlCommand("SELECT CategoryName FROM tblCategory WHERE CategoryName=@CategoryName", dbCon.GetCon());
                cmd.Parameters.AddWithValue("@CategoryName", txtCategoryName.Text);
                dbCon.OpenCon();
                var result = cmd.ExecuteScalar();
                if (result != null)
                {
                    MessageBox.Show(String.Format("Category already exist!"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtClear();
                }
                else
                {
                    cmd = new SqlCommand("spCatInsert", dbCon.GetCon());
                    cmd.Parameters.AddWithValue("@CategoryName", txtCategoryName.Text);
                    cmd.Parameters.AddWithValue("@CategoryDesc", rtbCatDesc.Text);
                    cmd.CommandType = CommandType.StoredProcedure;
                    int i = cmd.ExecuteNonQuery();
                    if(i > 0)
                    {
                        MessageBox.Show("Category Inserted Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtClear();
                        BindCategory();
                    }
                }
                dbCon.ClosCon();
            }
        }
        private void txtClear()
        {
            txtCategoryName.Clear();
            rtbCatDesc.Clear();
        }
        private void BindCategory()
        {
            SqlCommand cmd = new SqlCommand("SELECT CatID AS [Category ID], CategoryName, CategoryDesc AS [Category Description] FROM tblCategory;", dbCon.GetCon());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dbCon.OpenCon();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void frmCategory_Load(object sender, EventArgs e)
        {
            btnUpdate.Visible = false;
            btnDelete.Visible = false;
            lblCatID.Visible = false;
            BindCategory();
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            btnDelete.Visible = true;
            btnUpdate.Visible = true;
            btnAddCat.Visible = false;
            lblCatID.Visible = true;

            lblCatID.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            txtCategoryName.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            rtbCatDesc.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblCatID.Text == String.Empty)
                {
                    MessageBox.Show("Please Select CategoryID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCategoryName.Focus();
                    return;
                }
                if (txtCategoryName.Text == String.Empty)
                {
                    MessageBox.Show("Please Enter Category Name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCategoryName.Focus();
                    return;
                }
                else if (rtbCatDesc.Text == String.Empty)
                {
                    MessageBox.Show("Please Enter Category Description.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rtbCatDesc.Focus();
                    return;
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("SELECT CategoryName FROM tblCategory WHERE CategoryName=@CategoryName", dbCon.GetCon());
                    cmd.Parameters.AddWithValue("@CategoryName", txtCategoryName.Text);
                    dbCon.OpenCon();
                    var result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        MessageBox.Show(String.Format("Category Name {0} already exist."), "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtClear();
                    }
                    else
                    {
                        cmd = new SqlCommand("spCatUpadate", dbCon.GetCon());
                        cmd.Parameters.AddWithValue("@CatID", Convert.ToInt32(lblCatID.Text));
                        cmd.Parameters.AddWithValue("@CategoryName", txtCategoryName.Text);
                        cmd.Parameters.AddWithValue("@CategoryDesc", rtbCatDesc.Text);
                        cmd.CommandType = CommandType.StoredProcedure;
                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            MessageBox.Show("Category Updated Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtClear();
                            BindCategory();
                            btnUpdate.Visible = false;
                            btnDelete.Visible = false;
                            lblCatID.Visible = false;
                            btnAddCat.Visible = true;
                        }
                        else
                        {
                            MessageBox.Show("Update Fail...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtClear();
                            BindCategory();
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
                if (lblCatID.Text == String.Empty)
                {
                    MessageBox.Show("Please Select CategoryID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (lblCatID.Text != String.Empty)
                {
                    if(DialogResult.Yes==MessageBox.Show("Do you want to delete?","Confirmation",MessageBoxButtons.YesNo,MessageBoxIcon.Warning)) 
                    {
                        SqlCommand cmd = new SqlCommand("spCatDelete", dbCon.GetCon());
                        cmd.Parameters.AddWithValue("@CatID", Convert.ToInt32(lblCatID.Text));
                        dbCon.OpenCon();
                        cmd.CommandType = CommandType.StoredProcedure;
                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            MessageBox.Show("Category Deleted Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtClear();
                            BindCategory();
                            btnUpdate.Visible = false;
                            btnDelete.Visible = false;
                            lblCatID.Visible = false;
                            btnAddCat.Visible = true;
                        }
                        else
                        {
                            MessageBox.Show("Delete Fail...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtClear();
                            BindCategory();
                        }
                    }
                   
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
