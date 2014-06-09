using System;
using System.Collections.Generic;
using CourseSchedulerClass;
using CourseScheduler;

namespace CourseSchedulerUtils
{
	public static class GlobalScore
	{
		static uint currentScore;
		static int _iteration;
		static long permutations;
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
		public static Stack<Stack<Course>> creativelyOrganizeClasses(List<Course>[,] classes, string[] classNames)
		{
			GlobalScore.CurrentScore = 2000000;
			Random rand = new Random();

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

			Console.WriteLine("Testing {0} permutations, this may take some time.\n", count);
			GlobalScore.Permutations = count;

			addClass(classes, schedule, idealSchedules, 0, 0, 0);
			Console.Write("\rDone! Best Score: {0}", GlobalScore.CurrentScore);

			return idealSchedules;
		}
		/* Recursively generate a tree using a stack to store the classes, calculate the score,
		 * and store it into the idealSchedules stack. If score of the next is smaller than the
		 * current one in the stack, pop it and push the new one, if it's 0, add it into the
		 * stack.
		 */
		private static void addClass(List<Course>[,] classList, Stack<Course> classes, Stack<Stack<Course>> storage, int currentClass, int currentType, int currentDepth)
		{
			//Base case, all classes filled.
			if (currentClass == 5)
			{
				double progress = ((double)GlobalScore.CurrentIteration+1) / (double)GlobalScore.Permutations * (double)100;
				Console.Write("\rProgress: {0:F2}%", progress);
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
				addClass(classList, classes, storage, currentClass + 1, 0, 0);
			}
			//Still need more types
			else
			{
				if (classList[currentClass, currentType].Count == 0)
				{
					addClass(classList, classes, storage, currentClass, currentType + 1, 0);
				}
				else
				{
					for (int i = 0; i < classList[currentClass, currentType].Count; i++)
					{
						dynamic newClass = classList[currentClass, currentType][i];
						classes.Push(newClass);
						addClass(classList, classes, storage, currentClass, currentType + 1, i);
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
					if (!classCheck(checkClass1, checkClass2))
					{
						conflictScore += conflictValue;
					}
				}
			}
			return conflictScore;
		}
	}
}