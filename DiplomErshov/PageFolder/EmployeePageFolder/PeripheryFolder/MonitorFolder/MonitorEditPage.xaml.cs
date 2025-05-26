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
using DiplomErshov.PageFolder.EmployeePageFolder.PeripheryFolder.MicrophoneFolder;

namespace DiplomErshov.PageFolder.EmployeePageFolder.PeripheryFolder.MonitorFolder
{
    /// <summary>
    /// Логика взаимодействия для MonitorEditPage.xaml
    /// </summary>
    public partial class MonitorEditPage : Page
    {
        string saveSerial = "";
        private Monitor originalMonitor;
        public MonitorEditPage(Monitor monitor)
        {
            InitializeComponent();
            DBEntities.nullContext();
            DBEntities.nullContext(); originalMonitor = DBEntities.GetContext().Monitor
                .FirstOrDefault(u => u.IdMonitor == monitor.IdMonitor);
            DataContext = monitor;
            this.originalMonitor.IdMonitor = monitor.IdMonitor;
            SerialTB.Text = saveSerial = monitor.SerialNumberMonitor;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            var checkSerialMonitor = DBEntities.GetContext()
                            .Monitor.FirstOrDefault(u => u.SerialNumberMonitor == SerialTB.Text);
            if (checkSerialMonitor != null && saveSerial != SerialTB.Text)
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
                    originalMonitor = DBEntities.GetContext().Monitor
                        .FirstOrDefault(u => u.IdMonitor == originalMonitor.IdMonitor);
                    originalMonitor.NameMonitor = NameTB.Text;
                    originalMonitor.SerialNumberMonitor = SerialTB.Text;
                    originalMonitor.GuaranteeMonitor = Convert.ToDateTime(DateDP.SelectedDate);
                    DBEntities.GetContext().SaveChanges();
                    MBClass.InformationMB("Данные успешно отредактированы");
                    NavigationService.Navigate(new MonitorListPage());
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
            NavigationService.Navigate(new PageFolder.EmployeePageFolder.PeripheryFolder.MonitorFolder.MonitorListPage());
        }
    }
}
