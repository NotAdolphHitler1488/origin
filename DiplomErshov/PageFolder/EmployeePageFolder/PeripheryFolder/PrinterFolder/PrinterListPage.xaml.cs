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
using DiplomErshov.PageFolder.EmployeePageFolder.PeripheryFolder.MonitorFolder;

namespace DiplomErshov.PageFolder.EmployeePageFolder.PeripheryFolder.PrinterFolder
{
    /// <summary>
    /// Логика взаимодействия для PrinterListPage.xaml
    /// </summary>
    public partial class PrinterListPage : Page
    {
        public PrinterListPage()
        {
            InitializeComponent();
            ListPrinterDG.ItemsSource = DBEntities.GetContext().Printer.ToList()
                .OrderBy(c => c.IdPrinter);
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            Printer printer = ListPrinterDG.SelectedItem as Printer;
            if (ListPrinterDG.SelectedItem == null)
            {
                MBClass.ErrorMB("Выберите принтер" +
                    " для удаления");
            }
            else
            {
                if (MBClass.QestionMB("Удалить " +
                    $"принтер под названием " +
                    $"{printer.NamePrinter}?"))
                {
                    DBEntities.GetContext().Printer
                        .Remove(ListPrinterDG.SelectedItem as Printer);
                    DBEntities.GetContext().SaveChanges();

                    MBClass.InformationMB("Принтер удален");
                    ListPrinterDG.ItemsSource = DBEntities.GetContext()
                        .Printer.ToList().OrderBy(u => u.NamePrinter);
                }
            }
        }

        private void Red_Click(object sender, RoutedEventArgs e)
        {
            if (ListPrinterDG.SelectedItem == null)
            {
                MBClass.ErrorMB("Выберите " +
                    "принтер для редактирования");
            }
            else
            {
                NavigationService.Navigate(
                    new PrinterEditPage(ListPrinterDG.SelectedItem as Printer));
            }
        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            ListPrinterDG.ItemsSource = DBEntities.GetContext()
                .Printer.Where(u => u.NamePrinter.StartsWith(SearchTb.Text))
                .ToList().OrderBy(u => u.NamePrinter);
        }

        private void Plus_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new PageFolder.EmployeePageFolder.PeripheryFolder.PrinterFolder.PrinterAddPage());
        }
    }
}
