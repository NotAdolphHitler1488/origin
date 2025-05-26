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
using DiplomErshov.PageFolder.EmployeePageFolder.ComputerComponentsFolder.PowerSupplyFolder;


namespace DiplomErshov.PageFolder.EmployeePageFolder.ComputerComponentsFolder.MotherBoardFolder
{
    /// <summary>
    /// Логика взаимодействия для MotherBoardListPage.xaml
    /// </summary>
    public partial class MotherBoardListPage : Page
    {
        public MotherBoardListPage()
        {
            InitializeComponent();
            ListMBDG.ItemsSource = DBEntities.GetContext().MotherBoard.ToList()
                .OrderBy(c => c.IdMotherBoard);
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            MotherBoard motherboard = ListMBDG.SelectedItem as MotherBoard;
            if (ListMBDG.SelectedItem == null)
            {
                MBClass.ErrorMB("Выберите мат. плату" +
                    " для удаления");
            }
            else
            {
                if (MBClass.QestionMB("Удалить " +
                    $"мат. мат плату с названием " +
                    $"{motherboard.NameMotherBoard}?"))
                {
                    DBEntities.GetContext().MotherBoard
                        .Remove(ListMBDG.SelectedItem as MotherBoard);
                    DBEntities.GetContext().SaveChanges();

                    MBClass.InformationMB("Мат. плата удалена");
                    ListMBDG.ItemsSource = DBEntities.GetContext()
                        .MotherBoard.ToList().OrderBy(u => u.NameMotherBoard);
                }
            }
        }

        private void Red_Click(object sender, RoutedEventArgs e)
        {
            if (ListMBDG.SelectedItem == null)
            {
                MBClass.ErrorMB("Выберите " +
                    "мат. плату для редактирования");
            }
            else
            {
                NavigationService.Navigate(
                    new MotherBoardEditPage(ListMBDG.SelectedItem as MotherBoard));
            }
        }

        private void Plus_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new PageFolder.EmployeePageFolder.ComputerComponentsFolder.MotherBoardFolder.MotherBoardAddPage());
        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            ListMBDG.ItemsSource = DBEntities.GetContext()
                .MotherBoard.Where(u => u.NameMotherBoard.StartsWith(SearchTb.Text))
                .ToList().OrderBy(u => u.NameMotherBoard);
        }
    }
}
