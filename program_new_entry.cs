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
    public partial class program_new_entry : Form
    {
        public program_new_entry()
        {
            InitializeComponent();
        }
        SqlConnection con;
        SqlCommand cmd;      
       
        private void program_new_entry_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(connection.cs);
            con.Open();
            textBox1.Focus();      
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (check())
            {
                MessageBox.Show("Rectify error");
            }
            else
            {
                cmd = new SqlCommand("insert into program values(@p,@s,@f,@d) ", con);
                cmd.Parameters.Add(new SqlParameter("@p", textBox1.Text));
                cmd.Parameters.Add(new SqlParameter("@s", textBox2.Text));
                cmd.Parameters.Add(new SqlParameter("@f", textBox3.Text));
                cmd.Parameters.Add(new SqlParameter("@d", textBox4.Text));
                cmd.ExecuteReader();
                MessageBox.Show("Record Saved");
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox1.Focus();
            }
        }

        public bool check()
        {
            int i = Convert.ToInt32(textBox3.Text);
            bool b = false;
            errorProvider1.Clear();
            errorProvider1.BlinkRate = 100;
            errorProvider1.BlinkStyle = ErrorBlinkStyle.AlwaysBlink;
            if(textBox1.Text.Length==0)
            {
                errorProvider1.SetError(textBox1,"Please enter the program name");
                textBox1.Focus();
                b=true;
            }
             if(textBox2.Text.Length==0)
            {
                errorProvider1.SetError(textBox2,"Please enter number of semester in program");
                textBox2.Focus();
                b=true;
            }
             if((textBox3.Text.Length==0)||((i==0)&&(i>70000)))
            {
                errorProvider1.SetError(textBox3,"Please enter the fees and not more than 70000");
                textBox3.Focus();
                b=true;
            }
             if(textBox4.Text.Length==0)
            {
                errorProvider1.SetError(textBox4,"Please enter the duration of program");
                textBox4.Focus();
                b=true;
            }

            return b;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            con.Close();
            this.Close();
        }
      
        private void textBox1_Enter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(textBox1, "Add a new Program");
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(textBox2, "Enter number of semesters in the program");
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(textBox3, "Enter a fees of the program");
        }

        private void textBox4_Enter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(textBox4, "Duration of a program");
        }     

        }
    
}
