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
    public partial class Registration : Form
    {
        public Registration()
        {
            InitializeComponent();
        }
        SqlConnection cn = new SqlConnection("Data Source=DESKTOP-16LL5T8;Initial Catalog=UniDB;Integrated Security=True;Pooling=False");
        SqlCommand cmd;
        private void Registration_Load(object sender, EventArgs e)
        {

        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {

            try
            {
                cn.Open();

                if (txtConfirmPass.Text != string.Empty || txtPass.Text != string.Empty || txtUserName.Text != string.Empty)
                {
                    if (txtPass.Text == txtConfirmPass.Text)
                    {
                        cmd = new SqlCommand("select * from LoginTable where username='" + txtUserName.Text + "'", cn);
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            dr.Close();
                            MessageBox.Show("Username Already exist please try another ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            dr.Close();
                            cmd = new SqlCommand("insert into LoginTable values(@username,@password)", cn);
                            cmd.Parameters.AddWithValue("username", txtUserName.Text);
                            cmd.Parameters.AddWithValue("password", txtPass.Text);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Your Account is created . Please login now.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please enter both password same ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Please enter value in all field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cn.Close();
            }
        }

        private void lnkLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LogIn l = new LogIn();
            this.Hide();
            l.Show();
        }
    }
}
