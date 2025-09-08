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
    public partial class StatusForm : Form
    {
        private Form1 _mainForm;
        public StatusForm(Form1 mainForm)
        {
            InitializeComponent();
            _mainForm = mainForm;

            this.Load += StatusForm_Load;
            _mainForm = mainForm;
        }

        private void StatusForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _mainForm.Show();
        }

        private void StatusForm_Load(object sender, EventArgs e)
        {
            listReports.Items.Clear();//clear old items

            IssueRepository.LoadIssues(); // Ensure we have the latest issues
            var issues = IssueRepository.GetAllIssues();

            if (issues.Count == 0)
            {
                MessageBox.Show("No issues reported yet.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Close the form if no issues
                this.BeginInvoke(new Action(() => this.DialogResult =  DialogResult.Cancel));
                return;
            }

            //Display selected issue details in ListBox
            foreach (var issue in issues)
            {
                listReports.Items.Add(issue.ToString());
            }
        }

        private void btn_Main_Click(object sender, EventArgs e)
        {
            Form1 mainForm = new Form1();
            this.Hide();
            mainForm.ShowDialog();
            this.Show();
           
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            int selectedIndex = listReports.SelectedIndex;

            if (selectedIndex == -1)
            {
                MessageBox.Show("Please select an issue to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show("Are you sure you want to delete the selected issue?", "Confirm Deletion", MessageBoxButtons.YesNo,MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                IssueRepository.DeleteIssue(selectedIndex);
                listReports.Items.RemoveAt(selectedIndex);

                MessageBox.Show("Issue deleted successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
