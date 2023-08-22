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
using System.Data;

namespace StudentInformation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            GetStudentRecord();
        }

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-KKEEJ3M\\SQLEXPRESS01;Initial Catalog=Test1;Integrated Security=True");
        public int StudentID;

        private void GetStudentRecord()
        {
            SqlCommand cmd = new SqlCommand("Select StudentID,StudName,FatherName,MotherName,Age,Address,RegisDate from Student", con);
            DataTable dt = new DataTable();


            con.Open();
            SqlDataReader sdr=cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();

            StudentRecordDataGridView.DataSource = dt;


            

           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(IsValid())
            {
                SqlCommand cmd = new SqlCommand("insert into Student Values (@name,@FatherName,@motherName,@age,@address,@regiDate)", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@name", txtStudentName.Text);
                cmd.Parameters.AddWithValue("@FatherName", txtFatherName.Text);
                cmd.Parameters.AddWithValue("@motherName", txtMotherName.Text);
                cmd.Parameters.AddWithValue("@age", txtAge.Text);
                cmd.Parameters.AddWithValue("@address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@regiDate", txtRegiDate.Text);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("New Student is Successfully Saved in the database", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                GetStudentRecord();
                ResetFormConstrols();

            }
        }

        private bool IsValid()
        {
            if(txtStudentName.Text==string.Empty)
            {
                MessageBox.Show("student Name is required","Failed",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ResetFormConstrols();
        }

        private void ResetFormConstrols()
        {
            StudentID = 0;
            txtStudentName.Clear();
            txtFatherName.Clear();
            txtMotherName.Clear();
            txtAge.Clear();
            txtAddress.Clear();
            txtRegiDate.Clear();

            txtStudentName.Focus();

        }

        private void StudentRecordDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            StudentID = Convert.ToInt32(StudentRecordDataGridView.SelectedRows[0].Cells[0].Value);
            txtStudentName.Text = StudentRecordDataGridView.SelectedRows[0].Cells[1].Value.ToString();
            txtFatherName.Text= StudentRecordDataGridView.SelectedRows[0].Cells[2].Value.ToString();
            txtMotherName.Text = StudentRecordDataGridView.SelectedRows[0].Cells[3].Value.ToString();
            txtAge.Text = StudentRecordDataGridView.SelectedRows[0].Cells[4].Value.ToString();
            txtAddress.Text = StudentRecordDataGridView.SelectedRows[0].Cells[5].Value.ToString();
            txtRegiDate.Text = StudentRecordDataGridView.SelectedRows[0].Cells[6].Value.ToString();
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(StudentID>0)
            {
                SqlCommand cmd = new SqlCommand("update Student set StudName=@name,FatherName=@FatherName,MotherName=@motherName,Age=@age,Address=@address,RegisDate=@regiDate where StudentID=@studID", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@name", txtStudentName.Text);
                cmd.Parameters.AddWithValue("@FatherName", txtFatherName.Text);
                cmd.Parameters.AddWithValue("@motherName", txtMotherName.Text);
                cmd.Parameters.AddWithValue("@age", txtAge.Text);
                cmd.Parameters.AddWithValue("@address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@regiDate", txtRegiDate.Text);
                cmd.Parameters.AddWithValue("@studID", this.StudentID);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Studnet Information Successfully Updated", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);

                GetStudentRecord();
                ResetFormConstrols();

            }

            else
            {
                MessageBox.Show("please Select an Student to update his information ", "Select", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(StudentID>0)
            {
                SqlCommand cmd = new SqlCommand("delete from Student where StudentID=@studID", con);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@studID", this.StudentID);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Studnet Information Successfully Deleted", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);

                GetStudentRecord();
                ResetFormConstrols();
            }
            else
            {
                MessageBox.Show("please Select an Student to delete his information ", "Select", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    }
}
