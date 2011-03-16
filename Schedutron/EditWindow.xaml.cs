using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DataExperiment
{
    /// <summary>
    /// Interaction logic for EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        private int _itemIndex;

        public EditWindow(int index)
        {
            InitializeComponent();
            _itemIndex = index;
            Course course = Catalog.CourseList.ElementAt( _itemIndex );
            CourseNumText.Text = course.Name;
            CourseNameText.Text = course.LongName;
            //DaysText.Text
            StartText.Text = course.Times.ElementAt( 0 ).Start.ToString("HH:mm");
            EndText.Text = course.Times.ElementAt( 0 ).End.ToString("HH:mm");
            HoursText.Text = course.Hours.ToString();

        }

        private void OkButton_Click( object sender, RoutedEventArgs e )
        {
            Course course = Catalog.CourseList.ElementAt( _itemIndex );
            /*Catalog.CourseList.ElementAt( _itemIndex ).Name = CourseNumText.Text;
            Catalog.CourseList.ElementAt( _itemIndex ).LongName = CourseNameText.Text;
            // TODO: Daycode conversion
            // TODO: Support for multiple time blocks
            Catalog.CourseList.ElementAt( _itemIndex ).Times.ElementAt( 0 ).Start = DateTime.ParseExact( StartText.Text, "HH:mm", null);
            Catalog.CourseList.ElementAt( _itemIndex ).Times.ElementAt( 0 ).End = DateTime.ParseExact( EndText.Text, "HH:mm", null );
            Catalog.CourseList.ElementAt( _itemIndex ).Hours = Int32.Parse(HoursText.Text);
            this.Close();*/
            course.Name = CourseNumText.Text;
            course.LongName = CourseNameText.Text;
            foreach (TimeBlock time in course.Times)
            {
                System.Console.WriteLine( time.ToString() );
            }
            TimeBlock timep = course.Times.ElementAt( 0 );
            timep.Start = DateTime.ParseExact( StartText.Text, "HH:mm", null );
            timep.End = DateTime.ParseExact( EndText.Text, "HH:mm", null );
            System.Console.WriteLine( timep );
            Schedule.Courses.Clear();
            Schedule.Available = Catalog.CourseList;
            this.Close();
        }

        private void CancelButton_Click( object sender, RoutedEventArgs e )
        {
            this.Close();
        }
    }
}
