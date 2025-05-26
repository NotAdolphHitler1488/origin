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
    /// Логика взаимодействия для ScannerAddPage.xaml
    /// </summary>
    public partial class ScannerAddPage : Page
    {
        public ScannerAddPage()
        {
            InitializeComponent();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            var checkSerialNumberScanner = DBEntities.GetContext()
                .Scanner.FirstOrDefault(u => u.SerialNumberScanner == SerialTB.Text);
            if (checkSerialNumberScanner != null)
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

            else if (string.IsNullOrWhiteSpace(NameTB.Text))
            {
                MBClass.ErrorMB("Пожалуйста, введите название");
                NameTB.Focus();
            }

            else if (string.IsNullOrWhiteSpace(DateDP.Text))
            {
                MBClass.ErrorMB("Пожалуйста, выберете дату гарантии");
                DateDP.Focus();
            }
            else
            {
                try
                {
                    DBEntities.GetContext().Scanner.Add(new Scanner()
                    {
                        NameScanner = NameTB.Text,
                        SerialNumberScanner = SerialTB.Text,
                        GuaranteeScanner = Convert.ToDateTime(DateDP.SelectedDate),
                    });
                    DBEntities.GetContext().SaveChanges();
                    MBClass.InformationMB("Успешно");
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
