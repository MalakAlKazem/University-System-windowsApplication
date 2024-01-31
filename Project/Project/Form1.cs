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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CountStudents();
            CountProfessors();
            CountCourses();
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-16LL5T8;Initial Catalog=UniDB;Integrated Security=True;Pooling=False");
        private void button1_Click(object sender, EventArgs e)
        {
            
        }

     

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }


        
        private void CountStudents()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from StudentTable", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            labelStudent.Text = dt.Rows[0][0].ToString();
            con.Close();
        }
        private void CountProfessors()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from ProfessorTable", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            labelProNumbers.Text = dt.Rows[0][0].ToString();
            con.Close();
        }
        private void CountCourses()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from CourseTable", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            labelCoursesNumber.Text = dt.Rows[0][0].ToString();
            con.Close();
        }

       
        private void btnHome_Click(object sender, EventArgs e)
        {
            MessageBox.Show("You are at home");
        }

        private void btnStudent_Click_1(object sender, EventArgs e)
        {
            Student student = new Student();
            this.Hide();
            student.Show();
        }

        private void btnGrades_Click_1(object sender, EventArgs e)
        {
            Grades g = new Grades();
            this.Hide();
            g.Show();
        }

        private void btnExit_Click_1(object sender, EventArgs e)
        {
            
            LogIn l = new LogIn();
            this.Hide();
            l.Show();
        }

        private void btnCourses_Click(object sender, EventArgs e)
        {
            Course course = new Course();
            this.Hide();
            course.Show();
        }

        private void btnProfessor_Click(object sender, EventArgs e)
        {
            Professor pro = new Professor();
            //pro.MdiParent = this;
            pro.Show();
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
