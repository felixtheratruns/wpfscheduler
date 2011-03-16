using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Collections.ObjectModel;

namespace DataExperiment
{
    public class Catalog 
    {
        static ObservableCollection<Course> _courseList = new ObservableCollection<Course>();

        public static ObservableCollection<Course> CourseList 
        {
            get 
            {
                return _courseList;
            }
            set 
            {
                _courseList = value;
            }
        }
    }
}
