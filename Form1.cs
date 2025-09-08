using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MunicipalServiceApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnReportIssues_Click(object sender, EventArgs e)
        {
            ReportIssuesForm reportForm = new ReportIssuesForm(this);
            this.Hide();
            reportForm.ShowDialog();
            if (!this.IsDisposed && !this.Disposing)
                this.Show();
        }

        private void btnEvents_Click(object sender, EventArgs e)
        {
            //Implement in Part 2
        }

        private void btnStatus_Click(object sender, EventArgs e)
        {
            //Implement in Part 3
        }

        private void btn_StatusIssues_Click(object sender, EventArgs e)
        {
            StatusForm statusForm = new StatusForm(this);
            this.Hide();
            statusForm.ShowDialog();
            if (!this.IsDisposed && !this.Disposing)
                this.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
           Application.Exit();
        }
    }
}
