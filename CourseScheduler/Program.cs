using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CourseSchedulerClass;
using CourseSchedulerUtils;

namespace CourseScheduler
{
	class Program
	{
		static void Main(string[] args)
		{
			string fileLocation;
			//Check if argument present.
			if (args.Length != 1)
			{
				Console.WriteLine("Specify CSV file of courses:");
				fileLocation = Console.ReadLine();
			}
			else {
				fileLocation = args[0];
			}

			//Requires CSV file to run
			if (!fileLocation.Contains("csv") && !fileLocation.Contains("CSV"))
			{
				Console.WriteLine("Error: Expecting CSV file.");
				Console.WriteLine("Press any key to exit.");
				Console.ReadLine();
				return;
			}

			StreamReader file;
			//Attempt file open
			try
			{
				file = new StreamReader(fileLocation);
			}
			catch (Exception e) 
			{
				//If it fails, catch the exception, error message, and exit
				Console.Write(e.Message);
				Console.WriteLine("Press any key to exit.");
				Console.ReadLine();
				return;
			}

			string[] courseCodes = new string[5];

			//Read the courses to search from
			Console.WriteLine("Enter course codes, one at a time. (Limit 5)");
			for (var i = 0; i < 5; i++)
			{
				courseCodes[i] = Console.ReadLine();
			}

			//Create a new list of relevant courses
			List<Course> fullCourseList = new List<Course>();

			parseFile(fullCourseList, file, courseCodes);

			Console.WriteLine("\nFound {0} matches", fullCourseList.Count);

			foreach (string course in courseCodes)
			{
				if(!fullCourseList.Exists(delegate(Course toCheck) {
					return (String.Equals(toCheck.courseName.Substring(0,6), course));
				}))
				{
					Console.WriteLine("Warning: {0} cannot be found. Continue? (Y|N)", course);
					string cont = Console.ReadLine();
					if (!(String.Equals(cont, "Y") || String.Equals(cont, "y")))
					{
						Console.WriteLine("Press any key to close.");
						Console.ReadKey(true);
						return;
					}
				}
			}

			List<Course>[,] courses = new List<Course>[5, 3];
			for (var i = 0; i < 5; i++)
			{
				for(var j = 0; j < 3; j++) {
					courses[i, j] = new List<Course>();
					courses[i, j] = fullCourseList.FindAll(delegate(Course classToCheck)
					{
						if (j == 0)
						{
							return (String.Equals(classToCheck.courseName.Substring(0, 6), courseCodes[i]) && (String.Equals(classToCheck.courseType, "LEC")));
						}
						else if (j == 1)
						{
							return (String.Equals(classToCheck.courseName.Substring(0, 6), courseCodes[i]) && (String.Equals(classToCheck.courseType, "TUT")));
						}
						else 
						{
							return (String.Equals(classToCheck.courseName.Substring(0, 6), courseCodes[i]) && (String.Equals(classToCheck.courseType, "PRA")));
						}
					});
				}
				
			}//Index will match courseCodes index

			Stack<Stack<Course>> schedules = Utils.creativelyOrganizeClasses(courses, courseCodes);

			Console.WriteLine();

			foreach (Stack<Course> possibleSchedule in schedules)
			{
				foreach (Course possibleClass in possibleSchedule)
				{
					if (String.Equals(possibleClass.courseType, "LEC")) 
					{
						Console.WriteLine("{0} LEC010{1}", possibleClass.courseName, possibleClass.courseSection);
					}
					else if (String.Equals(possibleClass.courseType, "PRA"))
					{
						Console.WriteLine("{0} PRA010{1}", possibleClass.courseName, possibleClass.courseSection);
					}
					else
					{
						Console.WriteLine("{0} TUT010{1}", possibleClass.courseName, possibleClass.courseSection);
					}
				}
			}
			Console.WriteLine("Press any key to close.");
			Console.ReadKey(true);
			return;
		}

