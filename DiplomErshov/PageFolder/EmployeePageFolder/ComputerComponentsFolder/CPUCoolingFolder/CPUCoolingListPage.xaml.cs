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
    /// Логика взаимодействия для CPUCoolingListPage.xaml
    /// </summary>
    public partial class CPUCoolingListPage : Page
    {
        public CPUCoolingListPage()
        {
            InitializeComponent();
            ListCoolDG.ItemsSource = DBEntities.GetContext().CPUСooling.ToList()
                .OrderBy(c => c.IdCPUСooling);
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            CPUСooling cpucooling = ListCoolDG.SelectedItem as CPUСooling;
            if (ListCoolDG.SelectedItem == null)
            {
                MBClass.ErrorMB("Выберите охлаждение для CPU" +
                    " для удаления");
            }
            else
            {
                if (MBClass.QestionMB("Удалить " +
                    $"охлаждение для CPU с названием " +
                    $"{cpucooling.NameCPUСooling}?"))
                {
                    DBEntities.GetContext().CPUСooling
                        .Remove(ListCoolDG.SelectedItem as CPUСooling);
                    DBEntities.GetContext().SaveChanges();

                    MBClass.InformationMB("Охлаждение для CPU удалено");
                    ListCoolDG.ItemsSource = DBEntities.GetContext()
                        .CPUСooling.ToList().OrderBy(u => u.NameCPUСooling);
                }
            }
        }

        private void Red_Click(object sender, RoutedEventArgs e)
        {
            if (ListCoolDG.SelectedItem == null)
            {
                MBClass.ErrorMB("Выберите " +
                    "охлаждение CPU для редактирования");
            }
            else
            {
                NavigationService.Navigate(
                    new CPUCoolingEditPage(ListCoolDG.SelectedItem as CPUСooling));
            }
        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            ListCoolDG.ItemsSource = DBEntities.GetContext()
                .CPUСooling.Where(u => u.NameCPUСooling.StartsWith(SearchTb.Text))
                .ToList().OrderBy(u => u.NameCPUСooling);
        }

        private void Plus_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new PageFolder.EmployeePageFolder.ComputerComponentsFolder.CPUCoolingFolder.CPUCoolingAddPage());
        }
    }
}
