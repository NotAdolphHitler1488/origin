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
using DiplomErshov.PageFolder.EmployeePageFolder.ComputerComponentsFolder.PowerSupplyFolder;


namespace DiplomErshov.PageFolder.EmployeePageFolder.ComputerComponentsFolder.HDDFolder
{
    /// <summary>
    /// Логика взаимодействия для HDDAddPage.xaml
    /// </summary>
    public partial class HDDAddPage : Page
    {
        public HDDAddPage()
        {
            InitializeComponent();
            StorageCb.ItemsSource = DBEntities.GetContext()
                .DigitalStorageCapacityHDD.ToList();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            var checkSerialNumberHDD = DBEntities.GetContext()
                .HDD.FirstOrDefault(u => u.SerialNumberHDD == SerialTB.Text);

            if (checkSerialNumberHDD != null)
            {
                MBClass.ErrorMB("Такой серийный номер уже существует");
                SerialTB.Focus();
            }

            else if (string.IsNullOrWhiteSpace(SerialTB.Text))
            {
                MBClass.ErrorMB("Пожалуйста, введите серийный номер");
                SerialTB.Focus();
            }

            else if (string.IsNullOrWhiteSpace(QuantityTB.Text))
            {
                MBClass.ErrorMB("Пожалуйста, введите количество дисков");
                QuantityTB.Focus();
            }

            else if (string.IsNullOrWhiteSpace(NameTB.Text))
            {
                MBClass.ErrorMB("Пожалуйста, введите название");
                NameTB.Focus();
            }

            else if (string.IsNullOrWhiteSpace(StorageCb.Text))
            {
                MBClass.ErrorMB("Пожалуйста, выберете объем жесткого диска");
                StorageCb.Focus();
            }

            else
            {
                try
                {
                    DBEntities.GetContext().HDD.Add(new HDD()
                    {
                        NameHDD = NameTB.Text,
                        IdDigitalStorageCapacityHDD = Int32.Parse(StorageCb.SelectedValue.ToString()),
                        QuantityHDD = Int32.Parse(QuantityTB.Text),
                        SerialNumberHDD = SerialTB.Text,
                    });
                    DBEntities.GetContext().SaveChanges();
                    MBClass.InformationMB("Успешно");
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
