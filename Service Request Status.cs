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
    public partial class Service_Request_Status : Form
    {
        private BinarySearchTree bst = new BinarySearchTree();
        private MinHeap heap = new MinHeap();
        private Graph graph = new Graph();

        // Reference to main form
        private Form1 _mainForm;

        public Service_Request_Status(Form1 form1)
        {
            _mainForm = form1;
            InitializeComponent();
            LoadRequests();
            DisplayRequests();
        }

       

        private void LoadRequests()
        {
            List<ServiceRequest> requests = new List<ServiceRequest>
            {
                new ServiceRequest { RequestID = 1, RequestType = "Water Leak", Status = "Pending", DateSubmitted = DateTime.Now.AddDays(-2), Priority = 2 },
                new ServiceRequest { RequestID = 2, RequestType = "Electric Fault", Status = "In Progress", DateSubmitted = DateTime.Now.AddDays(-1), Priority = 1 },
                new ServiceRequest { RequestID = 3, RequestType = "Waste Collection", Status = "Completed", DateSubmitted = DateTime.Now.AddDays(-5), Priority = 3 },
                new ServiceRequest { RequestID = 4, RequestType = "Road Repair", Status = "Pending", DateSubmitted = DateTime.Now.AddDays(-3), Priority = 2 },
                new ServiceRequest { RequestID = 5, RequestType = "Streetlight Repair", Status = "In Progress", DateSubmitted = DateTime.Now.AddDays(-4), Priority = 1 }           
        };

            foreach (var r in requests)
            {
                bst.Insert(r);
                heap.Insert(r);
            }

            DisplayRequests();
        }

        private void DisplayRequests()
        {
            listViewRequests.Items.Clear();
            foreach (var req in bst.InOrderTraversal())
            {
                var item = new ListViewItem(req.RequestID.ToString());
                item.SubItems.Add(req.RequestType);
                item.SubItems.Add(req.Status);
                item.SubItems.Add(req.DateSubmitted.ToString("yyyy-MM-dd"));
                item.SubItems.Add(req.Priority.ToString());
                listViewRequests.Items.Add(item);
            }

            toolStripStatusLabel1.Text = $"Total Requests: {listViewRequests.Items.Count}";
        }

        private void btnMostUrgent_Click(object sender, EventArgs e)
        {
            var urgent = heap.ExtractMin();
            if (urgent != null)
                MessageBox.Show($"Most urgent: {urgent.RequestType}", "Urgent Request");
            else
                MessageBox.Show("No requests available.");
        }

        private void btnViewDependencies_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtSearchID.Text, out int id))
            {
                var deps = graph.GetDependencies(id);
                if (deps.Count > 0)
                    MessageBox.Show($"Request {id} depends on: {string.Join(", ", deps)}");
                else
                    MessageBox.Show("No dependencies found.");
            }
        }

        private void btnMenu_Click(object sender, EventArgs e)
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
                MessageBox.Show($"Error returning to main menu: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtSearchID.Text))
                {
                    MessageBox.Show("Please enter a Request ID to search.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(txtSearchID.Text, out int searchid))
                {
                    MessageBox.Show("Invalid Request ID format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSearchID.Clear();
                    return;
                }

                ServiceRequest foundRequest = bst.Search(searchid);

                listViewRequests.Items.Clear();

                if (foundRequest != null)
                {
                    var item = new ListViewItem(foundRequest.RequestID.ToString());
                    item.SubItems.Add(foundRequest.RequestType);
                    item.SubItems.Add(foundRequest.Status);
                    item.SubItems.Add(foundRequest.DateSubmitted.ToString("yyyy-MM-dd"));
                    item.SubItems.Add(foundRequest.Priority.ToString());
                    listViewRequests.Items.Add(item);

                    toolStripStatusLabel1.Text = $"Request {searchid} found.";
                    MessageBox.Show($"Request {foundRequest.RequestID} found successfully!", "Search Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"Request ID {searchid} not found.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    toolStripStatusLabel1.Text = $"Request {searchid} not found.";
                }
                // Clear search box after search
                txtSearchID.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during search: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSearchID.Clear();
            }
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
           LoadRequests();
            txtSearchID.Clear();
        }
    }
}
