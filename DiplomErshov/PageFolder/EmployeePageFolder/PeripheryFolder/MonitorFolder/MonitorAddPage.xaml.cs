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
    /// Логика взаимодействия для MonitorAddPage.xaml
    /// </summary>
    public partial class MonitorAddPage : Page
    {
        public MonitorAddPage()
        {
            InitializeComponent();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            var checkSerialNumberMonitor = DBEntities.GetContext()
                .Monitor.FirstOrDefault(u => u.SerialNumberMonitor == SerialTB.Text);
            if (checkSerialNumberMonitor != null)
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

            else if (string.IsNullOrWhiteSpace(NameTB.Text))
            {
                MBClass.ErrorMB("Пожалуйста, введите название");
                NameTB.Focus();
            }

            else if (string.IsNullOrWhiteSpace(DateDP.Text))
            {
                MBClass.ErrorMB("Пожалуйста, выберете дату гарантии");
                DateDP.Focus();
            }
            else
            {
                try
                {
                    DBEntities.GetContext().Monitor.Add(new Monitor()
                    {
                        NameMonitor = NameTB.Text,
                        SerialNumberMonitor = SerialTB.Text,
                        GuaranteeMonitor = Convert.ToDateTime(DateDP.SelectedDate),
                    });
                    DBEntities.GetContext().SaveChanges();
                    MBClass.InformationMB("Успешно");
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