		/* Function takes an open CSV file and parses. The structure is based off the online
		 * version of the timetable with the time converted to integer times, and the names
		 * only have the last name. It returns a List with all the matched courses. 
		 */
		private static void parseFile(List<Course> list, StreamReader file, string[] courses)
		{
			string line = file.ReadLine();
			do
			{
				string[] lineValues = line.Split(',');
				bool hit = false;
				for (int i = 0; i < 5; i++)
				{
					if (String.Compare(lineValues[0].Substring(0, 6), courses[i]) == 0)
					{//Compare current line with list of courses
						hit = true;
						if (lineValues[1].Length > 2 && String.Compare(lineValues[1].Substring(0, 3), "LEC") == 0)
						{//Handle lectures
							string lectureName = lineValues[0];
							int lectureSession = int.Parse(lineValues[1].Substring(7, 1));
							string[] daysOfTheWeek = new string[5];
							string[] lectureLocation = new string[5];
							int[] startTimes = new int[5];
							int[] endTimes = new int[6];
							string prof = lineValues[9];
							//Somewhat doubt there'd be more than 5 lectures a week
							int j = 0; //Count position in daysOfTheWeek and number of sessions
							do
							{
								daysOfTheWeek[j] = lineValues[4];
								lectureLocation[j] = lineValues[7];
								startTimes[j] = int.Parse(lineValues[5]);
								endTimes[j] = int.Parse(lineValues[6]);
								j++;
								line = file.ReadLine();
								if (line == null)
								{
									break;
								}
								lineValues = line.Split(',');
							} while (String.Compare(lineValues[1], " ") == 0);
							//daysOfTheWeek will now contain letters representing the days
							Course newLecture = new Course(lectureName, j, daysOfTheWeek, lectureSession, startTimes, endTimes, lectureLocation, "LEC");
							list.Add(newLecture);
						}
						else if (lineValues[1].Length > 2 && String.Compare(lineValues[1].Substring(0, 3), "TUT") == 0)
						{//Handle tutorials
							string lectureName = lineValues[0];
							int lectureSession = int.Parse(lineValues[1].Substring(7, 1));
							string[] daysOfTheWeek = new string[5];
							string[] lectureLocation = new string[5];
							int[] startTimes = new int[5];
							int[] endTimes = new int[6];
							string prof = lineValues[9];
							//Somewhat doubt there'd be more than 5 lectures a week
							int j = 0; //Count position in daysOfTheWeek and number of sessions
							do
							{
								daysOfTheWeek[j] = lineValues[4];
								lectureLocation[j] = lineValues[7];
								startTimes[j] = int.Parse(lineValues[5]);
								endTimes[j] = int.Parse(lineValues[6]);
								j++;
								line = file.ReadLine();
								if (line == null)
								{
									break;
								}
								lineValues = line.Split(',');
							} while (String.Compare(lineValues[1], " ") == 0);
							//daysOfTheWeek will now contain letters representing the days
							Course newTutorial = new Course(lectureName, j, daysOfTheWeek, lectureSession, startTimes, endTimes, lectureLocation, "TUT");
							list.Add(newTutorial);
						}
						else if (lineValues[1].Length > 2 && String.Compare(lineValues[1].Substring(0, 3), "PRA") == 0)
						{//Handle labs
							string lectureName = lineValues[0];
							int lectureSession = int.Parse(lineValues[1].Substring(7, 1));
							string[] daysOfTheWeek = new string[5];
							string[] lectureLocation = new string[5];
							int[] startTimes = new int[5];
							int[] endTimes = new int[6];
							string prof = lineValues[9];
							//Somewhat doubt there'd be more than 5 lectures a week
							int j = 0; //Count position in daysOfTheWeek and number of sessions
							do
							{
								daysOfTheWeek[j] = lineValues[4];
								lectureLocation[j] = lineValues[7];
								startTimes[j] = int.Parse(lineValues[5]);
								endTimes[j] = int.Parse(lineValues[6]);
								j++;
								line = file.ReadLine();
								if (line == null)
								{
									break;
								}
								lineValues = line.Split(',');
							} while (String.Compare(lineValues[1], " ") == 0);
							//daysOfTheWeek will now contain letters representing the days
							Course newPractical = new Course(lectureName, j, daysOfTheWeek, lectureSession, startTimes, endTimes, lectureLocation, "PRA");
							list.Add(newPractical);
						}
					}
				}
				if (!hit)
				{
					line = file.ReadLine();
				}
			}
			while (line != null);
		}
	}
}
