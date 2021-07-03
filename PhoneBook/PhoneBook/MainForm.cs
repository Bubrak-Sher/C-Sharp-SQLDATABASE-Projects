using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PhoneBook
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            DataGridViewShowALL();
            GetData();
        }

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\Github Repo\C - Sharp - SQLDATABASE - Projects\PhoneBook\PhoneBook\myDb.mdf;Integrated Security=True");
        public int AbID;
        /* NEW CODE
            SQLiteConnection con = new SQLiteConnection(@"Data Source =.\myDb.db;version=3");
            con.Open();
            String Query = "Select * from myTable";
            SQLiteCommand cmd = new SQLiteCommand(Query, con);
            DataTable dt = new DataTable();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
            adapter.Fill(dt);
            DataGridView.DataSource = dt;
            con.Close(); 
         
         
         
         
         
         
        */



        private void Reset_Fields()
        {
            AbID = 0;
            txtName.Clear();
            txtMobile.Clear();
            txtSemester.Clear();
            txtName.Focus();

        }



        private void GetData()
        {
            SqlCommand cmd = new SqlCommand("Select * from Table", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            DataGridView.DataSource = dt;
        }

        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            if (IsValid())
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Table Values(@Name, @Mobile, @Semester)", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Name", txtName.Text);
                cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text);
                cmd.Parameters.AddWithValue("@Semester", txtSemester.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                GetData();
                Reset_Fields();
                MessageBox.Show("Contact Added into DataBase", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Provide Name, Mobile and Semester", "Some Fields Are Missing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void DataGridViewShowALL()
        {
            SqlCommand cmd = new SqlCommand("Select * from Table", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            DataGridViewShowAll.DataSource = dt;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (AbID > 0)
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Table WHERE Id = @Idd", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Idd", this.AbID);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Reset_Fields();
                GetData();
                MessageBox.Show("Contact Deleted from DataBase", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Please Select Contact to Delete from DataBase", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Table WHERE Name = '" + txtSearch.Text + "'", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            DataGridViewSearch.DataSource = dt;
            txtSearch.Text = "";
        }
        private bool IsValid()
        {
            if (txtName.Text != string.Empty && txtMobile.Text != string.Empty && txtSemester.Text != string.Empty)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        private void DataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            AbID = Convert.ToInt32(DataGridView.SelectedRows[0].Cells[0].Value);
            txtName.Text = DataGridView.SelectedRows[0].Cells[1].Value.ToString();
            txtMobile.Text = DataGridView.SelectedRows[0].Cells[2].Value.ToString();
            txtSemester.Text = DataGridView.SelectedRows[0].Cells[3].Value.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (AbID > 0)
            {
                SqlCommand cmd = new SqlCommand("UPDATE Table SET Name=@name, Mobile=@mobile, Semester=@Semester WHERE Id = @idd", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@mobile", txtMobile.Text);
                cmd.Parameters.AddWithValue("@Semester", txtSemester.Text);
                cmd.Parameters.AddWithValue("@Idd", this.AbID);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                GetData();
                Reset_Fields();
                MessageBox.Show("Contact Updated", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Please Select Contact to Update", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsAdd_Click(object sender, EventArgs e)
        {
            PurchasePro();
        }

        private void tsRemove_Click(object sender, EventArgs e)
        {
            PurchasePro();
        }

        private void tsSearch_Click(object sender, EventArgs e)
        {
            PurchasePro();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnClearDataBase_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM Table", con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Reset_Fields();
            GetData();
            MessageBox.Show("All DataBase Cleared", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void PurchasePro()
        {
            RegisterForm rForm = new RegisterForm();
            this.Hide();
            rForm.Show();
        }

        private void addContactToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PurchasePro();
        }

        private void removeConatactToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PurchasePro();
        }

        private void searchContactToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PurchasePro();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutAppToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Contacts Application, user can Create new contacts, search and delete existing Contacts", "About Applicaiton", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void aboutDeveloperToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Application is Developed By: Scorpio-BS (*_*)", "About Developer (@_@)",MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
