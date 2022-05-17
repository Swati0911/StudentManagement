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
    public partial class admin_menu : Form
    {
        public admin_menu()
        {
            InitializeComponent();
        }

        private void sTUDENTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            student_details sd = new student_details();
            sd.MdiParent = this;
            sd.Show();
        }

        private void fACULTYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            employee_details emp = new employee_details();
            emp.MdiParent = this;
            emp.Show();
        }

        private void cOURSEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            program_new_entry p=new program_new_entry ();
            p.MdiParent = this;
            p.Show();
        }

        private void nOOFASSIGNMENTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            no_assign_submission nos = new no_assign_submission();
            nos.MdiParent = this;
            nos.Show();
        }

     
        private void aSSIGNMENTREPORTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            assign_status ass=new assign_status();
            ass.MdiParent = this;
            ass.Show();
        }

        private void tHEORYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            marksheet mark = new marksheet();
            mark.MdiParent = this;
            mark.Show();
        }

        private void tHEORYToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            marksheet_update mu = new marksheet_update();
            mu.MdiParent = this;
            mu.Show();
        }

        private void sTUDENTToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            update_student_details upstd = new update_student_details();
            upstd.MdiParent = this;
            upstd.Show();
        }
       

        private void fACULTYToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            emp_query em = new emp_query();
            em.MdiParent = this;
            em.Show();
        }

        private void fACULTYToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            employee_update_detail upemp = new employee_update_detail();
            upemp.MdiParent = this;
            upemp.Show();
        }

        private void aSSIGNMENTToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            assign_result_update upas = new assign_result_update();
            upas.MdiParent = this;
            upas.Show();
        }

       
        private void cOURSEToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            program_query qu = new program_query();
            qu.MdiParent = this;
            qu.Show();
        }

       
        private void cOURSEToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            update_program_detail up = new update_program_detail();
            up.MdiParent = this;
            up.Show();
        }

        private void sTUDENTToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            std_query sq = new std_query();
            sq.MdiParent = this;
            sq.Show();

        }

        private void cOURSEDETAILSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            course cou = new course();
            cou.MdiParent = this;
            cou.Show();
        }

        private void aSSIGNMENTCODEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            program_assignment prog = new program_assignment();
            prog.MdiParent = this;
            prog.Show();
        }

        private void aSSIGNMENTUPDATEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            prog_assigncode_update progup=new prog_assigncode_update();
            progup.MdiParent=this;
            progup.Show();
        }

        private void aSSIGNMENTToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            prog_assign_query progquery = new prog_assign_query();
            progquery.MdiParent = this;
            progquery.Show();
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void eXITToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            master m = new master();
            m.Show();
            this.Hide();
        }

        private void eVALUATIONToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void aSSIGNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            eval_assign_manage assign = new eval_assign_manage();
            assign.MdiParent = this;
            assign.Show();
        }

        private void uPDATEEVALUATORToolStripMenuItem_Click(object sender, EventArgs e)
        {
            eval_assign_update_manage update = new eval_assign_update_manage();
            update.MdiParent = this;
            update.Show();
        }

        private void admin_menu_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void sEMESTERREPORTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mark_query mark = new mark_query();
            mark.MdiParent = this;
            mark.Show();
        }

        private void sEMESTERDETAILToolStripMenuItem_Click(object sender, EventArgs e)
        {
            semester_details SEM = new semester_details();
            SEM.MdiParent = this;
            SEM.Show();
        }

        private void admin_menu_Load(object sender, EventArgs e)
        {
            
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
                
            }
        }

        private void sUBMISSIONDETAILToolStripMenuItem_Click(object sender, EventArgs e)
        {
            std_assign_submission std_assign = new std_assign_submission();
            std_assign.MdiParent = this;
            std_assign.Show();
        }

        private void masterToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void aSSIGNMENTDETAILSToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }      
    }
}
