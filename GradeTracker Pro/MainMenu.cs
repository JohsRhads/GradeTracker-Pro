using System;
using System.Collections.Generic; // If needed
// No need for System.Linq here unless you use LINQ in MainMenu

namespace GradeTracker_Pro
{
    class MainMenu
    {
        private GradeManager Grademanage;

        public MainMenu(GradeManager manage)
        {
            Grademanage = manage;
        }

        public void show()
        {
            while (true)
            {
                Console.WriteLine("════════════════════════════════");
                Console.WriteLine("     STUDENT GRADE SYSTEM");
                Console.WriteLine("════════════════════════════════");
                Console.WriteLine("1. Add Student");
                Console.WriteLine("2. View All Students");
                Console.WriteLine("3. Search Student");
                Console.WriteLine("4. Update Grades");
                Console.WriteLine("5. Remove Student");
                Console.WriteLine("6. Class Statistics");
                Console.WriteLine("7. Exit");
                Console.WriteLine("════════════════════════════════");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                if (int.TryParse(choice, out int parseChoice))
                {
                    switch (parseChoice)
                    {
                        case 1:
                            AddStudentFlow();
                            break;
                        case 2:
                            Grademanage.ViewAllStudent();
                            break;
                        case 3:
                            SearchStudentFlow();
                            break;
                        case 4:
                            UpdateGradesFlow();
                            break;
                        case 5:
                            RemoveStudentFlow();
                            break;
                        case 6:
                            Grademanage.ClassStatistics();
                            break;
                        case 7:
                            Console.WriteLine("\n════════════════════════════════");
                            Console.WriteLine("     THANK YOU FOR USING");
                            Console.WriteLine("     STUDENT GRADE SYSTEM");
                            Console.WriteLine("════════════════════════════════");
                            return;
                        default:
                            Console.WriteLine("Invalid choice! Please enter 1-7.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input! Please enter a number between 1-7.");
                }

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        private void AddStudentFlow()
        {
            Console.Clear();
            Console.WriteLine("════════════════════════════════");
            Console.WriteLine("          ADD STUDENT");
            Console.WriteLine("════════════════════════════════");

            Console.Write("Enter student name: ");
            string name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Name cannot be empty!");
                return;
            }

            double math, science, english;

            while (true)
            {
                Console.Write("Enter Math grade (0-100): ");
                if (double.TryParse(Console.ReadLine(), out math) && math >= 0 && math <= 100)
                    break;
                Console.WriteLine("Invalid input! Please enter a number between 0 and 100.");
            }

            while (true)
            {
                Console.Write("Enter Science grade (0-100): ");
                if (double.TryParse(Console.ReadLine(), out science) && science >= 0 && science <= 100)
                    break;
                Console.WriteLine("Invalid input! Please enter a number between 0 and 100.");
            }

            while (true)
            {
                Console.Write("Enter English grade (0-100): ");
                if (double.TryParse(Console.ReadLine(), out english) && english >= 0 && english <= 100)
                    break;
                Console.WriteLine("Invalid input! Please enter a number between 0 and 100.");
            }

            Student newStudent = new Student(name, math, science, english);
            Grademanage.AddStudent(newStudent);
        }

        private void SearchStudentFlow()
        {
            Console.Clear();
            Console.Write("Enter student name to search: ");
            string name = Console.ReadLine();
            Grademanage.SearchStudent(name);
        }

        private void UpdateGradesFlow()
        {
            Console.Clear();
            Console.Write("Enter student name to update grades: ");
            string name = Console.ReadLine();
            Grademanage.UpdateGrades(name);
        }

        private void RemoveStudentFlow()
        {
            Console.Clear();
            Console.Write("Enter student name to remove: ");
            string name = Console.ReadLine();
            Grademanage.RemoveStudent(name);
        }
    }
}