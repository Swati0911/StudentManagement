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
    public partial class master : Form
    {
        public master()
        {
            InitializeComponent();
        }
        
        SqlConnection con;
        private void master_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(connection.cs);
            con.Open();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            login l = new login();
            l.Show();
            this.Hide();
        }
       
        private void button2_Click(object sender, EventArgs e)
        {
            user_menu user = new user_menu();
            user.Show();
            this.Hide();
        }

        private void master_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
