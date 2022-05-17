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
    public partial class prog_assigncode_update : Form
    {
        public prog_assigncode_update()
        {
            InitializeComponent();
        }

        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
       
        private void prog_assigncode_update_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(connection.cs);//connection develop
            con.Open();                       
            cmd = new SqlCommand("select c_code from program_assign",con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox3.Items.Add(dr[0].ToString());
            }         
            comboBox3.Text = "Select Course";
            comboBox3.Focus();
        }

      
        private void button2_Click(object sender, EventArgs e)
        {
            if (check())
            {
                MessageBox.Show("Rectify Error");
            }
            else
            {
                cmd = new SqlCommand("update program_assign set p_name=@p,s_centre=@st,c_code=@c,sem=@sem,assign_code=@a,c_title=@t,l_sub=@l,assign_marks=@m,viva=@v where c_code=@c ", con);
                cmd.Parameters.Add(new SqlParameter("@p", label11.Text));//PROGRAM
                cmd.Parameters.Add(new SqlParameter("@st", textBox1.Text));//STUDY CENTRE
                cmd.Parameters.Add(new SqlParameter("@c", comboBox3.Text));//COURSE_CODE
                cmd.Parameters.Add(new SqlParameter("@sem", label12.Text));//SEMESTER
                cmd.Parameters.Add(new SqlParameter("@a", textBox2.Text));//ASSIGN CODE
                cmd.Parameters.Add(new SqlParameter("@t", textBox3.Text));//COURSE TITLE
                cmd.Parameters.Add(new SqlParameter("@l", dateTimePicker1.Value));//last date of submission
                cmd.Parameters.Add(new SqlParameter("@m", textBox4.Text));//assign marks
                cmd.Parameters.Add(new SqlParameter("@v", textBox5.Text));//viva
                cmd.ExecuteReader();
                MessageBox.Show("Record Updated");               
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";             
                comboBox3.Text = "Select Course";
            }
        }

       
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmd = new SqlCommand("select * from program_assign where c_code=@c ", con);           
            cmd.Parameters.Add(new SqlParameter("@c", comboBox3.Text));
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                textBox1.Text = dr[1].ToString();
                label11.Text = dr[0].ToString();//p_code
                label12.Text = dr[3].ToString();//sem
                textBox2.Text = dr[4].ToString();
                textBox3.Text = dr[5].ToString();
                textBox4.Text = dr[7].ToString();
                textBox5.Text = dr[8].ToString();
                dateTimePicker1.Value = Convert.ToDateTime(dr[6].ToString());
            }
            else
                MessageBox.Show("Data not found");
        }

        public bool check()
        {
            int i = Convert.ToInt32(textBox4.Text);
            int j = Convert.ToInt32(textBox5.Text);
            bool b = false;
            errorProvider1.Clear();
            errorProvider1.BlinkRate = 500;
            errorProvider1.BlinkStyle = ErrorBlinkStyle.AlwaysBlink;
            if (comboBox3.Text == "Select Course")
            {
                errorProvider1.SetError(comboBox3, "Please select course");
                comboBox3.Focus();
                b = true;
            }
            if (textBox1.Text.Length == 0)
            {
                errorProvider1.SetError(textBox1, "Please enter study center");
                textBox1.Focus();
                b = true;
            }           
            if (textBox2.Text.Length == 0)
            {
                errorProvider1.SetError(textBox2, "Please enter assignment code");
                textBox2.Focus();
                b = true;
            }
            if (textBox3.Text.Length == 0)
            {
                errorProvider1.SetError(textBox3, "Please enter course title");
                textBox3.Focus();
                b = true;
            }
            if (dateTimePicker1.Value > DateTime.Now)
            {
                errorProvider1.SetError(dateTimePicker1, "Enter Proper date of submission");
                dateTimePicker1.Focus();
                b = true;
            }
            if ((textBox4.Text.Length == 0) || ((i == 0) && (i > 80)))
            {
                errorProvider1.SetError(textBox4, "Please enter assignment marks");
                textBox4.Focus();
                b = true;
            }
            if ((textBox5.Text.Length == 0) || ((j == 0) && (j > 20)))
            {
                errorProvider1.SetError(textBox5, "Please enter viva marks");
                textBox5.Focus();
                b = true;
            }
            return b;
        }

        private void comboBox3_Enter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(comboBox3,"Please select course code");
        }
      
        private void textBox1_Enter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(textBox1, "Enter study center for program");
        }
       
        private void textBox2_Enter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(textBox2, "Update assignment code");
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(textBox3, "Update course title");
        }

        private void dateTimePicker1_Enter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(dateTimePicker1, "Update last date of submission");
        }

        private void textBox4_Enter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(textBox4, "Update assignment marks");
        }

        private void textBox5_Enter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(textBox5, "Update viva marks");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            con.Close();
            this.Close();
        }
    }
}
