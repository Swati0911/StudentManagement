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
    public partial class program_query : Form
    {
        public program_query()
        {
            InitializeComponent();
        }
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;

       
        private void program_query_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(connection.cs);
            con.Open();
            comboBox1.Text = "...Select Program...";
            cmd = new SqlCommand("select p_name from program", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr[0].ToString());
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmd = new SqlCommand("select * from program where p_name=@p", con);
            cmd.Parameters.Add(new SqlParameter("@p", comboBox1.Text));
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                label6.Text = dr[1].ToString();
                label7.Text = dr[2].ToString();
                label8.Text = dr[3].ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Close();
            this.Close();
        }        
     
    }
}
