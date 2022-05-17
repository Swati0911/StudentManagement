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
    public partial class eval_assign_manage : Form
    {
        public eval_assign_manage()
        {
            InitializeComponent();
        }

        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
              
        private void eval_assign_manage_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(connection.cs);
            con.Open();
            cmd = new SqlCommand("select p_name from program", con);
            cmd.Parameters.Add(new SqlParameter("@p", comboBox1.Text));
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr[0].ToString());
            }
            cmd = new SqlCommand("select distinct c_code from program_Assign where not exists (select * from evaluator where program_assign.c_code=evaluator.c_code)", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox3.Items.Add(dr[0].ToString());               
            }
            cmd = new SqlCommand("select distinct(e_id) from emp where not exists (select * from evaluator where emp.e_id=evaluator.e_id);", con);
           // cmd.Parameters.Add(new SqlParameter("@p", comboBox1.Text));
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox2.Items.Add(dr[0].ToString());
            }
            comboBox1.Focus();
            comboBox1.Text = "Select Program";
            comboBox2.Text = "Choose ID";
            comboBox3.Text = "Choose Course ID";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           /* cmd = new SqlCommand("select distinct(e_id), f_name from emp where not exists (select * from evaluator where emp.e_id=evaluator.e_id);", con);
            cmd.Parameters.Add(new SqlParameter("@p", comboBox1.Text));
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox2.Items.Add(dr[0].ToString());
            }*/
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmd = new SqlCommand("select f_name from emp where e_id=@e",con);
            cmd.Parameters.Add(new SqlParameter("@e",comboBox2.Text));
            dr=cmd.ExecuteReader();
            if (dr.Read())
            {
                label5.Text = dr[0].ToString();
            }           
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmd = new SqlCommand("select sem from sem_detail where c_code=@c",con);
            cmd.Parameters.Add(new SqlParameter("@c", comboBox3.Text));
            dr = cmd.ExecuteReader();                       
                if (dr.Read())
                {
                    label8.Text = dr[0].ToString();
                }
            }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DateTime rd, ld, sd;
            rd = dateTimePicker1.Value;
            ld = dateTimePicker2.Value;
            sd = rd.AddDays(21);
         /*   if (ld > rd.AddDays(14))
            {
                MessageBox.Show("Submit the assignment in a week");
            }*/

            dateTimePicker2.Value = sd;
        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            if (check())
            {
                MessageBox.Show("Rectify Error");
            }
            else
            {
                cmd = new SqlCommand("insert into evaluator values(@e_id,@c_code,@r_date,@l_eval,@s_eval)", con);
                cmd.Parameters.Add(new SqlParameter("@e_id", comboBox2.Text));
                cmd.Parameters.Add(new SqlParameter("@c_code", comboBox3.Text));
                cmd.Parameters.Add(new SqlParameter("@r_date", dateTimePicker1.Value));
                cmd.Parameters.Add(new SqlParameter("@l_eval", dateTimePicker3.Value));
                cmd.Parameters.Add(new SqlParameter("@s_eval", dateTimePicker2.Value));
                cmd.ExecuteReader();
                MessageBox.Show("Record Saved  ");
                comboBox1.Text = "Select Program";
                comboBox2.Text = "Choose ID";
                comboBox3.Text = "Choose Course ID";
                label5.Text = "...";
                label8.Text = "...";
                dateTimePicker1.Value = DateTime.Now;
                dateTimePicker2.Value = DateTime.Now;
                dateTimePicker3.Value = DateTime.Now;
                comboBox2.Items.Clear();
            }
        }

        private void dateTimePicker1_Enter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(dateTimePicker1, "Enter receiving date of assignment of evaluators");
        }
       
        private void dateTimePicker3_Enter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(dateTimePicker3, "enter Submission date of assignment by evaluator");           
        }

        private void comboBox1_Enter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(comboBox1, "Select Program");
        }

        private void comboBox2_Enter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(comboBox2, "Select Employee Id for evaluation of assignment");
        }

        private void comboBox3_Enter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(comboBox3, "Select Course for evalutor to evaluate");
        }

        private void dateTimePicker2_Enter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(dateTimePicker2, "Updated automatically of submission date according to receving date");
        }
       
        public bool check()
        {
            bool b = false;
            errorProvider1.Clear();
            errorProvider1.BlinkRate = 100;
            errorProvider1.BlinkStyle = ErrorBlinkStyle.AlwaysBlink;
            if (comboBox1.Text == "Select Program")
            {
                errorProvider1.SetError(comboBox1,"Please select program for insertion");
                comboBox1.Focus();
                b = true;
            }
            if (comboBox2.Text == "Choose ID")
            {
                errorProvider1.SetError(comboBox2, "Please select ID for insertion");
                comboBox2.Focus();
                b = true;
            }
            if (comboBox3.Text == "Choose Course ID")
            {
                errorProvider1.SetError(comboBox3,"Please select course code for inmsertion");
                comboBox3.Focus();
                b = true;
            }
            if (dateTimePicker1.Value > DateTime.Now)
            {
                errorProvider1.SetError(dateTimePicker1, "Please enter proper date of receving assignment for evaluation");
                dateTimePicker1.Focus();
                b = true;
            }
            cmd = new SqlCommand("select dateadd(week,3,r_date) from evaluator where s_eval=dateadd(week,3,r_date)", con);
            dr = cmd.ExecuteReader();
            dr.Read();
            if (Convert.ToDateTime(dateTimePicker2.Value) < Convert.ToDateTime(dateTimePicker3.Value))
            {
                errorProvider1.SetError(dateTimePicker3,"Submission date is over assign new evaluator for evaluation");
                dateTimePicker3.Focus();
                b = true;
            }
            return b;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            con.Close();
            this.Close();
        }           
        }
    }

