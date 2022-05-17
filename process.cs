using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace student
{
    public partial class process : Form
    {
        public process()
        {
            InitializeComponent();
        }       

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void process_Load(object sender, EventArgs e)
        {
            backgroundWorker1.WorkerReportsProgress = true; 
            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;///change the value of the progress bar to background worker progress
            label2.Text =  e.ProgressPercentage.ToString() + "%";//set the text
            if (label2.Text == "100%")
            {
                master m = new master();
                m.Show();
                this.Hide();
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 1; i <= 100; i++)
            {
                Thread.Sleep(50);//wait till 50 miliseconds
                backgroundWorker1.ReportProgress(i);//report progress
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }
    }
}
