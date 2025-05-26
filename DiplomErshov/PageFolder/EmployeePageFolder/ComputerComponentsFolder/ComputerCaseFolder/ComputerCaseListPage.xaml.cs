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
using DiplomErshov.PageFolder.EmployeePageFolder.ComputerComponentsFolder.GPUFolder;


namespace DiplomErshov.PageFolder.EmployeePageFolder.ComputerComponentsFolder.ComputerCaseFolder
{
    /// <summary>
    /// Логика взаимодействия для ComputerCaseListPage.xaml
    /// </summary>
    public partial class ComputerCaseListPage : Page
    {
        public ComputerCaseListPage()
        {
            InitializeComponent();
            ListCaseUDG.ItemsSource = DBEntities.GetContext().ComputerCase.ToList()
                .OrderBy(c => c.IdComputerCase);
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            ComputerCase computercase = ListCaseUDG.SelectedItem as ComputerCase;
            if (ListCaseUDG.SelectedItem == null)
            {
                MBClass.ErrorMB("Выберите корпус" +
                    " для удаления");
            }
            else
            {
                if (MBClass.QestionMB("Удалить " +
                    $"корпус с названием " +
                    $"{computercase.NameComputerCase}?"))
                {
                    DBEntities.GetContext().ComputerCase
                        .Remove(ListCaseUDG.SelectedItem as ComputerCase);
                    DBEntities.GetContext().SaveChanges();

                    MBClass.InformationMB("Корпус удален");
                    ListCaseUDG.ItemsSource = DBEntities.GetContext()
                        .ComputerCase.ToList().OrderBy(u => u.NameComputerCase);
                }
            }
        }

        private void Red_Click(object sender, RoutedEventArgs e)
        {
            if (ListCaseUDG.SelectedItem == null)
            {
                MBClass.ErrorMB("Выберите " +
                    "корпус для редактирования");
            }
            else
            {
                NavigationService.Navigate(
                    new ComputerCaseEditPage(ListCaseUDG.SelectedItem as ComputerCase));
            }
        }

        private void Plus_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new PageFolder.EmployeePageFolder.ComputerComponentsFolder.ComputerCaseFolder.ComputerCaseAddPage());
        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            ListCaseUDG.ItemsSource = DBEntities.GetContext()
                .ComputerCase.Where(u => u.NameComputerCase.StartsWith(SearchTb.Text))
                .ToList().OrderBy(u => u.NameComputerCase);
        }
    }
}
