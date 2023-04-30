using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Employee_Management_System
{
    public partial class Registration : Form
    {
        public Registration()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(@"Data Source = YASANTHA; Initial Catalog = ems; Integrated Security = True");

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                string firstName = txtFname.Text;
                string lastName = txtLname.Text;
                dtpDob.Format = DateTimePickerFormat.Custom;
                dtpDob.CustomFormat = "yyyy/MM/dd";
                string gender;
                if (rbMale.Checked)
                {
                    gender = "Male";
                }
                else
                {
                    gender = "Female";
                }
                string address = txtAddress.Text;
                string email = txtEmail.Text;
                string txtMobile = "123";
                int mobilePhone = int.Parse(txtMobile);
                string txtHphone = "123";
                int homePhone = int.Parse(txtHphone);
                string departmentName = txtDName.Text;
                string designation = txtDesignation.Text;
                string employeeType = txtEtype.Text;
                string query_insert = "insert into Employee value('" + firstName + "','" + lastName + "','" + dtpDob.Text + "','" + gender + "','" + address + "','" + email + "'," +
                    mobilePhone + "," + homePhone + ",'" + departmentName + "','" + designation + "'," + employeeType + ")";

                con.Open();
                SqlCommand cmnd = new SqlCommand(query_insert);

                cmnd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Record Added Successfully!", "Registered Employee!", MessageBoxButtons.OK, MessageBoxIcon.Information);

          
            }
            catch (SqlException ex)
            {
                string msg = "Insert Error:";
                msg += ex.Message;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string no = cmbReg.Text;

            if (no != "New Register")
            {
                string firstName = txtFname.Text;
                string lastName = txtLname.Text;
                dtpDob.Format = DateTimePickerFormat.Custom;
                dtpDob.CustomFormat = "yyyy/MM/dd";
                string gender;
                if (rbMale.Checked)
                {
                    gender = "Male";
                }
                else
                {
                    gender = "Female";
                }
                string address = txtAddress.Text;
                string email = txtEmail.Text;
                int mobilePhone = int.Parse(txtMobile.Text);
                int homePhone = int.Parse(txtHphone.Text);
                string departmentName = txtDName.Text;
                string designation = txtDesignation.Text;   
                string employeeType = txtEtype.Text;

                string query_insert = "UPDATE Employee SET firstName = '" + firstName + "',lastName = '" + lastName + "',dateofBirth = '" + dtpDob.Text + "',gender = '" +
                    gender + "',address = '" + address + "',email = '" + email + "',mobilePhone = " + mobilePhone + ",homePhone = " + homePhone + ",departmentName = '" +
                    departmentName + "',designation = '" + designation + "',employeeType = " + employeeType + "WHERE empNo = " + no;

            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            cmbReg.Text = "";
            txtFname.Text = "";
            txtLname.Text = "";
            dtpDob.Format = DateTimePickerFormat.Custom;
            dtpDob.CustomFormat = "yyyy/MM/dd";
            DateTime thisDay = DateTime.Today;
            dtpDob.Text = thisDay.ToString();

            rbMale.Checked = false;
            rbFemale.Checked = false;

            txtAddress.Text = "";
            txtEmail.Text = "";
            txtMobile.Text = "";
            txtHphone.Text = "";
            txtDName.Text = "";
            txtDesignation.Text = "";
            txtEtype.Text = "";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure, Do you really want to Delete this Record...?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                string no = cmbReg.Text;

                string query_insert = "DELETE FROM Employee WHERE empNo = " + no + "";
                con.Open();
                SqlCommand cmnd = new SqlCommand(query_insert, con);
                cmnd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Record Deleted Successfully!", "Deleted Employee", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void linkLabellogout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Close();
        }

        private void linkLabelExit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure, Do you really want to exit...?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
            else if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void Registration_Load(object sender, EventArgs e)
        {
            con.Open();
            string query_select = "SELECT * FROM Employee";
            SqlCommand cmnd = new SqlCommand(query_select, con);
            SqlDataReader row = cmnd.ExecuteReader();
            cmbReg.Items.Add("New Register");
            while (row.Read())
            {
                cmbReg.Items.Add(row[0].ToString());
            }
            con.Close();
        }

        private void cmbReg_SelectedIndexChanged(object sender, EventArgs e)
        {
            string no = cmbReg.Text;
            if (no != "New Register")
            {
                con.Open();
                string query_select = "SELECT * FROM Registration2 WHERE regNo =" + no;
                SqlCommand cmd = new SqlCommand(query_select, con);
                SqlDataReader row = cmd.ExecuteReader();
                while (row.Read())
                {
                    txtFname.Text = row[1].ToString();
                    txtLname.Text = row[2].ToString();
                    dtpDob.Format = DateTimePickerFormat.Custom;
                    dtpDob.CustomFormat = "yyyy/MM/dd";
                    dtpDob.Text = row[3].ToString();
                    if (row[4].ToString() == "Male")
                    {
                        rbMale.Checked = true;
                        rbFemale.Checked = false;
                    }
                    else
                    {
                        rbMale.Checked = false;
                        rbFemale.Checked = true;
                    }
                    txtAddress.Text = row[5].ToString(); 
                    txtEmail.Text = row[6].ToString();
                    txtMobile.Text = row[7].ToString();
                    txtHphone.Text = row[8].ToString();
                    txtDName.Text = row[9].ToString();
                    txtDesignation.Text = row[10].ToString();
                    txtEtype.Text = row[11].ToString();
                }
                con.Close();
                btnClear.Enabled = false;
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;
            }
            else
            {
                txtFname.Text = "";
                txtLname.Text = "";
                dtpDob.Format = DateTimePickerFormat.Custom;
                dtpDob.CustomFormat = "yyyy/MM/dd";
                DateTime thisDay = DateTime.Today;
                dtpDob.Text = thisDay.ToString();

                rbMale.Checked = true;
                rbFemale.Checked = false;

                txtAddress.Text = "";
                txtEmail.Text = "";
                txtMobile.Text = "";
                txtHphone.Text = "";
                txtDName.Text = "";
                txtDesignation.Text = "";
                txtEtype.Text = "";
                btnRegister.Enabled = true;
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;
            }
        }
    }
}
