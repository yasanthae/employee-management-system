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
using System.Windows.Markup;

namespace Employee_Management_System
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(@"Data Source = YASANTHA; Initial Catalog = ems; Integrated Security = True");

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtusername.Clear();
            txtpassword.Clear();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            con.Open();

            string username = txtusername.Text;
            string password = txtpassword.Text;

            string query_select = "SELECT * FROM login WHERE username = '" + username + "' and password='" + password + "'";
            SqlCommand cmnd = new SqlCommand(query_select, con);
            SqlDataReader row = cmnd.ExecuteReader();

            if (row.HasRows) 
            {
                this.Hide();
                Registration obj = new Registration();
                obj.Show();
            }
            else
            {
                MessageBox.Show("Invaild Login Credentials, Please check Username & Password and try again !", "Invaild Login Details", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure, Do you really want to exit...?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
