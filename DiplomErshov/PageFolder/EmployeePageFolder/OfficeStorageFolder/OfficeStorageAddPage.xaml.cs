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


namespace DiplomErshov.PageFolder.EmployeePageFolder.OfficeStorageFolder
{
    /// <summary>
    /// Логика взаимодействия для OfficeStorageAddPage.xaml
    /// </summary>
    public partial class OfficeStorageAddPage : Page
    {
        public OfficeStorageAddPage()
        {
            InitializeComponent();
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
                    OfficeStorage officestorage = new OfficeStorage();
                    DBEntities.GetContext().OfficeStorage.Add(officestorage);

                    officestorage.IdComputer = Int32.Parse(ComputerCb.SelectedValue.ToString());
                    officestorage.IdStaff = Int32.Parse(StaffCb.SelectedValue.ToString());
                    officestorage.IdKeyboard = Int32.Parse(KeyboardCb.SelectedValue.ToString());
                    officestorage.IdComputerMouse = Int32.Parse(ComputerMouseCb.SelectedValue.ToString());
                    officestorage.IdScanner = ScannerCb.SelectedValue == null ? null : (int?)Convert.ToInt32(ScannerCb.SelectedValue);
                    officestorage.IdMicrophone = MicrophoneCb.SelectedValue == null ? null : (int?)Convert.ToInt32(MicrophoneCb.SelectedValue);
                    officestorage.IdWebCamera = WebCameraCb.SelectedValue == null ? null : (int?)Convert.ToInt32(WebCameraCb.SelectedValue);
                    officestorage.IdMonitor = Int32.Parse(MonitorCb.SelectedValue.ToString());
                    officestorage.IdPrinter = PrinterCb.SelectedValue == null ? null : (int?)Convert.ToInt32(PrinterCb.SelectedValue);
                    officestorage.IdHeadphones = HeadphonesCb.SelectedValue == null ? null : (int?)Convert.ToInt32(HeadphonesCb.SelectedValue);
                    officestorage.IdGarniture = GarnitureCb.SelectedValue == null ? null : (int?)Convert.ToInt32(GarnitureCb.SelectedValue);
                    DBEntities.GetContext().SaveChanges();
                    MBClass.InformationMB("Успешно");
                    NavigationService.Navigate(new OfficeStorageListPage());
                }
                catch (Exception ex)
                {
                    MBClass.ErrorMB(ex);
                }
            }
        }

        private void Back_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new PageFolder.EmployeePageFolder.OfficeStorageFolder.OfficeStorageListPage());
        }
    }
}
