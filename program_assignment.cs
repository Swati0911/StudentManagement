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
    public partial class program_assignment : Form
    {
        public program_assignment()
        {
            InitializeComponent();
        }
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;        
       
        private void program_assignment_Load(object sender, EventArgs e)//form load
        {
            con =new SqlConnection(connection.cs);//connection develop
            con.Open();
            comboBox1.Focus();
            cmd=new SqlCommand("select p_name from program",con);
            dr=cmd.ExecuteReader();
            while(dr.Read())
            {
                comboBox1.Items.Add(dr[0].ToString());
            }
            comboBox1.Text="Select Program";
            comboBox2.Text = "Select Semester";
            comboBox3.Text = "Select Course";
        }

        private void button1_Click(object sender, EventArgs e)//insertion
        {
            if (check())
            {
                MessageBox.Show("Rectify error");
            }
            else
            {
            cmd = new SqlCommand("insert into program_assign values(@p,@st,@c,@sem,@a,@t,@l,@m,@v)",con);
            cmd.Parameters.Add(new SqlParameter("@p",comboBox1.Text));//PROGRAM
            cmd.Parameters.Add(new SqlParameter("@st", textBox1.Text));//STUDY CENTRE
            cmd.Parameters.Add(new SqlParameter("@c", comboBox3.Text));//COURSE_CODE
            cmd.Parameters.Add(new SqlParameter("@sem", comboBox2.Text));//SEMESTER
            cmd.Parameters.Add(new SqlParameter("@a", textBox2.Text));//ASSIGN CODE
            cmd.Parameters.Add(new SqlParameter("@t", textBox3.Text));//COURSE TITLE
            cmd.Parameters.Add(new SqlParameter("@l",dateTimePicker1.Value));//last date of submission
            cmd.Parameters.Add(new SqlParameter("@m", textBox4.Text));//assign marks
            cmd.Parameters.Add(new SqlParameter("@v", textBox5.Text));//viva
            cmd.ExecuteReader();
            MessageBox.Show("Record Inserted");
            comboBox1.Text = "Select Program";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            comboBox2.Text = "Select Semester";
            dateTimePicker1.Value = DateTime.Now;
            comboBox3.Text = "Select Course";
            comboBox3.Items.Clear();
            }
        }

        public bool check()
        {
            bool b = false;
            errorProvider1.Clear();
            errorProvider1.BlinkRate = 100;
            errorProvider1.BlinkStyle = ErrorBlinkStyle.AlwaysBlink;
            if (comboBox1.Text == "Select program")
            {
                errorProvider1.SetError(comboBox1, "Please choose program");
                comboBox1.Focus();
                b = true;
            }
            if(textBox1.Text=="")
            {
                errorProvider1.SetError(textBox1,"Please enter study center code");
                textBox1.Focus();
                b = true;
            }
            if (comboBox2.Text == "Select Semester")
            {
                errorProvider1.SetError(comboBox2, "Please choose Semester");
                comboBox2.Focus();
                b = true;
            }
            if (comboBox3.Text == "Select Course")
            {
                errorProvider1.SetError(comboBox3, "Please choose Course");
                comboBox3.Focus();
                b = true;
            }
            if(textBox2.Text=="")
            {
                errorProvider1.SetError(textBox2,"Please enter assignment code");
                textBox2.Focus();
                b=true;
            }
            if (textBox3.Text == "")
            {
                errorProvider1.SetError(textBox3, "Please enter Course title");
                textBox3.Focus();
                b = true;
            }
            if ((textBox4.Text.Length == 0)|| (Convert.ToInt32(textBox4.Text)>80))
            {
                errorProvider1.SetError(textBox4, "Please enter maximum assignment marks(theory/80)");
                textBox4.Focus();
                b = true;
            }
            if ((textBox5.Text.Length == 0) || (Convert.ToInt32(textBox5.Text) > 20))
            {
                errorProvider1.SetError(textBox5, "Please enter maximum viva marks(20) of the assignment");
                textBox5.Focus();
                b = true;
            }

            return b;
        }            

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmd = new SqlCommand("select c_code from sem_detail where p_name=@p AND sem=@s", con);
            cmd.Parameters.Add(new SqlParameter("@p", comboBox1.Text));
            cmd.Parameters.Add(new SqlParameter("@s", comboBox2.Text));
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox3.Items.Add(dr[0].ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)//close connection
        {
            con.Close();
            this.Close();
        }

        private void comboBox1_Enter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(comboBox1,"Select Program for assignment");
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(textBox1, "Enter Proper center code(study center)");
        }

        private void comboBox2_Enter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(comboBox2, "Enter Semester");
        }

        private void comboBox3_Enter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(comboBox3, "Enter Course Code according to Semester");
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(textBox2, "Enter assignment code for the course");
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(textBox3, "Enter Course Title of the course");
        }

        private void dateTimePicker1_Enter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(dateTimePicker1, "Insert the last date of submission");
        }

        private void textBox4_Enter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(textBox4, "Enter the total marks of assignment(theory)");
        }

        private void textBox5_Enter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(textBox5, "Enter the total marks of viva of the assignment");
        }

        private void checkalpha(object sender, EventArgs e)
        {
            TextBox t = (TextBox)sender;
            char[] ch = t.Text.ToCharArray();
            bool b = false;
            for (int i = 0; i < ch.Length; i++)
            {
                if (ch[i] >= 65 && ch[i] <= 90 || ch[i] >= 97 && ch[i] <= 122 || ch[i] == 32)
                    b = false;
                else
                {
                    b = true;
                    break;
                }
            }
            if (b)
            {
                MessageBox.Show("Please enter only alphabets");
                t.Text = "";
            }
        }

        private void checknumeric(object sender, EventArgs e)
        {
            TextBox t = (TextBox)sender;
            char[] ch = t.Text.ToCharArray();
            for (int i = 0; i < ch.Length; i++)
            {
                if (ch[i] < 48 || ch[i] > 57)
                {
                    MessageBox.Show("Please enter numeric value");
                    t.Text = "";
                }
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            checknumeric(sender, e);
        }       

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            checkalpha(sender, e);
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            checknumeric(sender, e);
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            checknumeric(sender, e);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }       
      
    }
}
