using System;
using System.Collections;
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
using DiplomErshov.PageFolder.DirectorPageFolder;

namespace DiplomErshov.PageFolder.EmployeePageFolder.OfficeStorageFolder
{
    /// <summary>
    /// Логика взаимодействия для OfficeStorageEditPage.xaml
    /// </summary>
    public partial class OfficeStorageEditPage : Page
    {
        private OfficeStorage OriginalStorage;
        public OfficeStorageEditPage(OfficeStorage officestorage)
        {
            InitializeComponent();
            DBEntities.nullContext();
            DBEntities.nullContext(); OriginalStorage = DBEntities.GetContext().OfficeStorage
                .FirstOrDefault(u => u.IdOfficeStorage == officestorage.IdOfficeStorage);
            DataContext = officestorage;
            this.OriginalStorage.IdOfficeStorage = officestorage.IdOfficeStorage;
            ComputerCb.ItemsSource = DBEntities.GetContext()
                .Computer.ToList();
            StaffCb.ItemsSource = DBEntities.GetContext()
                .Staff.ToList();
            KeyboardCb.ItemsSource = DBEntities.GetContext()
                .Keyboard.ToList();
            ComputerMouseCb.ItemsSource = DBEntities.GetContext()
                .ComputerMouse.ToList();
            ScannerCb.ItemsSource = DBEntities.GetContext()
                .Scanner.ToList();
            MicrophoneCb.ItemsSource = DBEntities.GetContext()
                .Microphone.ToList();
            WebCameraCb.ItemsSource = DBEntities.GetContext()
                .WebCamera.ToList();
            MonitorCb.ItemsSource = DBEntities.GetContext()
                .Monitor.ToList();
            PrinterCb.ItemsSource = DBEntities.GetContext()
                .Printer.ToList();
            HeadphonesCb.ItemsSource = DBEntities.GetContext()
                .Headphones.ToList();
            GarnitureCb.ItemsSource = DBEntities.GetContext()
                .Garniture.ToList();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ComputerCb.Text))
            {
                MBClass.ErrorMB("Пожалуйста, выберете компьютер");
                ComputerCb.Focus();
            }

            else if (string.IsNullOrWhiteSpace(StaffCb.Text))
            {
                MBClass.ErrorMB("Пожалуйста, выберете сотрудника");
                StaffCb.Focus();
            }

            else if (string.IsNullOrWhiteSpace(KeyboardCb.Text))
            {
                MBClass.ErrorMB("Пожалуйста, выберрете клавиатуру");
                KeyboardCb.Focus();
            }

            else if (string.IsNullOrWhiteSpace(KeyboardCb.Text))
            {
                MBClass.ErrorMB("Пожалуйста, выберете комп. мышь");
                KeyboardCb.Focus();
            }

            else if (string.IsNullOrWhiteSpace(MonitorCb.Text))
            {
                MBClass.ErrorMB("Пожалуйста, выберете монитор");
                MonitorCb.Focus();
            }

            else
            {
                try
                {
                    OriginalStorage = DBEntities.GetContext().OfficeStorage
                        .FirstOrDefault(u => u.IdOfficeStorage == OriginalStorage.IdOfficeStorage);
                    OriginalStorage.IdComputer = Int32.Parse(
                        ComputerCb.SelectedValue.ToString());
                    OriginalStorage.IdStaff = Int32.Parse(
                        StaffCb.SelectedValue.ToString());
                    OriginalStorage.IdKeyboard = Int32.Parse(
                        KeyboardCb.SelectedValue.ToString());
                    OriginalStorage.IdComputerMouse = Int32.Parse(
                        ComputerMouseCb.SelectedValue.ToString());
                    OriginalStorage.IdScanner = Int32.Parse(
                        ScannerCb.SelectedValue.ToString());
                    OriginalStorage.IdMicrophone = Int32.Parse(
                        MicrophoneCb.SelectedValue.ToString());
                    OriginalStorage.IdWebCamera = Int32.Parse(
                        WebCameraCb.SelectedValue.ToString());
                    OriginalStorage.IdMonitor = Int32.Parse(
                        MonitorCb.SelectedValue.ToString());
                    OriginalStorage.IdPrinter = Int32.Parse(
                        PrinterCb.SelectedValue.ToString());
                    OriginalStorage.IdHeadphones = Int32.Parse(
                        HeadphonesCb.SelectedValue.ToString());
                    OriginalStorage.IdGarniture = Int32.Parse(
                        GarnitureCb.SelectedValue.ToString());
                    DBEntities.GetContext().SaveChanges();
                    MBClass.InformationMB("Данные успешно отредактированы");
                    NavigationService.Navigate(new OfficeStorageListPage());
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
            NavigationService.Navigate(new PageFolder.EmployeePageFolder.OfficeStorageFolder.OfficeStorageListPage());
        }
    }
}
