using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Логика взаимодействия для PrinterEditPage.xaml
    /// </summary>
    public partial class PrinterEditPage : Page
    {
        string saveSerial = "";
        private Printer originalPrinter;
        public PrinterEditPage(Printer printer)
        {
            InitializeComponent();
            DBEntities.nullContext();
            DBEntities.nullContext(); originalPrinter = DBEntities.GetContext().Printer
                .FirstOrDefault(u => u.IdPrinter == printer.IdPrinter);
            DataContext = printer;
            this.originalPrinter.IdPrinter = printer.IdPrinter;
            SerialTB.Text = saveSerial = printer.SerialNumberPrinter;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            var checkSerialPrinter = DBEntities.GetContext()
                            .Printer.FirstOrDefault(u => u.SerialNumberPrinter == SerialTB.Text);
            if (checkSerialPrinter != null && saveSerial != SerialTB.Text)
            {
                MBClass.ErrorMB("Такой серийный номер уже существует");
                SerialTB.Focus();
                return;
            }

            else if (string.IsNullOrWhiteSpace(SerialTB.Text))
            {
                MBClass.ErrorMB("Пожалуйста, введите серийный номер");
                SerialTB.Focus();
            }

            else
            {
                try
                {
                    originalPrinter = DBEntities.GetContext().Printer
                        .FirstOrDefault(u => u.IdPrinter == originalPrinter.IdPrinter);
                    originalPrinter.NamePrinter = NameTB.Text;
                    originalPrinter.SerialNumberPrinter = SerialTB.Text;
                    originalPrinter.GuaranteePrinter = Convert.ToDateTime(DateDP.SelectedDate);
                    DBEntities.GetContext().SaveChanges();
                    MBClass.InformationMB("Данные успешно отредактированы");
                    NavigationService.Navigate(new PrinterListPage());
                }
                catch (Exception ex)
                {
                    MBClass.ErrorMB(ex);
                    throw;
                }
            }
        }

        private void Back_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new PageFolder.EmployeePageFolder.PeripheryFolder.PrinterFolder.PrinterListPage());
        }
    }
}
