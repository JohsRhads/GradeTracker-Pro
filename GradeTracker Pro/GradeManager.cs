using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace GradeTracker_Pro
{
    public class GradeManager
    {
        public List<Student> Students = new List<Student>();
        private readonly string _filePath = "students.json";

        // ==========================================
        // CONSTRUCTOR — Load data on startup
        // ==========================================
        public GradeManager()
        {
            LoadFromFile();
        }

        // ==========================================
        // FILE I/O — Save to JSON
        // ==========================================
        public void SaveToFile()
        {
            try
            {
                string json = JsonSerializer.Serialize(Students);
                File.WriteAllText(_filePath, json);
                Console.WriteLine("Data saved successfully.");
            }
            catch (IOException)
            {
                Console.WriteLine("Error: Could not save file.");
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Error: Permission denied. Cannot save.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error while saving: {ex.Message}");
            }
        }

        // ==========================================
        // FILE I/O — Load from JSON
        // ==========================================
        public void LoadFromFile()
        {
            if (!File.Exists(_filePath))
            {
                Console.WriteLine("No save file found. Starting fresh.");
                return;
            }

            try
            {
                string json = File.ReadAllText(_filePath);
                Students = JsonSerializer.Deserialize<List<Student>>(json) ?? new List<Student>();
                Console.WriteLine($"Data loaded. {Students.Count} student(s) found.");
            }
            catch (JsonException)
            {
                Console.WriteLine("Save file corrupted. Starting fresh.");
                Students = new List<Student>();
            }
            catch (IOException)
            {
                Console.WriteLine("Error reading file. Starting fresh.");
                Students = new List<Student>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
                Students = new List<Student>();
            }
        }

        // ==========================================
        // ADD STUDENT
        // ==========================================
        public void AddStudent(Student student)
        {
            Console.WriteLine("════════════════════════════════");
            Console.WriteLine("          ADD STUDENT");
            Console.WriteLine("════════════════════════════════");

            if (Students.Any(S => S.Name.Equals(student.Name, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("THIS STUDENT ALREADY EXISTS!");
                return;
            }
            Students.Add(student);
            Console.WriteLine("Student added successfully!");
            SaveToFile();  // ← AUTO-SAVE
        }

        // ==========================================
        // VIEW ALL STUDENTS
        // ==========================================
        public void ViewAllStudent()
        {
            if (Students == null || Students.Count == 0)
            {
                Console.WriteLine("══════════════════════════════════════════════════════════════════════");
                Console.WriteLine("                         NO STUDENTS FOUND");
                Console.WriteLine("══════════════════════════════════════════════════════════════════════");
                return;
            }

            Console.WriteLine("══════════════════════════════════════════════════════════════════════");
            Console.WriteLine("                          STUDENT LIST");
            Console.WriteLine($"{"Name",-21}{"Math",-8}{"Science",-9}{"English",-9}{"Average",-9}Status");
            Console.WriteLine("──────────────────────────────────────────────────────────────────────");

            foreach (var student in Students)
            {
                Console.WriteLine(
                    $"{student.Name,-21}" +
                    $"{student.MathGrade,-8:F0}" +
                    $"{student.ScienceGrade,-9:F0}" +
                    $"{student.EnglishGrade,-9:F0}" +
                    $"{student.Average,-9:F1}" +
                    $"{student.Status.ToUpper()}"
                );
            }

            Console.WriteLine("══════════════════════════════════════════════════════════════════════");
        }

        // ==========================================
        // SEARCH STUDENT
        // ==========================================
        public void SearchStudent(string studentName)
        {
            var searchingstudent = Students.Find(s => s.Name.Equals(studentName, StringComparison.OrdinalIgnoreCase));

            if (searchingstudent == null)
            {
                Console.WriteLine("══════════════════════════════════════════════════════════════════════");
                Console.WriteLine($"               STUDENT '{studentName.ToUpper()}' NOT FOUND");
                Console.WriteLine("══════════════════════════════════════════════════════════════════════");
                return;
            }

            Console.WriteLine("══════════════════════════════════════════════════════════════════════");
            Console.WriteLine("                         SEARCH RESULT");
            Console.WriteLine("══════════════════════════════════════════════════════════════════════");
            Console.WriteLine($"{"Name",-21}{"Math",-8}{"Science",-9}{"English",-9}{"Average",-9}Status");
            Console.WriteLine("──────────────────────────────────────────────────────────────────────");

            Console.WriteLine(
                $"{searchingstudent.Name,-21}" +
                $"{searchingstudent.MathGrade,-8:F0}" +
                $"{searchingstudent.ScienceGrade,-9:F0}" +
                $"{searchingstudent.EnglishGrade,-9:F0}" +
                $"{searchingstudent.Average,-9:F1}" +
                $"{searchingstudent.Status.ToUpper()}"
            );

            Console.WriteLine("══════════════════════════════════════════════════════════════════════");
        }

        // ==========================================
        // UPDATE GRADES
        // ==========================================
        public void UpdateGrades(string studentName)
        {
            var searchingstudent = Students.Find(s => s.Name.Equals(studentName, StringComparison.OrdinalIgnoreCase));
            if (searchingstudent == null)
            {
                Console.WriteLine("══════════════════════════════════════════════════════════════════════");
                Console.WriteLine($"               STUDENT '{studentName.ToUpper()}' NOT FOUND");
                Console.WriteLine("══════════════════════════════════════════════════════════════════════");
                return;
            }

            Console.WriteLine("══════════════════════════════════════════════════════════════════════");
            Console.WriteLine("                         CURRENT GRADES");
            Console.WriteLine("══════════════════════════════════════════════════════════════════════");
            Console.WriteLine($"{"Name",-21}{"Math",-8}{"Science",-9}{"English",-9}{"Average",-9}Status");
            Console.WriteLine("──────────────────────────────────────────────────────────────────────");

            Console.WriteLine(
                $"{searchingstudent.Name,-21}" +
                $"{searchingstudent.MathGrade,-8:F0}" +
                $"{searchingstudent.ScienceGrade,-9:F0}" +
                $"{searchingstudent.EnglishGrade,-9:F0}" +
                $"{searchingstudent.Average,-9:F1}" +
                $"{searchingstudent.Status.ToUpper()}"
            );
            Console.WriteLine("══════════════════════════════════════════════════════════════════════");

            Console.WriteLine("\nEnter new grades (0-100):");

            double newMathGrade, newScienceGrade, newEnglishGrade;

            while (true)
            {
                Console.Write("Math Grade: ");
                if (double.TryParse(Console.ReadLine(), out newMathGrade) && newMathGrade >= 0 && newMathGrade <= 100)
                    break;
                Console.WriteLine("Invalid input! Please enter a number between 0 and 100.");
            }

            while (true)
            {
                Console.Write("Science Grade: ");
                if (double.TryParse(Console.ReadLine(), out newScienceGrade) && newScienceGrade >= 0 && newScienceGrade <= 100)
                    break;
                Console.WriteLine("Invalid input! Please enter a number between 0 and 100.");
            }

            while (true)
            {
                Console.Write("English Grade: ");
                if (double.TryParse(Console.ReadLine(), out newEnglishGrade) && newEnglishGrade >= 0 && newEnglishGrade <= 100)
                    break;
                Console.WriteLine("Invalid input! Please enter a number between 0 and 100.");
            }

            searchingstudent.MathGrade = newMathGrade;
            searchingstudent.ScienceGrade = newScienceGrade;
            searchingstudent.EnglishGrade = newEnglishGrade;

            Console.WriteLine("\n══════════════════════════════════════════════════════════════════════");
            Console.WriteLine("                         UPDATED GRADES");
            Console.WriteLine("══════════════════════════════════════════════════════════════════════");
            Console.WriteLine($"{"Name",-21}{"Math",-8}{"Science",-9}{"English",-9}{"Average",-9}Status");
            Console.WriteLine("──────────────────────────────────────────────────────────────────────");

            Console.WriteLine(
                $"{searchingstudent.Name,-21}" +
                $"{searchingstudent.MathGrade,-8:F0}" +
                $"{searchingstudent.ScienceGrade,-9:F0}" +
                $"{searchingstudent.EnglishGrade,-9:F0}" +
                $"{searchingstudent.Average,-9:F1}" +
                $"{searchingstudent.Status.ToUpper()}"
            );
            Console.WriteLine("══════════════════════════════════════════════════════════════════════");

            Console.WriteLine("\nGrades updated successfully!");
            SaveToFile();  // ← AUTO-SAVE
        }

        // ==========================================
        // REMOVE STUDENT
        // ==========================================
        public void RemoveStudent(string Studentname)
        {
            Console.WriteLine("════════════════════════════════");
            Console.WriteLine("     REMOVE STUDENT");
            Console.WriteLine("════════════════════════════════");

            var studentremove = Students.RemoveAll(s => s.Name.Equals(Studentname, StringComparison.OrdinalIgnoreCase));
            if (studentremove > 0)
            {
                Console.WriteLine($"Student {Studentname} has been removed.");
                SaveToFile();  // ← AUTO-SAVE
            }
            else
            {
                Console.WriteLine($"Student {Studentname} not found.");
            }
        }

        // ==========================================
        // CLASS STATISTICS
        // ==========================================
        public void ClassStatistics()
        {
            if (Students == null || Students.Count == 0)
            {
                Console.WriteLine("══════════════════════════════════════════════════════════════════════");
                Console.WriteLine("                      NO STUDENTS TO ANALYZE");
                Console.WriteLine("══════════════════════════════════════════════════════════════════════");
                return;
            }

            double highestAvg = Students.Max(s => s.Average);
            double lowestAvg = Students.Min(s => s.Average);
            double classOverallAvg = Students.Average(s => s.Average);
            int totalStudents = Students.Count;
            int passedCount = Students.Count(s => s.Status.ToUpper() == "PASSED");
            int failedCount = Students.Count(s => s.Status.ToUpper() == "FAILED");
            var topStudent = Students.FirstOrDefault(s => s.Average == highestAvg);
            var lowestStudent = Students.FirstOrDefault(s => s.Average == lowestAvg);

            Console.WriteLine("══════════════════════════════════════════════════════════════════════");
            Console.WriteLine("                         CLASS STATISTICS");
            Console.WriteLine("══════════════════════════════════════════════════════════════════════");

            Console.WriteLine($"\nOVERVIEW:");
            Console.WriteLine($"   Total Students:        {totalStudents}");
            Console.WriteLine($"   Students Passed:       {passedCount} ({(double)passedCount / totalStudents * 100:F1}%)");
            Console.WriteLine($"   Students Failed:       {failedCount} ({(double)failedCount / totalStudents * 100:F1}%)");

            Console.WriteLine($"\nAVERAGE ANALYSIS:");
            Console.WriteLine($"   Highest Student Average:  {highestAvg:F1}");
            Console.WriteLine($"      {topStudent?.Name} - {topStudent?.Average:F1}");
            Console.WriteLine($"   Lowest Student Average:   {lowestAvg:F1}");
            Console.WriteLine($"      {lowestStudent?.Name} - {lowestStudent?.Average:F1}");
            Console.WriteLine($"   Class Overall Average:    {classOverallAvg:F1}");

            Console.WriteLine($"\nGRADE DISTRIBUTION:");
            int excellentCount = Students.Count(s => s.Average >= 90);
            int goodCount = Students.Count(s => s.Average >= 75 && s.Average < 90);
            int averageCount = Students.Count(s => s.Average >= 60 && s.Average < 75);
            int poorCount = Students.Count(s => s.Average < 60);

            Console.WriteLine($"   Excellent (90-100):  {excellentCount} student(s)");
            Console.WriteLine($"   Good (75-89):        {goodCount} student(s)");
            Console.WriteLine($"   Average (60-74):     {averageCount} student(s)");
            Console.WriteLine($"   Poor (Below 60):     {poorCount} student(s)");

            Console.WriteLine("\n══════════════════════════════════════════════════════════════════════");
        }
    }
}