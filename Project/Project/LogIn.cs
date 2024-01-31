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

namespace Project
{
    public partial class LogIn : Form
    {
        public LogIn()
        {
            InitializeComponent();
            Reset();
        }
        SqlConnection cn = new SqlConnection("Data Source=DESKTOP-16LL5T8;Initial Catalog=UniDB;Integrated Security=True;Pooling=False");
        SqlCommand cmd;

        private void Reset()
        {
            txtUserName.Text = "";
            txtPass.Text = "";
        }



        private void txtUserName_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void txtUserName_TextChanged_1(object sender, EventArgs e)
        {
          
        }

        private void chkShowPass_CheckedChanged_1(object sender, EventArgs e)
        {
            if (chkShowPass.Checked)
            {
                txtPass.UseSystemPasswordChar = false;

            }
            else
                txtPass.UseSystemPasswordChar = true;
        }

        private void btnLogIn_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (txtPass.Text != string.Empty || txtUserName.Text != string.Empty)
                {
                    cn.Open();
                    cmd = new SqlCommand("select * from LoginTable where username='" + txtUserName.Text + "' and password='" + txtPass.Text + "'", cn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        dr.Close();
                        Form1 Home = new Form1();
                        this.Hide();
                        Home.Show();
                    }
                    else
                    {
                        dr.Close();
                        MessageBox.Show("No Account avilable with this username and password ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                else
                {
                    MessageBox.Show("Please enter value in all field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally { cn.Close(); }
        }

        private void lnkCreateAnAccount_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Registration r = new Registration();
            this.Hide();
            r.Show();
        }

        private void btbReset_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Please check your email");
        }

        private void txtPass_TextChanged(object sender, EventArgs e)
        {
           txtPass.UseSystemPasswordChar=true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
