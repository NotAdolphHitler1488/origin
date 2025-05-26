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
using DiplomErshov.PageFolder.EmployeePageFolder.ComputerComponentsFolder.RAM1Folder;


namespace DiplomErshov.PageFolder.EmployeePageFolder.ComputerComponentsFolder.RAM2Folder
{
    /// <summary>
    /// Логика взаимодействия для RAM2ListPage.xaml
    /// </summary>
    public partial class RAM2ListPage : Page
    {
        public RAM2ListPage()
        {
            InitializeComponent();
            ListComputerDG.ItemsSource = DBEntities.GetContext().RAM2.ToList()
                .OrderBy(c => c.IdRAM2);
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
                    new RAM2EditPage(ListComputerDG.SelectedItem as RAM2));
            }
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            RAM2 ram2 = ListComputerDG.SelectedItem as RAM2;
            if (ListComputerDG.SelectedItem == null)
            {
                MBClass.ErrorMB("Выберите ОЗУ" +
                    " для удаления");
            }
            else
            {
                if (MBClass.QestionMB("Удалить " +
                    $"ОЗУ для второго слота под номером " +
                    $"{ram2.IdRAM2}?"))
                {
                    DBEntities.GetContext().RAM2
                        .Remove(ListComputerDG.SelectedItem as RAM2);
                    DBEntities.GetContext().SaveChanges();

                    MBClass.InformationMB("ОЗУ для второго слота удалено");
                    ListComputerDG.ItemsSource = DBEntities.GetContext()
                        .RAM2.ToList().OrderBy(u => u.IdRAM2);
                }
            }
        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            ListComputerDG.ItemsSource = DBEntities.GetContext()
                .RAM2.Where(u => u.IdRAM2.ToString().StartsWith(SearchTb.Text))
                .ToList().OrderBy(u => u.IdRAM2);
        }

        private void Plus_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new PageFolder.EmployeePageFolder.ComputerComponentsFolder.RAM2Folder.RAM2AddPage());
        }
    }
}
