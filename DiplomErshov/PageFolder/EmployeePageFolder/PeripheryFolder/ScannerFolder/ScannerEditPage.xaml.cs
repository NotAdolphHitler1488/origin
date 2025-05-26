using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
    /// Логика взаимодействия для ScannerEditPage.xaml
    /// </summary>
    public partial class ScannerEditPage : Page
    {
        string saveSerial = "";
        private Scanner originalScanner;
        public ScannerEditPage(Scanner scanner)
        {
            InitializeComponent();
            DBEntities.nullContext();
            DBEntities.nullContext(); originalScanner = DBEntities.GetContext().Scanner
                .FirstOrDefault(u => u.IdScanner == scanner.IdScanner);
            DataContext = scanner;
            this.originalScanner.IdScanner = scanner.IdScanner;
            SerialTB.Text = saveSerial = scanner.SerialNumberScanner;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            var checkSerialScanner = DBEntities.GetContext()
                            .Scanner.FirstOrDefault(u => u.SerialNumberScanner == SerialTB.Text);
            if (checkSerialScanner != null && saveSerial != SerialTB.Text)
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
                    originalScanner = DBEntities.GetContext().Scanner
                        .FirstOrDefault(u => u.IdScanner == originalScanner.IdScanner);
                    originalScanner.NameScanner = NameTB.Text;
                    originalScanner.SerialNumberScanner = SerialTB.Text;
                    originalScanner.GuaranteeScanner = Convert.ToDateTime(DateDP.SelectedDate);
                    DBEntities.GetContext().SaveChanges();
                    MBClass.InformationMB("Данные успешно отредактированы");
                    NavigationService.Navigate(new ScannerListPage());
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
            NavigationService.Navigate(new PageFolder.EmployeePageFolder.PeripheryFolder.ScannerFolder.ScannerListPage());
        }
    }
}
