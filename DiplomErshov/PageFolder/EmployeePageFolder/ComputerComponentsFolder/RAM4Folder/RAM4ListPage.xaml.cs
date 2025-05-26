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
using DiplomErshov.PageFolder.EmployeePageFolder.ComputerComponentsFolder.RAM3Folder;


namespace DiplomErshov.PageFolder.EmployeePageFolder.ComputerComponentsFolder.RAM4Folder
{
    /// <summary>
    /// Логика взаимодействия для RAM4ListPage.xaml
    /// </summary>
    public partial class RAM4ListPage : Page
    {
        public RAM4ListPage()
        {
            InitializeComponent();
            ListComputerDG.ItemsSource = DBEntities.GetContext().RAM4.ToList()
                .OrderBy(c => c.IdRAM4);
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            RAM4 ram4 = ListComputerDG.SelectedItem as RAM4;
            if (ListComputerDG.SelectedItem == null)
            {
                MBClass.ErrorMB("Выберите ОЗУ" +
                    " для удаления");
            }
            else
            {
                if (MBClass.QestionMB("Удалить " +
                    $"ОЗУ для третьего слота под номером " +
                    $"{ram4.IdRAM4}?"))
                {
                    DBEntities.GetContext().RAM4
                        .Remove(ListComputerDG.SelectedItem as RAM4);
                    DBEntities.GetContext().SaveChanges();

                    MBClass.InformationMB("ОЗУ для четвертого слота удалено");
                    ListComputerDG.ItemsSource = DBEntities.GetContext()
                        .RAM4.ToList().OrderBy(u => u.IdRAM4);
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
                    new RAM4EditPage(ListComputerDG.SelectedItem as RAM4));
            }
        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            ListComputerDG.ItemsSource = DBEntities.GetContext()
                .RAM4.Where(u => u.IdRAM4.ToString().StartsWith(SearchTb.Text))
                .ToList().OrderBy(u => u.IdRAM4);
        }

        private void Plus_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new PageFolder.EmployeePageFolder.ComputerComponentsFolder.RAM4Folder.RAM4AddPage());
        }
    }
}
