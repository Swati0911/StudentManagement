using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace student
{
    public partial class user_menu : Form
    {
        public user_menu()
        {
            InitializeComponent();
        }

       

        private void user_menu_Load(object sender, EventArgs e)
        {

        }

        private void eXITToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pROGRAMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            program_query prog_query = new program_query();
            prog_query.MdiParent = this;
            prog_query.Show();
        }

        private void iDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            std_query query = new std_query();
            query.MdiParent = this;
            query.Show();
        }

        private void rEPORTToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void aSSIGNMENTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            assign_status stat = new assign_status();
            stat.MdiParent = this;
            stat.Show();
        }

        private void tERMENDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mark_query m_query = new mark_query();
            m_query.MdiParent = this;
            m_query.Show();
        }

        private void bACKToolStripMenuItem_Click(object sender, EventArgs e)
        {
            master m = new master();
            m.Show();
            this.Hide();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }
        }

        private void user_menu_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

       

       
    }
}
