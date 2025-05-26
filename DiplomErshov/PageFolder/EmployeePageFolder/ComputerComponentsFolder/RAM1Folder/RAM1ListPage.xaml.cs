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
using DiplomErshov.PageFolder.EmployeePageFolder.ComputersFolder;


namespace DiplomErshov.PageFolder.EmployeePageFolder.ComputerComponentsFolder.RAM1Folder
{
    /// <summary>
    /// Логика взаимодействия для RAM1ListPage.xaml
    /// </summary>
    public partial class RAM1ListPage : Page
    {
        public RAM1ListPage()
        {
            InitializeComponent();
            ListComputerDG.ItemsSource = DBEntities.GetContext().RAM1.ToList()
                .OrderBy(c => c.IdRAM1);
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            RAM1 ram1 = ListComputerDG.SelectedItem as RAM1;
            if (ListComputerDG.SelectedItem == null)
            {
                MBClass.ErrorMB("Выберите ОЗУ" +
                    " для удаления");
            }
            else
            {
                if (MBClass.QestionMB("Удалить " +
                    $"ОЗУ для первого слота под номером " +
                    $"{ram1.IdRAM1}?"))
                {
                    DBEntities.GetContext().RAM1
                        .Remove(ListComputerDG.SelectedItem as RAM1);
                    DBEntities.GetContext().SaveChanges();

                    MBClass.InformationMB("ОЗУ для первого слота удалено");
                    ListComputerDG.ItemsSource = DBEntities.GetContext()
                        .RAM1.ToList().OrderBy(u => u.IdRAM1);
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
                    new RAM1EditPage(ListComputerDG.SelectedItem as RAM1));
            }
        }

        private void Plus_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new PageFolder.EmployeePageFolder.ComputerComponentsFolder.RAM1Folder.RAM1ADDPage());
        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            ListComputerDG.ItemsSource = DBEntities.GetContext()
                .RAM1.Where(u => u.IdRAM1.ToString().StartsWith(SearchTb.Text))
                .ToList().OrderBy(u => u.IdRAM1);
        }
    }
}
