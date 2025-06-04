using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DiplomErshov.ClassFolder;
using DiplomErshov.DataFolder;

namespace DiplomErshov.PageFolder.DirectorPageFolder
{
    public partial class DirectorEditPage : Page
    {
        private Staff originalStaff;

        public DirectorEditPage(Staff staff)
        {
            InitializeComponent();

            DBEntities.nullContext();
            originalStaff = DBEntities.GetContext()
                                      .Staff
                                      .FirstOrDefault(u => u.IdStaff == staff.IdStaff);

            DataContext = originalStaff;
            UserCb.ItemsSource = DBEntities.GetContext()
                                           .User
                                           .ToList();
        }

        private void ChangePhotoBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog
            {
                Filter = "Изображения|*.jpg;*.jpeg;*.png;*.bmp",
                Title = "Выберите фото сотрудника"
            };

            if (dlg.ShowDialog() == true)
            {
                byte[] bytes = File.ReadAllBytes(dlg.FileName);
                originalStaff.photoUser = bytes;

                // Обновить превью
                PhotoPreview.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(dlg.FileName));
            }
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(LastNameTB.Text))
            {
                MBClass.ErrorMB("Пожалуйста, введите фамилию");
                LastNameTB.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(FirstNameTB.Text))
            {
                MBClass.ErrorMB("Пожалуйста, введите имя");
                FirstNameTB.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(PhoneTB.Text))
            {
                MBClass.ErrorMB("Пожалуйста, введите номер телефона");
                PhoneTB.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(EmailTB.Text))
            {
                MBClass.ErrorMB("Пожалуйста, введите электронную почту");
                EmailTB.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(UserCb.Text))
            {
                MBClass.ErrorMB("Пожалуйста, выберите привязку к пользователю");
                UserCb.Focus();
                return;
            }

            try
            {
                originalStaff.LastNameStaff = LastNameTB.Text;
                originalStaff.FirstNameStaff = FirstNameTB.Text;
                originalStaff.MiddleNameStaff = MiddleNameTB.Text;
                originalStaff.PhoneNumberStaff = PhoneTB.Text;
                originalStaff.EmailStaff = EmailTB.Text;
                originalStaff.IdUser = int.Parse(UserCb.SelectedValue.ToString());

                DBEntities.GetContext().SaveChanges();
                MBClass.InformationMB("Данные успешно отредактированы");
                NavigationService.Navigate(new DirectorListPage());
            }
            catch (Exception ex)
            {
                MBClass.ErrorMB(ex);
            }
        }

        private void Back_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new DirectorListPage());
        }
    }
}
