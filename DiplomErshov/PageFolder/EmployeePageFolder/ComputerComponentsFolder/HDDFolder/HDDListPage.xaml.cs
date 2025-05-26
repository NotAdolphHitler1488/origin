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
    /// Логика взаимодействия для HDDListPage.xaml
    /// </summary>
    public partial class HDDListPage : Page
    {
        public HDDListPage()
        {
            InitializeComponent();
            ListHDDDG.ItemsSource = DBEntities.GetContext().HDD.ToList()
                .OrderBy(c => c.IdHDD);
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            HDD hdd = ListHDDDG.SelectedItem as HDD;
            if (ListHDDDG.SelectedItem == null)
            {
                MBClass.ErrorMB("Выберите жесткий диск" +
                    " для удаления");
            }
            else
            {
                if (MBClass.QestionMB("Удалить " +
                    $"жесткий диск с названием " +
                    $"{hdd.NameHDD}?"))
                {
                    DBEntities.GetContext().HDD
                        .Remove(ListHDDDG.SelectedItem as HDD);
                    DBEntities.GetContext().SaveChanges();

                    MBClass.InformationMB("Жетский диск удален");
                    ListHDDDG.ItemsSource = DBEntities.GetContext()
                        .HDD.ToList().OrderBy(u => u.NameHDD);
                }
            }
        }

        private void Red_Click(object sender, RoutedEventArgs e)
        {
            if (ListHDDDG.SelectedItem == null)
            {
                MBClass.ErrorMB("Выберите " +
                    "жесткий диск для редактирования");
            }
            else
            {
                NavigationService.Navigate(
                    new HDDEditPage(ListHDDDG.SelectedItem as HDD));
            }
        }

        private void Plus_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new PageFolder.EmployeePageFolder.ComputerComponentsFolder.HDDFolder.HDDAddPage());
        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            ListHDDDG.ItemsSource = DBEntities.GetContext()
                .HDD.Where(u => u.NameHDD.StartsWith(SearchTb.Text))
                .ToList().OrderBy(u => u.NameHDD);
        }
    }
}
