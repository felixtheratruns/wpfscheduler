using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DataExperiment
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        public AddWindow()
        {
            InitializeComponent();
        }

        private void CancelButton_Click( object sender, RoutedEventArgs e )
        {
            this.Close();
        }

        private void OkButton_Click( object sender, RoutedEventArgs e )
        {
            String name = CourseNumText.Text;
            String longname = CourseNameText.Text;
            String dayCodes = DaysText.Text;
            String start = StartText.Text;
            String end = EndText.Text;
            int hours = Int32.Parse(HoursText.Text);
            Course course = new Course( name, longname, dayCodes, start, end, hours );
            Catalog.CourseList.Add( course );
            Schedule.Courses.Clear();
            Schedule.Available = Catalog.CourseList;
            this.Close();
        }
    }
}
