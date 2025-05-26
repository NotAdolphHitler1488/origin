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
using DiplomErshov.PageFolder.EmployeePageFolder.PeripheryFolder.HeadphonesFolder;
using Keyboard = DiplomErshov.DataFolder.Keyboard;

namespace DiplomErshov.PageFolder.EmployeePageFolder.PeripheryFolder.KeyboardFolder
{
    /// <summary>
    /// Логика взаимодействия для KeyboardListPage.xaml
    /// </summary>
    public partial class KeyboardListPage : Page
    {
        public KeyboardListPage()
        {
            InitializeComponent();
            ListKBDG.ItemsSource = DBEntities.GetContext().Keyboard.ToList()
                .OrderBy(c => c.IdKeyboard);
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            Keyboard keyboard = ListKBDG.SelectedItem as Keyboard;
            if (ListKBDG.SelectedItem == null)
            {
                MBClass.ErrorMB("Выберите клавиатуру" +
                    " для удаления");
            }
            else
            {
                if (MBClass.QestionMB("Удалить " +
                    $"клавиатуру под названием " +
                    $"{keyboard.NameKeyboard}?"))
                {
                    DBEntities.GetContext().Keyboard
                        .Remove(ListKBDG.SelectedItem as Keyboard);
                    DBEntities.GetContext().SaveChanges();

                    MBClass.InformationMB("Клавиатура удалена");
                    ListKBDG.ItemsSource = DBEntities.GetContext()
                        .Keyboard.ToList().OrderBy(u => u.NameKeyboard);
                }
            }
        }

        private void Red_Click(object sender, RoutedEventArgs e)
        {
            if (ListKBDG.SelectedItem == null)
            {
                MBClass.ErrorMB("Выберите " +
                    "клавиатуру для редактирования");
            }
            else
            {
                NavigationService.Navigate(
                    new KeyboardEditPage(ListKBDG.SelectedItem as Keyboard));
            }
        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            ListKBDG.ItemsSource = DBEntities.GetContext()
                .Keyboard.Where(u => u.NameKeyboard.StartsWith(SearchTb.Text))
                .ToList().OrderBy(u => u.NameKeyboard);
        }

        private void Plus_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new PageFolder.EmployeePageFolder.PeripheryFolder.KeyboardFolder.KeyboardAddPage());
        }
    }
}
