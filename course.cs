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
    public partial class course : Form
    {
        public course()
        {
            InitializeComponent();
        }
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
      
        private void course_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(connection.cs);
            con.Open();
            cmd = new SqlCommand("select p_name from program",con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr[0].ToString());
            }            
                comboBox1.Text="Select Program";
                comboBox2.Text="...";
                comboBox3.Text = "...";
        }
       
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmd=new SqlCommand("select distinct c_code from sem_detail where sem=@s ",con);
            cmd.Parameters.Add(new SqlParameter("@s",comboBox2.Text));
            dr=cmd.ExecuteReader();
            if (dr.Read())
            {                
                while (dr.Read())
                {
                    comboBox3.Items.Add(dr[0].ToString());
                }
            }
            
                comboBox3.Items.Clear();
                comboBox3.Text = "Select Course";          
            
        }        
       
        private void button3_Click(object sender, EventArgs e)
        {
            con.Close();
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           /* cmd = new SqlCommand("select distinct sem from sem_detail where p_name=@p ", con);
            cmd.Parameters.Add(new SqlParameter("@p", comboBox1.Text));
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                while (dr.Read())
                {
                    comboBox2.Items.Add(dr[0].ToString());
                }
            }
            else
            {
                comboBox2.Items.Clear();
                comboBox2.Text = "Select Semester";            
            }     */           
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmd = new SqlCommand("select c_title from program_assign where c_code=@c",con);
            cmd.Parameters.Add(new SqlParameter("@c", comboBox3.Text));
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                label6.Text = dr[0].ToString();
            }
            else
                label6.Text = "N/A";
        }               
    }
}
