# MunicipalServiceApp
## Project Overview
- This project is a C# Windows Forms Application designed to improve resident engagement with municipal services by reporting issues in their community. The app is built around the user engagement strategy of Participatory Digital Platforms, making it easier for                            citizens to submit complaints, attach images, and track service request statuses. 
Users can:
- Report municipal issues (roads, water, electricity, etc.).
- View previously reported issues.
- Delete issues if needed.
- Track engagement with feedback messages, labels, and counters.
The Main Menu (Form1) acts as the hub:
- It never closes (only hides) when other forms are open.
- When a sub-form is closed, Form1 reappears automatically.
___________________________________________________________________________________________________________________________________________________________________________________________________________________________
## Requirements
- Windows OS
- Visual Studio 2019 or later
- .NET Framework 4.7.2 or later
___________________________________________________________________________________________________________________________________________________________________________________________________________________________
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
│
├── Form1.cs                # Main Menu (hub)
├── ReportIssuesForm.cs     # Form to report issues
├── StatusForm.cs           # Form to view/delete issues
├── Issue.cs                # Class to store issue data
├── IssueRepository.cs      # Handles saving/loading issues
├── IssueReports.txt        # File storing reported issues
└── README.md               # Documentation
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
2. Report Issues Form
   - Fill the following fields:
     - Location (TextBox)
     - Cateory (ListBox)
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
______________________________________________________________________________________________________________________________________________________________________________________________________________________
## Data Handling (Data Structures)
- Issues are stored in a List<Issue>, making it easy to:
  - Add new reports.
  - Delete reports.
  - Display all reports.
- The list is synchronised with IssueReports.txt for persistent storage.
______________________________________________________________________________________________________________________________________________________________________________________________________________________
## Future Enhancements
- Enable Local Events and Announcements, and Service Request Status.
______________________________________________________________________________________________________________________________________________________________________________________________________________________
## Author
Developed by Cassidy Motto as part of a municipal engagement project.
