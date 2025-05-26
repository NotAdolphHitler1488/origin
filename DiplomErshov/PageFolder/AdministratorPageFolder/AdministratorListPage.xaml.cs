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
            User user = ListAdminDG.SelectedItem as User;
            if (ListAdminDG.SelectedItem == null)
            {
                MBClass.ErrorMB("Выберите пользователя" +
                    " для удаления");
            }
            else
            {
                if (MBClass.QestionMB("Удалить " +
                    $"пользователя с логином " +
                    $"{user.LoginUser}?"))
                {
                    DBEntities.GetContext().User
                        .Remove(ListAdminDG.SelectedItem as User);
                    DBEntities.GetContext().SaveChanges();

                    MBClass.InformationMB("Пользователь удален");
                    ListAdminDG.ItemsSource = DBEntities.GetContext()
                        .User.ToList().OrderBy(u => u.LoginUser);
                }
            }
        }
        private void Plus_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new PageFolder.AdministratorPageFolder.AdministratorAddPage());
        }
    }
}
