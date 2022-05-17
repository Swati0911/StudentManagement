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
    public partial class assign_result_update : Form
    {
        public assign_result_update()
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
                MessageBox.Show("Rectify error");
            }
            else
            {
                cmd = new SqlCommand("update assign set enrol=@e,c_code=@c,sub_date=@s,e_name=@nam,assign_marks=@a,viva_marks=@v where enrol=@e", con);
                cmd.Parameters.Add(new SqlParameter("@e", comboBox1.Text));
                cmd.Parameters.Add(new SqlParameter("@c", comboBox2.Text));
                cmd.Parameters.Add(new SqlParameter("@s", label14.Text));
                cmd.Parameters.Add(new SqlParameter("@nam", label15.Text));
                cmd.Parameters.Add(new SqlParameter("@a", textBox1.Text));
                cmd.Parameters.Add(new SqlParameter("@v", textBox2.Text));
                cmd.ExecuteReader();
                MessageBox.Show("Record Updated");
                comboBox1.Text = "...ID...";
                label10.Text = "...";
                label11.Text = "...";
                label7.Text = "...";
                textBox1.Text = "";
                label14.Text = "";
                textBox2.Text = "";
                comboBox2.Text = "Select course";
                label15.Text = "Select evaluator";
            }
        }

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
            cmd = new SqlCommand("select c_code from program_assign where exists (select * from assign where program_assign.c_code=assign.c_code and enrol=@e)", con);
            cmd.Parameters.Add(new SqlParameter("@e", comboBox1.Text));
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox2.Items.Add(dr[0].ToString());
            }      
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmd = new SqlCommand("select sub_date,e_name,assign_marks,viva_marks from assign where c_code=@c and enrol=@e", con);
            cmd.Parameters.Add(new SqlParameter("@e", comboBox1.Text));
            cmd.Parameters.Add(new SqlParameter("@c", comboBox2.Text));
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                label14.Text = dr[0].ToString();
                label15.Text = dr[1].ToString();
                textBox1.Text = dr[2].ToString();
                textBox2.Text = dr[3].ToString();
            }
            else
            {
                label14.Text = "N/A";
                label15.Text = "N/A";
                textBox2.Text = "";
                textBox1.Text = "";

            }
        }
        

        private void assign_result_update_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(connection.cs);
            con.Open();
            cmd = new SqlCommand("select distinct enrol from assign", con);
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

        public bool check()
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
                errorProvider1.SetError(comboBox2, "Please select course");
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

        private void button3_Click(object sender, EventArgs e)
        {
            con.Close();
            this.Close();
        }
    }
}
