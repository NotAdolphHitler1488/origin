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
using DiplomErshov.PageFolder.EmployeePageFolder.ComputerComponentsFolder.RAMFolder;


namespace DiplomErshov.PageFolder.EmployeePageFolder.ComputerComponentsFolder.PowerSupplyFolder
{
    /// <summary>
    /// Логика взаимодействия для PowerSupplyEditPage.xaml
    /// </summary>
    public partial class PowerSupplyEditPage : Page
    {
        string saveSerial = "";
        private PowerSupply originalPowerSupply;
        public PowerSupplyEditPage(PowerSupply powersupply)
        {
            InitializeComponent();
            DBEntities.nullContext();
            DBEntities.nullContext(); originalPowerSupply = DBEntities.GetContext().PowerSupply
                .FirstOrDefault(u => u.IdPowerSupply == powersupply.IdPowerSupply);
            DataContext = powersupply;
            this.originalPowerSupply.IdPowerSupply = powersupply.IdPowerSupply;
            WattageCb.ItemsSource = DBEntities.GetContext()
                .Wattage.ToList();
            SerialTB.Text = saveSerial = powersupply.SerialNumberPowerSupply;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            var checkSerialNumberPS = DBEntities.GetContext()
                            .PowerSupply.FirstOrDefault(u => u.SerialNumberPowerSupply == SerialTB.Text);
            if (checkSerialNumberPS != null && saveSerial != SerialTB.Text)
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
                    originalPowerSupply = DBEntities.GetContext().PowerSupply
                        .FirstOrDefault(u => u.IdPowerSupply == originalPowerSupply.IdPowerSupply);
                    originalPowerSupply.NamePowerSupply = NameTB.Text;
                    originalPowerSupply.IdWattage = Int32.Parse(
                        WattageCb.SelectedValue.ToString());
                    originalPowerSupply.SerialNumberPowerSupply = SerialTB.Text;
                    DBEntities.GetContext().SaveChanges();
                    MBClass.InformationMB("Данные успешно отредактированы");
                    NavigationService.Navigate(new PowerSupplyListPage());
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
            NavigationService.Navigate(new PageFolder.EmployeePageFolder.ComputerComponentsFolder.PowerSupplyFolder.PowerSupplyListPage());
        }
    }
}
