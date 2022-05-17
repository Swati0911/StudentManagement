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
    public partial class prog_assign_query : Form
    {
        public prog_assign_query()
        {
            InitializeComponent();
        }
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;

        public string getconnection()
        {
            return "data Source=SWATI-PC;Initial Catalog=swati;Integrated Security=true;MultipleActiveResultSets=true";
        }       

        private void prog_assign_query_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(getconnection());
            con.Open();
            comboBox1.Text = "Select Program";
            cmd = new SqlCommand("select c_code from program_assign", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr[0].ToString());
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmd = new SqlCommand("select * from program_assign where c_code=@c ", con);
            cmd.Parameters.Add(new SqlParameter("@c", comboBox1.Text));
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                label11.Text = dr[0].ToString();
                label12.Text = dr[1].ToString();
                label13.Text = dr[3].ToString();
                label14.Text = dr[4].ToString();
                label15.Text = dr[5].ToString();
                label16.Text = dr[6].ToString();
                label17.Text = dr[7].ToString();
                label18.Text = dr[8].ToString();
            }
            
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Close();
            this.Close();
        }
       
    }
}
