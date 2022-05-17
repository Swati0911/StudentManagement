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
    public partial class no_assign_submission : Form
    {
        public no_assign_submission()
        {
            InitializeComponent();
        }
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;

      
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmd = new SqlCommand("select count(program_assign.assign_code) from assign_sub,program_assign where assign_sub.assign_code=program_assign.assign_code and ASSIGN_SUB.c_code=@c", con);
            cmd.Parameters.Add(new SqlParameter("@c", comboBox1.Text));
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                label4.Text = dr[0].ToString();
            }
            cmd = new SqlCommand("Select count(std.sem) from std,program_assign where std.sem=program_assign.sem and c_code=@c", con);
            cmd.Parameters.Add(new SqlParameter("@c", comboBox1.Text));
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                label6.Text = dr[0].ToString();
            }
            if (Convert.ToInt32(label4.Text) != Convert.ToInt32( label6.Text))
            {
                MessageBox.Show("All Students has not submitted the assignments","Report",MessageBoxButtons.OK);
            }
            else
                MessageBox.Show("All Students has submitted the assignment","Report",MessageBoxButtons.OK);
        }

        private void no_assign_submission_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(connection.cs);
            con.Open();
            comboBox1.Text = "Select Course";
            cmd = new SqlCommand("select c_code from program_assign", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr[0].ToString());
            }
        }        
    }
}
