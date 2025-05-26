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
using DiplomErshov.PageFolder.EmployeePageFolder.PeripheryFolder.GarnitureFolder;

namespace DiplomErshov.PageFolder.EmployeePageFolder.PeripheryFolder.HeadphonesFolder
{
    /// <summary>
    /// Логика взаимодействия для HeadphonesListPage.xaml
    /// </summary>
    public partial class HeadphonesListPage : Page
    {
        public HeadphonesListPage()
        {
            InitializeComponent();
            ListHPDG.ItemsSource = DBEntities.GetContext().Headphones.ToList()
                .OrderBy(c => c.IdHeadphones);
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            Headphones headphones = ListHPDG.SelectedItem as Headphones;
            if (ListHPDG.SelectedItem == null)
            {
                MBClass.ErrorMB("Выберите наушники" +
                    " для удаления");
            }
            else
            {
                if (MBClass.QestionMB("Удалить " +
                    $"наушники под названием " +
                    $"{headphones.NameHeadphones}?"))
                {
                    DBEntities.GetContext().Headphones
                        .Remove(ListHPDG.SelectedItem as Headphones);
                    DBEntities.GetContext().SaveChanges();

                    MBClass.InformationMB("Наушники удалены");
                    ListHPDG.ItemsSource = DBEntities.GetContext()
                        .Headphones.ToList().OrderBy(u => u.NameHeadphones);
                }
            }
        }

        private void Red_Click(object sender, RoutedEventArgs e)
        {
            if (ListHPDG.SelectedItem == null)
            {
                MBClass.ErrorMB("Выберите " +
                    "наушники для редактирования");
            }
            else
            {
                NavigationService.Navigate(
                    new HeadphonesEditPage(ListHPDG.SelectedItem as Headphones));
            }
        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            ListHPDG.ItemsSource = DBEntities.GetContext()
                .Headphones.Where(u => u.NameHeadphones.StartsWith(SearchTb.Text))
                .ToList().OrderBy(u => u.NameHeadphones);
        }

        private void Plus_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new PageFolder.EmployeePageFolder.PeripheryFolder.HeadphonesFolder.HeadphonesAddPage());
        }
    }
}
