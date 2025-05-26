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
    /// Логика взаимодействия для PowerSupplyListPage.xaml
    /// </summary>
    public partial class PowerSupplyListPage : Page
    {
        public PowerSupplyListPage()
        {
            InitializeComponent();
            ListPSDG.ItemsSource = DBEntities.GetContext().PowerSupply.ToList()
                .OrderBy(c => c.IdPowerSupply);
        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            ListPSDG.ItemsSource = DBEntities.GetContext()
                .PowerSupply.Where(u => u.NamePowerSupply.StartsWith(SearchTb.Text))
                .ToList().OrderBy(u => u.NamePowerSupply);
        }

        private void Plus_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new PageFolder.EmployeePageFolder.ComputerComponentsFolder.PowerSupplyFolder.PowerSupplyAddPage());
        }

        private void Red_Click(object sender, RoutedEventArgs e)
        {
            if (ListPSDG.SelectedItem == null)
            {
                MBClass.ErrorMB("Выберите " +
                    "блок питания для редактирования");
            }
            else
            {
                NavigationService.Navigate(
                    new PowerSupplyEditPage(ListPSDG.SelectedItem as PowerSupply));
            }
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            PowerSupply powersupply = ListPSDG.SelectedItem as PowerSupply;
            if (ListPSDG.SelectedItem == null)
            {
                MBClass.ErrorMB("Выберите блок питания" +
                    " для удаления");
            }
            else
            {
                if (MBClass.QestionMB("Удалить " +
                    $"блок питания с названием " +
                    $"{powersupply.NamePowerSupply}?"))
                {
                    DBEntities.GetContext().PowerSupply
                        .Remove(ListPSDG.SelectedItem as PowerSupply);
                    DBEntities.GetContext().SaveChanges();

                    MBClass.InformationMB("Блок питания удален");
                    ListPSDG.ItemsSource = DBEntities.GetContext()
                        .PowerSupply.ToList().OrderBy(u => u.NamePowerSupply);
                }
            }
        }
    }
}
