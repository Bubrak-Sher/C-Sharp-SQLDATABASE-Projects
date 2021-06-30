using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;



namespace CRUD_Operations
{
    //public partial class Form1 : MetroFramework.Forms.MetroForm
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GetStudentData();
        }
        // Connection to Database
        SqlConnection con = new SqlConnection(@"Data Source=SCORPIO\SQLSERVER2017;Initial Catalog=crudOperation;Integrated Security=True");
        public int StudentID;
        
        
        //
        private void GetStudentData()
        {
            
            SqlCommand cmd = new SqlCommand("Select * from StudentsTb", con);

            DataTable dt = new DataTable();
            con.Open();

            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            StudentRecordDataGridView.DataSource = dt;
        }





        private void button2_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("User IS NOT REGISTERED", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            if(StudentID > 0)
            {
                SqlCommand cmd = new SqlCommand("UPDATE StudentsTb SET Name = @name, FatherName = @FatherName, RollNumber = @Roll, Address = @Address, Mobile = @Mobile WHERE StudentID = @ID", con);

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@name", txtStudentName.Text);
                cmd.Parameters.AddWithValue("FatherName", txtFatherName.Text);
                cmd.Parameters.AddWithValue("@Roll", txtRollNumber.Text);
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text);
                cmd.Parameters.AddWithValue("@ID", this.StudentID);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Student Information Successfully Updated in DataBase", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);

                GetStudentData();
                ResetData();
            }
            else
            {
                MessageBox.Show("Please Select Student to update his information", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (IsValid())
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO StudentsTb Values (@name, @FatherName, @Roll, @Address, @Mobile)", con);

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@name", txtStudentName.Text);
                cmd.Parameters.AddWithValue("FatherName", txtFatherName.Text);
                cmd.Parameters.AddWithValue("@Roll", txtRollNumber.Text);
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Student Information Successfully Saved in DataBase", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                GetStudentData();
                ResetData();
            }
            else
            {

            }
        }

        private bool IsValid()
        {
            if(txtStudentName.Text == string.Empty)
            {
                MessageBox.Show("Student Name is Required","Student Name is Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtFatherName.Text == string.Empty)
            {
                MessageBox.Show("Student Father Name is Required", "Student Name is Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtMobile.Text == string.Empty)
            {
                MessageBox.Show("Student Mobile Number is Required", "Student Name is Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtRollNumber.Text == string.Empty)
            {
                MessageBox.Show("Student Roll Number is Required", "Student Name is Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtAddress.Text == string.Empty)
            {
                MessageBox.Show("Student Address is Required", "Student Name is Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void ResetData()
        {
            StudentID = 0;
            txtStudentName.Clear();
            txtFatherName.Clear();
            txtRollNumber.Clear();
            txtMobile.Clear();
            txtAddress.Clear();

            txtStudentName.Focus();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (StudentID > 0)
            { 
                SqlCommand cmd = new SqlCommand("DELETE FROM StudentsTb WHERE StudentID = @ID", con);

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ID", this.StudentID);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Student Information Deleted Successfully from DataBase", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);

                GetStudentData();
                ResetData();
            }
            else
            {
                MessageBox.Show("Please Select Student to Delete from DataBase", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // To quit Application
            //Application.Exit();
            ResetData();
        }

        private void StudentRecordDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            StudentID = Convert.ToInt32(StudentRecordDataGridView.SelectedRows[0].Cells[0].Value);
            txtStudentName.Text = StudentRecordDataGridView.SelectedRows[0].Cells[1].Value.ToString();
            txtFatherName.Text = StudentRecordDataGridView.SelectedRows[0].Cells[2].Value.ToString();
            txtRollNumber.Text = StudentRecordDataGridView.SelectedRows[0].Cells[3].Value.ToString();
            txtMobile.Text = StudentRecordDataGridView.SelectedRows[0].Cells[5].Value.ToString();
            txtAddress.Text = StudentRecordDataGridView.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM StudentsTb WHERE Name='" + txtSearch.Text.ToString() + "'", con);

            //cmd.CommandType = CommandType.Text;
                        
            DataTable dt = new DataTable();
            con.Open();

            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            StudentRecordDataGridView.DataSource = dt;
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            txtSearch.Text = string.Empty;
            ResetData();
            GetStudentData();
        }
    }
}
