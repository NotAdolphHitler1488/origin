using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DiplomErshov.ClassFolder;
using DiplomErshov.DataFolder;
using DiplomErshov.PageFolder.EmployeePageFolder.ComputerComponentsFolder.RAM1Folder;
using DiplomErshov.PageFolder.EmployeePageFolder.ComputerComponentsFolder.RAM4Folder;


namespace DiplomErshov.PageFolder.EmployeePageFolder.ComputerComponentsFolder.RAMFolder
{
    /// <summary>
    /// Логика взаимодействия для RAMAddPage.xaml
    /// </summary>
    public partial class RAMAddPage : Page
    {
        RAM ram = new RAM();
        public RAMAddPage()
        {
            InitializeComponent();
            RAMCb.ItemsSource = DBEntities.GetContext()
                .TypeOfRAM.ToList();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameRAMTB.Text))
            {
                MBClass.ErrorMB("Пожалуйста, введите название ОЗУ");
                NameRAMTB.Focus();
            }

            else if (string.IsNullOrWhiteSpace(SeriesRAMTB.Text))
            {
                MBClass.ErrorMB("Пожалуйста, введите серийный номер ОЗУ");
                SeriesRAMTB.Focus();
            }
            else
            {
                try
                {
                    DBEntities.GetContext().RAM.Add(new RAM()
                    {
                        NameRAM = NameRAMTB.Text,
                        IdTypeOfRAM = Int32.Parse(RAMCb.SelectedValue.ToString()),
                        SerialNumberRAM = SeriesRAMTB.Text,
                    });
                    DBEntities.GetContext().SaveChanges();
                    MBClass.InformationMB("Успешно");
                    NavigationService.Navigate(new RAMListPage());
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
            NavigationService.Navigate(new PageFolder.EmployeePageFolder.ComputerComponentsFolder.RAMFolder.RAMListPage());
        }
    }
}
