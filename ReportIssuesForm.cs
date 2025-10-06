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
    public partial class ReportIssuesForm : Form
    {
        private Form1 _mainForm;
        public ReportIssuesForm(Form1 mainForm)
        {
            InitializeComponent();
            _mainForm = mainForm;
        }

        private void ReportIssuesForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _mainForm.Show();
        }

        //Upload media button
        private void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openfile = new OpenFileDialog()
                {
                    Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif|All Files|*.*"
                };

                if (openfile.ShowDialog() == DialogResult.OK)
                {
                    // Display selected image in PictureBox
                    pictureBox1.Image = Image.FromFile(openfile.FileName);
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
            catch (OutOfMemoryException)
            {
                MessageBox.Show("The selected file is not a valid image format.", "Invalid Image", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading the image: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Submit report button
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                // clear old errors
                errorProvider1.Clear();
                bool hasError = false;

                //Check category
                if (listCategory.SelectedItem == null)
                {
                    errorProvider1.SetError(listCategory, "Please select a category.");
                    hasError = true;
                }

                //Check description
                if (string.IsNullOrWhiteSpace(richtxtDescription.Text))
                {
                    errorProvider1.SetError(richtxtDescription, "Description cannot be empty.");
                    hasError = true;
                }

                //Check location
                if (string.IsNullOrWhiteSpace(txtLocation.Text))
                {
                    errorProvider1.SetError(txtLocation, "Location cannot be empty.");
                    hasError = true;
                }
                if (hasError) return; //stop submission if there are errors

                var issue = new Issue
                {
                    Category = listCategory.SelectedItem.ToString(),
                    Description = richtxtDescription.Text.Trim(),
                    Location = txtLocation.Text.Trim(),
                    DateReported = DateTime.Now
                };


                try
                {

                    //Add to repository (adds to list + file)
                    IssueRepository.AddIssue(issue);

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to save the issue report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }



                //Simulate progress
                try
                {
                    progressBar1.Value = 0;
                    for (int i = 0; i <= 100; i += 10)
                    {
                        progressBar1.Value = i;
                        System.Threading.Thread.Sleep(50); //small delay to simulate progress
                        Application.DoEvents(); // refresh UI
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Progress simulation error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                MessageBox.Show("Thank you for reporting this issue!\n\nYour feedback helps improve services.", "Report Submitted", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Closes the form and returns to main
                this.BeginInvoke(new Action(() => this.DialogResult = DialogResult.Cancel));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            try
            {
                Form1 mainForm = new Form1();
                this.Hide();
                mainForm.ShowDialog();
                this.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while returning to the main menu: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void richtxtDescription_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtLocation_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
