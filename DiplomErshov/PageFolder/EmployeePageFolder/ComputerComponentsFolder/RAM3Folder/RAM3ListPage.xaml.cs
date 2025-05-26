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
using DiplomErshov.PageFolder.EmployeePageFolder.ComputerComponentsFolder.RAM2Folder;


namespace DiplomErshov.PageFolder.EmployeePageFolder.ComputerComponentsFolder.RAM3Folder
{
    /// <summary>
    /// Логика взаимодействия для RAM3ListPage.xaml
    /// </summary>
    public partial class RAM3ListPage : Page
    {
        public RAM3ListPage()
        {
            InitializeComponent();
            ListComputerDG.ItemsSource = DBEntities.GetContext().RAM3.ToList()
                .OrderBy(c => c.IdRAM3);
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            RAM3 ram3 = ListComputerDG.SelectedItem as RAM3;
            if (ListComputerDG.SelectedItem == null)
            {
                MBClass.ErrorMB("Выберите ОЗУ" +
                    " для удаления");
            }
            else
            {
                if (MBClass.QestionMB("Удалить " +
                    $"ОЗУ для третьего слота под номером " +
                    $"{ram3.IdRAM3}?"))
                {
                    DBEntities.GetContext().RAM3
                        .Remove(ListComputerDG.SelectedItem as RAM3);
                    DBEntities.GetContext().SaveChanges();

                    MBClass.InformationMB("ОЗУ для третьего слота удалено");
                    ListComputerDG.ItemsSource = DBEntities.GetContext()
                        .RAM3.ToList().OrderBy(u => u.IdRAM3);
                }
            }
        }

        private void Red_Click(object sender, RoutedEventArgs e)
        {
            if (ListComputerDG.SelectedItem == null)
            {
                MBClass.ErrorMB("Выберите " +
                    "ОЗУ для редактирования");
            }
            else
            {
                NavigationService.Navigate(
                    new RAM3EditPage(ListComputerDG.SelectedItem as RAM3));
            }
        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            ListComputerDG.ItemsSource = DBEntities.GetContext()
                .RAM3.Where(u => u.IdRAM3.ToString().StartsWith(SearchTb.Text))
                .ToList().OrderBy(u => u.IdRAM3);
        }

        private void Plus_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new PageFolder.EmployeePageFolder.ComputerComponentsFolder.RAM3Folder.RAM3AddPage());
        }
    }
}
