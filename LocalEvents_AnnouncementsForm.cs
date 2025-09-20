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
    public partial class LocalEvents_AnnocumentsForm : Form
    {
        private PriorityQueue<Event> eventQueue = new PriorityQueue<Event>();

        // SortedDictionary for automatic date sorting
        private SortedDictionary<string, List<Event>> eventDictionary = new SortedDictionary<string, List<Event>>();
        
        // Stores unique event categories
        private HashSet<string> eventCategories = new HashSet<string>();

        // Stores unique event dates (just the date part)
        private HashSet<DateTime> eventDates = new HashSet<DateTime>();

        // Reference to main form
        private Form1 _mainForm;
        public LocalEvents_AnnocumentsForm(Form1 mainForm)
        {
            InitializeComponent();
           _mainForm = mainForm;


            
            // Load test data
            LoadEvents();
            DisplayEvents(eventDictionary.SelectMany(kvp => kvp.Value).ToList());
            LoadedCategories();
            LoadAvailableDates();

        }

        // Load Events
        private void LoadEvents()
        {
            AddEvent(new Event { Name = "Community Cleanup", Date = DateTime.Now.AddDays(1), Location = "Helderberg Park", Description = "Join us for a community cleanup event at the local park.", Category = "Community" });
            AddEvent(new Event { Name = "Community Meeting", Date = DateTime.Now.AddDays(2), Location = "Town Hall", Description = "Monthly community update", Category = "Community" });
            AddEvent(new Event { Name = "Concert", Date = DateTime.Now.AddDays(1), Location = "City Park", Description = "Live music event", Category = "Music" });
            AddEvent(new Event { Name = "Food Festival", Date = DateTime.Now.AddDays(5), Location = "Downtown", Description = "Taste dishes from around the world", Category = "Festival" });
            AddEvent(new Event { Name = "Tech Workshop", Date = DateTime.Now.AddDays(3), Location = "Library", Description = "Intro to programming workshop", Category = "Education" });
        }

        private void AddEvent (Event ev)
        {
            // Add to Priority Queue (priority based on date)
            int priority = (ev.Date - DateTime.Now).Days;
            if (priority < 0) priority = int.MaxValue;  // Past event goes to the end

            eventQueue.Enqueue(ev, priority);

            // Add to SortedDictionary
            if (!eventDictionary.ContainsKey(ev.Category))
            {
                eventDictionary[ev.Category] = new List<Event>();
            }

            eventDictionary[ev.Category].Add(ev);

            // Update sets
            eventCategories.Add(ev.Category);
            eventDates.Add(ev.Date.Date); // only date part
        }

        // Display a list of events in ListView
        private void DisplayEvents(List<Event> events)
        {
            listViewEvents.Items.Clear();

            var snapshot = eventQueue.Snapshot();
            snapshot.Sort((a, b) => a.Priority.CompareTo(b.Priority));

            foreach (var entry in snapshot)
            {
                Event ev = entry.Item;
                ListViewItem item = new ListViewItem(ev.Name);
                item.SubItems.Add(ev.Date.ToShortDateString());
                item.SubItems.Add(ev.Category);
                item.SubItems.Add(ev.Location);
                item.SubItems.Add(ev.Description);
                listViewEvents.Items.Add(item);
            }
            label3.Text = $"Total Events:{listViewEvents.Items.Count}";
        }

        private void LoadedCategories()
        {
            comboBoxCategory.Items.Clear();
            foreach (var category in eventCategories)
            {
                comboBoxCategory.Items.Add(category);
            }
        }

        private void LoadEvents_Announcements_FormClosed(object sender, FormClosedEventArgs e)
        {
            _mainForm.Show();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            
            string category = comboBoxCategory.SelectedItem?.ToString();
            DateTime selectedDate = dateTimePicker1.Value.Date;

            listViewEvents.Items.Clear();

            IEnumerable<Event> results;

            if (!string.IsNullOrEmpty(category) && eventDictionary.ContainsKey(category))
            {
                results = eventDictionary[category];
            }
            else
            {
                results = eventDictionary.Values.SelectMany(list => list);
            }

            results = results.Where(ev =>
            category == "All" || ev.Category.Equals(category, StringComparison.OrdinalIgnoreCase)).OrderBy(ev => ev.Date);
            

            foreach (var ev in results.OrderBy(ev => ev.Date))
            {
                ListViewItem item = new ListViewItem(ev.Name);
                item.SubItems.Add(ev.Date.ToShortDateString());
                item.SubItems.Add(ev.Category);
                item.SubItems.Add(ev.Location);
                item.SubItems.Add(ev.Description);
                listViewEvents.Items.Add(item);

            }

        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            Form1 mainForm = new Form1();
            this.Hide();
            mainForm.ShowDialog();
            this.Show();
        }

        // Event Button (Priority Queue)
        private void btnUrgent_Click(object sender, EventArgs e)
        {
            if (eventQueue.Count > 0)
            {
                Event urgentEvent = eventQueue.Dequeue();
                MessageBox.Show($"Most urgent event:\n{urgentEvent.Name}\nDate:{urgentEvent.Date.ToShortDateString()}",
                    "Urgent Event");
            }
            else
            {
                MessageBox.Show("No urgent events available.", "Urgent Event");
            }
        }

        // Load available dates into DateTimePicker
        private void LoadAvailableDates()
        {
            if (eventDates.Count > 0)
            {
                dateTimePicker1.MinDate = eventDates.Min();
                dateTimePicker1.MaxDate = eventDates.Max();
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DateTime selecteddate = dateTimePicker1.Value.Date;

            if (eventDates.Contains(selecteddate))
            {
                 DateTime selectedDate = dateTimePicker1.Value.Date;

                if (eventDates.Contains(selectedDate))
                {
                    var eventsDate = eventDictionary
                        .SelectMany(kvp => kvp.Value)
                        .Where(ev => ev.Date.Date == selectedDate)
                        .ToList();

                    DisplayEvents(eventsDate);
                }
            }
        }
    }
}
