using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalServiceApp
{
    public static class IssueRepository
    {
        private static readonly List<Issue> issues = new List<Issue>();
        private static readonly string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"IssueReports.txt");

        public static List<Issue> Issues => issues;

        //Load issues from file
        public static void LoadIssues()
        {
            issues.Clear();

            if (!File.Exists(filePath)) return;


            foreach (var line in File.ReadAllLines(filePath))
            {
                var parts = line.Split('|');
                if (parts.Length != 4) continue;

                if (!DateTime.TryParse(parts[3], out var when))
                    when = DateTime.MinValue;
                
                    Issues.Add(new Issue
                    {
                        Category = parts[0],
                        Description = parts[1],
                        Location = parts[2],
                        DateReported = when
                    });
                }
            }
            

        //Save a new issue to file
        public static void AddIssue(Issue issue)
        {
            Issues.Add(issue);
            Directory.CreateDirectory(Path.GetDirectoryName(filePath)); File.AppendAllText(filePath, $"{issue.Category}|{issue.Description}|{issue.Location}|{issue.DateReported}{Environment.NewLine}");
        }

        //Return all issues
        public static List<Issue> GetAllIssues()
        {
            return Issues;
        }

        public static void DeleteIssue(int index)
        {
            if (index >=0 && index < issues.Count)
            {
                issues.RemoveAt(index);

                // Rewrite the file with updated list
                List<string> lines = new List<string>();
                foreach (var issue in issues)
                {
                     lines.Add($"{issue.Category}|{issue.Description}|{issue.Location}|{issue.DateReported}");
                }
                System.IO.File.WriteAllLines(filePath, lines);
            }
        }
    }
}
