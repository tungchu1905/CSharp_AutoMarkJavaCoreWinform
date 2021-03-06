using Group3_Project.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Group3_Project
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }
        private bool checkTextBox()
        {
            if (txtPass.Text.Trim().Length == 0)
            {
                MessageBox.Show("Password is blank!");
                return false;
            }
            if (txtUser.Text.Trim().Length == 0)
            {
                MessageBox.Show("Username is blank!");
                return false;
            }
            return true;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (!checkTextBox())
            {
                return;
            }
            Account a = Database.Login(txtUser.Text, txtPass.Text);
            if (a == null)
            {
                MessageBox.Show("Username or Password is wrong!");
            }
            else
            {
                this.Hide();
                frmManage manage = new frmManage(Convert.ToString(a.Id));
                manage.ShowDialog();
                this.Close();
            }
        }
    }
}
