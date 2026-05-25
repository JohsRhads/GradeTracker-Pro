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
        // CONSTRUCTOR
        // ==========================================
        public GradeManager()
        {
            LoadFromFile();
        }

        // ==========================================
        // SAVE TO FILE
        // ==========================================
        public void SaveToFile()
        {
            try
            {
                string json = JsonSerializer.Serialize(Students);
                File.WriteAllText(_filePath, json);
                Console.WriteLine("Data saved successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Save failed: {ex.Message}");
            }
        }

        // ==========================================
        // LOAD FROM FILE
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
                var result = JsonSerializer.Deserialize<List<Student>>(json);

                if (result != null)
                {
                    Students = result;
                }
                else
                {
                    Students = new List<Student>();
                }

                Console.WriteLine($"Data loaded. {Students.Count} student(s) found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Load failed: {ex.Message}. Starting fresh.");
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
            SaveToFile();
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
            var student = Students.Find(s => s.Name.Equals(studentName, StringComparison.OrdinalIgnoreCase));

            if (student == null)
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
                $"{student.Name,-21}" +
                $"{student.MathGrade,-8:F0}" +
                $"{student.ScienceGrade,-9:F0}" +
                $"{student.EnglishGrade,-9:F0}" +
                $"{student.Average,-9:F1}" +
                $"{student.Status.ToUpper()}"
            );

            Console.WriteLine("══════════════════════════════════════════════════════════════════════");
        }

        // ==========================================
        // UPDATE GRADES
        // ==========================================
        public void UpdateGrades(string studentName)
        {
            var student = Students.Find(s => s.Name.Equals(studentName, StringComparison.OrdinalIgnoreCase));

            if (student == null)
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
                $"{student.Name,-21}" +
                $"{student.MathGrade,-8:F0}" +
                $"{student.ScienceGrade,-9:F0}" +
                $"{student.EnglishGrade,-9:F0}" +
                $"{student.Average,-9:F1}" +
                $"{student.Status.ToUpper()}"
            );
            Console.WriteLine("══════════════════════════════════════════════════════════════════════");

            Console.WriteLine("\nEnter new grades (0-100):");

            double newMath, newScience, newEnglish;

            while (true)
            {
                Console.Write("Math Grade: ");
                if (double.TryParse(Console.ReadLine(), out newMath) && newMath >= 0 && newMath <= 100)
                    break;
                Console.WriteLine("Invalid! Enter a number between 0 and 100.");
            }

            while (true)
            {
                Console.Write("Science Grade: ");
                if (double.TryParse(Console.ReadLine(), out newScience) && newScience >= 0 && newScience <= 100)
                    break;
                Console.WriteLine("Invalid! Enter a number between 0 and 100.");
            }

            while (true)
            {
                Console.Write("English Grade: ");
                if (double.TryParse(Console.ReadLine(), out newEnglish) && newEnglish >= 0 && newEnglish <= 100)
                    break;
                Console.WriteLine("Invalid! Enter a number between 0 and 100.");
            }

            student.MathGrade = newMath;
            student.ScienceGrade = newScience;
            student.EnglishGrade = newEnglish;

            Console.WriteLine("\n══════════════════════════════════════════════════════════════════════");
            Console.WriteLine("                         UPDATED GRADES");
            Console.WriteLine("══════════════════════════════════════════════════════════════════════");
            Console.WriteLine($"{"Name",-21}{"Math",-8}{"Science",-9}{"English",-9}{"Average",-9}Status");
            Console.WriteLine("──────────────────────────────────────────────────────────────────────");

            Console.WriteLine(
                $"{student.Name,-21}" +
                $"{student.MathGrade,-8:F0}" +
                $"{student.ScienceGrade,-9:F0}" +
                $"{student.EnglishGrade,-9:F0}" +
                $"{student.Average,-9:F1}" +
                $"{student.Status.ToUpper()}"
            );
            Console.WriteLine("══════════════════════════════════════════════════════════════════════");

            Console.WriteLine("\nGrades updated successfully!");
            SaveToFile();
        }

        // ==========================================
        // REMOVE STUDENT
        // ==========================================
        public void RemoveStudent(string studentName)
        {
            Console.WriteLine("════════════════════════════════");
            Console.WriteLine("     REMOVE STUDENT");
            Console.WriteLine("════════════════════════════════");

            int removedCount = Students.RemoveAll(s => s.Name.Equals(studentName, StringComparison.OrdinalIgnoreCase));

            if (removedCount > 0)
            {
                Console.WriteLine($"Student '{studentName}' has been removed.");
                SaveToFile();
            }
            else
            {
                Console.WriteLine($"Student '{studentName}' not found.");
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
            double classAvg = Students.Average(s => s.Average);
            int total = Students.Count;
            int passed = Students.Count(s => s.Status.ToUpper() == "PASSED");
            int failed = Students.Count(s => s.Status.ToUpper() == "FAILED");

            var topStudent = Students.FirstOrDefault(s => s.Average == highestAvg);
            var bottomStudent = Students.FirstOrDefault(s => s.Average == lowestAvg);

            Console.WriteLine("══════════════════════════════════════════════════════════════════════");
            Console.WriteLine("                         CLASS STATISTICS");
            Console.WriteLine("══════════════════════════════════════════════════════════════════════");

            Console.WriteLine($"\nOVERVIEW:");
            Console.WriteLine($"   Total Students:        {total}");
            Console.WriteLine($"   Students Passed:       {passed} ({(double)passed / total * 100:F1}%)");
            Console.WriteLine($"   Students Failed:       {failed} ({(double)failed / total * 100:F1}%)");

            Console.WriteLine($"\nAVERAGE ANALYSIS:");
            Console.WriteLine($"   Highest Average:  {highestAvg:F1} — {topStudent?.Name}");
            Console.WriteLine($"   Lowest Average:   {lowestAvg:F1} — {bottomStudent?.Name}");
            Console.WriteLine($"   Class Average:    {classAvg:F1}");

            Console.WriteLine("\n══════════════════════════════════════════════════════════════════════");
        }
    }
}