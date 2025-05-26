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
    /// Логика взаимодействия для RAM1ADDPage.xaml
    /// </summary>
    public partial class RAM1ADDPage : Page
    {
        public RAM1ADDPage()
        {
            InitializeComponent();
            RAMCb.ItemsSource = DBEntities.GetContext()
                .RAM.ToList();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(RAMCb.Text))
            {
                MBClass.ErrorMB("Пожалуйста, выберите оперативную память");
                RAMCb.Focus();
            }

            else
            {
                try
                {
                    DBEntities.GetContext().RAM1.Add(new RAM1()
                    {
                        IdRAM = Int32.Parse(RAMCb.SelectedValue.ToString()),
                    });
                    DBEntities.GetContext().SaveChanges();
                    MBClass.InformationMB("Успешно");
                    NavigationService.Navigate(new RAM1ListPage());
                }
                catch (Exception ex)
                {
                    MBClass.ErrorMB(ex);
                    throw;
                }
            }
        }

        private void Back_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new PageFolder.EmployeePageFolder.ComputerComponentsFolder.RAM1Folder.RAM1ListPage());
        }
    }
}
