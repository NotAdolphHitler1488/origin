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
using DiplomErshov.PageFolder.EmployeePageFolder.ComputersFolder;

namespace DiplomErshov.PageFolder.EmployeePageFolder.OfficeStorageFolder
{
    /// <summary>
    /// Логика взаимодействия для OfficeStorageListPage.xaml
    /// </summary>
    public partial class OfficeStorageListPage : Page
    {
        public OfficeStorageListPage()
        {
            InitializeComponent();
            ListStorageDG.ItemsSource = DBEntities.GetContext().OfficeStorage.ToList()
                .OrderBy(c => c.IdOfficeStorage);
        }

        private void ListComputerMouseBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageFolder.EmployeePageFolder.PeripheryFolder.ComputerMouseFolder.ComputerMouseListPage());
        }

        private void ListGarnitureCoolingBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageFolder.EmployeePageFolder.PeripheryFolder.GarnitureFolder.GarnitureListPage());
        }

        private void ListHeadphonesBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageFolder.EmployeePageFolder.PeripheryFolder.HeadphonesFolder.HeadphonesListPage());
        }

        private void ListKeyboardBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageFolder.EmployeePageFolder.PeripheryFolder.KeyboardFolder.KeyboardListPage());
        }

        private void ListMicrophoneBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageFolder.EmployeePageFolder.PeripheryFolder.MicrophoneFolder.MicrophoneListPage());
        }

        private void ListMonitorBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageFolder.EmployeePageFolder.PeripheryFolder.MonitorFolder.MonitorListPage());
        }

        private void ListPrinterBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageFolder.EmployeePageFolder.PeripheryFolder.PrinterFolder.PrinterListPage());
        }

        private void ListScannerBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageFolder.EmployeePageFolder.PeripheryFolder.ScannerFolder.ScannerListPage());
        }

        private void ListWebCameraBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageFolder.EmployeePageFolder.PeripheryFolder.WebCameraFolder.WebCameraListPage());
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            OfficeStorage officestorage = ListStorageDG.SelectedItem as OfficeStorage;
            if (ListStorageDG.SelectedItem == null)
            {
                MBClass.ErrorMB("Выберите рабочее место" +
                    " для удаления");
            }
            else
            {
                if (MBClass.QestionMB("Удалить " +
                    $"рабочее место под номером " +
                    $"{officestorage.IdOfficeStorage}?"))
                {
                    DBEntities.GetContext().OfficeStorage
                        .Remove(ListStorageDG.SelectedItem as OfficeStorage);
                    DBEntities.GetContext().SaveChanges();

                    MBClass.InformationMB("Рабочее место удалено");
                    ListStorageDG.ItemsSource = DBEntities.GetContext()
                        .OfficeStorage.ToList().OrderBy(u => u.IdOfficeStorage);
                }
            }
        }

        private void Red_Click(object sender, RoutedEventArgs e)
        {
            if (ListStorageDG.SelectedItem == null)
            {
                MBClass.ErrorMB("Выберите " +
                    "рабочее место для редактирования");
            }
            else
            {
                NavigationService.Navigate(
                    new OfficeStorageEditPage(ListStorageDG.SelectedItem as OfficeStorage));
            }
        }

        private void Plus_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new PageFolder.EmployeePageFolder.OfficeStorageFolder.OfficeStorageAddPage());
        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            ListStorageDG.ItemsSource = DBEntities.GetContext()
                .OfficeStorage.Where(u => u.IdComputer.ToString()
                .StartsWith(SearchTb.Text))
                .ToList().OrderBy(u => u.IdComputer);
        }
    }
}
