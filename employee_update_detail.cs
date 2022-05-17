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
    public partial class employee_update_detail : Form
    {
        public employee_update_detail()
        {
            InitializeComponent();
        }
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
       
        private void eval_update_detail_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(connection.cs);
            con.Open();
            cmd = new SqlCommand("Select e_id from emp order by e_id", con);
            dr = cmd.ExecuteReader();
            comboBox1.Text = "Select ID";
            while (dr.Read())
            {
                comboBox1.Items.Add(dr[0].ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (check())
            {
                MessageBox.Show("Rectify error");
            }
            else
            {
                cmd = new SqlCommand("update emp set e_id=@e,f_name=@f,l_name=@l,sal=@s,d_join=@j,p_name=@p where e_id=@e", con);
                cmd.Parameters.Add(new SqlParameter("@e", comboBox1.Text));
                cmd.Parameters.Add(new SqlParameter("@f", textBox1.Text));
                cmd.Parameters.Add(new SqlParameter("@l", textBox2.Text));
                cmd.Parameters.Add(new SqlParameter("@s", textBox3.Text));
                cmd.Parameters.Add(new SqlParameter("@j", dateTimePicker1.Value));
                cmd.Parameters.Add(new SqlParameter("@p", textBox4.Text));
                cmd.ExecuteReader();
                MessageBox.Show("Recorded Updated ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                comboBox1.Text = "Select ID";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox1.Text = "";
                dateTimePicker1.Value = DateTime.Now;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            con.Close();
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
             cmd = new SqlCommand("select * from emp where e_id=@e", con);
            cmd.Parameters.Add(new SqlParameter("@e", comboBox1.Text));
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                textBox1.Text = dr[1].ToString();
                textBox2.Text = dr[2].ToString();
                textBox3.Text = dr[3].ToString();
                dateTimePicker1.Value = Convert.ToDateTime(dr[4].ToString());
                textBox4.Text = dr[5].ToString();
            }
        }

        public bool check()
        {
           // int a;
            bool b = false;
            errorProvider1.Clear();
            errorProvider1.BlinkRate = 100;
            errorProvider1.BlinkStyle = ErrorBlinkStyle.AlwaysBlink;
            if ((comboBox1.Text.Length == 0) || (comboBox1.Text == "Select ID"))
            {
                errorProvider1.SetError(comboBox1, "Please enter employee ID");
                comboBox1.Focus();
                b = true;
            }
            if (textBox1.Text.Length == 0)
            {
                errorProvider1.SetError(textBox1, "Please enter first name of employee");
                textBox1.Focus();
                b = true;
            }
            if (textBox2.Text.Length == 0)
            {
                errorProvider1.SetError(textBox2, "Please enter second name of employee");
                textBox2.Focus();
                b = true;
            }
            if (textBox3.Text.Length == 0) 
            {
                errorProvider1.SetError(textBox3, "Please enter employee's salary");
                textBox3.Focus();
                b = true;
            }
            if (dateTimePicker1.Value > DateTime.Now)
            {
                errorProvider1.SetError(dateTimePicker1, "Please enter proper date of joining");
                dateTimePicker1.Focus();
                b = true;
            }
            if (textBox4.Text.Length == 0)
            {
                errorProvider1.SetError(textBox4, "Please enter program assign to employee");
                textBox4.Focus();
                b = true;
            }
            else
            {
                int i = Convert.ToInt32(textBox3.Text);
                if ((i < 5000) || (i > 50000))
                {
                    errorProvider1.SetError(textBox3, "Please enter salary between 5000 to 50000");
                    textBox3.Focus();
                    b = true;
                }
            }
            return b;
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
            checkalpha(sender, e);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            checkalpha(sender, e);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            checknumeric(sender, e);
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            checkalpha(sender, e);
        }

        private void comboBox1_Enter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(comboBox1, "Select Employee ID");
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(textBox1, "Update First Name of employee");
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(textBox2, "Update last name");
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(textBox3, "Upudate Salary of employee");
        }

        private void textBox4_Enter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(textBox4, "Update the programn");
        }

        private void dateTimePicker1_Enter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(dateTimePicker1, "Update Joining date og Employee");
        }

    }
}
