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
using DiplomErshov.PageFolder.EmployeePageFolder.ComputerComponentsFolder.PowerSupplyFolder;


namespace DiplomErshov.PageFolder.EmployeePageFolder.ComputerComponentsFolder.MotherBoardFolder
{
    /// <summary>
    /// Логика взаимодействия для MotherBoardAddPage.xaml
    /// </summary>
    public partial class MotherBoardAddPage : Page
    {
        public MotherBoardAddPage()
        {
            InitializeComponent();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            var checkSerialNumberMB = DBEntities.GetContext()
                .MotherBoard.FirstOrDefault(u => u.SerialNumberMotherBoard == SerialTB.Text);

            if (checkSerialNumberMB != null)
            {
                MBClass.ErrorMB("Такой серийный номер уже существует");
                SerialTB.Focus();
            }

            else if (string.IsNullOrWhiteSpace(SocketTB.Text))
            {
                MBClass.ErrorMB("Пожалуйста, введите сокет");
                SocketTB.Focus();
            }

            else if (string.IsNullOrWhiteSpace(NameTB.Text))
            {
                MBClass.ErrorMB("Пожалуйста, введите название");
                NameTB.Focus();
            }
            else
            {
                try
                {
                    DBEntities.GetContext().MotherBoard.Add(new MotherBoard()
                    {
                        NameMotherBoard = NameTB.Text,
                        SocketMotherBoard = SocketTB.Text,
                        SerialNumberMotherBoard = SerialTB.Text,
                    });
                    DBEntities.GetContext().SaveChanges();
                    MBClass.InformationMB("Успешно");
                    NavigationService.Navigate(new MotherBoardListPage());
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
            NavigationService.Navigate(new PageFolder.EmployeePageFolder.ComputerComponentsFolder.MotherBoardFolder.MotherBoardListPage());
        }
    }
}
