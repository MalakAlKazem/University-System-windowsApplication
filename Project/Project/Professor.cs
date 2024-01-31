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
    public partial class Professor : Form
    {
        public Professor()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-16LL5T8;Initial Catalog=UniDB;Integrated Security=True;Pooling=False");
        UniDBDataSet UniDS = new UniDBDataSet();
        UniDBDataSetTableAdapters.ProfessorTableTableAdapter PT = new UniDBDataSetTableAdapters.ProfessorTableTableAdapter();
        BindingSource UniBS = new BindingSource();
        private void Professor_Load(object sender, EventArgs e)
        {
            PT.Fill(UniDS.ProfessorTable);
             
            comboProGender.Items.Add("Female");
            comboProGender.Items.Add("Male");
            comboQualification.Items.Add("PHD");
            comboQualification.Items.Add("Master");
            comboQualification.Items.Add("Bachelor");
            ShowProfessors();
            dataGridProf.EnableHeadersVisualStyles = false;
            dataGridProf.ColumnHeadersDefaultCellStyle.BackColor = Color.MidnightBlue;
            dataGridProf.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }
        private void ShowProfessors()
        {
            con.Open();
            string Query = "select * from ProfessorTable";
            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            dataGridProf.DataSource = ds.Tables[0];
            con.Close();
        }
        private void Reset()
        {
            txtProAddress.Text = "";
            txtProfessor.Text = "";
            txtProfessorPhone.Text = "";
            txtSalary.Text = "";
            comboProGender.SelectedIndex = -1;
            comboQualification.SelectedIndex = -1;
            txtProID.Text = "";

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtProfessor.Text == "" || txtProAddress.Text == "" || txtProfessorPhone.Text == "" || comboQualification.SelectedIndex == -1 || comboProGender.SelectedIndex == -1 || txtSalary.Text=="")
            {
                MessageBox.Show("Missing Information");
            }
            else
                try
                { con.Open();
                    SqlCommand cmd = new SqlCommand("Insert into ProfessorTable(PrName,PrDOB,PrGender,PrAdd,PrQual,PrSalary,PrPhone)" +
                                                                       "values(@PN,@PD,@PG,@PA,@PQ,@PS,@PP)", con);
                    cmd.Parameters.AddWithValue("@PN", txtProfessor.Text);
                    cmd.Parameters.AddWithValue("@PD", PDOB.Value.Date);
                    cmd.Parameters.AddWithValue("@PG", comboProGender.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@PA", txtProAddress.Text);
                    cmd.Parameters.AddWithValue("@PQ", comboQualification.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@PS", txtSalary.Text);
                    cmd.Parameters.AddWithValue("@PP", txtProfessorPhone.Text);
                    cmd.ExecuteNonQuery();
                   
                
                    MessageBox.Show("Professor Added");
                    con.Close();
                    ShowProfessors();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            if (dataGridProf.SelectedRows.Count == 0)
            {
                MessageBox.Show("please select a student");
                return;
            }
            int id, sal;
            string name, phone, add, gen, qual;

            DateTime d;
            foreach (DataGridViewRow row in dataGridProf.SelectedRows)
            {
                try
                {
                    //dt.ToShortDateString()); 
                    id = Convert.ToInt32(row.Cells["PrId"].Value);
                    name = row.Cells["PrName"].Value.ToString();
                    phone = row.Cells["PrPhone"].Value.ToString();
                    d = Convert.ToDateTime(row.Cells["PrDOB"].Value);
                    gen = row.Cells["PrGender"].Value.ToString();
                    add = row.Cells["PrAdd"].Value.ToString();
                    sal = Convert.ToInt32(row.Cells["PrSalary"].Value);
                    qual = row.Cells["PrQual"].Value.ToString();
                    PT.Delete(id, name, d, gen, add, qual, sal, phone);
                    MessageBox.Show("Professor deleted successfully");
                    Reset();
                    ShowProfessors();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
                if (dataGridProf.SelectedRows.Count == 0)
                {
                    MessageBox.Show("please select a professor");
                    return;
                }
            }
        }
        int key = 0;
        private void btnEdit_Click(object sender, EventArgs e)
        {

            if (txtProAddress.Text == "" || txtProfessor.Text == "" || txtSalary.Text == "" || txtProfessorPhone.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Update ProfessorTable set PrName=@PN,PrDOB=@PD,PrGender=@PG,PrAdd=@PA,PrQual=@PQ,PrSalary=@PS, PrPhone=@PP where PrID=@SKey", con);
                    cmd.Parameters.AddWithValue("@PN", txtProfessor.Text);
                    cmd.Parameters.AddWithValue("@PD", PDOB.Value.Date);
                    cmd.Parameters.AddWithValue("PG", comboProGender.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@PA", txtProAddress.Text);
                    cmd.Parameters.AddWithValue("@PP", txtProfessorPhone.Text);
                    cmd.Parameters.AddWithValue("@PQ", comboQualification.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@PS", txtSalary.Text);
                    cmd.Parameters.AddWithValue("@SKey", key);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Professor Updated");
                    ShowProfessors();
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



            private void dataGridProf_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
            {
              

            }

        private void dataGridProf_RowHeaderMouseClick_1(object sender, DataGridViewCellMouseEventArgs e)
        {
                txtProID.Text = dataGridProf.SelectedRows[0].Cells[0].Value.ToString();
                txtProfessor.Text = dataGridProf.SelectedRows[0].Cells[1].Value.ToString();
                PDOB.Text = dataGridProf.SelectedRows[0].Cells[2].Value.ToString();
                comboProGender.SelectedItem = dataGridProf.SelectedRows[0].Cells[3].ToString();
                txtProAddress.Text = dataGridProf.SelectedRows[0].Cells[4].Value.ToString();
                comboQualification.SelectedItem = dataGridProf.SelectedRows[0].Cells[5].ToString();
                txtSalary.Text = dataGridProf.SelectedRows[0].Cells[6].Value.ToString();
                txtProfessorPhone.Text = dataGridProf.SelectedRows[0].Cells[7].Value.ToString();
                if (txtProID.Text == "")
                {
                    key = 0;

                }
                else
                    key = Convert.ToInt32(dataGridProf.SelectedRows[0].Cells[0].Value.ToString());
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
             
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            Form1 home = new Form1();
            this.Hide();
            home.Show();
        }

        private void btnProfessor_Click(object sender, EventArgs e)
        {
            MessageBox.Show("You are in Ptofessor page");
        }

        private void btnStudent_Click(object sender, EventArgs e)
        {
            Student s = new Student();
            this.Hide();
            s.Show();
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

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            if (txtProfessor.Text == "" || txtProAddress.Text == "" || txtProfessorPhone.Text == "" || comboQualification.SelectedIndex == -1 || comboProGender.SelectedIndex == -1 || txtSalary.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Insert into ProfessorTable(PrName,PrDOB,PrGender,PrAdd,PrQual,PrSalary,PrPhone)" +
                                                                       "values(@PN,@PD,@PG,@PA,@PQ,@PS,@PP)", con);
                    cmd.Parameters.AddWithValue("@PN", txtProfessor.Text);
                    cmd.Parameters.AddWithValue("@PD", PDOB.Value.Date);
                    cmd.Parameters.AddWithValue("@PG", comboProGender.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@PA", txtProAddress.Text);
                    cmd.Parameters.AddWithValue("@PQ", comboQualification.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@PS", txtSalary.Text);
                    cmd.Parameters.AddWithValue("@PP", txtProfessorPhone.Text);
                    cmd.ExecuteNonQuery();

                   
                    MessageBox.Show("Professor Added");
                    con.Close();
                    ShowProfessors();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
        }

        private void btnDelete_Click_1(object sender, EventArgs e)
        {
            if (dataGridProf.SelectedRows.Count == 0)
            {
                MessageBox.Show("please select a student");
                return;
            }
            int id, sal;
            string name, phone, add, gen, qual;

            DateTime d;
            foreach (DataGridViewRow row in dataGridProf.SelectedRows)
            {
                try
                {
                    //dt.ToShortDateString()); 
                    id = Convert.ToInt32(row.Cells["PrId"].Value);
                    name = row.Cells["PrName"].Value.ToString();
                    phone = row.Cells["PrPhone"].Value.ToString();
                    d = Convert.ToDateTime(row.Cells["PrDOB"].Value);
                    gen = row.Cells["PrGender"].Value.ToString();
                    add = row.Cells["PrAdd"].Value.ToString();
                    sal = Convert.ToInt32(row.Cells["PrSalary"].Value);
                    qual = row.Cells["PrQual"].Value.ToString();
                    PT.Delete(id, name, d, gen, add, qual, sal, phone);
                    MessageBox.Show("Professor deleted successfully");
                    Reset();
                    ShowProfessors();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
                if (dataGridProf.SelectedRows.Count == 0)
                {
                    MessageBox.Show("please select a professor");
                    return;
                }
            }
        }

        private void btnEdit_Click_1(object sender, EventArgs e)
        {
            if (txtProAddress.Text == "" || txtProfessor.Text == "" || txtSalary.Text == "" || txtProfessorPhone.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Update ProfessorTable set PrName=@PN,PrDOB=@PD,PrGender=@PG,PrAdd=@PA,PrQual=@PQ,PrSalary=@PS, PrPhone=@PP where PrID=@SKey", con);
                    cmd.Parameters.AddWithValue("@PN", txtProfessor.Text);
                    cmd.Parameters.AddWithValue("@PD", PDOB.Value.Date);
                    cmd.Parameters.AddWithValue("PG", comboProGender.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@PA", txtProAddress.Text);
                    cmd.Parameters.AddWithValue("@PP", txtProfessorPhone.Text);
                    cmd.Parameters.AddWithValue("@PQ", comboQualification.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@PS", txtSalary.Text);
                    cmd.Parameters.AddWithValue("@SKey", key);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Professor Updated");
                    ShowProfessors();
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

        private void dataGridProf_RowHeaderMouseClick_2(object sender, DataGridViewCellMouseEventArgs e)
        {
            txtProID.Text = dataGridProf.SelectedRows[0].Cells[0].Value.ToString();
            txtProfessor.Text = dataGridProf.SelectedRows[0].Cells[1].Value.ToString();
            PDOB.Text = dataGridProf.SelectedRows[0].Cells[2].Value.ToString();
            comboProGender.SelectedItem = dataGridProf.SelectedRows[0].Cells[3].ToString();
            txtProAddress.Text = dataGridProf.SelectedRows[0].Cells[4].Value.ToString();
            comboQualification.SelectedItem = dataGridProf.SelectedRows[0].Cells[5].ToString();
            txtSalary.Text = dataGridProf.SelectedRows[0].Cells[6].Value.ToString();
            txtProfessorPhone.Text = dataGridProf.SelectedRows[0].Cells[7].Value.ToString();
            if (txtProID.Text == "")
            {
                key = 0;

            }
            else
                key = Convert.ToInt32(dataGridProf.SelectedRows[0].Cells[0].Value.ToString());
        }
    }
    } 
