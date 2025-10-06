# MunicipalServiceApp
## Project Overview
- This project is a C# Windows Forms Application designed to improve resident engagement with municipal services by reporting issues in their community, display local events, and announcements. The app is built around the user engagement strategy of Participatory Digital Platforms, making it easier for                            citizens to submit complaints, attach images, track service request statuses and view local events & announcements. 
Users can:
- Report municipal issues (roads, water, electricity, etc.).
- View previously reported issues.
- Delete issues if needed.
- Track engagement with feedback messages, labels, and counters.
- Allows users to browse, search, and filter events.
The Main Menu (Form1) acts as the hub:
- It never closes (only hides) when other forms are open.
- When a sub-form is closed, Form1 reappears automatically.
___________________________________________________________________________________________________________________________________________________________________________________________________________
## Requirements
- Windows OS
- Visual Studio 2019 or later
- .NET Framework 4.7.2 or later
____________________________________________________________________________________________________________________________________________________________________________________________________________
## GitHub Link
https://github.com/Cassidy-02/MunicipalServiceApp
___________________________________________________________________________________________________________________________________________________________________________________________________________
## How to Compile
1. Open Visual Studio.
2. Click File > Open > Project/Solution.
3. Select the .sln file of this project.
4. Go to Build > Build Solution
   - This will compile the project and check for errors.
___________________________________________________________________________________________________________________________________________________________________________________________________________________________
## How to Run 
1. Press F5 (Start Debugging) or click the Start button in Visual Studio.
2. The Main Menu (Form1) will appear.
___________________________________________________________________________________________________________________________________________________________________________________________________________________________
## Project Structure
MunicipalServicesApp/
- Event.cs — Class representing an event with properties: Name, Date, Location, Description, Category.
- LocalEvents_AnnouncementsForm.cs — Handles event display, search, filtering, and urgent event selection.
- ReportIssuesForm.cs — Handles issue reporting and media upload.
- Form1.cs — Main form with navigation.
- IssueRepository.cs — Handles storage of reported issues.
- Program.cs — Application entry point.
_________________________________________________________________________________________________________________________________________________________________________________________________________________________
## How the User Engagement Strategy is Applied
The project applies the Participatory Digital Platforms strategy by:
 - Providing a simple digital platform where citizens can log issues.
 - Offering instant feedback (thank-you message after submission).
 - Ensuring transparency (users can see their submitted reports in StatusForm).
This encourages ongoing user participation and builds trust between residents and municipalities.
________________________________________________________________________________________________________________________________________________________________________________________________________________________ 
## How to Use
1. Main Menu (Form1)
   - Entry point of the program.
   - Two options:
     - Report Issues
     - View Reports
     - Local Events and Announcements
2. Report Issues Form
   - Fill the following fields:
     - Location (TextBox)
     - Cateory (ComboBox)
     - Description (RichTextBox)
     - Media Attachment (Button)
   - Click Submit to save the report.
   - A thank-you message will appear, then the form closes and returns to the main menu.
   - Reports are stored in IssueReports.txt.
3. Status Form
   - Displays all previously reported issues from IssueReports.txt
   - Features:
     - List of issues displayed in a list box.
     - Delete Issue button to remove one issue.
     - Close button returns to the main menu.
     - If no issue exist, a message appears and the form closes automatically.
4. Local Events Announcements
   - Click Local Events & Announcements.
   - The ListView shows upcoming events with the following columns:
     - Event Name 
     - Date
     - Category
     - Location
     - Description
   - The Status Bar at the bottom shows the total number of events currently displayed.
   - Search and filter events by:
     - Category (ComboBox)
       - Use the Category drop-down
       - Select a specific category or "All" to see all events.
     - Date (DatePicker)
       - Use the Date picker to select a date.
   - Highlight the most urgent event using a priority queue:
     - Urgent Event (Button)
       - Click Urgent Event to see the most urgent upcoming event.
       - A pop-up displays the event name date.
   - Automatically organises events by date and category using SortedDictionary.
   - Maintains unique categories and dates using HashSet.
   - Click "Search" (button) to search event/announcement.
5. Navigation
   - Use "Back to Menu" buttons to return to the main menu.
   - You can navigate between Events and Report Issues without losing data...
______________________________________________________________________________________________________________________________________________________________________________________________________________________
## Data Handling (Data Structures)
- Issues are stored in a List<Issue>, making it easy to:
  - Add new reports.
  - Delete reports.
  - Display all reports.
- The list is synchronised with IssueReports.txt for persistent storage.
- SortedDictionary<string>,List<Event>> - Automatically sorts events by category.
- PriorityQueue<Event> - Identifies and retrieves the most urgent events.
- HashSet<string> / HashSet<DateTime> - Stores unique categories and dates efficiently.
______________________________________________________________________________________________________________________________________________________________________________________________________________________
## Error Handling
- Try-catch blocks are implemented for:
  - Event display
  - Filtering and searching
  - Navigation
  - Issue submission
- Errors are displayed via MessageBox to ensure smooth user experience.
______________________________________________________________________________________________________________________________________________________________________________________________________________________
## Future Enhancements
- Enable Service Request Status.
______________________________________________________________________________________________________________________________________________________________________________________________________________________
## Author
Developed by Cassidy Motto as part of a municipal engagement project.
