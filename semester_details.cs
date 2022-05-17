using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace student
{
    public partial class semester_details : Form
    {
        public semester_details()
        {
            InitializeComponent();
        }

        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;             

        private void sem1_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(connection.cs);
            con.Open();
            cmd = new SqlCommand("select p_name from program",con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr[0].ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand("insert into sem_detail values(@p,@s,@c)",con);
            cmd.Parameters.Add(new SqlParameter("@p",comboBox1.Text));
            cmd.Parameters.Add(new SqlParameter("@s",comboBox2.Text));
            cmd.Parameters.Add(new SqlParameter("@c", textBox1.Text));
            cmd.ExecuteReader();
            MessageBox.Show("REcord Saved");
            comboBox1.Text = "...select Program...";
            comboBox2.Text = "";
            textBox1.Text="";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            con.Close();
            this.Close();
        }

        private void comboBox1_Enter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(comboBox1, "Select program name");
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(textBox1, "Enter Course code");
        }

       

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_Enter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(comboBox2, "Select Semester");
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
