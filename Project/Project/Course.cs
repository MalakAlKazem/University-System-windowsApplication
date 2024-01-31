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
    public partial class Course : Form
    {
        public Course()
        {
            InitializeComponent();
            showCourse();
            getProfessorID();
            Reset();
        }
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-16LL5T8;Initial Catalog=UniDB;Integrated Security=True;Pooling=False");
      
        private void getProfessorID()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select PrID from ProfessorTable", con);
            SqlDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("PrID", typeof(int));
            dt.Load(reader);
            cmbProfID.ValueMember = "PrID";
            cmbProfID.DataSource = dt;
            con.Close();
        }
        private void getProfessorName()
        {
            con.Open();
            string q = "select * from ProfessorTable where PrID=" + cmbProfID.SelectedValue.ToString() + "";
            SqlCommand cmd = new SqlCommand(q, con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                txtPrName.Text = dr["PrName"].ToString();
            }
            con.Close();
        }
        private void showCourse()
        {
            con.Open();
            string query = "select * from CourseTable";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            dgvCourses.DataSource = ds.Tables[0];
            con.Close();
        }
        private void Reset()
        {
            txtCourseID.Text = "";
            txtCourseName.Text = "";
            txtPrName.Text = "";
            cmbProfID.SelectedIndex = -1;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtCourseName.Text == "" || cmbProfID.SelectedIndex == -1 || txtDuration.Text == "")
            {
                MessageBox.Show("Missing Information");
                return;
            }
            else
            {


                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Insert into CourseTable(CName,CDuration,CproId,CproName)values(@CN,@CD,@PID,@PN)", con);
                    cmd.Parameters.AddWithValue("@CN", txtCourseName.Text);
                    cmd.Parameters.AddWithValue("@CD", txtDuration.Text);
                    cmd.Parameters.AddWithValue("@PID", cmbProfID.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@PN", txtPrName.Text);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Course Added");
                    showCourse();
                    Reset();

                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
        }
        int key = 0;
        private void dgvCourses_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            txtCourseID.Text = dgvCourses.SelectedRows[0].Cells[0].Value.ToString();
            txtCourseName.Text = dgvCourses.SelectedRows[0].Cells[1].Value.ToString();
            txtDuration.Text = dgvCourses.SelectedRows[0].Cells[2].Value.ToString();
            cmbProfID.SelectedItem = dgvCourses.SelectedRows[0].Cells[3].Value.ToString();
            txtPrName.Text = dgvCourses.SelectedRows[0].Cells[4].Value.ToString();

            if (txtCourseID.Text == "")
            {
                key = 0;

            }
            else
                key = Convert.ToInt32(dgvCourses.SelectedRows[0].Cells[0].Value.ToString());
        }

        private void txtPrName_MouseClick(object sender, MouseEventArgs e)
        {
            getProfessorName();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Select Course to Delete");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("delete from CourseTable where CID=@CKey", con);
                    cmd.Parameters.AddWithValue("@CKey", key);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Course Deleted");
                    showCourse();
                    Reset();

                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    con.Close();
                }

            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if ( txtCourseName.Text == "" || txtDuration.Text == "" || cmbProfID.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Update CourseTable set CName=@CN,CDuration=@CD,CproId=@CPID,CproName=@CPN where CID=@CKey", con);
                    cmd.Parameters.AddWithValue("@CN", txtCourseName.Text);
                    cmd.Parameters.AddWithValue("@CD", txtDuration.Text);
                    cmd.Parameters.AddWithValue("@CPID", cmbProfID.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@CPN", txtPrName.Text);
                    cmd.Parameters.AddWithValue("@CKey", key);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Course Updated...");
                    showCourse();
                    Reset();

                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            Form1 home = new Form1();
            this.Hide();
            home.Show();
        }

        private void btnStudent_Click(object sender, EventArgs e)
        {
            Student s = new Student();
            this.Hide();
            s.Show();
        }

        private void btnCourses_Click(object sender, EventArgs e)
        {
            MessageBox.Show("You are in the Courses page");
        }

        private void btnProfessor_Click(object sender, EventArgs e)
        {
            Professor g = new Professor();
            this.Hide();
            g.Show();
        }

        private void btnGrades_Click(object sender, EventArgs e)
        {
            Grades g = new Grades();
            this.Hide();
            g.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            LogIn l = new LogIn();
            this.Hide();
            l.Show();
        }

        private void Course_Load(object sender, EventArgs e)
        {
            dgvCourses.EnableHeadersVisualStyles = false;
            dgvCourses.ColumnHeadersDefaultCellStyle.BackColor = Color.MidnightBlue;
            dgvCourses.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }

        private void cmbProfID_SelectedIndexChanged(object sender, EventArgs e)
        {
        
        }
    }
}
