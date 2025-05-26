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
using DiplomErshov.PageFolder.EmployeePageFolder.ComputerComponentsFolder.HDDFolder;


namespace DiplomErshov.PageFolder.EmployeePageFolder.ComputerComponentsFolder.CPUCoolingFolder
{
    /// <summary>
    /// Логика взаимодействия для CPUCoolingEditPage.xaml
    /// </summary>
    public partial class CPUCoolingEditPage : Page
    {
        string saveSerial = "";
        private CPUСooling originalCPUСooling;
        public CPUCoolingEditPage(CPUСooling cpucooling)
        {
            InitializeComponent();
            DBEntities.nullContext();
            DBEntities.nullContext(); originalCPUСooling = DBEntities.GetContext().CPUСooling
                .FirstOrDefault(u => u.IdCPUСooling == cpucooling.IdCPUСooling);
            DataContext = cpucooling;
            this.originalCPUСooling.IdCPUСooling = cpucooling.IdCPUСooling;
            TypeCb.ItemsSource = DBEntities.GetContext()
                .TypeOfCPUСooling.ToList();
            SerialTB.Text = saveSerial = cpucooling.SerialNumberCPUCooling;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            var checkSerialNumberPC = DBEntities.GetContext()
                            .CPUСooling.FirstOrDefault(u => u.SerialNumberCPUCooling == SerialTB.Text);
            if (checkSerialNumberPC != null && saveSerial != SerialTB.Text)
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
                    originalCPUСooling = DBEntities.GetContext().CPUСooling
                        .FirstOrDefault(u => u.IdCPUСooling == originalCPUСooling.IdCPUСooling);
                    originalCPUСooling.NameCPUСooling = NameTB.Text;
                    originalCPUСooling.IdTypeOfCPUСooling = Int32.Parse(
                        TypeCb.SelectedValue.ToString());
                    originalCPUСooling.SerialNumberCPUCooling = SerialTB.Text;
                    DBEntities.GetContext().SaveChanges();
                    MBClass.InformationMB("Данные успешно отредактированы");
                    NavigationService.Navigate(new CPUCoolingListPage());
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
            NavigationService.Navigate(new PageFolder.EmployeePageFolder.ComputerComponentsFolder.CPUCoolingFolder.CPUCoolingListPage());
        }
    }
}
