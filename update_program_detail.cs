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
    public partial class update_program_detail : Form
    {
        public update_program_detail()
        {
            InitializeComponent();
        }

        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        private void update_program_detail_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(connection.cs);
            con.Open();
            cmd = new SqlCommand("select * from program", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr[0].ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(check())
            {
                MessageBox.Show("Rectify Error");
            }
            else
            {
                cmd = new SqlCommand("update program SET p_name=@p,sem=@s,fees=@f,dur=@d where p_name=@p",con);
                cmd.Parameters.Add(new SqlParameter("@p",comboBox1.Text));
                cmd.Parameters.Add(new SqlParameter("@s",textBox1.Text));
                cmd.Parameters.Add(new SqlParameter("@f", textBox2.Text));
                cmd.Parameters.Add(new SqlParameter("@d", textBox3.Text));
                cmd.ExecuteReader();
                comboBox1.Text = "Select Program";
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
            }

        }

        public bool check()
        {
            int i = Convert.ToInt32(textBox2.Text); ;
            bool b = false;
            errorProvider1.Clear();
            errorProvider1.BlinkRate = 100;
            errorProvider1.BlinkStyle = ErrorBlinkStyle.AlwaysBlink;
            if ((comboBox1.Text== "Select Program")||(comboBox1.Text.Length==0))
            {
                errorProvider1.SetError(textBox1, "Please select the program");
                textBox1.Focus();
                b = true;
            }
            if (textBox1.Text.Length == 0)
            {
                errorProvider1.SetError(textBox1, "Please enter number of semester in program");
                textBox1.Focus();
                b = true;
            }
            if ((textBox2.Text.Length == 0) || ((i == 0) && (i > 90000)))
            {
                errorProvider1.SetError(textBox2, "Please enter the fees and not more than 90000");
                textBox2.Focus();
                b = true;
            }
            if (textBox3.Text.Length == 0)
            {
                errorProvider1.SetError(textBox3, "Please enter the duration of program");
                textBox3.Focus();
                b = true;
            }
            return b;
        }

        private void comboBox1_Enter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(comboBox1, "Select Program from the list");
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(textBox1, "Enter number of semesters in a program");
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(textBox2, "Enter the total fees of the program");
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(textBox3, "Enter Duration of the program");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmd = new SqlCommand("select * from program where p_name=@p", con);
            cmd.Parameters.Add(new SqlParameter("@p",comboBox1.Text));
            dr=cmd.ExecuteReader();
            if(dr.Read())
            {
                textBox1.Text=dr[1].ToString();
                textBox2.Text=dr[2].ToString();
                textBox3.Text=dr[3].ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            con.Close();
            this.Close();
        }
    }
}
