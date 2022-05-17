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
    public partial class login : Form
    {
        public login()
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
                MessageBox.Show("Error Rectified");
            }
            else
            {
                cmd = new SqlCommand("select * from login_form where nam=@n and passwrd=@p", con);
                cmd.Parameters.Add(new SqlParameter("@n", textBox1.Text));
                cmd.Parameters.Add(new SqlParameter("@p", textBox2.Text));
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    this.Hide();
                    //textBox1.Text = dr[0].ToString();
                    //textBox2.Text = dr[1].ToString();
                    admin_menu mdfrm = new admin_menu();
                    mdfrm.Show();
                    dr.Close();
                }
                else
                {
                    dr.Close();
                    MessageBox.Show("Not a valid user.", "Login", MessageBoxButtons.OK);
                    textBox1.Text = "";
                    textBox2.Text = "";
                }
            }
        }

        private void login_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(connection.cs);
            con.Open();
            cmd = new SqlCommand("insert into login_form values(@u,@p)", con);
            cmd.Parameters.Add(new SqlParameter("@u", textBox1.Text));
            cmd.Parameters.Add(new SqlParameter("@p", textBox2.Text));
            dr = cmd.ExecuteReader();
            textBox1.Text = "";
            textBox2.Text = "";
        }

       
        public bool check()
        {           
            bool b = false;
            errorProvider1.Clear();
            errorProvider1.BlinkRate = 100;
            errorProvider1.BlinkStyle = ErrorBlinkStyle.AlwaysBlink;
            if (textBox1.Text.Length == 0)
            {
                errorProvider1.SetError(textBox1, "Enter User ID");
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

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SIGNUP up = new SIGNUP();
            up.Show();
            this.Hide();
        }
    }
}
