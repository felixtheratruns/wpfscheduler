using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Resolvers;
using System.Xml;
using System.IO;


namespace DataExperiment
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private static Brush[] colors = { Brushes.DarkKhaki, Brushes.Khaki, Brushes.LavenderBlush, Brushes.LightCoral };

        private Schedule _schedule = new Schedule();
        private DateTime _start = new DateTime();
        private DateTime _end = new DateTime();
        private TimeSpan _interval = new TimeSpan();
        private int _numintervals;
        private int _rowheight;
        
        private CatalogWindow _catalogWindow;

        public static DateTime StringToTime(String timeString)
        {
            // TODO: Refactor into a DateTime handling Class
            DateTime time = DateTime.MinValue;
            try
            {
                time = DateTime.ParseExact( timeString, "HH:mm", null );
            }
            catch (FormatException)
            {
                try
                {
                    time = DateTime.ParseExact( timeString, "H:mm", null );
                }
                catch (FormatException)
                {
                    
                }
            }
            return time;
        }

        private TimeSpan StringToTimeSpan( String timeString )
        {
            TimeSpan time = new TimeSpan();
            try
            {
                time = TimeSpan.ParseExact( timeString, "mm", null );
            }
            catch (FormatException)
            {
                try
                {
                    time = TimeSpan.ParseExact( timeString, "m", null );
                }
                catch (FormatException)
                {
                    try
                    {
                        time = TimeSpan.ParseExact( timeString, @"hh\:mm", null );
                    }
                    catch (FormatException)
                    {
                        try
                        {
                            System.Console.WriteLine( "Attempting H:mm" + timeString );
                            time = TimeSpan.ParseExact( timeString, @"h\:mm", null );
                            System.Console.WriteLine( "TIME: " + time.Ticks );
                        }
                        catch (FormatException)
                        {

                        }
                    }
                }
            }
            return time;
        }
        
        private void GenerateGrid()
        {
            for (int i = 0; i < 6; i++ ) 
            {
                ColumnDefinitionCollection coldefs = ScheduleGrid.ColumnDefinitions;
                coldefs.Add( new ColumnDefinition() );
            }

            DateTime time = _start;
            for (int i = 0; i < _numintervals; i++)
            {
                RowDefinitionCollection rowdefs = ScheduleGrid.RowDefinitions;
                rowdefs.Add( new RowDefinition()
                {
                    MaxHeight=_rowheight,
                    MinHeight=_rowheight,
                });
                Label TimeLabel = new Label()
                {
                    Content = time.ToString( "HH:mm" ),
                };
                Grid.SetRow( TimeLabel, i );
                Grid.SetColumn( TimeLabel, 0 );
                ScheduleGrid.Children.Add( TimeLabel );
                time = time.Add( _interval );
            }
            DrawCourses();
        }

        private void DrawCourses()
        {
            foreach (Course course in Schedule.Courses)
            {
                foreach (TimeBlock time in course.Times)
                {
                    TextBlock CourseLabel = MapBlock( time );
                    CourseLabel.Text = course.Name + "\n" + course.LongName;
                    CourseLabel.TextWrapping = TextWrapping.Wrap;
                    CourseLabel.Background = colors[Schedule.Courses.IndexOf( course )];
                    CourseLabel.Padding = new Thickness( 5, 5, 5, 5 );
                    ScheduleGrid.Children.Add( CourseLabel );
                }
            }
        }

        private TextBlock MapBlock(TimeBlock block)
        {
            String days = "MTWRF";
            long interval = _interval.Ticks;
            long start = block.Start.Ticks - (block.Start.Ticks % interval);
            long end = block.End.Ticks - (block.End.Ticks % interval);
            int row = (int) ((start - _start.Ticks) / interval);
            int rowcount = (int) ((end - start) / interval) + 1;
            int column = days.IndexOf( block.Day ) + 1;
            TextBlock CourseLabel = new TextBlock();
            Grid.SetColumn(CourseLabel, column);
            Grid.SetRow(CourseLabel, row);
            Grid.SetRowSpan( CourseLabel, rowcount );
            return CourseLabel; // Return for further handling.
        }

        private void GenerateList()
        {
            AvailableList.Items.Clear();
            ScheduledList.Items.Clear();
            foreach (Course course in Schedule.Available)
            {
                ListBoxItem item = new ListBoxItem()
                {
                    Content = course.Name + "/" + course.LongName,
                };
                AvailableList.Items.Add( item );
            }
            foreach (Course course in Schedule.Courses)
            {
                ListBoxItem item = new ListBoxItem()
                {
                    Content = course.Name + "/" + course.LongName,
                };
                ScheduledList.Items.Add( item );
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            Catalog.CourseList.Add( new Course( "CECS 310", "Discrete Structures", "MWF", "13:00", "14:15", 3 ) );
            Catalog.CourseList.Add( new Course( "ECE 210", "Logic Design", "MWF", "16:30", "17:45", 3 ) );
            Catalog.CourseList.Add( new Course( "ECE 211", "Logic Design - Lab", "F", "9:30", "12:00", 3 ) );
            Catalog.CourseList.Add( new Course( "IE 360", "Statistics", "TR", "13:00", "14:45", 3 ) );
            Schedule.Available = new ObservableCollection<Course>( Catalog.CourseList );
            // TODO: Link this up to Catalog.
            GenerateList();
        }

        private void Button_Click( object sender, RoutedEventArgs e )
        {
            ScheduleGrid.Children.Clear();
            ScheduleGrid.RowDefinitions.Clear();
            ScheduleGrid.ColumnDefinitions.Clear();

            _start      = StringToTime(StartTime.Text);
            _end        = StringToTime(EndTime.Text);
            _interval   = StringToTimeSpan( Interval.Text );
            _numintervals = (int) ((_end.Ticks - _start.Ticks) / _interval.Ticks) + 2;
            _rowheight = (int) this.Height / _numintervals;
            GenerateGrid();
        }
         
        private void AddButton_Click( object sender, RoutedEventArgs e )
        {
            int index = AvailableList.SelectedIndex;
            if (index == -1) { return; }
            MoveItem( Schedule.Available, Schedule.Courses, index );
        }

        private void RemoveButton_Click( object sender, RoutedEventArgs e )
        {
            int index = ScheduledList.SelectedIndex;
            if (index == -1) { return; }
            MoveItem( Schedule.Courses, Schedule.Available, index );
        }

        private void MoveItem( Collection<Course> from, Collection<Course> to, int index )
        {
            Course course = from.ElementAt( index );
            to.Add( course );
            from.RemoveAt( index );
            GenerateList();
        }

        private void CatalogButton_Click( object sender, RoutedEventArgs e )
        {
            if ((_catalogWindow == null) || (_catalogWindow.IsLoaded == false))
            {
                _catalogWindow = new CatalogWindow();
                _catalogWindow.Show();
            }
            else
            {
                _catalogWindow.Focus();
            }
        }

    }
}
