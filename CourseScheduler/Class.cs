using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseSchedulerClass
{
	class Class
	{
		internal string className;
		internal int startTime;
		internal int endTime;
		internal int duration;
		internal int classSection;
		internal string location;
		internal string day;

		public Class()
		{
			//Do nothing
		}

		public Class(string _className, int _classSection)
		{
			className = _className;
			classSection = _classSection;
		}
		public Class(string _className, int _startTime, int _endTime, string _location, int _classSection, string _day)
		{
			className = _className;
			startTime = _startTime;
			endTime = _endTime;
			duration = _endTime - _startTime;
			location = _location;
			classSection = _classSection;
			day = _day;
		}

		public Class Clone()
		{
			Class newClass = new Class();
			newClass.className = className;
			newClass.startTime = startTime;
			newClass.endTime = endTime;
			newClass.duration = duration;
			newClass.classSection = classSection;
			newClass.location = location;
			newClass.day = day;
			return newClass;
		}

		~Class()
		{
			//Nothing to do here~
		}
	}

	class Course
	{
		internal string courseName;
		internal int sessionsPerWeek;
		internal string courseType;
		internal int courseSection;
		internal List<Class> lectures;

		public Course()
		{

		}

		public Course(string _courseName, int _sessionsPerWeek, string[] _sessionDays, int _courseSection,
			int[] _startTime, int[] _endTime, string[] _location, string _classType)
		{
			courseName = _courseName;
			courseSection = _courseSection;
			courseType = _classType;
			lectures = new List<Class>(_sessionsPerWeek);
			sessionsPerWeek = _sessionsPerWeek;
			for (int i = 0; i < sessionsPerWeek; i++)
			{
				Class newClass = new Class(_courseName, _startTime[i], _endTime[i], _location[i], _courseSection, _sessionDays[i]);
				lectures.Add(newClass);
			}
		}

		public Course Clone()
		{
			Course courseCopy = new Course();

			courseCopy.courseName = courseName;
			courseCopy.sessionsPerWeek = sessionsPerWeek;
			courseCopy.courseType = courseType;
			courseCopy.courseSection = courseSection;
			courseCopy.lectures = new List<Class>(sessionsPerWeek);
			for (int i = 0; i < sessionsPerWeek; i++)
			{
				Class newClass = new Class();
				newClass = lectures[i].Clone();
				courseCopy.lectures.Add(newClass);
			}

			return courseCopy;
		}

		~Course()
		{
			lectures.Clear();
		}
	}
}
