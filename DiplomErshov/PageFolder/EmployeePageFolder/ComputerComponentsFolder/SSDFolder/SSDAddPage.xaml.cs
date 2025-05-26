using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using System.Web.UI.WebControls;
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
using DiplomErshov.PageFolder.AdministratorPageFolder;
using DiplomErshov.PageFolder.EmployeePageFolder.ComputerComponentsFolder.HDDFolder;

namespace DiplomErshov.PageFolder.EmployeePageFolder.ComputerComponentsFolder.SSDFolder
{
    /// <summary>
    /// Логика взаимодействия для SSDAddPage.xaml
    /// </summary>
    public partial class SSDAddPage : Page
    {
        public SSDAddPage()
        {
            InitializeComponent();
            StorageCb.ItemsSource = DBEntities.GetContext()
                .DigitalStorageCapacitySSD.ToList();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            var checkSerialNumberSSD = DBEntities.GetContext()
                .SSD.FirstOrDefault(u => u.SerialNumberSSD == SerialTB.Text);

            if (checkSerialNumberSSD != null)
            {
                MBClass.ErrorMB("Такой серийный номер уже существует");
                SerialTB.Focus();
            }

            else if (string.IsNullOrWhiteSpace(StorageCb.Text))
            {
                MBClass.ErrorMB("Пожалуйста, выберите объем");
                StorageCb.Focus();
            }

            else if (string.IsNullOrWhiteSpace(NameTB.Text))
            {
                MBClass.ErrorMB("Пожалуйста, введите название");
                NameTB.Focus();
            }
            else if (string.IsNullOrWhiteSpace(QuantityTB.Text))
            {
                MBClass.ErrorMB("Пожалуйста, введите количество SSD");
                QuantityTB.Focus();
            }
            else
            {
                try
                {
                    DBEntities.GetContext().SSD.Add(new SSD()
                    {
                        NameSSD = NameTB.Text,
                        IdDigitalStorageCapacitySSD = Int32.Parse(StorageCb.SelectedValue.ToString()),
                        QuantitySSD = Int32.Parse(QuantityTB.Text),
                        SerialNumberSSD = SerialTB.Text,
                    });
                    DBEntities.GetContext().SaveChanges();
                    MBClass.InformationMB("Успешно");
                    NavigationService.Navigate(new SSDListPage());
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
            NavigationService.Navigate(new PageFolder.EmployeePageFolder.ComputerComponentsFolder.SSDFolder.SSDListPage());
        }
    }
}
