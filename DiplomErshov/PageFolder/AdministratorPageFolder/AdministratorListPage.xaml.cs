using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using DiplomErshov.ClassFolder;
using DiplomErshov.DataFolder;

namespace DiplomErshov.PageFolder.AdministratorPageFolder
{
    /// <summary>
    /// Логика взаимодействия для AdministratorListPage.xaml
    /// </summary>
    public partial class AdministratorListPage : Page
    {
        public AdministratorListPage()
        {
            InitializeComponent();
            ListAdminDG.ItemsSource = DBEntities.GetContext().User.ToList()
                .OrderBy(c => c.IdUser);
        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            ListAdminDG.ItemsSource = DBEntities.GetContext()
                .User.Where(u => u.LoginUser.StartsWith(SearchTb.Text))
                .ToList().OrderBy(u => u.LoginUser);
        }

        private void Red_Click(object sender, RoutedEventArgs e)
        {
            if (ListAdminDG.SelectedItem == null)
            {
                MBClass.ErrorMB("Выберите " +
                    "пользователя для редактирования");
            }
            else
            {
                NavigationService.Navigate(
                    new AdministratorEditPage(ListAdminDG.SelectedItem as User));
            }
        }
        private void Del_Click(object sender, RoutedEventArgs e)
        {
            if (ListAdminDG.SelectedItem == null)
            {
                MBClass.ErrorMB("Выберите пользователя для удаления");
                return;
            }

            User user = ListAdminDG.SelectedItem as User;

            if (MBClass.QestionMB($"Удалить пользователя с логином {user.LoginUser}?"))
            {
                try
                {
                    // Удаление пользователя
                    DBEntities.GetContext().User.Remove(user);
                    DBEntities.GetContext().SaveChanges();

                    MBClass.InformationMB("Пользователь удалён");
                    ListAdminDG.ItemsSource = DBEntities.GetContext()
                        .User.ToList().OrderBy(u => u.LoginUser);
                }
                catch
                {
                    // Проверка, есть ли сотрудник, связанный с этим пользователем
                    var employee = DBEntities.GetContext().Staff
                        .FirstOrDefault(emp => emp.IdUser == user.IdUser);

                    if (employee != null)
                    {
                        MBClass.ErrorMB($"Эта учетная запись закреплена за сотрудником: {employee.LastNameStaff}.\n" +
                            $"Сначала удалите сотрудника.");
                    }
                    else
                    {
                        MBClass.ErrorMB("Ошибка при удалении пользователя.");
                    }
                }

            }
        }

        private void Plus_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new PageFolder.AdministratorPageFolder.AdministratorAddPage());
        }
    }
}
