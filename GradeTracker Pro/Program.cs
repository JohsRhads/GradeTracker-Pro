using GradeTracker_Pro;
using System;
namespace GradeTrackerPro
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            GradeManager gradeManager = new GradeManager();
            MainMenu main = new MainMenu(gradeManager);
            main.show();
        }
    }
    

}