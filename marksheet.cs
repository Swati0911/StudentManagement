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
    public partial class marksheet : Form
    {
        public marksheet()
        {
            InitializeComponent();
        }
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;

        private void marksheet_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(connection.cs);
            con.Open();
            cmd = new SqlCommand("select enrol from std", con);
            dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                comboBox1.Items.Add(dr[0].ToString());
            }
            comboBox1.Text = "Select enrol";
            comboBox2.Text="Select Course";
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
          //  cmd = new SqlCommand("select c_code from ASSIGN where enrol=@e" ,con);//collect course code from sem detail table
           // cmd = new SqlCommand("select distinct c_code from assign where not exists(select * from marksheet where marksheet.c_code=assign.c_code and enrol=@e)", con);
            cmd = new SqlCommand("select distinct c_code from assign where enrol=@e and not exists (select * from marksheet where marksheet.c_code=assign.c_code and enrol=@e)", con);
            cmd.Parameters.Add(new SqlParameter("@e", comboBox1.Text));
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox2.Items.Add(dr[0].ToString());
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)//collect course code from sem_detail table
        {
            cmd = new SqlCommand("select assign_marks,viva_marks from assign where enrol=@e and c_code=@c", con);
            cmd.Parameters.Add(new SqlParameter("@e", comboBox1.Text));
            cmd.Parameters.Add(new SqlParameter("@c", comboBox2.Text));
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                label11.Text = dr[0].ToString();
                label12.Text = dr[1].ToString();
            }
            else
            {
                label11.Text="N/A";
                label12.Text = "N/A";
            }
        }

        private void button2_Click(object sender, EventArgs e)//exit
        {
            con.Close();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)//insert
        {
            if (check())
            {
                MessageBox.Show("Rectify Error");
            }
            else
            {
                cmd = new SqlCommand("insert into marksheet values(@e,@c,@a,@prac,@v,@t,@per,@s)", con);
                cmd.Parameters.Add(new SqlParameter("@e", comboBox1.Text));
                cmd.Parameters.Add(new SqlParameter("@c", comboBox2.Text));
                cmd.Parameters.Add(new SqlParameter("@a", label11.Text));
                cmd.Parameters.Add(new SqlParameter("@prac", textBox1.Text));
                cmd.Parameters.Add(new SqlParameter("@v", label12.Text));
                cmd.Parameters.Add(new SqlParameter("@t", textBox2.Text));
                cmd.Parameters.Add(new SqlParameter("@per", label19.Text));
                cmd.Parameters.Add(new SqlParameter("@s", label21.Text));
                cmd.ExecuteReader();
                MessageBox.Show("Record Inserted");
                comboBox1.Text = "...Select ID...";
                comboBox2.Text = "..Select Course..";
                textBox1.Text = "";
                textBox2.Text = "";
                label21.Text = "...";
                label11.Text = "...";
                label12.Text = "...";                
                label6.Text = "...";
                label16.Text = "...";
                label19.Text = "...";
                label5.Text = "...";
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

        private void comboBox1_Enter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(comboBox1, "Select enrollment number");
        }

        private void comboBox2_Enter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(comboBox2, "Select course code");
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(textBox1, "Enter practical marks");
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(textBox2, "Enter term end marks");
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
            if (textBox1.Text.Length==0||(Convert.ToInt32(textBox1.Text)<0 || Convert.ToInt32(textBox1.Text)>100))
            {
                errorProvider1.SetError(textBox1, "Enter practical marks");
                textBox1.Focus();
                b = true;
            }
            if (textBox2.Text.Length == 0 || (Convert.ToInt32(textBox2.Text) < 0 || Convert.ToInt32(textBox2.Text) > 100))
            {
                errorProvider1.SetError(textBox2, "Enter theory marks");
                textBox2.Focus();
                b = true;
            }
            return b;
        }       

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            checknumeric(sender, e);
        }

        private void textBox2_Validating_1(object sender, CancelEventArgs e)
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

        private void comboBox1_Click(object sender, EventArgs e)
        {
            comboBox2.Text = "Select Course";
            label11.Text = "...";
            label12.Text = "...";
            textBox1.Text = "";
            textBox2.Text = "";
            label19.Text = "...";
            label21.Text = "...";
            label5.Text = "...";
            label6.Text = "...";
            label16.Text = "...";
        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        
    }
}
