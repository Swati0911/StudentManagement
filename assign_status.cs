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
    public partial class assign_status : Form
    {
        public assign_status()
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

        private void assign_status_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(connection.cs);
            con.Open();
            cmd = new SqlCommand("select enrol from std",con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr[0].ToString());
            }
            cmd = new SqlCommand("select distinct c_code from marksheet where c_code like 'MCS-%'", con);
            cmd.Parameters.Add(new SqlParameter("@c", comboBox2.Text));
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox2.Items.Add(dr[0].ToString());
            }
            cmd = new SqlCommand("select c_code from marksheet where c_code like 'MCSL%'", con);
            cmd.Parameters.Add(new SqlParameter("@c", comboBox3.Text));
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox3.Items.Add(dr[0].ToString());
            }
            cmd = new SqlCommand("select c_code from marksheet where c_code like 'MCSP%'", con);
            cmd.Parameters.Add(new SqlParameter("@c", comboBox4.Text));
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox4.Items.Add(dr[0].ToString());
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmd = new SqlCommand("select nam from std where enrol=@e",con);
            cmd.Parameters.Add(new SqlParameter("@e", comboBox1.Text));
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                label14.Text = dr[0].ToString();
            }
            comboBox2.Text = "Select Course";
            label15.Text = "";
            label16.Text = "";
            label17.Text = "";
            label18.Text = "";
            label19.Text = "";
            label20.Text = "";
            label21.Text = "";
            label22.Text = "";
            label23.Text = "";
            comboBox3.Text = "Select Course";
            comboBox4.Text = "Select Course";
        }
       
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            cmd = new SqlCommand("select marksheet.ASSign_marks,stats,sub_date from marksheet,assign where marksheet.c_code=@c and marksheet.enrol=@e", con);
            cmd.Parameters.Add(new SqlParameter("@c", comboBox2.Text));
            cmd.Parameters.Add(new SqlParameter("@e", comboBox1.Text));
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                label15.Text = dr[0].ToString();
                label16.Text = dr[1].ToString();
                label17.Text = dr[2].ToString();
            }
            else
            {
                label15.Text = "N/A";
                label16.Text = "N/A";
                label17.Text = "N/A";
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmd = new SqlCommand("select marksheet.ASSign_marks,stats,sub_date from marksheet,assign where marksheet.c_code=@c and marksheet.enrol=@e", con);
            cmd.Parameters.Add(new SqlParameter("@c", comboBox3.Text));
            cmd.Parameters.Add(new SqlParameter("@e", comboBox1.Text));
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                label18.Text = dr[0].ToString();
                label19.Text = dr[1].ToString();
                label20.Text = dr[2].ToString();
            }
            else
            {
                label18.Text = "N/A";
                label19.Text = "N/A";
                label20.Text = "N/A";

            }
            label18.Text = "...";
            label19.Text = "...";
            label20.Text = "...";
        }


        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmd = new SqlCommand("select marksheet.ASSign_marks,stats,sub_date from marksheet,assign where marksheet.c_code=@c", con);
            cmd.Parameters.Add(new SqlParameter("@c", comboBox4.Text));
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                label21.Text = dr[0].ToString();
                label12.Text = dr[1].ToString();
                label23.Text = dr[2].ToString();
            }
        }
    }
}
