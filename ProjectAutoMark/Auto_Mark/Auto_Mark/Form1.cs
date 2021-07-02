using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Auto_Mark
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            DataTable login = Database.getAccount(txtName.Text,txtPass.Text);
            if (login.Rows.Count > 0)
            {
                this.Hide();
                Mainform main = new Mainform(login.Rows[0]["accountID"].ToString());
                main.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Wrong Account");
            }
        }
    }
}
