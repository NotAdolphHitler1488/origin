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
    /// Логика взаимодействия для RAM4EditPage.xaml
    /// </summary>
    public partial class RAM4EditPage : Page
    {
        private RAM4 originalRAM4;
        public RAM4EditPage(RAM4 ram4)
        {
            InitializeComponent();
            DBEntities.nullContext();
            DBEntities.nullContext(); originalRAM4 = DBEntities.GetContext().RAM4
                .FirstOrDefault(u => u.IdRAM4 == ram4.IdRAM4);
            DataContext = ram4;
            this.originalRAM4.IdRAM4 = ram4.IdRAM4;
            RAMCb.ItemsSource = DBEntities.GetContext()
                .RAM.ToList();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                originalRAM4 = DBEntities.GetContext().RAM4
                        .FirstOrDefault(u => u.IdRAM4 == originalRAM4.IdRAM4);
                originalRAM4.IdRAM = Int32.Parse(
                    RAMCb.SelectedValue.ToString());
                DBEntities.GetContext().SaveChanges();
                MBClass.InformationMB("Данные успешно отредактированы");
                NavigationService.Navigate(new RAM4ListPage());
            }
            catch (Exception ex)
            {
                MBClass.ErrorMB(ex);
            }
        }

        private void Back_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new PageFolder.EmployeePageFolder.ComputerComponentsFolder.RAM4Folder.RAM4ListPage());
        }
    }
}
