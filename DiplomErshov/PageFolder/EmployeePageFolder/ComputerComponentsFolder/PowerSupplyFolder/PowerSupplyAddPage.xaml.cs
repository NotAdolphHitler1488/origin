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
using DiplomErshov.PageFolder.EmployeePageFolder.ComputerComponentsFolder.SSDFolder;


namespace DiplomErshov.PageFolder.EmployeePageFolder.ComputerComponentsFolder.PowerSupplyFolder
{
    /// <summary>
    /// Логика взаимодействия для PowerSupplyAddPage.xaml
    /// </summary>
    public partial class PowerSupplyAddPage : Page
    {
        public PowerSupplyAddPage()
        {
            InitializeComponent();
            WattageCb.ItemsSource = DBEntities.GetContext()
                .Wattage.ToList();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            var checkSerialNumberPowerSupply = DBEntities.GetContext()
                .PowerSupply.FirstOrDefault(u => u.SerialNumberPowerSupply == SerialTB.Text);

            if (checkSerialNumberPowerSupply != null)
            {
                MBClass.ErrorMB("Такой серийный номер уже существует");
                SerialTB.Focus();
            }

            else if (string.IsNullOrWhiteSpace(WattageCb.Text))
            {
                MBClass.ErrorMB("Пожалуйста, выберите вольтаж");
                WattageCb.Focus();
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
                    DBEntities.GetContext().PowerSupply.Add(new PowerSupply()
                    {
                        NamePowerSupply = NameTB.Text,
                        IdWattage = Int32.Parse(WattageCb.SelectedValue.ToString()),
                        SerialNumberPowerSupply = SerialTB.Text,
                    });
                    DBEntities.GetContext().SaveChanges();
                    MBClass.InformationMB("Успешно");
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
