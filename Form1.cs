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

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Load();
        }
      

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        //connect the database
        SqlConnection con = new SqlConnection("Data Source=SURYA;Initial Catalog=employeedetails;Integrated Security=True");
        SqlCommand cmd;
        SqlDataReader read;
        SqlDataAdapter drr;
        string id;
        bool mode = true;
        string sql;

        public void Load()
        {
            try
            {
                
                sql = "select * from employeetable";
                cmd = new SqlCommand(sql, con);
                con.Open();
                read = cmd.ExecuteReader();
                drr = new SqlDataAdapter(sql,con);
                dataGridView1.Rows.Clear();
                while(read.Read())
                { 

                    dataGridView1.Rows.Add(read[0], read[1], read[2], read[3],read[4],read[5],read[6]);


                }
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        
        }
        public void getID(string id)
        {
            sql = "select * from employeetable where id='" + id + "'";
            cmd = new SqlCommand(sql, con);
            con.Open();
            read = cmd.ExecuteReader();
            while (read.Read())
            {

                
                txtname.Text = read[1].ToString();
                txtsurname.Text = read[2].ToString();
                txtgender.Text = read[3].ToString();
                txtdob.Text = read[4].ToString();
                txtnation.Text = read[5].ToString();
                txtaddress.Text = read[6].ToString();


            }
            con.Close();


        }
        //Save and update record done by the samebutton
        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                string name = txtname.Text;
                string surname = txtsurname.Text;
                string gender = txtgender.Text;
                string dateofbirth = txtdob.Text;
                string nationality = txtnation.Text;
                string Address = txtaddress.Text;

                if (mode == true)
                {
                    sql = "insert into employeetable(employeename,surname,Gender,dateofbirth,nationality,Address) values(@employeename,@surname,@Gender,@dateofbirth,@nationality,@Address)";
                    con.Open();
                    cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@employeename", name);
                    cmd.Parameters.AddWithValue("@surname", surname);
                    cmd.Parameters.AddWithValue("@Gender", gender);
                    cmd.Parameters.AddWithValue("dateofbirth", dateofbirth);
                    cmd.Parameters.AddWithValue("nationality", nationality);
                    cmd.Parameters.AddWithValue("Address", Address);
                    MessageBox.Show("Data added successfully");
                    cmd.ExecuteNonQuery();

                    txtname.Clear();
                    txtsurname.Clear();
                    txtgender.Clear();
                    txtdob.Clear();
                    txtnation.Clear();
                    txtaddress.Clear();
                    txtname.Focus();




                }
                else
                {
                    id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    sql = "update  employeetable set employeename = @employeename,surname = @surname,Gender=@Gender,dateofbirth=@dateofbirth,nationality=@nationality,Address=@Address where id = @id";
                    con.Open();
                    cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@employeename", name);
                    cmd.Parameters.AddWithValue("@surname", surname);
                    cmd.Parameters.AddWithValue("@Gender", gender);
                    cmd.Parameters.AddWithValue("@dateofbirth", dateofbirth);
                    cmd.Parameters.AddWithValue("@nationality", nationality);
                    cmd.Parameters.AddWithValue("@Address", Address);
                    cmd.Parameters.AddWithValue("@id", id);
                    MessageBox.Show("Data updated");
                    cmd.ExecuteNonQuery();
                    txtname.Clear();
                    txtsurname.Clear();
                    txtgender.Clear();
                    txtdob.Clear();
                    txtnation.Clear();
                    txtaddress.Clear();
                    txtname.Focus();
                    button1.Text = "Save";
                    mode = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            con.Close();

        }
        

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["Edit"].Index && e.RowIndex >= 0)
            {
                mode = false;
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                getID(id);
                button1.Text = "Edit";

            }
            else if (e.ColumnIndex == dataGridView1.Columns["Delete"].Index && e.RowIndex >= 0)
            {
                mode = false;
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                sql = "delete from employeetable where id = @id";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Data Deleted");
                con.Close();

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Load();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtname.Clear();
            txtsurname.Clear();
            txtgender.Clear();
            txtdob.Clear();
            txtnation.Clear();
            txtaddress.Clear();
            txtname.Focus();
            button1.Text = "Save";
            mode = true;
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void txtname_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
