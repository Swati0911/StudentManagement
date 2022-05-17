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
    public partial class update_student_details : Form
    {
        public update_student_details()
        {
            InitializeComponent();
        }
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;       

        private void update_student_details_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(connection.cs);
            con.Open();
            cmd = new SqlCommand("Select enrol from std", con);
            dr = cmd.ExecuteReader();
            comboBox5.Text = "Select enrol";
            while (dr.Read())
            {
                comboBox5.Items.Add(dr[0].ToString());
            }
            comboBox5.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (check())
            {
                MessageBox.Show("Rectify Eror");
            }
            else
            {
                cmd = new SqlCommand("update std set enrol=@enrol,app_no=@app_no,nam=@name,f_name=@f_name,addr=@addr,city=@city,stat=@state,pin_code=@pin_code,b_date=@b_date,a_date=@a_date,email=@email,prog=@course,sem=@sem,contact_no=@contact,gender=@gender where enrol=@enrol", con);
                cmd.Parameters.Add(new SqlParameter("@enrol", comboBox5.Text));
                cmd.Parameters.Add(new SqlParameter("@app_no", textBox2.Text));
                cmd.Parameters.Add(new SqlParameter("@name", textBox3.Text));
                cmd.Parameters.Add(new SqlParameter("@f_name", textBox4.Text));
                cmd.Parameters.Add(new SqlParameter("@addr", textBox5.Text));
                cmd.Parameters.Add(new SqlParameter("@city", comboBox1.Text));
                cmd.Parameters.Add(new SqlParameter("@state", comboBox2.Text));
                cmd.Parameters.Add(new SqlParameter("@pin_code", textBox8.Text));
                cmd.Parameters.Add(new SqlParameter("@b_date", dateTimePicker1.Value));
                cmd.Parameters.Add(new SqlParameter("@a_date", dateTimePicker2.Value));
                cmd.Parameters.Add(new SqlParameter("@email", textBox6.Text));
                cmd.Parameters.Add(new SqlParameter("@course", comboBox3.Text));
                cmd.Parameters.Add(new SqlParameter("@sem", comboBox4.Text));
                cmd.Parameters.Add(new SqlParameter("@contact", textBox7.Text));
                if (radioButton1.Checked)
                    cmd.Parameters.Add(new SqlParameter("@gender", radioButton1.Text));
                else
                    cmd.Parameters.Add(new SqlParameter("@gender", radioButton2.Text));
                cmd.ExecuteReader();

                MessageBox.Show("Recorded Updated ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                comboBox5.Text = "... SELECT ENROLL...";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                textBox7.Text = "";
                textBox8.Text = "";
                comboBox1.Text = "Select City";
                comboBox2.Text = "Select State";
                comboBox3.Text = "Select Program";
                comboBox4.Text = "Select Sem";
                dateTimePicker1.Value = DateTime.Now;
                dateTimePicker2.Value = DateTime.Now;
                radioButton1.Checked = false;
                radioButton2.Checked = false;
                comboBox5.Focus();
            }
        }       

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)//retrieve all data of selected enroll.
        {
            cmd = new SqlCommand("select * from std where enrol=@e", con);
            cmd.Parameters.Add(new SqlParameter("@e", comboBox5.Text));
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                textBox2.Text = dr[1].ToString();
                textBox3.Text = dr[2].ToString();
                textBox4.Text = dr[3].ToString();
                textBox5.Text = dr[4].ToString();
                comboBox1.Text = dr[5].ToString();
                comboBox2.Text = dr[6].ToString();
                textBox8.Text = dr[7].ToString();
                dateTimePicker1.Value = Convert.ToDateTime(dr[8].ToString());
                dateTimePicker2.Value = Convert.ToDateTime(dr[9].ToString());
                textBox6.Text = dr[10].ToString();
                comboBox3.Text = dr[11].ToString();
                comboBox4.Text = dr[12].ToString();
                textBox7.Text = dr[13].ToString();
                string gender = dr[14].ToString();
                if (gender == "Male")
                    radioButton1.Checked = true;
                else radioButton2.Checked = true;
            }
            else
                MessageBox.Show("Data Not Found");
        }

        private void button4_Click(object sender, EventArgs e)
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

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            checknumeric(sender, e);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            checkalpha(sender, e);
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            checkalpha(sender, e);
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            checkalpha(sender, e);
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            checknumeric(sender, e);
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            checknumeric(sender, e);
        }

        public bool check()
        {
            bool b = false;
            errorProvider1.Clear();
            errorProvider1.BlinkRate = 100;
            errorProvider1.BlinkStyle = ErrorBlinkStyle.AlwaysBlink;
            if ((comboBox5.Text.Length == 0) || (comboBox5.Text == "Select enroll")) 
            {
                errorProvider1.SetError(comboBox5, "Select existing enrollment number");
                comboBox5.Focus();
                b = true;
            }
            if (textBox2.Text.Length == 0)
            {
                errorProvider1.SetError(textBox2, "Please enter application number");
                textBox2.Focus();
                b = true;
            }
            if (textBox3.Text.Length == 0)
            {
                errorProvider1.SetError(textBox3, "Please enter Name");
                textBox3.Focus();
                b = true;
            }
            if (textBox4.Text.Length == 0)
            {
                errorProvider1.SetError(textBox4, "Please enter Father's name");
                textBox4.Focus();
                b = true;
            }
            if (textBox5.Text.Length == 0)
            {
                errorProvider1.SetError(textBox5, "Please enter Address");
                textBox5.Focus();
                b = true;
            }
            if ((comboBox1.Text == "Select City") || (comboBox1.Text.Length == 0))
            {
                errorProvider1.SetError(comboBox1, "Please enter city");
                comboBox1.Focus();
                b = true;
            }
            if ((comboBox2.Text == "Select State") || (comboBox2.Text.Length == 0))
            {
                errorProvider1.SetError(comboBox2, "Please enter State");
                comboBox2.Focus();
                b = true;
            }
            if ((comboBox3.Text == "Select Program") || (comboBox3.Text.Length == 0))
            {
                errorProvider1.SetError(comboBox3, "Please enter Program");
                comboBox3.Focus();
                b = true;
            }
            if ((comboBox4.Text == "Select State") || (comboBox4.Text.Length == 0))
            {
                errorProvider1.SetError(comboBox4, "Please enter Semmester");
                comboBox4.Focus();
                b = true;
            }
            if ((textBox8.Text.Length == 0) || (textBox8.Text.Length >= 7))
            {
                errorProvider1.SetError(textBox8, "Please enter Postal code");
                textBox8.Focus();
                b = true;
            }
            if (dateTimePicker2.Value > DateTime.Now)
            {
                errorProvider1.SetError(dateTimePicker2, "Please enter admission date");
                dateTimePicker2.Focus();
                b = true;
            }
            if (textBox6.Text.Length == 0)
            {
                errorProvider1.SetError(textBox6, "Please enter email id");
                textBox6.Focus();
                b = true;
            }
            if ((textBox7.Text.Length == 0) || (textBox7.Text.Length >= 11))
            {
                errorProvider1.SetError(textBox7, "Please enter Contact number");
                textBox7.Text = "";
                textBox7.Focus();
                b = true;
            }
            else
            {
                cmd = new SqlCommand("select datediff(yy,@d,getdate())", con);
                cmd.Parameters.Add(new SqlParameter("@d", dateTimePicker1.Value));
                dr = cmd.ExecuteReader();
                dr.Read();
                int a = Convert.ToInt32(dr[0].ToString());
                if (a <= 19)
                {
                    errorProvider1.SetError(dateTimePicker1, "Age should be greater than 18");
                    dateTimePicker1.Focus();
                    b = true;
                }
            }
            return b;
        }

        private void comboBox5_Enter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(comboBox5,"Select Enrollment no.");
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(textBox3, "Update student Name");
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(textBox2,"Update application number");
        }

        private void textBox4_Enter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(textBox4, "Update Father's Name");
        }

        private void textBox5_Enter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(textBox5, "Update Address");
        }

        private void comboBox1_Enter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(comboBox1, "Select City");
        }

        private void comboBox2_Enter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(comboBox2, "Select State");
        }

        private void textBox8_Enter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(textBox8, "Update Postal Code");
        }

        private void dateTimePicker1_Enter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(dateTimePicker1, "Update Date of Birth");
        }

        private void dateTimePicker2_Enter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(dateTimePicker2, "Update Admission Date");
        }

        private void textBox7_Enter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(textBox7, "Update Contact Number");
        }

        private void comboBox3_Enter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(comboBox3, "Update Program");
        }

        private void textBox6_Enter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(textBox6, "Update email ID");
        }

        private void comboBox4_Enter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(comboBox4, "Update Semester");
        }

       
    }
}
