using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataExperiment
{
    public class Course
    {
        private String _name;
        private String _longname;
        private int _hours;
        private List<TimeBlock> _times;

        public Course( String name, String longname, String dayCodes, String start, String end, int hours )
        {
            _name = name;
            _longname = longname;
            char[] daysArray = dayCodes.ToCharArray();
            _times = new List<TimeBlock>();
            foreach ( char day in daysArray )
            {
                TimeBlock time = new TimeBlock( day, start, end );
                _times.Add( time );
            }
        }

        public List<TimeBlock> Times
        {
            get
            {
                return _times;
            }
            set
            {
                _times = value;
            }
        }

        public String Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public String LongName
        {
            get
            {
                return _longname;
            }
            set
            {
                _longname = value;
            }
        }

        public int Hours
        {
            get
            {
                return _hours;
            }
            set
            {
                _hours = value;
            }
        }

        public Boolean ConflictsWith(Course course)
        {
            foreach (TimeBlock timeA in _times)
            {
                foreach (TimeBlock timeB in course.Times)
                {
                    if ( timeA.ConflictsWith( timeB ) ) {
                        return true;
                    }
                }
            }
        return false;
        }

        public Boolean ConflictsWith( TimeBlock time )
        {
            foreach (TimeBlock timeA in _times)
            {
                if (time.ConflictsWith( timeA ))
                {
                    return true;
                }
            }
            return false;
        }

        public String DisplayName
        {
            get
            {
                return ToString();
            }
            set
            {
                Name = value;
            }
        }

        public override String ToString()
        {
            return _name + " | " + _longname;
        }
    }
}
