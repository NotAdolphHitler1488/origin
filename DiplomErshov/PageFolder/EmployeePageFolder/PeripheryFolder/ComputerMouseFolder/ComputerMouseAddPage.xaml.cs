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

namespace DiplomErshov.PageFolder.EmployeePageFolder.PeripheryFolder.ComputerMouseFolder
{
    /// <summary>
    /// Логика взаимодействия для ComputerMouseAddPage.xaml
    /// </summary>
    public partial class ComputerMouseAddPage : Page
    {
        public ComputerMouseAddPage()
        {
            InitializeComponent();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            var checkSerialNumberCM = DBEntities.GetContext()
                .ComputerMouse.FirstOrDefault(u => u.SerialNumberComputerMouse == SerialTB.Text);
            if (checkSerialNumberCM != null)
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

            else if (string.IsNullOrWhiteSpace(NameTB.Text))
            {
                MBClass.ErrorMB("Пожалуйста, введите название");
                NameTB.Focus();
            }

            else if (string.IsNullOrWhiteSpace(DateDP.Text))
            {
                MBClass.ErrorMB("Пожалуйста, выберете дату гарантии");
                DateDP.Focus();
            }
            else
            {
                try
                {
                    DBEntities.GetContext().ComputerMouse.Add(new ComputerMouse()
                    {
                        NameComputerMouse = NameTB.Text,
                        SerialNumberComputerMouse = SerialTB.Text,
                        GuaranteeComputerMouse = Convert.ToDateTime(DateDP.SelectedDate),
                    });
                    DBEntities.GetContext().SaveChanges();
                    MBClass.InformationMB("Успешно");
                    NavigationService.Navigate(new ComputerMouseListPage());
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
            NavigationService.Navigate(new PageFolder.EmployeePageFolder.PeripheryFolder.ComputerMouseFolder.ComputerMouseListPage());
        }
    }
}
