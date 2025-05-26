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
    /// Логика взаимодействия для MonitorListPage.xaml
    /// </summary>
    public partial class MonitorListPage : Page
    {
        public MonitorListPage()
        {
            InitializeComponent();
            ListMonDG.ItemsSource = DBEntities.GetContext().Monitor.ToList()
                .OrderBy(c => c.IdMonitor);
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            Monitor monitor = ListMonDG.SelectedItem as Monitor;
            if (ListMonDG.SelectedItem == null)
            {
                MBClass.ErrorMB("Выберите монитор" +
                    " для удаления");
            }
            else
            {
                if (MBClass.QestionMB("Удалить " +
                    $"монитор под названием " +
                    $"{monitor.NameMonitor}?"))
                {
                    DBEntities.GetContext().Monitor
                        .Remove(ListMonDG.SelectedItem as Monitor);
                    DBEntities.GetContext().SaveChanges();

                    MBClass.InformationMB("Монитор удален");
                    ListMonDG.ItemsSource = DBEntities.GetContext()
                        .Monitor.ToList().OrderBy(u => u.NameMonitor);
                }
            }
        }

        private void Red_Click(object sender, RoutedEventArgs e)
        {
            if (ListMonDG.SelectedItem == null)
            {
                MBClass.ErrorMB("Выберите " +
                    "монитор для редактирования");
            }
            else
            {
                NavigationService.Navigate(
                    new MonitorEditPage(ListMonDG.SelectedItem as Monitor));
            }
        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            ListMonDG.ItemsSource = DBEntities.GetContext()
                .Monitor.Where(u => u.NameMonitor.StartsWith(SearchTb.Text))
                .ToList().OrderBy(u => u.NameMonitor);
        }

        private void Plus_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new PageFolder.EmployeePageFolder.PeripheryFolder.MonitorFolder.MonitorAddPage());
        }
    }
}
