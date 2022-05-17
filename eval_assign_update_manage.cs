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
    public partial class eval_assign_update_manage : Form
    {
        public eval_assign_update_manage()
        {
            InitializeComponent();
        }
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;

        private void eval_assign_update_manage_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(connection.cs);
            con.Open();
            cmd = new SqlCommand("select c_code from sem_detail", con);
            cmd.Parameters.Add(new SqlParameter("@c", comboBox3.Text));
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox3.Items.Add(dr[0].ToString());
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmd = new SqlCommand("select sem from sem_detail where c_code=@c", con);
            cmd.Parameters.Add(new SqlParameter("@c", comboBox3.Text));
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                label8.Text = dr[0].ToString();
            }
            cmd = new SqlCommand("select * from evaluator where c_code=@c", con);
            cmd.Parameters.Add(new SqlParameter("@c",comboBox3.Text));
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                comboBox2.Items.Add(dr[0].ToString());
                dateTimePicker1.Value = Convert.ToDateTime(dr[2].ToString());
                dateTimePicker2.Value = Convert.ToDateTime(dr[3].ToString());
                dateTimePicker3.Value = Convert.ToDateTime(dr[4].ToString());
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmd = new SqlCommand("select f_name from emp where e_id=@e", con);
            cmd.Parameters.Add(new SqlParameter("@e", comboBox2.Text));
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                label5.Text = dr[0].ToString();
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
                cmd = new SqlCommand("update evaluator set e_id=@e,c_code=@c,r_date=@r,l_eval=@l,s_eval=@s where c_code=@c", con);
                cmd.Parameters.Add(new SqlParameter("@e", comboBox2.Text));
                cmd.Parameters.Add(new SqlParameter("@c", comboBox3.Text));
                cmd.Parameters.Add(new SqlParameter("@r", dateTimePicker1.Value));
                cmd.Parameters.Add(new SqlParameter("@l", dateTimePicker2.Value));
                cmd.Parameters.Add(new SqlParameter("@s", dateTimePicker3.Value));
                cmd.ExecuteReader();
                MessageBox.Show("Recorded Updated ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                comboBox3.Text = "Select Course";
                comboBox2.Text = "Select ID";
                dateTimePicker1.Value = DateTime.Now;
                dateTimePicker2.Value = DateTime.Now;
                dateTimePicker3.Value = DateTime.Now;
                label5.Text = "";
                label8.Text = "";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            con.Close();
            this.Close();
        }

        public bool check()
        {
            bool b = false;
            errorProvider1.Clear();
            errorProvider1.BlinkRate = 100;
            errorProvider1.BlinkStyle = ErrorBlinkStyle.AlwaysBlink;
            if (comboBox2.Text == "Select ID")
            {
                errorProvider1.SetError(comboBox2, "Please select Id for update");
                comboBox2.Focus();
                b = true;
            }
            if (comboBox3.Text == "Select Course")
            {
                errorProvider1.SetError(comboBox3, "Please select course for update");
                comboBox3.Focus();
                b = true;
            }
            if (dateTimePicker1.Value > DateTime.Now)
            {
                errorProvider1.SetError(dateTimePicker1,"Please update proper date of receiving a assignment");
                dateTimePicker1.Focus();
                b = true;
            }
            cmd = new SqlCommand("select dateadd(week,3,r_date) from evaluator where s_eval=dateadd(week,3,r_date)", con);
            dr = cmd.ExecuteReader();
            dr.Read();
            if (Convert.ToDateTime(dateTimePicker2.Value) < Convert.ToDateTime(dateTimePicker3.Value))
            {
                errorProvider1.SetError(dateTimePicker3, "Submission date is over assign new evaluator for evaluation");
                dateTimePicker3.Focus();
                b = true;
            }
            return b;
        }

        private void comboBox3_Enter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(comboBox3,"Select Course code for updation");
        }

        private void comboBox2_Enter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(comboBox2, "Select Employee Id for evaluation of assignment");
        }

        private void dateTimePicker1_Enter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(dateTimePicker1, "Update receiving date of assignment of an evaluator");
        }

        private void dateTimePicker2_Enter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(dateTimePicker2, "Updated automatically of submission date according to receving date");
        }

        private void dateTimePicker3_Enter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(dateTimePicker3, "Update Submission date of assignment by evaluator");
        }
    }
}
