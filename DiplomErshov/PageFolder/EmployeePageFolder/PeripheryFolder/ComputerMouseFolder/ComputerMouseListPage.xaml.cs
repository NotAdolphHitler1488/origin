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
using DiplomErshov.PageFolder.EmployeePageFolder.ComputerComponentsFolder.CPUFolder;


namespace DiplomErshov.PageFolder.EmployeePageFolder.PeripheryFolder.ComputerMouseFolder
{
    /// <summary>
    /// Логика взаимодействия для ComputerMouseListPage.xaml
    /// </summary>
    public partial class ComputerMouseListPage : Page
    {
        public ComputerMouseListPage()
        {
            InitializeComponent();
            ListCMDG.ItemsSource = DBEntities.GetContext().ComputerMouse.ToList()
                .OrderBy(c => c.IdComputerMouse);
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            ComputerMouse computermouse = ListCMDG.SelectedItem as ComputerMouse;
            if (ListCMDG.SelectedItem == null)
            {
                MBClass.ErrorMB("Выберите комп. мышь" +
                    " для удаления");
            }
            else
            {
                if (MBClass.QestionMB("Удалить " +
                    $"комп. мышь под названием " +
                    $"{computermouse.NameComputerMouse}?"))
                {
                    DBEntities.GetContext().ComputerMouse
                        .Remove(ListCMDG.SelectedItem as ComputerMouse);
                    DBEntities.GetContext().SaveChanges();

                    MBClass.InformationMB("комп. мышь удалена");
                    ListCMDG.ItemsSource = DBEntities.GetContext()
                        .ComputerMouse.ToList().OrderBy(u => u.NameComputerMouse);
                }
            }
        }

        private void Red_Click(object sender, RoutedEventArgs e)
        {
            if (ListCMDG.SelectedItem == null)
            {
                MBClass.ErrorMB("Выберите " +
                    "комп. мышь для редактирования");
            }
            else
            {
                NavigationService.Navigate(
                    new ComputerMouseEditPage(ListCMDG.SelectedItem as ComputerMouse));
            }
        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            ListCMDG.ItemsSource = DBEntities.GetContext()
                .ComputerMouse.Where(u => u.NameComputerMouse.StartsWith(SearchTb.Text))
                .ToList().OrderBy(u => u.NameComputerMouse);
        }

        private void Plus_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new PageFolder.EmployeePageFolder.PeripheryFolder.ComputerMouseFolder.ComputerMouseAddPage());
        }
    }
}
