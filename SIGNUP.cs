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
    public partial class SIGNUP : Form
    {
        public SIGNUP()
        {
            InitializeComponent();
        }
        SqlConnection con;
        SqlCommand cmd;
       // SqlDataReader dr;

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void SIGNUP_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(connection.cs);
            con.Open();
            textBox1.Focus();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (check())
            {
                MessageBox.Show("Rectify error", "Error", MessageBoxButtons.OK);
            }
            else
            {
                cmd = new SqlCommand("insert into login_form values(@u,@p)", con);
                cmd.Parameters.Add(new SqlParameter("@u", textBox1.Text));
                cmd.Parameters.Add(new SqlParameter("@p", textBox2.Text));
                cmd.ExecuteReader();
                textBox1.Text = "";
                textBox2.Text = "";
                MessageBox.Show("Data Saved", "Record", MessageBoxButtons.OK);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            login l = new login();
            l.Show();
            this.Hide();
        }

        public bool check()
        {
            bool b = false;
            errorProvider1.Clear();
            errorProvider1.BlinkRate = 100;
            errorProvider1.BlinkStyle = ErrorBlinkStyle.AlwaysBlink;
            if(textBox1.Text.Length==0)
            {
                errorProvider1.SetError(textBox1, "enter user name");
                textBox1.Focus();
                b = true;
            }
            if (textBox2.Text.Length == 0)
            {
                errorProvider1.SetError(textBox2, "Enter Password");
                textBox2.Focus();
                b = true;
            }
            return b;
        }
    }
}
