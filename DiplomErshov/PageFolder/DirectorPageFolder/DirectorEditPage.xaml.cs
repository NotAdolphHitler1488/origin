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
    /// Логика взаимодействия для DirectorEditPage.xaml
    /// </summary>
    public partial class DirectorEditPage : Page
    {
        private Staff originalStaff;
        public DirectorEditPage(Staff staff)
        {
            InitializeComponent();
            DBEntities.nullContext();
            DBEntities.nullContext(); originalStaff = DBEntities.GetContext().Staff
                .FirstOrDefault(u => u.IdStaff == staff.IdStaff);
            DataContext = staff;
            this.originalStaff.IdStaff = staff.IdStaff;
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
                    originalStaff = DBEntities.GetContext().Staff
                        .FirstOrDefault(u => u.IdStaff == originalStaff.IdStaff);
                    originalStaff.LastNameStaff = LastNameTB.Text;
                    originalStaff.FirstNameStaff = FirstNameTB.Text;
                    originalStaff.MiddleNameStaff = MiddleNameTB.Text;
                    originalStaff.PhoneNumberStaff = PhoneTB.Text;
                    originalStaff.EmailStaff = EmailTB.Text;
                    originalStaff.IdUser = Int32.Parse(
                        UserCb.SelectedValue.ToString());
                    DBEntities.GetContext().SaveChanges();
                    MBClass.InformationMB("Данные успешно отредактированы");
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
