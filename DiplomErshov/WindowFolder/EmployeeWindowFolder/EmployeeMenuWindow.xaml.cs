using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DiplomErshov.ClassFolder;
using DiplomErshov.PageFolder.AdministratorPageFolder;
using DiplomErshov.PageFolder.EmployeePageFolder.ComputersFolder;
using DiplomErshov.PageFolder.EmployeePageFolder.OfficeStorageFolder;

namespace DiplomErshov.WindowFolder.EmployeeWindowFolder
{
    /// <summary>
    /// Логика взаимодействия для EmployeeMenuWindow.xaml
    /// </summary>
    public partial class EmployeeMenuWindow : Window
    {
        public EmployeeMenuWindow()
        {
            InitializeComponent();
            MaiFrame.Navigate(new PageFolder.EmployeePageFolder.OfficeStorageFolder.OfficeStorageListPage());
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            {
                this.BeginAnimation(Window.HeightProperty, null);
                this.BeginAnimation(Window.WidthProperty, null);
                this.BeginAnimation(Window.OpacityProperty, null);
                this.Height = 750;
                this.Width = 1000;
                this.Opacity = 1;
            }
        }

        private void BorderTop_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Minimize_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Storyboard minimizeStoryboard = (Storyboard)FindResource("MinimizeStoryboard");
            minimizeStoryboard.Completed += MinimizeStoryboard_Completed;
            minimizeStoryboard.Begin(this);
        }

        private void MinimizeStoryboard_Completed(object sender, EventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Close_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MBClass.ExitMB();
        }

        private void ListComp_Click(object sender, RoutedEventArgs e)
        {
            MaiFrame.Navigate(new ComputersListPage());
        }

        private void ListPer_Click(object sender, RoutedEventArgs e)
        {
            MaiFrame.Navigate(new OfficeStorageListPage());
        }

        private void ChangeUser_Click(object sender, RoutedEventArgs e)
        {
            if (MBClass.QestionMB("Вы действительно хотите выйти из аккаунта?"))
            {
                new AuthorizationWindow().Show();
                Close();
            }
        }
    }
}
