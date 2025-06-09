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

namespace WinFormCRUD
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string connectionString = "Data Source=DESKTOP-L3SMK21\\SQLEXPRESS;Initial Catalog=DotNetTraining;User ID=sa;Password=sasa@123;";


        void formClear()
        {
            txtName.Clear();
            txtNrc.Clear();
            txtNationality.Clear();
            saveBtn.Visible = true;
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {           
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = $"insert person (name, nrc, nationality) values ('{txtName.Text}', '{txtNrc.Text}', '{txtNationality.Text}')";
                using(var cmd = new SqlCommand(query, conn))
                {
                    var affectRow = cmd.ExecuteNonQuery();
                    if(affectRow > 0)
                    {
                        MessageBox.Show("Save successfull.");
                    }
                    formClear();
                }
                conn.Close();
            }
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            formClear();
        }

        private void DataLoad()
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "Select * from person";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                adapter.Fill(ds, "person");
                dt = ds.Tables["person"];
                personDGView.DataSource = dt;

                personDGView.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 10F, FontStyle.Bold);
                personDGView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                personDGView.AllowUserToAddRows = false;
                conn.Close();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DataLoad();
        }

        private void personDGView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtId.Text = personDGView.CurrentRow.Cells[0].Value.ToString();
            txtName.Text = personDGView.CurrentRow.Cells[1].Value.ToString();
            txtNrc.Text = personDGView.CurrentRow.Cells[2].Value.ToString();
            txtNationality.Text = personDGView.CurrentRow.Cells[3].Value.ToString();
            saveBtn.Visible = false;
        }

        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            using(var conn = new SqlConnection(connectionString))
            {
                conn.Open();               
                string query = @"UPDATE person SET name = @txtName, nrc = @txtNrc, nationality = @txtNationality
                                WHERE Id = @txtId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.Add("@txtId", SqlDbType.VarChar).Value = txtId.Text;
                cmd.Parameters.Add("@txtName", SqlDbType.VarChar).Value = txtName.Text;
                cmd.Parameters.Add("@txtNrc", SqlDbType.VarChar).Value = txtNrc.Text;
                cmd.Parameters.Add("@txtNationality", SqlDbType.VarChar).Value = txtNationality.Text;
                cmd.ExecuteNonQuery();
                MessageBox.Show("Update Successfull.");
                DataLoad();
                formClear();
                conn.Close();
            }
        }

        private void delBtn_Click(object sender, EventArgs e)
        {
            using(var conn = new SqlConnection(connectionString))
            {
                conn.Open();               
                string query = "DELETE FROM person WHERE Id = @txtId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@txtId", txtId.Text);
                cmd.ExecuteNonQuery();
                DataLoad();
                formClear();
                MessageBox.Show("Delete successfull");
                conn.Close();
            }
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
