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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DiplomErshov.ClassFolder;
using DiplomErshov.DataFolder;
using DiplomErshov.PageFolder.EmployeePageFolder.PeripheryFolder.ScannerFolder;

namespace DiplomErshov.PageFolder.EmployeePageFolder.PeripheryFolder.WebCameraFolder
{
    /// <summary>
    /// Логика взаимодействия для WebCameraListPage.xaml
    /// </summary>
    public partial class WebCameraListPage : Page
    {
        public WebCameraListPage()
        {
            InitializeComponent();
            ListWebDG.ItemsSource = DBEntities.GetContext().WebCamera.ToList()
                .OrderBy(c => c.IdWebCamera);
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            WebCamera webCamera = ListWebDG.SelectedItem as WebCamera;
            if (ListWebDG.SelectedItem == null)
            {
                MBClass.ErrorMB("Выберите вебкамеру" +
                    " для удаления");
            }
            else
            {
                if (MBClass.QestionMB("Удалить " +
                    $"вебкамеру под названием " +
                    $"{webCamera.NameWebCamera}?"))
                {
                    DBEntities.GetContext().WebCamera
                        .Remove(ListWebDG.SelectedItem as WebCamera);
                    DBEntities.GetContext().SaveChanges();

                    MBClass.InformationMB("Вебкамера удалена");
                    ListWebDG.ItemsSource = DBEntities.GetContext()
                        .WebCamera.ToList().OrderBy(u => u.NameWebCamera);
                }
            }
        }

        private void Red_Click(object sender, RoutedEventArgs e)
        {
            if (ListWebDG.SelectedItem == null)
            {
                MBClass.ErrorMB("Выберите " +
                    "вебкамеру для редактирования");
            }
            else
            {
                NavigationService.Navigate(
                    new WebCameraEditPage(ListWebDG.SelectedItem as WebCamera));
            }
        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            ListWebDG.ItemsSource = DBEntities.GetContext()
                .WebCamera.Where(u => u.NameWebCamera.StartsWith(SearchTb.Text))
                .ToList().OrderBy(u => u.NameWebCamera);
        }

        private void Plus_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new PageFolder.EmployeePageFolder.PeripheryFolder.WebCameraFolder.WebCameraAddPage());
        }
    }
}
