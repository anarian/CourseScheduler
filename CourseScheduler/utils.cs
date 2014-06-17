using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;

namespace CourseScheduler
{
	public static class GlobalScore
	{
		static uint currentScore;
		static int _iteration;
		static long permutations;
		static bool running;

		public static bool Running
		{
			get { return running; }
			set { running = value; }
		}
		public static uint CurrentScore
		{
			get { return currentScore; }
			set { currentScore = value; }
		}
		public static int CurrentIteration
		{
			get { return _iteration; }
			set { _iteration = value; }
		}
		public static long Permutations
		{
			get { return permutations; }
			set { permutations = value; }
		}
	}
	class Utils
	{
		public static Stack<Stack<Course>> creativelyOrganizeClasses(List<Course>[,] classes, string[] classNames, IProgress<int> worker)
		{
			GlobalScore.CurrentScore = 2000000;
			GlobalScore.CurrentIteration = 0;
			Stack<Stack<Course>> idealSchedules = new Stack<Stack<Course>>(classes.Length);
			Stack<Course> schedule = new Stack<Course>();
			
			long count = 1;
			for (int i = 0; i < 5; i++)
			{
				for (int j = 0; j < 3; j++)
				{
					if (classes[i, j].Count != 0)
					{
						count *= classes[i, j].Count;
					}
				}
			}

			//Console.WriteLine("Testing {0} permutations, this may take some time.\n", count);
			GlobalScore.Permutations = count;
			GlobalScore.Running = true;

			addClass(classes, schedule, idealSchedules, 0, 0, 0, worker);
			//Console.Write("\rDone! Best Score: {0}", GlobalScore.CurrentScore);

			return idealSchedules;
		}
		/* Recursively generate a tree using a stack to store the classes, calculate the score,
		 * and store it into the idealSchedules stack. If score of the next is smaller than the
		 * current one in the stack, pop it and push the new one, if it's 0, add it into the
		 * stack.
		 */
		private static void addClass(List<Course>[,] classList, Stack<Course> classes, Stack<Stack<Course>> storage, int currentClass, int currentType, int currentDepth, IProgress<int> worker)
		{
			if(!GlobalScore.Running) {
				return;
			} 
			//Base case, all classes filled.
			else if (currentClass == 5)
			{
				double progress = ((double)GlobalScore.CurrentIteration+1) / (double)GlobalScore.Permutations * (double)100;
				if ((int)progress % 5 == 0)
				{
					worker.Report((int)progress);
				}
				//Console.Write("\rProgress: {0:F2}%", progress);
				GlobalScore.CurrentIteration += 1;
				uint score = calculateScore(classes);
				if (score == 0 && GlobalScore.CurrentScore == 0)
				{
					Stack<Course> toSave = new Stack<Course>();
					foreach(Course courseToSave in classes) 
					{
						toSave.Push(courseToSave.Clone());
					}
					storage.Push(toSave);
				}
				else if (score < GlobalScore.CurrentScore) 
				{
					if (storage.Count != 0) { storage.Pop(); }
					Stack<Course> toSave = new Stack<Course>();
					foreach (Course courseToSave in classes)
					{
						toSave.Push(courseToSave.Clone());
					}
					GlobalScore.CurrentScore = score;
					storage.Push(toSave);
				}
			}
			//Added all types for this course, continue to next course
			else if (currentType == 3)
			{
				addClass(classList, classes, storage, currentClass + 1, 0, 0, worker);
			}
			//Still need more types
			else 
			{
				if (classList[currentClass, currentType].Count == 0)
				{
					addClass(classList, classes, storage, currentClass, currentType + 1, 0, worker);
				}
				else
				{
					for (int i = 0; i < classList[currentClass, currentType].Count; i++)
					{
						dynamic newClass = classList[currentClass, currentType][i];
						classes.Push(newClass);
						addClass(classList, classes, storage, currentClass, currentType + 1, i, worker);
						classes.Pop();
					}
				}
			}
		}

