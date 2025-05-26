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
using DiplomErshov.PageFolder.EmployeePageFolder.ComputerComponentsFolder.RAM1Folder;


namespace DiplomErshov.PageFolder.EmployeePageFolder.ComputerComponentsFolder.RAM2Folder
{
    /// <summary>
    /// Логика взаимодействия для RAM2EditPage.xaml
    /// </summary>
    public partial class RAM2EditPage : Page
    {
        private RAM2 originalRAM2;
        public RAM2EditPage(RAM2 ram2)
        {
            InitializeComponent();
            DBEntities.nullContext();
            DBEntities.nullContext(); originalRAM2 = DBEntities.GetContext().RAM2
                .FirstOrDefault(u => u.IdRAM2 == ram2.IdRAM2);
            DataContext = ram2;
            this.originalRAM2.IdRAM2 = ram2.IdRAM2;
            RAMCb.ItemsSource = DBEntities.GetContext()
                .RAM.ToList();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                originalRAM2 = DBEntities.GetContext().RAM2
                        .FirstOrDefault(u => u.IdRAM2 == originalRAM2.IdRAM2);
                originalRAM2.IdRAM = Int32.Parse(
                    RAMCb.SelectedValue.ToString());
                DBEntities.GetContext().SaveChanges();
                MBClass.InformationMB("Данные успешно отредактированы");
                NavigationService.Navigate(new RAM2ListPage());
            }
            catch (Exception ex)
            {
                MBClass.ErrorMB(ex);
            }
        }

        private void Back_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new PageFolder.EmployeePageFolder.ComputerComponentsFolder.RAM2Folder.RAM2ListPage());
        }
    }
}
