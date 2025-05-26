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
using DiplomErshov.PageFolder.EmployeePageFolder.PeripheryFolder.PrinterFolder;

namespace DiplomErshov.PageFolder.EmployeePageFolder.PeripheryFolder.ScannerFolder
{
    /// <summary>
    /// Логика взаимодействия для ScannerListPage.xaml
    /// </summary>
    public partial class ScannerListPage : Page
    {
        public ScannerListPage()
        {
            InitializeComponent();
            ListScannerDG.ItemsSource = DBEntities.GetContext().Scanner.ToList()
                .OrderBy(c => c.IdScanner);
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            Scanner scanner = ListScannerDG.SelectedItem as Scanner;
            if (ListScannerDG.SelectedItem == null)
            {
                MBClass.ErrorMB("Выберите сканнер" +
                    " для удаления");
            }
            else
            {
                if (MBClass.QestionMB("Удалить " +
                    $"сканнер под названием " +
                    $"{scanner.NameScanner}?"))
                {
                    DBEntities.GetContext().Scanner
                        .Remove(ListScannerDG.SelectedItem as Scanner);
                    DBEntities.GetContext().SaveChanges();

                    MBClass.InformationMB("Сканер удален");
                    ListScannerDG.ItemsSource = DBEntities.GetContext()
                        .Scanner.ToList().OrderBy(u => u.NameScanner);
                }
            }
        }

        private void Red_Click(object sender, RoutedEventArgs e)
        {
            if (ListScannerDG.SelectedItem == null)
            {
                MBClass.ErrorMB("Выберите " +
                    "сканер для редактирования");
            }
            else
            {
                NavigationService.Navigate(
                    new ScannerEditPage(ListScannerDG.SelectedItem as Scanner));
            }
        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            ListScannerDG.ItemsSource = DBEntities.GetContext()
                .Scanner.Where(u => u.NameScanner.StartsWith(SearchTb.Text))
                .ToList().OrderBy(u => u.NameScanner);
        }

        private void Plus_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new PageFolder.EmployeePageFolder.PeripheryFolder.ScannerFolder.ScannerAddPage());
        }
    }
}
