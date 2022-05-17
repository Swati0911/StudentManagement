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
    public partial class std_query : Form
    {
        public std_query()
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmd = new SqlCommand("select * from std where enrol=@e",con);
            cmd.Parameters.Add(new SqlParameter("@e",comboBox1.Text));
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                label14.Text = dr[1].ToString();
                label18.Text = dr[2].ToString();
                label19.Text = dr[3].ToString();
                label20.Text = dr[4].ToString();
                label21.Text = dr[5].ToString();
                label22.Text = dr[6].ToString();
                label23.Text = dr[7].ToString();
                label24.Text = dr[8].ToString();
                label25.Text = dr[9].ToString();
                label26.Text = dr[10].ToString();
                label27.Text = dr[11].ToString();
                label28.Text = dr[12].ToString();
                label29.Text = dr[13].ToString();
                label30.Text = dr[14].ToString();
            }
        }

        private void std_query_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(getconnection());
            con.Open();
            cmd = new SqlCommand("select enrol from std", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr[0].ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Close();
            this.Close();
        }
      
    }
}
