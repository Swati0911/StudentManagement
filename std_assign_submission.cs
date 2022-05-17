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
    public partial class std_assign_submission : Form
    {
        public std_assign_submission()
        {
            InitializeComponent();
        }
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
      
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           cmd = new SqlCommand("select enrol,nam,prog,sem from std where enrol=@e ", con);
           cmd.Parameters.Add(new SqlParameter("@e", comboBox1.Text));           
           dr= cmd.ExecuteReader();
           if (dr.Read())
           {            
               label9.Text = dr[0].ToString();
               label10.Text = dr[1].ToString();
               label11.Text = dr[2].ToString();
           }
           comboBox2.Items.Clear();
           cmd = new SqlCommand("select c_code from program_assign where not exists (select * from assign_sub where program_assign.c_code=assign_sub.c_code and enrol=@e) ", con);
           cmd.Parameters.Add(new SqlParameter("@e", comboBox1.Text));
           dr = cmd.ExecuteReader();
           while (dr.Read())
           {
               comboBox2.Items.Add(dr[0].ToString());
           }
        }

        private void std_assign_submission_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(connection.cs);
            con.Open();
            cmd = new SqlCommand("Select enrol from std", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr[0].ToString());
            }           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (check())
            {
                MessageBox.Show("Rectify Error");
            }
            else
            {
                cmd = new SqlCommand("insert into assign_sub values (@e,@c,@a,@s)", con);
                cmd.Parameters.Add(new SqlParameter("@e", comboBox1.Text));
                cmd.Parameters.Add(new SqlParameter("@c", comboBox2.Text));
                cmd.Parameters.Add(new SqlParameter("@a", label12.Text));
                cmd.Parameters.Add(new SqlParameter("@s", dateTimePicker1.Value));
                cmd.ExecuteReader();
                MessageBox.Show("Record Inserted");
                comboBox1.Text = "Select enrol";
                comboBox2.Text = "Select Course";
                label12.Text = "";
                dateTimePicker1.Value = DateTime.Now;
                label13.Text = "";
                label9.Text = "";
                label10.Text = "";
                label11.Text = "";
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmd = new SqlCommand("select assign_code from program_assign where c_code=@c", con);
            cmd.Parameters.Add(new SqlParameter("@c", comboBox2.Text));
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                label12.Text = dr[0].ToString();
            }
            else
                label12.Text = "N/A";
        }

        public bool check()
        {
            bool b = false;
            errorProvider1.Clear();
            errorProvider1.BlinkRate = 100;
            errorProvider1.BlinkStyle = ErrorBlinkStyle.AlwaysBlink;
            if (dateTimePicker1.Value > DateTime.Now)
            {
                errorProvider1.SetError(dateTimePicker1, "Cannot insert data in advance");
            }
            else
            {
                cmd = new SqlCommand("select l_sub from program_assign where c_code=@c",con);
                cmd.Parameters.Add(new SqlParameter("@c", comboBox2.Text));
               dr= cmd.ExecuteReader();
                dr.Read();
                DateTime a = Convert.ToDateTime(dr[0].ToString());
                if (dateTimePicker1.Value > a)
                {
                    errorProvider1.SetError(dateTimePicker1, "Submission date is over");
                    label13.Text = a.ToString();
                    dateTimePicker1.Focus();
                    b = true;
                }
            }
            return b;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            con.Close();
            this.Close();
        }

        private void comboBox1_Enter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(comboBox1, "Select Enrollment number");
        }

        private void comboBox2_Enter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(comboBox2, "Select Course code for submission of assignment");
        }

        private void dateTimePicker1_Enter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(dateTimePicker1,"Enter Submission date of assignment");
        }       
    }
}
