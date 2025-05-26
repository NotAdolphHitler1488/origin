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
using DiplomErshov.PageFolder.EmployeePageFolder.ComputerComponentsFolder.ComputerCaseFolder;

namespace DiplomErshov.PageFolder.DirectorPageFolder
{
    /// <summary>
    /// Логика взаимодействия для DirectorListPage.xaml
    /// </summary>
    public partial class DirectorListPage : Page
    {
        public DirectorListPage()
        {
            InitializeComponent();
            ListDirDG.ItemsSource = DBEntities.GetContext().Staff.ToList()
                .OrderBy(c => c.IdStaff);
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            Staff staff = ListDirDG.SelectedItem as Staff;
            if (ListDirDG.SelectedItem == null)
            {
                MBClass.ErrorMB("Выберите сотрудника" +
                    " для удаления");
            }
            else
            {
                if (MBClass.QestionMB("Удалить " +
                    $"сотрудника с фамилией " +
                    $"{staff.LastNameStaff}?"))
                {
                    DBEntities.GetContext().Staff
                        .Remove(ListDirDG.SelectedItem as Staff);
                    DBEntities.GetContext().SaveChanges();

                    MBClass.InformationMB("Сотрудник удален");
                    ListDirDG.ItemsSource = DBEntities.GetContext()
                        .Staff.ToList().OrderBy(u => u.LastNameStaff);
                }
            }
        }

        private void Red_Click(object sender, RoutedEventArgs e)
        {
            if (ListDirDG.SelectedItem == null)
            {
                MBClass.ErrorMB("Выберите " +
                    "сотрудника для редактирования");
            }
            else
            {
                NavigationService.Navigate(
                    new DirectorEditPage(ListDirDG.SelectedItem as Staff));
            }
        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            ListDirDG.ItemsSource = DBEntities.GetContext()
                .Staff.Where(u => u.LastNameStaff.StartsWith(SearchTb.Text))
                .ToList().OrderBy(u => u.LastNameStaff);
        }

        private void Plus_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new PageFolder.DirectorPageFolder.DirectorAddPage());
        }
    }
}
