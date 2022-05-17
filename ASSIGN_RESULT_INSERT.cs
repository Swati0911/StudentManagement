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
    public partial class ASSIGN_RESULT_INSERT : Form
    {
        public ASSIGN_RESULT_INSERT()
        {
            InitializeComponent();
        }
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmd = new SqlCommand("select enrol,nam,prog,sem from std where enrol=@enrol", con);
            cmd.Parameters.Add(new SqlParameter("@enrol", comboBox1.Text));
            dr = cmd.ExecuteReader();           
            label10.Enabled = true;
            label11.Enabled = true;
            label7.Enabled = true;
            if (dr.Read())
            {
                label10.Text = dr[1].ToString();
                label11.Text = dr[2].ToString();
                label7.Text = dr[3].ToString();
            }
            comboBox2.Items.Clear();
            //cmd = new SqlCommand("select program_assign.c_code from assign_sub,program_assign where program_assign.assign_code=assign_sub.assign_code and enrol=@e", con);
            cmd = new SqlCommand("select distinct c_code from assign_sub where not exists (select * from assign where assign.c_code=assign_sub.c_code and enrol=@e)", con);
            cmd.Parameters.Add(new SqlParameter("@e", comboBox1.Text));
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox2.Items.Add(dr[0].ToString());              
            }           
        }

        private void assignment_management_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(connection.cs);
            con.Open();
            cmd = new SqlCommand("select enrol from std", con);
            dr = cmd.ExecuteReader();
            comboBox1.Text = "Select ID";
            label10.Enabled = false;
            label11.Enabled = false;
            label7.Enabled = false;
            while (dr.Read())
            {
                comboBox1.Items.Add(dr[0].ToString());                              
            }
            comboBox1.Focus();
            comboBox2.Items.Clear();
            cmd = new SqlCommand("select distinct c_code from assign where c_code=@c", con);
            cmd.Parameters.Add(new SqlParameter("@c", comboBox2.Text));
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox2.Items.Add(dr[0].ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)//insert marks of assignment
        {
            if (check())
            {
                MessageBox.Show("Rectify error");
            }
            else
            {
                cmd = new SqlCommand("insert into assign values (@enrol,@c_code,@sub_date,@e_name,@assign,@viva)", con);
                cmd.Parameters.Add(new SqlParameter("@enrol", comboBox1.Text));
                cmd.Parameters.Add(new SqlParameter("@c_code", comboBox2.Text));
                cmd.Parameters.Add(new SqlParameter("@sub_date", label14.Text));
                cmd.Parameters.Add(new SqlParameter("@e_name", label15.Text));
                cmd.Parameters.Add(new SqlParameter("@assign", textBox1.Text));
                cmd.Parameters.Add(new SqlParameter("@viva", textBox2.Text));
                cmd.ExecuteReader();
                MessageBox.Show("Record Saved", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                comboBox1.Text = "Select ID";
                label10.Text = "...";
                label11.Text = "...";
                label7.Text = "...";
                comboBox2.Text = "Choose Course";
                textBox1.Text = "";
                label14.Text = "";
                textBox2.Text = "";
                label15.Text = "";
               
            }
        }

        private void button3_Click(object sender, EventArgs e)//exit the application.
        {
            con.Close();
            this.Close();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {            
            cmd = new SqlCommand("select sub_date,emp.f_name from evaluator,emp,assign_sub where evaluator.e_id=emp.e_id and evaluator.c_code=@c and enrol=@e", con);
            cmd.Parameters.Add(new SqlParameter("@e", comboBox1.Text));
            cmd.Parameters.Add(new SqlParameter("@c", comboBox2.Text));
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                label14.Text = dr[0].ToString();
                label15.Text = dr[1].ToString();
            }
            else
            {
                label14.Text = "N/A";
                label15.Text = "N/A";
            }
            
        }
            
        public bool check()//rectifying error through errorprovider
        {
            bool b = false;
            errorProvider1.Clear();
            errorProvider1.BlinkRate = 100;
            errorProvider1.BlinkStyle = ErrorBlinkStyle.AlwaysBlink;
            if (comboBox1.Text == "Select ID")
            {
                errorProvider1.SetError(comboBox1, "Please choose Enrol ID");
                comboBox1.Focus();
                b = true;
            }
            if (comboBox2.Text == "Choose Course")
            {
                errorProvider1.SetError(comboBox2,"Please select course");
                comboBox2.Focus();
                b = true;
            }          
            if ((textBox1.Text.Length == 0) || (Convert.ToInt32(textBox1.Text) < 0 || Convert.ToInt32(textBox1.Text) > 80))
            {
                errorProvider1.SetError(textBox1, "Please submit the assigment marks of student(Max marks(80)");
                textBox1.Focus();
                b = true;
            }
            if ((textBox2.Text.Length == 0) || (Convert.ToInt32(textBox2.Text) < 0 || Convert.ToInt32(textBox2.Text) > 80))
            {
                errorProvider1.SetError(textBox1, "Please submit the viva marks of student(Max marks:20)");
                textBox1.Focus();
                b = true;
            }
            return b;
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

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            checknumeric(sender, e);
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(textBox1, "Submit Assignment marks obtain by student");
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(textBox2, "ENter viva marks obtained by student");
        }

        private void comboBox1_Enter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(comboBox1, "Select Enrollmemt number");
        }

        private void comboBox2_Enter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(comboBox2, "Select Course code submitted by studennt");
        }
              
    }
}
