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
    public partial class Student : Form
    {
        public Student()
        { 
            InitializeComponent();
            getStudentID();
        }
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-16LL5T8;Initial Catalog=UniDB;Integrated Security=True;Pooling=False");
        UniDBDataSet UniDS = new UniDBDataSet();
        UniDBDataSetTableAdapters.StudentTableTableAdapter ST = new UniDBDataSetTableAdapters.StudentTableTableAdapter();
        BindingSource UniBS = new BindingSource();
        private void Student_Load(object sender, EventArgs e)
        {
            comboGender.Items.Add("Female");
            comboGender.Items.Add("Male");
            comboMajor.Items.Add("Computer Science");
            comboMajor.Items.Add("IT");
            comboMajor.Items.Add("Mechanical Engineering");
            comboMajor.Items.Add("Software Engineering");
            comboMajor.Items.Add("Computer Engineering");
            comboSemester.Items.Add("Fall 2023-2024");
            comboSemester.Items.Add("Spring 2022-2023");
            comboSemester.Items.Add("Summer 2022-2023");
            ShowStudents();
            dataGridStudent.EnableHeadersVisualStyles = false;
            dataGridStudent.ColumnHeadersDefaultCellStyle.BackColor = Color.MidnightBlue;
            dataGridStudent.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }
        private void ShowStudents()
        {
            con.Open();
            string Query = "select * from StudentTable";
            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            dataGridStudent.DataSource = ds.Tables[0];
            con.Close();
        }
        private void Reset()
        { 
                txtAddress.Text = "";
                txtStudent.Text = "";
                txtStudentPhone.Text = "";
                comboSemester.SelectedIndex = -1;
                comboMajor.SelectedIndex = -1;
                comboGender.SelectedIndex = -1;
            
        }
        private void txtStudent_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void txtStudentPhone_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtStudentID_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void DateOfBirth_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void comboGender_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboSemester_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void txtAddress_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(txtStudent.Text=="" || txtAddress.Text=="" || txtStudentPhone.Text=="" || comboSemester.SelectedIndex ==-1 || comboGender.SelectedIndex==-1 || comboMajor.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information");
            }
            else
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Insert into StudentTable"+"(StName,SDOB,StGender,StAddress,StPhone,StSem,StMajor)values(@SN,@SD,@SG,@SA,@SP,@SS,@SM)",con);
                    cmd.Parameters.AddWithValue("@SN", txtStudent.Text);
                    cmd.Parameters.AddWithValue("@SD", StudentDOB.Value.Date);
                    cmd.Parameters.AddWithValue("@SG", comboGender.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@SA", txtAddress.Text);
                    cmd.Parameters.AddWithValue("@SP", txtStudentPhone.Text);
                    cmd.Parameters.AddWithValue("@SS", comboSemester.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@SM", comboMajor.SelectedItem.ToString());
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Student Added");
                    con.Close();
                    ShowStudents();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
        }
       
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridStudent.SelectedRows.Count == 0)
            {
                MessageBox.Show("please select a student");
                return;
            }
            int id;
            string name, phone,add,gen,sem,maj;
            
            DateTime d;
            foreach (DataGridViewRow row in dataGridStudent.SelectedRows) {
                try {
                    //dt.ToShortDateString()); 
                    id = Convert.ToInt32(row.Cells["StId"].Value);
                    name = row.Cells["StName"].Value.ToString();
                    phone = row.Cells["StPhone"].Value.ToString();
                    add = row.Cells["StAddress"].Value.ToString();
                    gen = row.Cells["StGender"].Value.ToString();
                    sem = row.Cells["StSem"].Value.ToString();
                    maj = row.Cells["StMajor"].Value.ToString();
                    d = Convert.ToDateTime(row.Cells["SDOB"].Value);
                    ST.Delete(id, name, d, gen, add, phone, sem, maj);
                    MessageBox.Show("Studnet deleted successfully");
                    Reset();
                    ShowStudents();
            }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            } }
        int key = 0;
        private void dataGridStudent_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
        private void getStudentID()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select StId from StudentTable", con);
            SqlDataReader rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("StId", typeof(int));
            dt.Load(rdr);
            txtStudentID.Text = "StId";
            con.Close();

        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (txtAddress.Text == "" || txtStudent.Text == "" || txtStudentPhone.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Update StudentTable set StName=@SN,SDOB=@SD,StGender=@SG,StAddress=@SA,StPhone=@SP,StSem=@SS,StMajor=@SM where StId=@SKey", con);
                    cmd.Parameters.AddWithValue("@SN", txtStudent.Text);
                    cmd.Parameters.AddWithValue("@SD", StudentDOB.Value.Date);
                    cmd.Parameters.AddWithValue("@SG", comboGender.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@SA", txtAddress.Text);
                    cmd.Parameters.AddWithValue("@SP", txtStudentPhone.Text);
                    cmd.Parameters.AddWithValue("@SS", comboSemester.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@SM", comboMajor.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@SKey", key);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Student Updated");
                    ShowStudents();
                    Reset();

                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally { con.Close(); }
            }

        }

        private void dataGridStudent_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            txtStudentID.Text = dataGridStudent.SelectedRows[0].Cells[0].Value.ToString();
            txtStudent.Text = dataGridStudent.SelectedRows[0].Cells[1].Value.ToString();
            StudentDOB.Text = dataGridStudent.SelectedRows[0].Cells[2].Value.ToString();
            comboGender.SelectedItem = dataGridStudent.SelectedRows[0].Cells[3].ToString();
            txtAddress.Text = dataGridStudent.SelectedRows[0].Cells[4].Value.ToString();
            txtStudentPhone.Text = dataGridStudent.SelectedRows[0].Cells[5].Value.ToString();
            comboSemester.SelectedItem = dataGridStudent.SelectedRows[0].Cells[6].ToString();
            comboMajor.SelectedItem = dataGridStudent.SelectedRows[0].Cells[7].ToString();
            if (txtStudentID.Text == "")
            {
                key = 0;

            }
            else
                key = Convert.ToInt32(dataGridStudent.SelectedRows[0].Cells[0].Value.ToString());

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            Form1 home = new Form1();
            this.Hide();
            home.Show();
        }

        private void btnStudent_Click(object sender, EventArgs e)
        {
            MessageBox.Show("You are in student page");
        }

        private void btnProfessor_Click(object sender, EventArgs e)
        {
            Professor p = new Professor();
            this.Hide();
            p.Show();
        }

        private void btnCourses_Click(object sender, EventArgs e)
        {
            Course c = new Course();
            this.Hide();
            c.Show();
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

        private void dataGridStudent_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel10_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
