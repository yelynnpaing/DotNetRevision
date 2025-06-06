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

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void personDGView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtId.Text = personDGView.CurrentRow.Cells[0].Value.ToString();
            txtName.Text = personDGView.CurrentRow.Cells[1].Value.ToString();
            txtNrc.Text = personDGView.CurrentRow.Cells[2].Value.ToString();
            txtNationality.Text = personDGView.CurrentRow.Cells[3].Value.ToString();
        }
    }
}
