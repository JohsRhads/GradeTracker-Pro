using System;
using System.Collections.Generic;
using System.Text;

namespace GradeTracker_Pro
{
    public class Student
    {
        public string Name { get; set; } = string.Empty;
        private double mathGrade;
        private double scienceGrade;
        private double englishGrade;

        private bool IsValidGrade(double value)
        {
            if (value < 0 || value > 100)
            {
                Console.WriteLine("Invalid grade. Must be 0-100.");
                return false;
            }
            return true;
        }

        public double MathGrade
        {
            get => mathGrade;
            set
            {
                if (IsValidGrade(value))
                    mathGrade = value;
            }
        }

        public double ScienceGrade
        {
            get => scienceGrade;
            set
            {
                if (IsValidGrade(value))
                    scienceGrade = value;
            }
        }

        public double EnglishGrade
        {
            get => englishGrade;
            set
            {
                if (IsValidGrade(value))
                    englishGrade = value;
            }
        }

        public double Average
        {
            get { return (MathGrade + ScienceGrade + EnglishGrade) / 3.0; }
        }
        public string Status
        {
            get
            {
                if (Average >= 75)
                    return "Passed";
                else
                    return "Failed";    
            }
        }
        public Student(string name, double math, double science, double english)
        {
            Name = name;
            MathGrade = math;
            ScienceGrade = science;
            EnglishGrade = english;
        }

    }
}