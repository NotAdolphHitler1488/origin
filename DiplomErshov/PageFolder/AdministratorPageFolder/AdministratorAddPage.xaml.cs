using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
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

namespace DiplomErshov.PageFolder.AdministratorPageFolder
{
    /// <summary>
    /// Логика взаимодействия для AdministratorAddPage.xaml
    /// </summary>
    public partial class AdministratorAddPage : Page
    {
        public AdministratorAddPage()
        {
            InitializeComponent();
            RoleCb.ItemsSource = DBEntities.GetContext()
                .Role.ToList();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            var checkLogin = DBEntities.GetContext().User.FirstOrDefault(u => u.LoginUser == LoginTB.Text);
            if (checkLogin != null)
            {
                MBClass.ErrorMB("Такой логин уже существует");
                PasswordTB.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(LoginTB.Text))
            {
                MBClass.ErrorMB("Пожалуйста, введите логин");
                LoginTB.Focus();
            }
            else if (string.IsNullOrWhiteSpace(PasswordTB.Text))
            {
                MBClass.ErrorMB("Пожалуйста, введите пароль");
                PasswordTB.Focus();
            }
            else
            {
                try
                {
                    DBEntities.GetContext().User.Add(new User()
                    {
                        IdRole = Int32.Parse(RoleCb.SelectedValue.ToString()),
                        LoginUser = LoginTB.Text,
                        PasswordUser = PasswordTB.Text,
                    });
                    DBEntities.GetContext().SaveChanges();
                    MBClass.InformationMB("Успешно");
                    NavigationService.Navigate(new AdministratorListPage());
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
            NavigationService.Navigate(new PageFolder.AdministratorPageFolder.AdministratorListPage());
        }
    }
}
