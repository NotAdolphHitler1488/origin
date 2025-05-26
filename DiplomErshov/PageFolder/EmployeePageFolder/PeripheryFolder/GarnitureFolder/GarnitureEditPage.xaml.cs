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
    /// Логика взаимодействия для GarnitureEditPage.xaml
    /// </summary>
    public partial class GarnitureEditPage : Page
    {
        string saveSerial = "";
        private Garniture originalGarniture;
        public GarnitureEditPage(Garniture garniture)
        {
            InitializeComponent();
            DBEntities.nullContext();
            DBEntities.nullContext(); originalGarniture = DBEntities.GetContext().Garniture
                .FirstOrDefault(u => u.IdGarniture == garniture.IdGarniture);
            DataContext = garniture;
            this.originalGarniture.IdGarniture = garniture.IdGarniture;
            SerialTB.Text = saveSerial = garniture.SerialNumberGarniture;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            var checkSerialNumberG = DBEntities.GetContext()
                            .Garniture.FirstOrDefault(u => u.SerialNumberGarniture == SerialTB.Text);
            if (checkSerialNumberG != null && saveSerial != SerialTB.Text)
            {
                MBClass.ErrorMB("Такой серийный номер уже существует");
                SerialTB.Focus();
                return;
            }

            else if (string.IsNullOrWhiteSpace(SerialTB.Text))
            {
                MBClass.ErrorMB("Пожалуйста, введите серийный номер");
                SerialTB.Focus();
            }

            else
            {
                try
                {
                    originalGarniture = DBEntities.GetContext().Garniture
                        .FirstOrDefault(u => u.IdGarniture == originalGarniture.IdGarniture);
                    originalGarniture.NameGarniture = NameTB.Text;
                    originalGarniture.SerialNumberGarniture = SerialTB.Text;
                    originalGarniture.GaranteeGarniture = Convert.ToDateTime(DateDP.SelectedDate);
                    DBEntities.GetContext().SaveChanges();
                    MBClass.InformationMB("Данные успешно отредактированы");
                    NavigationService.Navigate(new GarnitureListPage());
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
            NavigationService.Navigate(new PageFolder.EmployeePageFolder.PeripheryFolder.GarnitureFolder.GarnitureListPage());
        }
    }
}
