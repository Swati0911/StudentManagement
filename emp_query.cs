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
    public partial class emp_query : Form
    {
        public emp_query()
        {
            InitializeComponent();
        }
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;        

        private void button1_Click(object sender, EventArgs e)
        {
            con.Close();
            this.Close();
        }

        private void emp_query_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(connection.cs);
            con.Open();
            cmd = new SqlCommand("Select e_id from emp order by e_id", con);
            dr = cmd.ExecuteReader();
            comboBox1.Text = " ... Select ID... ";
            while (dr.Read())
            {
                comboBox1.Items.Add(dr[0].ToString());
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmd = new SqlCommand("select * from emp where e_id=@e", con);
            cmd.Parameters.Add(new SqlParameter("@e", comboBox1.Text));
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                label8.Text = dr[1].ToString();
                label9.Text = dr[2].ToString();
                label10.Text = dr[3].ToString();
                label11.Text = dr[4].ToString();
                label12.Text = dr[5].ToString();
            }
            else
            {
                label8.Text = "N/A";
                label9.Text = "N/A";
                label10.Text = "N/A";
                label11.Text = "N/A";
                label12.Text = "N/A";
                MessageBox.Show("Record not found");
            }
        }
    }
}
