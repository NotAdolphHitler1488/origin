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
    /// Логика взаимодействия для SSDEditPage.xaml
    /// </summary>
    public partial class SSDEditPage : Page
    {
        string saveSerial = "";
        private SSD originalSSD;
        public SSDEditPage(SSD ssd)
        {
            InitializeComponent();
            DBEntities.nullContext();
            DBEntities.nullContext(); originalSSD = DBEntities.GetContext().SSD
                .FirstOrDefault(u => u.IdSSD == ssd.IdSSD);
            DataContext = ssd;
            this.originalSSD.IdSSD = ssd.IdSSD;
            StorageCb.ItemsSource = DBEntities.GetContext()
                .DigitalStorageCapacitySSD.ToList();
            SeriesTB.Text = saveSerial = ssd.SerialNumberSSD;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            var checkSerialNumberSSD = DBEntities.GetContext()
                            .SSD.FirstOrDefault(u => u.SerialNumberSSD == SeriesTB.Text);
            if (checkSerialNumberSSD != null && saveSerial != SeriesTB.Text)
            {
                MBClass.ErrorMB("Такой серийный номер уже существует");
                SeriesTB.Focus();
                return;
            }

            else if (string.IsNullOrWhiteSpace(SeriesTB.Text))
            {
                MBClass.ErrorMB("Пожалуйста, введите серийный номер");
                SeriesTB.Focus();
            }

            else
            {
                try
                {
                    originalSSD = DBEntities.GetContext().SSD
                        .FirstOrDefault(u => u.IdSSD == originalSSD.IdSSD);
                    originalSSD.NameSSD = NameTB.Text;
                    originalSSD.IdDigitalStorageCapacitySSD = Int32.Parse(
                        StorageCb.SelectedValue.ToString());
                    originalSSD.QuantitySSD = Int32.Parse(QuantityTB.Text);
                    originalSSD.SerialNumberSSD = SeriesTB.Text;
                    DBEntities.GetContext().SaveChanges();
                    MBClass.InformationMB("Данные успешно отредактированы");
                    NavigationService.Navigate(new SSDListPage());
                }
                catch (Exception ex)
                {
                    MBClass.ErrorMB(ex);
                }
            }
        }

        private void Back_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new PageFolder.EmployeePageFolder.ComputerComponentsFolder.SSDFolder.SSDListPage());
        }
    }
}
