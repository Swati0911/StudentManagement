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
    public partial class employee_details : Form
    {
        public employee_details()
        {
            InitializeComponent();
        }
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;

        private void eval_detail_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(connection.cs);
            con.Open();
            int id = 0;
            cmd = new SqlCommand("select max(e_id) from emp", con);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                string st = dr[0].ToString();
                if (st.Length > 0)
                {
                    id = Convert.ToInt32(st) + 1;
                }
                else
                    id = 101;
            }
            textBox1.Text = id.ToString();
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            dateTimePicker1.Value = DateTime.Now;
        }
       
        private void button1_Click(object sender, EventArgs e)//autogenerate the id
        {
            int id = 0;

            cmd = new SqlCommand("select max(e_id) from emp", con);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                string st = dr[0].ToString();
                if (st.Length > 0)
                {
                    id = Convert.ToInt32(st) + 1;
                }
                else
                    id = 101;
            }
            textBox1.Text = id.ToString();
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";                                    
            dateTimePicker1.Value = DateTime.Now;
            textBox1.Focus();
        }

        private void button2_Click(object sender, EventArgs e)//submit
        {
            if (check())
            {
                MessageBox.Show("Rectify Error");
            }
            else
            {
                cmd = new SqlCommand("insert into emp values(@e,@f,@l,@s,@j,@p)", con);
                cmd.Parameters.Add(new SqlParameter("@e", textBox1.Text));
                cmd.Parameters.Add(new SqlParameter("@f", textBox2.Text));
                cmd.Parameters.Add(new SqlParameter("@l", textBox3.Text));
                cmd.Parameters.Add(new SqlParameter("@s", textBox4.Text));
                cmd.Parameters.Add(new SqlParameter("@j", dateTimePicker1.Value));
                cmd.Parameters.Add(new SqlParameter("@p", textBox5.Text));
                cmd.ExecuteReader();
                MessageBox.Show("Record Saved", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                dateTimePicker1.Value = DateTime.Now;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            con.Close();
            this.Close();
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

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            checkalpha(sender, e);
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
            checkalpha(sender, e);
        }


        public bool check()
        {
           
               // int i ;
                bool b = false;
                errorProvider1.Clear();
                errorProvider1.BlinkRate = 100;
                errorProvider1.BlinkStyle = ErrorBlinkStyle.AlwaysBlink;
                if (textBox1.Text.Length == 0)
                {
                    errorProvider1.SetError(textBox1, "Please enter employee ID");
                    textBox1.Focus();
                    b = true;
                }
                if (textBox2.Text.Length == 0)
                {
                    errorProvider1.SetError(textBox2, "Please enter first name of employee");
                    textBox2.Focus();
                    b = true;
                }
                if (textBox3.Text.Length == 0)
                {
                    errorProvider1.SetError(textBox3, "Please enter second name of employee");
                    textBox3.Focus();
                    b = true;
                }
                if ((textBox4.Text.Length == 0) || ((Convert.ToInt32(textBox4.Text) < 5000) || (Convert.ToInt32(textBox4.Text) > 50000)))
                {
                    errorProvider1.SetError(textBox4, "Please enter employee's salary between 5000 to 50000");
                    textBox4.Focus();
                    b = true;
                }
                if (dateTimePicker1.Value > DateTime.Now)
                {
                    errorProvider1.SetError(dateTimePicker1, "Please enter proper date of joining");
                    dateTimePicker1.Focus();
                    b = true;
                }
                if (textBox5.Text.Length == 0)
                {
                    errorProvider1.SetError(textBox5, "Please enter program assign to employee");
                    textBox5.Focus();
                    b = true;
                }
                return b;
            }
            
        }
    }
