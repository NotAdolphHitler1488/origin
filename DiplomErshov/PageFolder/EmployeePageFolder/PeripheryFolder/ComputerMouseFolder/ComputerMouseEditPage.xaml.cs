using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
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
using DiplomErshov.PageFolder.EmployeePageFolder.ComputerComponentsFolder.CPUFolder;


namespace DiplomErshov.PageFolder.EmployeePageFolder.PeripheryFolder.ComputerMouseFolder
{
    /// <summary>
    /// Логика взаимодействия для ComputerMouseEditPage.xaml
    /// </summary>
    public partial class ComputerMouseEditPage : Page
    {
        string saveSerial = "";
        private ComputerMouse originalCM;
        public ComputerMouseEditPage(ComputerMouse computermouse)
        {
            InitializeComponent();
            DBEntities.nullContext();
            DBEntities.nullContext(); originalCM = DBEntities.GetContext().ComputerMouse
                .FirstOrDefault(u => u.IdComputerMouse == computermouse.IdComputerMouse);
            DataContext = computermouse;
            this.originalCM.IdComputerMouse = computermouse.IdComputerMouse;
            SerialTB.Text = saveSerial = computermouse.SerialNumberComputerMouse;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            var checkSerialNumberCM = DBEntities.GetContext()
                            .ComputerMouse.FirstOrDefault(u => u.SerialNumberComputerMouse == SerialTB.Text);
            if (checkSerialNumberCM != null && saveSerial != SerialTB.Text)
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
                    originalCM = DBEntities.GetContext().ComputerMouse
                        .FirstOrDefault(u => u.IdComputerMouse == originalCM.IdComputerMouse);
                    originalCM.NameComputerMouse = NameTB.Text;
                    originalCM.SerialNumberComputerMouse = SerialTB.Text;
                    originalCM.GuaranteeComputerMouse = Convert.ToDateTime(DateDP.SelectedDate);
                    DBEntities.GetContext().SaveChanges();
                    MBClass.InformationMB("Данные успешно отредактированы");
                    NavigationService.Navigate(new ComputerMouseListPage());
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
            NavigationService.Navigate(new PageFolder.EmployeePageFolder.PeripheryFolder.ComputerMouseFolder.ComputerMouseListPage());
        }
    }
}
