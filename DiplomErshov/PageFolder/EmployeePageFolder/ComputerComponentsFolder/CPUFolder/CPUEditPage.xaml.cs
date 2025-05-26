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


namespace DiplomErshov.PageFolder.EmployeePageFolder.ComputerComponentsFolder.CPUFolder
{
    /// <summary>
    /// Логика взаимодействия для CPUEditPage.xaml
    /// </summary>
    public partial class CPUEditPage : Page
    {
        string saveSerial = "";
        private CPU originalCPU;
        public CPUEditPage(CPU cpu)
        {
            InitializeComponent();
            DBEntities.nullContext();
            DBEntities.nullContext(); originalCPU = DBEntities.GetContext().CPU
                .FirstOrDefault(u => u.IdCPU == cpu.IdCPU);
            DataContext = cpu;
            this.originalCPU.IdCPU = cpu.IdCPU;
            SerialTB.Text = saveSerial = cpu.SerialNumberCPU;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            var checkSerialNumberCPU = DBEntities.GetContext()
                            .CPU.FirstOrDefault(u => u.SerialNumberCPU == SerialTB.Text);
            if (checkSerialNumberCPU != null && saveSerial != SerialTB.Text)
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
                    originalCPU = DBEntities.GetContext().CPU
                        .FirstOrDefault(u => u.IdCPU == originalCPU.IdCPU);
                    originalCPU.NameCPU = NameTB.Text;
                    originalCPU.SocketCPU = SocketTB.Text;
                    originalCPU.SerialNumberCPU = SerialTB.Text;
                    DBEntities.GetContext().SaveChanges();
                    MBClass.InformationMB("Данные успешно отредактированы");
                    NavigationService.Navigate(new CPUListPage());
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
            NavigationService.Navigate(new PageFolder.EmployeePageFolder.ComputerComponentsFolder.CPUFolder.CPUListPage());
        }
    }
}
