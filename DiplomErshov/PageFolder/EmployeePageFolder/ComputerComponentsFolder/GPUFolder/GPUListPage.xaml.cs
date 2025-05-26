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
using DiplomErshov.PageFolder.EmployeePageFolder.ComputerComponentsFolder.HDDFolder;


namespace DiplomErshov.PageFolder.EmployeePageFolder.ComputerComponentsFolder.GPUFolder
{
    /// <summary>
    /// Логика взаимодействия для GPUListPage.xaml
    /// </summary>
    public partial class GPUListPage : Page
    {
        public GPUListPage()
        {
            InitializeComponent();
            ListGPUDG.ItemsSource = DBEntities.GetContext().GPU.ToList()
                .OrderBy(c => c.IdGPU);
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            GPU gpu = ListGPUDG.SelectedItem as GPU;
            if (ListGPUDG.SelectedItem == null)
            {
                MBClass.ErrorMB("Выберите видеокарту" +
                    " для удаления");
            }
            else
            {
                if (MBClass.QestionMB("Удалить " +
                    $"видеокарту с названием " +
                    $"{gpu.NameGPU}?"))
                {
                    DBEntities.GetContext().GPU
                        .Remove(ListGPUDG.SelectedItem as GPU);
                    DBEntities.GetContext().SaveChanges();

                    MBClass.InformationMB("Видеокарта удалена");
                    ListGPUDG.ItemsSource = DBEntities.GetContext()
                        .GPU.ToList().OrderBy(u => u.NameGPU);
                }
            }
        }

        private void Red_Click(object sender, RoutedEventArgs e)
        {
            if (ListGPUDG.SelectedItem == null)
            {
                MBClass.ErrorMB("Выберите " +
                    "видеокарту для редактирования");
            }
            else
            {
                NavigationService.Navigate(
                    new GPUEditPage(ListGPUDG.SelectedItem as GPU));
            }
        }

        private void Plus_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new PageFolder.EmployeePageFolder.ComputerComponentsFolder.GPUFolder.GPUAddPage());
        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            ListGPUDG.ItemsSource = DBEntities.GetContext()
                .GPU.Where(u => u.NameGPU.StartsWith(SearchTb.Text))
                .ToList().OrderBy(u => u.NameGPU);
        }
    }
}
