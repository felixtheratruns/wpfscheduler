using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace DataExperiment
{
    public class Schedule
    {
        private static ObservableCollection<Course> _available = new ObservableCollection<Course>();
        private static ObservableCollection<Course> _courses = new ObservableCollection<Course>();

        public static ObservableCollection<Course> Courses
        {
            get
            {
                return _courses;
            }
            set
            {
                _courses = value;
            }
        }

        public static ObservableCollection<Course> Available
        {
            get
            {
                return _available;
            }
            set
            {
                _available = value;
            }
        }

        public static void AddCourse( Course course )
        {
            _courses.Add( course );
        }

        public static int TotalHours
        {
            get
            {
                int hours = 0;
                foreach ( Course course in _courses )
                {
                    hours += course.Hours;
                }
                return hours;
            }
        }

    }
}
