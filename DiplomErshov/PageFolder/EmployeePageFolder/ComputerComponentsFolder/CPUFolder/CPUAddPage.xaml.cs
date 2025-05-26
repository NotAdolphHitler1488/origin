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
    /// Логика взаимодействия для CPUAddPage.xaml
    /// </summary>
    public partial class CPUAddPage : Page
    {
        public CPUAddPage()
        {
            InitializeComponent();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            var checkSerialNumberCPU = DBEntities.GetContext()
                .CPU.FirstOrDefault(u => u.SerialNumberCPU == SerialTB.Text);

            if (checkSerialNumberCPU != null)
            {
                MBClass.ErrorMB("Такой серийный номер уже существует");
                SerialTB.Focus();
            }

            else if (string.IsNullOrWhiteSpace(SerialTB.Text))
            {
                MBClass.ErrorMB("Пожалуйста, введите серийный номер");
                SerialTB.Focus();
            }

            else if (string.IsNullOrWhiteSpace(SocketTB.Text))
            {
                MBClass.ErrorMB("Пожалуйста, введите сокет");
                NameTB.Focus();
            }

            else if (string.IsNullOrWhiteSpace(NameTB.Text))
            {
                MBClass.ErrorMB("Пожалуйста, введите название");
                NameTB.Focus();
            }

            else
            {
                try
                {
                    DBEntities.GetContext().CPU.Add(new CPU()
                    {
                        NameCPU = NameTB.Text,
                        SocketCPU = SocketTB.Text,
                        SerialNumberCPU = SerialTB.Text,
                    });
                    DBEntities.GetContext().SaveChanges();
                    MBClass.InformationMB("Успешно");
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
