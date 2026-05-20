using System;
using System.Collections.Generic;
using System.Text;

namespace GradeTracker_Pro
{
     class MainMenu
    {
        public void show()
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
        }
    }
}
