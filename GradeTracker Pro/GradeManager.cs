using System;
using System.Collections.Generic;
using System.Linq; // Enables the .Any() verification


namespace GradeTracker_Pro
{
    public class GradeManager
    {
        public List<Student> Students = new List<Student>();
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
        }
        public void ViewAllStudent()
        {
            // 1. Check if there's anything to display
            if (Students == null || Students.Count == 0)
            {
                Console.WriteLine("══════════════════════════════════════════════════════════════════════");
                Console.WriteLine("                         NO STUDENTS FOUND");
                Console.WriteLine("══════════════════════════════════════════════════════════════════════");
                return;
            }

            // 2. Top Header Banner
            Console.WriteLine("══════════════════════════════════════════════════════════════════════");
            Console.WriteLine("                          STUDENT LIST");

            // 3. Column Names (Using spaces/alignment to match your layout perfectly)
            Console.WriteLine($"{"Name",-21}{"Math",-8}{"Science",-9}{"English",-9}{"Average",-9}Status");
            Console.WriteLine("──────────────────────────────────────────────────────────────────────");

            // 4. Loop through students and print rows
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

            // 5. Bottom Footer Border
            Console.WriteLine("══════════════════════════════════════════════════════════════════════");
        }
        public void SearchStudent(string studentName)
        {
            // 1. Find the student (case-insensitive)
            var searchingstudent = Students.Find(s => s.Name.Equals(studentName, StringComparison.OrdinalIgnoreCase));

            // 2. Handle if the student is not found
            if (searchingstudent == null)
            {
                Console.WriteLine("══════════════════════════════════════════════════════════════════════");
                Console.WriteLine($"               STUDENT '{studentName.ToUpper()}' NOT FOUND");
                Console.WriteLine("══════════════════════════════════════════════════════════════════════");
                return;
            }

            // 3. Display the found student with matching table format
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
        public void UpdateGrades(string studentName)
        {
            // 1. User enters student name (already passed as parameter)
            // 2. System checks: Does student exist?
            var searchingstudent = Students.Find(s => s.Name.Equals(studentName, StringComparison.OrdinalIgnoreCase));
            if (searchingstudent == null)
            {
                Console.WriteLine("══════════════════════════════════════════════════════════════════════");
                Console.WriteLine($"               STUDENT '{studentName.ToUpper()}' NOT FOUND");
                Console.WriteLine("══════════════════════════════════════════════════════════════════════");
                return; // Back to menu
            }

            // 3. Show current grades (so user knows what they're changing)
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

            // 4,5,6. Ask for new grades with validation
            Console.WriteLine("\nEnter new grades (0-100):");

            double newMathGrade, newScienceGrade, newEnglishGrade;

            // 4. Ask for new Math grade
            while (true)
            {
                Console.Write("Math Grade: ");
                if (double.TryParse(Console.ReadLine(), out newMathGrade) && newMathGrade >= 0 && newMathGrade <= 100)
                    break;
                Console.WriteLine("Invalid input! Please enter a number between 0 and 100.");
            }

            // 5. Ask for new Science grade
            while (true)
            {
                Console.Write("Science Grade: ");
                if (double.TryParse(Console.ReadLine(), out newScienceGrade) && newScienceGrade >= 0 && newScienceGrade <= 100)
                    break;
                Console.WriteLine("Invalid input! Please enter a number between 0 and 100.");
            }

            // 6. Ask for new English grade
            while (true)
            {
                Console.Write("English Grade: ");
                if (double.TryParse(Console.ReadLine(), out newEnglishGrade) && newEnglishGrade >= 0 && newEnglishGrade <= 100)
                    break;
                Console.WriteLine("Invalid input! Please enter a number between 0 and 100.");
            }

            // 7. Validate all inputs (0-100, must be numbers) - DONE above
            // 8. Update student's grades
            searchingstudent.MathGrade = newMathGrade;
            searchingstudent.ScienceGrade = newScienceGrade;
            searchingstudent.EnglishGrade = newEnglishGrade;

            // 9. Show updated grades (confirmation)
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

            // 10. "Grades updated successfully!"
            Console.WriteLine("\nGrades updated successfully!");
            // 11. Back to menu (automatically returns)
        }
        public void RemoveStudent(string Studentname)
        {
            Console.WriteLine("════════════════════════════════");
            Console.WriteLine("     REMOVE STUDENT");
            Console.WriteLine("════════════════════════════════");

            var studentremove = Students.RemoveAll(s => s.Name.Equals(Studentname, StringComparison.OrdinalIgnoreCase));
            if (studentremove > 0)
            {
                Console.WriteLine($"Student {Studentname} has been removed.");
            }
            else
            {
                Console.WriteLine($"Student {Studentname} not found.");
            }
        }
    }
}
