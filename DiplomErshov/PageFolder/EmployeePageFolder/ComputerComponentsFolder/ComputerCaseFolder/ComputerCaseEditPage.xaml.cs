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
using DiplomErshov.PageFolder.EmployeePageFolder.ComputerComponentsFolder.GPUFolder;

namespace DiplomErshov.PageFolder.EmployeePageFolder.ComputerComponentsFolder.ComputerCaseFolder
{
    /// <summary>
    /// Логика взаимодействия для ComputerCaseEditPage.xaml
    /// </summary>
    public partial class ComputerCaseEditPage : Page
    {
        string saveSerial = "";
        private ComputerCase originalComputerCase;
        public ComputerCaseEditPage(ComputerCase computercase)
        {
            InitializeComponent();
            DBEntities.nullContext();
            DBEntities.nullContext(); originalComputerCase = DBEntities.GetContext().ComputerCase
                .FirstOrDefault(u => u.IdComputerCase == computercase.IdComputerCase);
            DataContext = computercase;
            this.originalComputerCase.IdComputerCase = originalComputerCase.IdComputerCase;
            SerialTB.Text = saveSerial = computercase.SerialNumberComputerCase;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            var checkSerialNumberCase = DBEntities.GetContext()
                            .ComputerCase.FirstOrDefault(u => u.SerialNumberComputerCase == SerialTB.Text);
            if (checkSerialNumberCase != null && saveSerial != SerialTB.Text)
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
                    originalComputerCase = DBEntities.GetContext().ComputerCase
                        .FirstOrDefault(u => u.IdComputerCase == originalComputerCase.IdComputerCase);
                    originalComputerCase.NameComputerCase = NameTB.Text;
                    originalComputerCase.SerialNumberComputerCase = SerialTB.Text;
                    DBEntities.GetContext().SaveChanges();
                    MBClass.InformationMB("Данные успешно отредактированы");
                    NavigationService.Navigate(new ComputerCaseListPage());
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
            NavigationService.Navigate(new PageFolder.EmployeePageFolder.ComputerComponentsFolder.ComputerCaseFolder.ComputerCaseListPage());
        }
    }
}
