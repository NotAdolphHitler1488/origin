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
using DiplomErshov.PageFolder.EmployeePageFolder.PeripheryFolder.KeyboardFolder;

namespace DiplomErshov.PageFolder.EmployeePageFolder.PeripheryFolder.MicrophoneFolder
{
    /// <summary>
    /// Логика взаимодействия для MicrophoneListPage.xaml
    /// </summary>
    public partial class MicrophoneListPage : Page
    {
        public MicrophoneListPage()
        {
            InitializeComponent();
            ListMicroDG.ItemsSource = DBEntities.GetContext().Microphone.ToList()
                .OrderBy(c => c.IdMicrophone);
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            Microphone microphone = ListMicroDG.SelectedItem as Microphone;
            if (ListMicroDG.SelectedItem == null)
            {
                MBClass.ErrorMB("Выберите микрофон" +
                    " для удаления");
            }
            else
            {
                if (MBClass.QestionMB("Удалить " +
                    $"микрофон под названием " +
                    $"{microphone.NameMicrophone}?"))
                {
                    DBEntities.GetContext().Microphone
                        .Remove(ListMicroDG.SelectedItem as Microphone);
                    DBEntities.GetContext().SaveChanges();

                    MBClass.InformationMB("Микрофон удален");
                    ListMicroDG.ItemsSource = DBEntities.GetContext()
                        .Microphone.ToList().OrderBy(u => u.NameMicrophone);
                }
            }
        }

        private void Red_Click(object sender, RoutedEventArgs e)
        {
            if (ListMicroDG.SelectedItem == null)
            {
                MBClass.ErrorMB("Выберите " +
                    "клавиатуру для редактирования");
            }
            else
            {
                NavigationService.Navigate(
                    new MicrophoneEditPage(ListMicroDG.SelectedItem as Microphone));
            }
        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            ListMicroDG.ItemsSource = DBEntities.GetContext()
                .Microphone.Where(u => u.NameMicrophone.StartsWith(SearchTb.Text))
                .ToList().OrderBy(u => u.NameMicrophone);
        }

        private void Plus_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new PageFolder.EmployeePageFolder.PeripheryFolder.MicrophoneFolder.MicrophoneAddPage());
        }
    }
}
