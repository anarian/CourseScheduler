using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace CourseScheduler
{
	static class Program
	{
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Interface());

			//string fileLocation;
			////Check if argument present.
			//if (args.Length != 1)
			//{
			//	Console.WriteLine("Specify CSV file of courses:");
			//	fileLocation = Console.ReadLine();
			//}
			//else {
			//	fileLocation = args[0];
			//}

			////Requires CSV file to run
			//if (!fileLocation.Contains("csv") && !fileLocation.Contains("CSV"))
			//{
			//	Console.WriteLine("Error: Expecting CSV file.");
			//	Console.WriteLine("Press any key to exit.");
			//	Console.ReadLine();
			//	return;
			//}

			//StreamReader file;
			////Attempt file open
			//try
			//{
			//	file = new StreamReader(fileLocation);
			//}
			//catch (Exception e) 
			//{
			//	//If it fails, catch the exception, error message, and exit
			//	Console.Write(e.Message);
			//	Console.WriteLine("Press any key to exit.");
			//	Console.ReadLine();
			//	return;
			//}

			//string[] courseCodes = new string[5];

			////Read the courses to search from
			//Console.WriteLine("Enter course codes, one at a time. (Limit 5)");
			//for (var i = 0; i < 5; i++)
			//{
			//	courseCodes[i] = Console.ReadLine();
			//}

			////Create a new list of relevant courses
			//List<Course> fullCourseList = new List<Course>();

			//parseFile(fullCourseList, file, courseCodes);

			//Console.WriteLine("\nFound {0} matches", fullCourseList.Count);

			//foreach (string course in courseCodes)
			//{
			//	if(!fullCourseList.Exists(delegate(Course toCheck) {
			//		return (String.Equals(toCheck.courseName.Substring(0,6), course));
			//	}))
			//	{
			//		Console.WriteLine("Warning: {0} cannot be found. Continue? (Y|N)", course);
			//		string cont = Console.ReadLine();
			//		if (!(String.Equals(cont, "Y") || String.Equals(cont, "y")))
			//		{
			//			Console.WriteLine("Press any key to close.");
			//			Console.ReadKey(true);
			//			return;
			//		}
			//	}
			//}

			//List<Course>[,] courses = new List<Course>[5, 3];
			//for (var i = 0; i < 5; i++)
			//{
			//	for(var j = 0; j < 3; j++) {
			//		courses[i, j] = new List<Course>();
			//		courses[i, j] = fullCourseList.FindAll(delegate(Course classToCheck)
			//		{
			//			if (j == 0)
			//			{
			//				return (String.Equals(classToCheck.courseName.Substring(0, 6), courseCodes[i]) && (String.Equals(classToCheck.courseType, "LEC")));
			//			}
			//			else if (j == 1)
			//			{
			//				return (String.Equals(classToCheck.courseName.Substring(0, 6), courseCodes[i]) && (String.Equals(classToCheck.courseType, "TUT")));
			//			}
			//			else 
			//			{
			//				return (String.Equals(classToCheck.courseName.Substring(0, 6), courseCodes[i]) && (String.Equals(classToCheck.courseType, "PRA")));
			//			}
			//		});
			//	}
				
			//}//Index will match courseCodes index

			//Stack<Stack<Course>> schedules = Utils.creativelyOrganizeClasses(courses, courseCodes);

			//Console.WriteLine();

			//foreach (Stack<Course> possibleSchedule in schedules)
			//{
			//	foreach (Course possibleClass in possibleSchedule)
			//	{
			//		if (String.Equals(possibleClass.courseType, "LEC")) 
			//		{
			//			Console.WriteLine("{0} LEC010{1}", possibleClass.courseName, possibleClass.courseSection);
			//		}
			//		else if (String.Equals(possibleClass.courseType, "PRA"))
			//		{
			//			Console.WriteLine("{0} PRA010{1}", possibleClass.courseName, possibleClass.courseSection);
			//		}
			//		else
			//		{
			//			Console.WriteLine("{0} TUT010{1}", possibleClass.courseName, possibleClass.courseSection);
			//		}
			//	}
			//}
			//Console.WriteLine("Press any key to close.");
			//Console.ReadKey(true);
			//return;
		}
	}
}
