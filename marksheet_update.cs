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
    public partial class marksheet_update : Form
    {
        public marksheet_update()
        {
            InitializeComponent();
        }
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
       
        private void button1_Click(object sender, EventArgs e)
        {
            if (check())
            {
                MessageBox.Show("Rectify Error");
            }
            else
            {
                cmd = new SqlCommand("update marksheet set enrol=@e,c_code=@c,assign_marks=@a,lab=@l,viva_marks=@v,theory_marks=@t,stats=@s where enrol=@e and c_code=@c", con);
                cmd.Parameters.Add(new SqlParameter("@e", comboBox1.Text));
                cmd.Parameters.Add(new SqlParameter("@c", comboBox2.Text));
                cmd.Parameters.Add(new SqlParameter("@a", label11.Text));
                cmd.Parameters.Add(new SqlParameter("@l", textBox1.Text));
                cmd.Parameters.Add(new SqlParameter("@v", label12.Text));
                cmd.Parameters.Add(new SqlParameter("@t", textBox2.Text));
                cmd.Parameters.Add(new SqlParameter("@s", label21.Text));
                cmd.ExecuteReader();
                MessageBox.Show("Record updated");
                label11.Text = "";
                label12.Text = "";
                textBox1.Text = "";
                textBox2.Text = "";
                label21.Text = "";
                comboBox1.Text = "Select Enrol";
                comboBox2.Text = "Select Course";
            }
        }

        private void marksheet_update_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(connection.cs);
            con.Open();
            cmd = new SqlCommand("select enrol from std", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr[0].ToString());
            }
            comboBox1.Text = "Select enrol";
            comboBox2.Text = "Select Course";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmd = new SqlCommand("select enrol,nam,prog,sem from std where enrol=@enrol", con);           
            cmd.Parameters.Add(new SqlParameter("@enrol", comboBox1.Text));
            dr = cmd.ExecuteReader();
            label5.Enabled = true;
            label6.Enabled = true;
            label16.Enabled = true;
            if (dr.Read())
            {
                label5.Text = dr[1].ToString();
                label6.Text = dr[2].ToString();
                label16.Text = dr[3].ToString();
            }           
            comboBox2.Items.Clear();
            cmd = new SqlCommand("select c_code from assign where enrol=@e", con);//collect c_code from assign(assignment result)
            cmd.Parameters.Add(new SqlParameter("@e", comboBox1.Text));
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox2.Items.Add(dr[0].ToString());
            }
            comboBox2.Text = "Select Course";
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmd = new SqlCommand("select assign.enrol,assign.c_code,assign.assign_marks,lab,assign.viva_marks,theory_marks,per,stats from marksheet,assign where assign.enrol=@e and assign.c_code=@c", con);
            cmd.Parameters.Add(new SqlParameter("@e", comboBox1.Text));
            cmd.Parameters.Add(new SqlParameter("@c", comboBox2.Text));
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                label11.Text = dr[2].ToString();
                label12.Text = dr[4].ToString();
                textBox1.Text = dr[3].ToString();
                textBox2.Text = dr[5].ToString();
                label20.Text = dr[6].ToString();
                label21.Text = dr[7].ToString();
            }
            else
            {
                label11.Text = "";
                label12.Text = "";
                textBox1.Text = "";
                textBox2.Text = "";
                label20.Text = "";
                label21.Text = "";
            }
        }

        public bool check()
        {
            bool b = false;
            errorProvider1.Clear();
            errorProvider1.BlinkRate = 100;
            errorProvider1.BlinkStyle = ErrorBlinkStyle.AlwaysBlink;
            if ((comboBox1.Text.Length == 0) || (comboBox1.Text == "Select enroll"))
            {
                errorProvider1.SetError(comboBox1, "Select existing enrollment number");
                comboBox1.Focus();
                b = true;
            }
            if ((comboBox2.Text.Length == 0) || (comboBox2.Text == "Select enroll"))
            {
                errorProvider1.SetError(comboBox2, "Select course code");
                comboBox2.Focus();
                b = true;
            }
            if (Convert.ToInt32(textBox1.Text) < 0 || Convert.ToInt32(textBox1.Text) > 100)
            {
                errorProvider1.SetError(textBox1, "Enter Correct practical marks");
                textBox1.Focus();
                b = true;
            }
            if (Convert.ToInt32(textBox2.Text) < 0 || Convert.ToInt32(textBox2.Text) > 100)
            {
                errorProvider1.SetError(textBox2, "Enter Correct practical marks");
                textBox2.Focus();
                b = true;
            }
            return b;
        }

        private void textBox2_Validating(object sender, CancelEventArgs e)
        {
            int a, b, c, d;
            if (textBox1.Text.Length == 0)
                a = 0;
            else
                a = (Convert.ToInt32(textBox1.Text));
            if (textBox2.Text.Length == 0)
                b = 0;
            else
                b = Convert.ToInt32(textBox2.Text);
            c = Convert.ToInt32(label11.Text);
            d = Convert.ToInt32(label12.Text);
            int t = (a + b) * 75 / 100 + (c + d) * 25 / 100;
            label19.Text = t.ToString();
            if (Convert.ToInt32(label19.Text) >= 40 && Convert.ToInt32(label19.Text) <= 100)
                label21.Text = "COMPLETED";
            else
                label21.Text = "NOT COMPLETED";
        }

        private void textBox1_Enter_1(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(textBox1, "Enter Practical marks");
        }

        private void textBox2_Enter_1(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(textBox2, "Enter term-end marks");
        }

        private void comboBox1_Enter_1(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(comboBox1, "Select enrollment number");
        }

        private void comboBox2_Enter_1(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(comboBox2, "Select course code");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            con.Close();
            this.Close();
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
    }
}
