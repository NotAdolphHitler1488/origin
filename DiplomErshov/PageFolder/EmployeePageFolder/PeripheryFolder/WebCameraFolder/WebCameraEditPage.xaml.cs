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
using DiplomErshov.PageFolder.EmployeePageFolder.PeripheryFolder.ScannerFolder;

namespace DiplomErshov.PageFolder.EmployeePageFolder.PeripheryFolder.WebCameraFolder
{
    /// <summary>
    /// Логика взаимодействия для WebCameraEditPage.xaml
    /// </summary>
    public partial class WebCameraEditPage : Page
    {
        string saveSerial = "";
        private WebCamera originalWebCamera;
        public WebCameraEditPage(WebCamera webCamera)
        {
            InitializeComponent();
            DBEntities.nullContext();
            DBEntities.nullContext(); originalWebCamera = DBEntities.GetContext().WebCamera
                .FirstOrDefault(u => u.IdWebCamera == webCamera.IdWebCamera);
            DataContext = webCamera;
            this.originalWebCamera.IdWebCamera = webCamera.IdWebCamera;
            SerialTB.Text = saveSerial = webCamera.SerialNumberWebCamera;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            var checkSerialWebCamera = DBEntities.GetContext()
                            .WebCamera.FirstOrDefault(u => u.SerialNumberWebCamera == SerialTB.Text);
            if (checkSerialWebCamera != null && saveSerial != SerialTB.Text)
            {
                MBClass.ErrorMB("Такой серийный номер уже существует");
                SerialTB.Focus();
                return;
            }

            else if (string.IsNullOrWhiteSpace(SerialTB.Text))
            {
                MBClass.ErrorMB("Пожалуйста, введите серийный номер");
                SerialTB.Focus();
            }

            else
            {
                try
                {
                    originalWebCamera = DBEntities.GetContext().WebCamera
                        .FirstOrDefault(u => u.IdWebCamera == originalWebCamera.IdWebCamera);
                    originalWebCamera.NameWebCamera = NameTB.Text;
                    originalWebCamera.SerialNumberWebCamera = SerialTB.Text;
                    originalWebCamera.GuaranteeWebCamera = Convert.ToDateTime(DateDP.SelectedDate);
                    DBEntities.GetContext().SaveChanges();
                    MBClass.InformationMB("Данные успешно отредактированы");
                    NavigationService.Navigate(new WebCameraListPage());
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
            NavigationService.Navigate(new PageFolder.EmployeePageFolder.PeripheryFolder.WebCameraFolder.WebCameraListPage());
        }
    }
}
