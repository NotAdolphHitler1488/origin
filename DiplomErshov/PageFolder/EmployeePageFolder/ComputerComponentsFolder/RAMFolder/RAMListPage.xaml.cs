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
using DiplomErshov.PageFolder.EmployeePageFolder.ComputerComponentsFolder.SSDFolder;



namespace DiplomErshov.PageFolder.EmployeePageFolder.ComputerComponentsFolder.RAMFolder
{
    /// <summary>
    /// Логика взаимодействия для RAMListPage.xaml
    /// </summary>
    public partial class RAMListPage : Page
    {
        public RAMListPage()
        {
            InitializeComponent();
            ListRAMDG.ItemsSource = DBEntities.GetContext().RAM.ToList()
                .OrderBy(c => c.IdRAM);
        }

        private void Plus_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new PageFolder.EmployeePageFolder.ComputerComponentsFolder.RAMFolder.RAMAddPage());
        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            ListRAMDG.ItemsSource = DBEntities.GetContext()
                .RAM.Where(u => u.NameRAM.StartsWith(SearchTb.Text))
                .ToList().OrderBy(u => u.NameRAM);
        }

        private void Red_Click(object sender, RoutedEventArgs e)
        {
            if (ListRAMDG.SelectedItem == null)
            {
                MBClass.ErrorMB("Выберите " +
                    "RAM для редактирования");
            }
            else
            {
                NavigationService.Navigate(
                    new RAMEditPage(ListRAMDG.SelectedItem as RAM));
            }
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            RAM ram = ListRAMDG.SelectedItem as RAM;
            if (ListRAMDG.SelectedItem == null)
            {
                MBClass.ErrorMB("Выберите RAM" +
                    " для удаления");
            }
            else
            {
                if (MBClass.QestionMB("Удалить " +
                    $"RAM с названием " +
                    $"{ram.NameRAM}?"))
                {
                    DBEntities.GetContext().RAM
                        .Remove(ListRAMDG.SelectedItem as RAM);
                    DBEntities.GetContext().SaveChanges();

                    MBClass.InformationMB("RAM удален");
                    ListRAMDG.ItemsSource = DBEntities.GetContext()
                        .RAM.ToList().OrderBy(u => u.NameRAM);
                }
            }
        }
    }
}