		private static uint calculateScore(Stack<Course> schedule)
		{
			uint score = 0;
			int count = schedule.Count;
			foreach(Course classCheck1 in schedule)
			{
				foreach(Course classCheck2 in schedule)
				{
					if (!Class.Equals(classCheck1, classCheck2))
					{
						score += (uint)scoreConflict(classCheck1, classCheck2);
					}
				}
			}
			return score;
		}

		//These functions just compare different possibilities of lectures, just in case. 
		private static bool classCheck(Class class1, Class class2)
		{
			if ((class1.startTime < class2.endTime) && (class1.startTime >= class2.startTime)) return true;
			else if ((class2.startTime > class1.startTime) && (class2.startTime <= class1.endTime)) return true;
			else if (class2.startTime == class1.startTime) return true;
			else return false;
		}

		private static int scoreConflict(Course class1, Course class2)
		{
			int conflictScore = 0;
			int conflictValue;
			if (String.Equals(class1.courseType, "LEC") && String.Equals(class2.courseType, "LEC"))
			{
				conflictValue = 5;
			}
			else if (String.Equals(class1.courseType, "PRA") && String.Equals(class2.courseType, "PRA"))
			{
				conflictValue = 1;
			}
			else if (String.Equals(class1.courseType, "TUT") && String.Equals(class2.courseType, "TUT"))
			{
				conflictValue = 2;
			}
			else if (String.Equals(class1.courseType, "LEC") && String.Equals(class2.courseType, "PRA"))
			{
				conflictValue = 4;
			}
			else if (String.Equals(class1.courseType, "PRA") && String.Equals(class2.courseType, "LEC"))
			{
				conflictValue = 3;
			}
			else if (String.Equals(class1.courseType, "LEC") && String.Equals(class2.courseType, "TUT"))
			{
				conflictValue = 3;
			}
			else if (String.Equals(class1.courseType, "TUT") && String.Equals(class2.courseType, "LEC"))
			{
				conflictValue = 3;
			}
			else if (String.Equals(class1.courseType, "PRA") && String.Equals(class2.courseType, "TUT"))
			{
				conflictValue = 3;
			}
			else
			{
				conflictValue = 3;
			}
			foreach (Class checkClass1 in class1.lectures)
			{
				foreach (Class checkClass2 in class2.lectures)
				{
					if (!(checkClass1 == checkClass2) && !classCheck(checkClass1, checkClass2))
					{
						conflictScore += conflictValue;
					}
				}
			}
			return conflictScore;
		}

		/* Function takes an open CSV file and parses. The structure is based off the online
		 * version of the timetable with the time converted to integer times, and the names
		 * only have the last name. 
		 */
		public static List<Course> parseFile(StreamReader file)
		{
			List<Course> list = new List<Course>();
			string line = file.ReadLine();
			do
			{
				string[] lineValues = line.Split(',');
				string lectureName = lineValues[0];
				int lectureSession = int.Parse(lineValues[1].Substring(7, 1));
				string[] daysOfTheWeek = new string[10]; //Just in case
				string[] lectureLocation = new string[10];
				float[] startTimes = new float[10];
				float[] endTimes = new float[10];
				string prof = lineValues[9];
				int j = 0; //Count position in daysOfTheWeek and number of sessions
				do
				{
					daysOfTheWeek[j] = lineValues[4];
					lectureLocation[j] = lineValues[7];
					startTimes[j] = float.Parse(lineValues[5]);
					endTimes[j] = float.Parse(lineValues[6]);
					j++;
					line = file.ReadLine();
					if (line == null)
					{
						break;
					}
					lineValues = line.Split(',');
				} while (String.Compare(lineValues[1], " ") == 0);
				string courseType;
				if (lineValues[1].Length > 2 && String.Compare(lineValues[1].Substring(0, 3), "LEC") == 0)
				{//Handle lectures
					courseType = "LEC";
				}
				else if (lineValues[1].Length > 2 && String.Compare(lineValues[1].Substring(0, 3), "TUT") == 0)
				{//Handle tutorials
					courseType = "TUT";
				}
				else {
					courseType = "PRA";
				}
				Course newLecture = new Course(lectureName, j, daysOfTheWeek, lectureSession, startTimes, endTimes, lectureLocation, courseType);
				list.Add(newLecture);
			}
			while (line != null);
			return list;
		}
	}
}