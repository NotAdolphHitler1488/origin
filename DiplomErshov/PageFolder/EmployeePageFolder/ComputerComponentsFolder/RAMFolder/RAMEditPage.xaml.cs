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


namespace DiplomErshov.PageFolder.EmployeePageFolder.ComputerComponentsFolder.RAMFolder
{
    /// <summary>
    /// Логика взаимодействия для RAMEditPage.xaml
    /// </summary>
    public partial class RAMEditPage : Page
    {
        string saveSerial = "";
        private RAM originalRAM;
        public RAMEditPage(RAM ram)
        {
            InitializeComponent();
            DBEntities.nullContext();
            DBEntities.nullContext(); originalRAM = DBEntities.GetContext().RAM
                .FirstOrDefault(u => u.IdRAM == ram.IdRAM);
            DataContext = ram;
            this.originalRAM.IdRAM = ram.IdRAM;
            TypeRAMCb.ItemsSource = DBEntities.GetContext()
                .TypeOfRAM.ToList();
            SeriesRAMTB.Text = saveSerial = ram.SerialNumberRAM;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            var checkSerialNumberRAM = DBEntities.GetContext()
                            .RAM.FirstOrDefault(u => u.SerialNumberRAM == SeriesRAMTB.Text);
            if (checkSerialNumberRAM != null && saveSerial != SeriesRAMTB.Text)
            {
                MBClass.ErrorMB("Такой серийный номер уже существует");
                SeriesRAMTB.Focus();
                return;
            }

            else if (string.IsNullOrWhiteSpace(SeriesRAMTB.Text))
            {
                MBClass.ErrorMB("Пожалуйста, введите серийный номер");
                SeriesRAMTB.Focus();
            }

            else
            {
                try
                {
                    originalRAM = DBEntities.GetContext().RAM
                        .FirstOrDefault(u => u.IdRAM == originalRAM.IdRAM);
                    originalRAM.NameRAM = NameRAMTB.Text;
                    originalRAM.IdTypeOfRAM = Int32.Parse(
                        TypeRAMCb.SelectedValue.ToString());
                    originalRAM.SerialNumberRAM = SeriesRAMTB.Text;
                    DBEntities.GetContext().SaveChanges();
                    MBClass.InformationMB("Данные успешно отредактированы");
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
