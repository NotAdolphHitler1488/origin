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
using DiplomErshov.PageFolder.EmployeePageFolder.PeripheryFolder.ComputerMouseFolder;


namespace DiplomErshov.PageFolder.EmployeePageFolder.PeripheryFolder.GarnitureFolder
{
    /// <summary>
    /// Логика взаимодействия для GarnitureListPage.xaml
    /// </summary>
    public partial class GarnitureListPage : Page
    {
        public GarnitureListPage()
        {
            InitializeComponent();
            ListGarnDG.ItemsSource = DBEntities.GetContext().Garniture.ToList()
                .OrderBy(c => c.IdGarniture);
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            Garniture garniture = ListGarnDG.SelectedItem as Garniture;
            if (ListGarnDG.SelectedItem == null)
            {
                MBClass.ErrorMB("Выберите гарнитуру" +
                    " для удаления");
            }
            else
            {
                if (MBClass.QestionMB("Удалить " +
                    $"гарнитуру под названием " +
                    $"{garniture.NameGarniture}?"))
                {
                    DBEntities.GetContext().Garniture
                        .Remove(ListGarnDG.SelectedItem as Garniture);
                    DBEntities.GetContext().SaveChanges();

                    MBClass.InformationMB("Гарнитура удалена");
                    ListGarnDG.ItemsSource = DBEntities.GetContext()
                        .Garniture.ToList().OrderBy(u => u.NameGarniture);
                }
            }
        }

        private void Red_Click(object sender, RoutedEventArgs e)
        {
            if (ListGarnDG.SelectedItem == null)
            {
                MBClass.ErrorMB("Выберите " +
                    "гарнитуру для редактирования");
            }
            else
            {
                NavigationService.Navigate(
                    new GarnitureEditPage(ListGarnDG.SelectedItem as Garniture));
            }
        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            ListGarnDG.ItemsSource = DBEntities.GetContext()
                .Garniture.Where(u => u.NameGarniture.StartsWith(SearchTb.Text))
                .ToList().OrderBy(u => u.NameGarniture);
        }

        private void Plus_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new PageFolder.EmployeePageFolder.PeripheryFolder.GarnitureFolder.GarnitureAddPage());
        }
    }
}
