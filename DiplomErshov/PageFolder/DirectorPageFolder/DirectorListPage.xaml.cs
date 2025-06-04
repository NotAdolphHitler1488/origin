using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DiplomErshov.ClassFolder;
using DiplomErshov.DataFolder;

namespace DiplomErshov.PageFolder.DirectorPageFolder
{
    public partial class DirectorListPage : Page
    {
        public DirectorListPage()
        {
            InitializeComponent();
            ListDirDG.ItemsSource = DBEntities.GetContext()
                                              .Staff
                                              .ToList()
                                              .OrderBy(c => c.IdStaff);
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            Staff staff = ListDirDG.SelectedItem as Staff;
            if (staff == null)
            {
                MBClass.ErrorMB("Выберите сотрудника для удаления");
                return;
            }

            if (!MBClass.QestionMB($"Удалить сотрудника с фамилией {staff.LastNameStaff}?"))
                return;

            try
            {
                DBEntities.GetContext().Staff.Remove(staff);
                DBEntities.GetContext().SaveChanges();

                MBClass.InformationMB("Сотрудник удален");
                ListDirDG.ItemsSource = DBEntities.GetContext()
                                                  .Staff
                                                  .ToList()
                                                  .OrderBy(u => u.LastNameStaff);
            }
            catch
            {
                MBClass.ErrorMB("Невозможно удалить сотрудника, так как он закреплен за рабочим местом.\n" +
                                "Сначала отвяжите сотрудника от рабочего места.");
            }
        }


        private void Red_Click(object sender, RoutedEventArgs e)
        {
            Staff staff = ListDirDG.SelectedItem as Staff;
            if (staff == null)
            {
                MBClass.ErrorMB("Выберите сотрудника для редактирования");
                return;
            }

            NavigationService.Navigate(new DirectorEditPage(staff));
        }

        private void Details_Click(object sender, RoutedEventArgs e)
        {
            Staff staff = ListDirDG.SelectedItem as Staff;
            if (staff == null)
            {
                MBClass.ErrorMB("Выберите сотрудника для просмотра");
                return;
            }

            NavigationService.Navigate(new DirectorDetailsPage(staff));
        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            ListDirDG.ItemsSource = DBEntities.GetContext()
                                              .Staff
                                              .Where(u => u.LastNameStaff.StartsWith(SearchTb.Text))
                                              .ToList()
                                              .OrderBy(u => u.LastNameStaff);
        }

        private void Plus_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new DirectorAddPage());
        }
    }
}
