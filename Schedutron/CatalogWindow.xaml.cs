using System;
using System.Collections.Generic;
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
    /// Interaction logic for CatalogWindow.xaml
    /// </summary>
    public partial class CatalogWindow : Window
    {
        private AddWindow _addWindow;
        private EditWindow _editWindow;

        public CatalogWindow()
        {
            InitializeComponent();
            System.Console.WriteLine("Children: " + LogicalTreeHelper.GetChildren( this ));
        }

        private void AddButton_Click( object sender, RoutedEventArgs e )
        {
            if ((_addWindow == null) || (_addWindow.IsLoaded == false))
            {
                _addWindow = new AddWindow();
                _addWindow.Show();
            }
            else
            {
                _addWindow.Focus();
            }
        }

        private void RemoveButton_Click( object sender, RoutedEventArgs e )
        {
            int index = CatalogList.SelectedIndex;
            if (index != -1)
            {
                Catalog.CourseList.RemoveAt( index );
            }
            
            
        }

        private void Window_Loaded( object sender, RoutedEventArgs e )
        {

            System.Windows.Data.ObjectDataProvider catalogViewSource = ((System.Windows.Data.ObjectDataProvider)(this.FindResource( "catalogViewSource" )));
            // Load data by setting the CollectionViewSource.Source property:
            // catalogViewSource.Source = [generic data source]
        }

        private void EditButton_Click( object sender, RoutedEventArgs e )
        {
            int index = CatalogList.SelectedIndex;
            if (index != -1)
            {
                if ((_editWindow == null) || (_editWindow.IsLoaded == false))
                {
                    _editWindow = new EditWindow( CatalogList.SelectedIndex );
                    _editWindow.Show();
                }
                else
                {
                    _editWindow.Focus();
                }

            }
        }
    }
}
