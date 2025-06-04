using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DiplomErshov.DataFolder;
using Microsoft.Win32;
using System.IO;

namespace DiplomErshov.PageFolder.DirectorPageFolder
{
    public partial class DirectorDetailsPage : Page
    {
        private Staff displayedStaff;

        public DirectorDetailsPage(Staff staff)
        {
            InitializeComponent();

            DBEntities.nullContext();
            displayedStaff = DBEntities.GetContext()
                                      .Staff
                                      .FirstOrDefault(u => u.IdStaff == staff.IdStaff);
            DataContext = displayedStaff;
        }

        private void Back_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new DirectorListPage());
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DirectorEditPage(displayedStaff));
        }
    }
}
