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
using DiplomErshov.PageFolder.EmployeePageFolder.ComputerComponentsFolder.GPUFolder;


namespace DiplomErshov.PageFolder.EmployeePageFolder.ComputerComponentsFolder.CPUFolder
{
    /// <summary>
    /// Логика взаимодействия для CPUListPage.xaml
    /// </summary>
    public partial class CPUListPage : Page
    {
        public CPUListPage()
        {
            InitializeComponent();
            ListCPUDG.ItemsSource = DBEntities.GetContext().CPU.ToList()
                .OrderBy(c => c.IdCPU);
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            CPU cpu = ListCPUDG.SelectedItem as CPU;
            if (ListCPUDG.SelectedItem == null)
            {
                MBClass.ErrorMB("Выберите процессор" +
                    " для удаления");
            }
            else
            {
                if (MBClass.QestionMB("Удалить " +
                    $"процессор с названием " +
                    $"{cpu.NameCPU}?"))
                {
                    DBEntities.GetContext().CPU
                        .Remove(ListCPUDG.SelectedItem as CPU);
                    DBEntities.GetContext().SaveChanges();

                    MBClass.InformationMB("Процессор удален");
                    ListCPUDG.ItemsSource = DBEntities.GetContext()
                        .CPU.ToList().OrderBy(u => u.NameCPU);
                }
            }
        }

        private void Red_Click(object sender, RoutedEventArgs e)
        {
            if (ListCPUDG.SelectedItem == null)
            {
                MBClass.ErrorMB("Выберите " +
                    "процессор для редактирования");
            }
            else
            {
                NavigationService.Navigate(
                    new CPUEditPage(ListCPUDG.SelectedItem as CPU));
            }
        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            ListCPUDG.ItemsSource = DBEntities.GetContext()
                .CPU.Where(u => u.NameCPU.StartsWith(SearchTb.Text))
                .ToList().OrderBy(u => u.NameCPU);
        }

        private void Plus_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new PageFolder.EmployeePageFolder.ComputerComponentsFolder.CPUFolder.CPUAddPage());
        }
    }
}
