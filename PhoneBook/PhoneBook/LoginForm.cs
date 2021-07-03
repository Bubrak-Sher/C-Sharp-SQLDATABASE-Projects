using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhoneBook
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            ProcessStartInfo psInfo = new ProcessStartInfo
            {
                FileName = "https://www.pakdaddy.com",
                UseShellExecute = true
            };
            Process.Start(psInfo);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if(txtUserName.Text.Trim() == "Scorpio" && txtPassword.Text.Trim() == "myLoveA")
            {
                this.Hide();
                MainForm MainF = new MainForm();
                MainF.Show(); 
            }
            else if(txtUserName.Text.Trim() == string.Empty || txtPassword.Text.Trim() == string.Empty)
            {
                
                if (txtUserName.Text.Trim() != string.Empty)
                {
                    MessageBox.Show("Password is Missing", "Password Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (txtPassword.Text.Trim() != string.Empty)
                {
                    MessageBox.Show("User Name is Missing", "User Name Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (txtUserName.Text.Trim() == string.Empty || txtPassword.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("User Name and Password are Missing (*_*)", "Missing Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } 
            else
            {
                MessageBox.Show("User Name or Password is invalid","Login Failed",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            this.Hide();
            RegisterForm nReg = new RegisterForm();
            nReg.Show();
        }
    }
}
