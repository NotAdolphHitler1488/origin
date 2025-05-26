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
using DiplomErshov.PageFolder.EmployeePageFolder.ComputerComponentsFolder.CPUCoolingFolder;

namespace DiplomErshov.PageFolder.DirectorPageFolder
{
    /// <summary>
    /// Логика взаимодействия для DirectorAddPage.xaml
    /// </summary>
    public partial class DirectorAddPage : Page
    {
        public DirectorAddPage()
        {
            InitializeComponent();
            UserCb.ItemsSource = DBEntities.GetContext()
                .User.ToList();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(LastNameTB.Text))
            {
                MBClass.ErrorMB("Пожалуйста, введите фамилию");
                LastNameTB.Focus();
            }

            else if (string.IsNullOrWhiteSpace(FirstNameTB.Text))
            {
                MBClass.ErrorMB("Пожалуйста, введите имя");
                FirstNameTB.Focus();
            }

            else if (string.IsNullOrWhiteSpace(PhoneTB.Text))
            {
                MBClass.ErrorMB("Пожалуйста, введите номер телефона");
                PhoneTB.Focus();
            }

            else if (string.IsNullOrWhiteSpace(EmailTB.Text))
            {
                MBClass.ErrorMB("Пожалуйста, введите электронную почту");
                EmailTB.Focus();
            }

            else if (string.IsNullOrWhiteSpace(UserCb.Text))
            {
                MBClass.ErrorMB("Пожалуйста, выберете привязку к пользователю");
                UserCb.Focus();
            }
            else
            {
                try
                {
                    DBEntities.GetContext().Staff.Add(new Staff()
                    {
                        LastNameStaff = LastNameTB.Text,
                        FirstNameStaff = FirstNameTB.Text,
                        MiddleNameStaff = MiddleNameTB.Text,
                        PhoneNumberStaff = PhoneTB.Text,
                        EmailStaff = EmailTB.Text,
                        IdUser = Int32.Parse(UserCb.SelectedValue.ToString()),
                    });
                    DBEntities.GetContext().SaveChanges();
                    MBClass.InformationMB("Успешно");
                    NavigationService.Navigate(new DirectorListPage());
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
            NavigationService.Navigate(new PageFolder.DirectorPageFolder.DirectorListPage());
        }
    }
}
