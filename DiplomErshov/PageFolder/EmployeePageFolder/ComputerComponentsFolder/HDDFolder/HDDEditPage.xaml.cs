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
using   DiplomErshov.DataFolder;
using DiplomErshov.PageFolder.EmployeePageFolder.ComputerComponentsFolder.PowerSupplyFolder;


namespace DiplomErshov.PageFolder.EmployeePageFolder.ComputerComponentsFolder.HDDFolder
{
    /// <summary>
    /// Логика взаимодействия для HDDEditPage.xaml
    /// </summary>
    public partial class HDDEditPage : Page
    {
        string saveSerial = "";
        private HDD originalHDD;
        public HDDEditPage(HDD hdd)
        {
            InitializeComponent();
            DBEntities.nullContext();
            DBEntities.nullContext(); originalHDD = DBEntities.GetContext().HDD
                .FirstOrDefault(u => u.IdHDD == hdd.IdHDD);
            DataContext = hdd;
            this.originalHDD.IdHDD = hdd.IdHDD;
            StorageCb.ItemsSource = DBEntities.GetContext()
                .DigitalStorageCapacityHDD.ToList();
            SerialTB.Text = saveSerial = hdd.SerialNumberHDD;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            var checkSerialNumberHDD = DBEntities.GetContext()
                            .HDD.FirstOrDefault(u => u.SerialNumberHDD == SerialTB.Text);
            if (checkSerialNumberHDD != null && saveSerial != SerialTB.Text)
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
                    originalHDD = DBEntities.GetContext().HDD
                        .FirstOrDefault(u => u.IdHDD == originalHDD.IdHDD);
                    originalHDD.NameHDD = NameTB.Text;
                    originalHDD.IdDigitalStorageCapacityHDD = Int32.Parse(
                        StorageCb.SelectedValue.ToString());
                    originalHDD.QuantityHDD = Int32.Parse(QuantityTB.Text);
                    originalHDD.SerialNumberHDD = SerialTB.Text;
                    DBEntities.GetContext().SaveChanges();
                    MBClass.InformationMB("Данные успешно отредактированы");
                    NavigationService.Navigate(new HDDListPage());
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
            NavigationService.Navigate(new PageFolder.EmployeePageFolder.ComputerComponentsFolder.HDDFolder.HDDListPage());
        }
    }
}
