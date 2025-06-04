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
using System.IO;                  // Для File.ReadAllBytes и MemoryStream
using Microsoft.Win32;           // Для OpenFileDialog
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
        // Для хранения байтов загруженного изображения
        private byte[] _photoData;

        public DirectorAddPage()
        {
            InitializeComponent();
            UserCb.ItemsSource = DBEntities.GetContext()
                .User.ToList();
        }

        private void AddPhotoBtn_Click(object sender, RoutedEventArgs e)
        {
            // Открываем диалог выбора файла
            var dialog = new OpenFileDialog
            {
                Title = "Выберите фото сотрудника",
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp",
                Multiselect = false
            };

            if (dialog.ShowDialog() == true)
            {
                try
                {
                    // Считываем байты файла
                    _photoData = File.ReadAllBytes(dialog.FileName);

                    // Отображаем превью
                    var bitmap = new BitmapImage();
                    using (var ms = new MemoryStream(_photoData))
                    {
                        bitmap.BeginInit();
                        bitmap.CacheOption = BitmapCacheOption.OnLoad;
                        bitmap.StreamSource = ms;
                        bitmap.EndInit();
                        bitmap.Freeze();
                    }
                    PhotoPreview.Source = bitmap;
                }
                catch (Exception ex)
                {
                    MBClass.ErrorMB(ex.Message);
                }
            }
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
                MBClass.ErrorMB("Пожалуйста, выберите привязку к пользователю");
                UserCb.Focus();
            }
            else
            {
                try
                {
                    var newStaff = new Staff()
                    {
                        LastNameStaff = LastNameTB.Text,
                        FirstNameStaff = FirstNameTB.Text,
                        MiddleNameStaff = MiddleNameTB.Text,
                        PhoneNumberStaff = PhoneTB.Text,
                        EmailStaff = EmailTB.Text,
                        IdUser = Int32.Parse(UserCb.SelectedValue.ToString()),
                        photoUser = _photoData  // сохраняем байты фото (может быть null, если не загружено)
                    };

                    DBEntities.GetContext().Staff.Add(newStaff);
                    DBEntities.GetContext().SaveChanges();

                    MBClass.InformationMB("Сотрудник успешно добавлен");
                    NavigationService.Navigate(new DirectorListPage());
                }
                catch (Exception ex)
                {
                    MBClass.ErrorMB(ex.Message);
                }
            }
        }

        private void Back_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new DirectorListPage());
        }
    }
}
