using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace MunicipalServiceApp
{
    public partial class LocalEvents_AnnocumentsForm : Form
    {
        // Priority Queue to manage events based on urgency (date)
        private PriorityQueue<Event> eventQueue = new PriorityQueue<Event>();

        // Queue to manage upcoming events in order
        private Queue<Event> upcomingEvents = new Queue<Event>();

        // Stack to manage recently added events
        private Stack<Event> recentEventsStack = new Stack<Event>();

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
            try
            {
                InitializeComponent();
                _mainForm = mainForm;



                // Load test data
                LoadEvents();
                DisplayEvents(eventDictionary.SelectMany(kvp => kvp.Value).ToList());
                LoadedCategories();
                LoadAvailableDates();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing event announcements form:\n{ex.Message}", "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Load Events
        private void LoadEvents()
        {
            try
            {
                AddEvent(new Event { Name = "Community Cleanup", Date = DateTime.Now.AddDays(1), Location = "Helderberg Park", Description = "Join us for a community cleanup event at the local park.", Category = "Community" });
                AddEvent(new Event { Name = "Community Meeting", Date = DateTime.Now.AddDays(2), Location = "Town Hall", Description = "Monthly community update", Category = "Community" });
                AddEvent(new Event { Name = "Concert", Date = DateTime.Now.AddDays(1), Location = "City Park", Description = "Live music event", Category = "Music" });
                AddEvent(new Event { Name = "Food Festival", Date = DateTime.Now.AddDays(5), Location = "Downtown", Description = "Taste dishes from around the world", Category = "Festival" });
                AddEvent(new Event { Name = "Tech Workshop", Date = DateTime.Now.AddDays(3), Location = "Library", Description = "Intro to programming workshop", Category = "Education" });
                AddEvent(new Event { Name = "Blood Donation Drive", Date = DateTime.Now.AddDays(7), Location = "Somerset Mall", Description = "Help save lives by donating blood. All donors welcome!", Category = "Community" });
                AddEvent(new Event { Name = "Tech Workshop", Date = DateTime.Now.AddDays(4), Location = "City Library", Description = "Intro to programming with C# and .NET", Category = "Education" });
                AddEvent(new Event { Name = "Career Guidance Fair", Date = DateTime.Now.AddDays(9), Location = "Community College", Description = "Career advice and workshops for young professionals.", Category = "Education" });
                AddEvent(new Event { Name = "Parenting Seminar", Date = DateTime.Now.AddDays(11), Location = "Town Hall", Description = "Learn effective parenting techniques from experts.", Category = "Education" });

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading events: {ex.Message}", "Data Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddEvent(Event ev)
        {
            try
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

                // Push onto stack
                recentEventsStack.Push(ev); // for undo/recent view
                upcomingEvents.Enqueue(ev); // for upcoming events queue
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding event '{ev?.Name}':\n{ex.Message}", "Add Event Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Display a list of events in ListView
        private void DisplayEvents(List<Event> events)
        {
            try
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
                toolStripStatusLabel1.Text = $"Total Events: {snapshot.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error displaying events:\n {ex.Message}", "Display Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Load categories into ComboBox
        private void LoadedCategories()
        {
            try
            {
                comboBoxCategory.Items.Clear();
                comboBoxCategory.Items.Add("All");
                foreach (var category in eventCategories)
                {
                    comboBoxCategory.Items.Add(category);
                }
                comboBoxCategory.SelectedIndex = 0; // Default to "All"
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading categories: {ex.Message}", "Category Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadEvents_Announcements_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                _mainForm.Show();
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
                string category = comboBoxCategory.SelectedItem?.ToString();
                DateTime selectedDate = dateTimePicker1.Value.Date;

                // Start with all events
                IEnumerable<Event> results = eventDictionary.Values.SelectMany(list => list);


                // Filter by category if not "All" 
                if (!string.IsNullOrEmpty(category) && category != "All")
                {
                    results = eventDictionary[category];
                }
                else
                {
                    results = Enumerable.Empty<Event>();
                }

                // Filter by date
                results = results.Where(ev => ev.Date.Date == selectedDate || !eventDates.Contains(selectedDate));


                // Display the results
                listViewEvents.Items.Clear();
                foreach (var ev in results.OrderBy(ev => ev.Date))
                {
                    ListViewItem item = new ListViewItem(ev.Name);
                    item.SubItems.Add(ev.Date.ToShortDateString());
                    item.SubItems.Add(ev.Category);
                    item.SubItems.Add(ev.Location);
                    item.SubItems.Add(ev.Description);
                    listViewEvents.Items.Add(item);
                }
                toolStripStatusLabel1.Text = $"Total Events: {listViewEvents.Items.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during search: {ex.Message}", "Search Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        // Event Button (Priority Queue)
        private void btnUrgent_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving urgent event:\n{ex.Message}", "Priority Queue Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Load available dates into DateTimePicker
        private void LoadAvailableDates()
        {
            try
            {
                if (eventDates.Count > 0)
                {
                    dateTimePicker1.MinDate = eventDates.Min();
                    dateTimePicker1.MaxDate = eventDates.Max();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading available dates:\n{ex.Message}", "Date Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                DateTime selecteddate = dateTimePicker1.Value.Date;
                string selectedCategory = comboBoxCategory.SelectedItem?.ToString();

                // Get all events
                var allEvents = eventDictionary.SelectMany(kvp => kvp.Value);

                //Filter by date
                var filteredByDate = allEvents.Where(ev => ev.Date.Date == selecteddate);

                // If a specific category is selected, filter further
                if (!string.IsNullOrEmpty(selectedCategory) && selectedCategory != "All")
                {
                    filteredByDate = filteredByDate.Where(ev => ev.Category.Equals(selectedCategory, StringComparison.OrdinalIgnoreCase));
                }

                // Display the filtered events
                DisplayEvents(filteredByDate.ToList());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error filtering by date:\n{ex.Message}", "Date Filter Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LocalEvents_AnnocumentsForm_Load(object sender, EventArgs e)
        {

        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            try
            {
                if (recentEventsStack.Count > 0)
                {
                    //Pop the most recent event
                    Event lastEvent = recentEventsStack.Pop();

                    //Remove from dictionary
                    if (eventDictionary.ContainsKey(lastEvent.Category))
                    {
                        eventDictionary[lastEvent.Category].Remove(lastEvent);
                        if (eventDictionary[lastEvent.Category].Count == 0)
                        {
                            eventDictionary.Remove(lastEvent.Category);
                        }

                        // Update display
                        DisplayEvents(eventDictionary.SelectMany(kvp => kvp.Value).ToList());

                        toolStripStatusLabel1.Text = $"Total Events: {eventDictionary.SelectMany(kvp => kvp.Value).Count()}";
                    }
                    else
                    {
                        MessageBox.Show("No recent events to undo.", "Undo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error undoing last event addition:\n{ex.Message}", "Undo Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRecent_Click(object sender, EventArgs e)
        {
            if (recentEventsStack.Count > 0)
            {
                string recentList = string.Join("\n", recentEventsStack.Select(ev => ev.ToString()));
                MessageBox.Show($"Recently Added Events:\n{recentList}", "Recent Events");
            }
            else
            {
                MessageBox.Show("No recent events.", "recent Events");
            }
        }

        private void btnNextEvent_Click(object sender, EventArgs e)
        {
            try
            {
                if (upcomingEvents.Count > 0)
                {
                    // Peek at the next upcoming event
                    Event nextevent = upcomingEvents.Peek();
                    MessageBox.Show($"Next Upcoming Event:\n{nextevent.Name}\nDate: {nextevent.Date.ToShortDateString()}",
                        "Next Event", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No upcoming events available.", "Next Event", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving next upcoming event:\n{ex.Message}", "Upcoming Event Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnProcessNext_Click(object sender, EventArgs e)
        {
            try
            {
                if (upcomingEvents.Count > 0)
                {
                    //Dequeue next event
                    Event processedEvent = upcomingEvents .Dequeue();
                    MessageBox.Show($"Processed Event:\n{processedEvent.Name}\nDate: {processedEvent.Date.ToShortDateString()}",
                        "Processed Event", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Update display
                    DisplayEvents(eventDictionary.SelectMany(kvp => kvp.Value).ToList());
                    toolStripStatusLabel1.Text = $"Total Events: {eventDictionary.SelectMany(kvp => kvp.Value).Count()}";
                }
                else
                {
                    MessageBox.Show("No upcoming events to process.", "Processed Event", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error processing next upcoming event:\n{ex.Message}", "Process Event Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }                       
        }

        private void btnShowQueue_Click(object sender, EventArgs e)
        {
            try
            {
                if (upcomingEvents.Count > 0)
                {
                    string list = string.Join("\n", upcomingEvents.Select(ev => ev.ToString()));
                    MessageBox.Show($"Upcoming Events Queue:\n{list}", "Upcoming Events");
                }
                else
                {
                    MessageBox.Show("No upcoming events in the queue.", "Upcoming Events");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error displaying upcoming events queue:\n{ex.Message}", "Display Queue Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}