using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using System.Web.UI.WebControls;
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
using DiplomErshov.PageFolder.AdministratorPageFolder;

namespace DiplomErshov.PageFolder.EmployeePageFolder.ComputersFolder
{
    /// <summary>
    /// Логика взаимодействия для ComputersAddPage.xaml
    /// </summary>
    public partial class ComputersAddPage : Page
    {
        public ComputersAddPage()
        {
            InitializeComponent();
            CPUCb.ItemsSource = DBEntities.GetContext()
                .CPU.ToList();
            MotherBoardCb.ItemsSource = DBEntities.GetContext()
                .MotherBoard.ToList();
            RAM1Cb.ItemsSource = DBEntities.GetContext()
                .RAM1.ToList();
            RAM2Cb.ItemsSource = DBEntities.GetContext()
                .RAM2.ToList();
            RAM3Cb.ItemsSource = DBEntities.GetContext()
                .RAM3.ToList();
            RAM4Cb.ItemsSource = DBEntities.GetContext()
                .RAM4.ToList();
            GPUCb.ItemsSource = DBEntities.GetContext()
                .GPU.ToList();
            HDDCb.ItemsSource = DBEntities.GetContext()
                .HDD.ToList();
            CPUСoolingCb.ItemsSource = DBEntities.GetContext()
                .CPUСooling.ToList();
            SSDCb.ItemsSource = DBEntities.GetContext()
                .SSD.ToList();
            ComputerCaseCb.ItemsSource = DBEntities.GetContext()
                .ComputerCase.ToList();
            PowerSupplyCb.ItemsSource = DBEntities.GetContext()
                .PowerSupply.ToList();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            var checkSerialNumberComputer = DBEntities.GetContext()
                .Computer.FirstOrDefault(u => u.SerialNumberComputer == SerialNumberComputerTB.Text);
            if (checkSerialNumberComputer != null)
            {
                MBClass.ErrorMB("Такой серийный номер уже существует");
                SerialNumberComputerTB.Focus();
                return;
            }

            else if (string.IsNullOrWhiteSpace(CPUCb.Text))
            {
                MBClass.ErrorMB("Пожалуйста, выберите процессор");
                CPUCb.Focus();
            }

            else if (string.IsNullOrWhiteSpace(MotherBoardCb.Text))
            {
                MBClass.ErrorMB("Пожалуйста, выберите мат. плату");
                MotherBoardCb.Focus();
            }

            else if (string.IsNullOrWhiteSpace(RAM1Cb.Text))
            {
                MBClass.ErrorMB("Пожалуйста, выберите ОЗУ в первый слот");
                RAM1Cb.Focus();
            }

            else if (string.IsNullOrWhiteSpace(GPUCb.Text))
            {
                MBClass.ErrorMB("Пожалуйста, выберите видеокарту");
                GPUCb.Focus();
            }

            else if (string.IsNullOrWhiteSpace(HDDCb.Text))
            {
                MBClass.ErrorMB("Пожалуйста, выберите жесткий диск");
                HDDCb.Focus();
            }

            else if (string.IsNullOrWhiteSpace(CPUСoolingCb.Text))
            {
                MBClass.ErrorMB("Пожалуйста, выберите охлаждение процессора");
                CPUСoolingCb.Focus();
            }

            else if (string.IsNullOrWhiteSpace(ComputerCaseCb.Text))
            {
                MBClass.ErrorMB("Пожалуйста, выберите корпус");
                ComputerCaseCb.Focus();
            }

            else if (string.IsNullOrWhiteSpace(PowerSupplyCb.Text))
            {
                MBClass.ErrorMB("Пожалуйста, выберите блок питания");
                PowerSupplyCb.Focus();
            }

            else if (string.IsNullOrWhiteSpace(DateDP.Text))
            {
                MBClass.ErrorMB("Пожалуйста, выберите срок гарантии");
                DateDP.Focus();
            }

            else if (string.IsNullOrWhiteSpace(SerialNumberComputerTB.Text))
            {
                MBClass.ErrorMB("Пожалуйста, введите серийный номер");
                SerialNumberComputerTB.Focus();
            }

            else
            {
                try
                {
                    Computer computer = new Computer();
                    DBEntities.GetContext().Computer.Add(computer);
                    computer.IdCPU = Int32.Parse(CPUCb.SelectedValue.ToString());
                    computer.IdMotherBoard = Int32.Parse(MotherBoardCb.SelectedValue.ToString());
                    computer.IdRAM1 = Int32.Parse(RAM1Cb.SelectedValue.ToString());
                    computer.IdRAM2 = RAM2Cb.SelectedValue == null ? null : (int?)Convert.ToInt32(RAM2Cb.SelectedValue);
                    computer.IdRAM3 = RAM3Cb.SelectedValue == null ? null : (int?)Convert.ToInt32(RAM3Cb.SelectedValue);
                    computer.IdRAM4 = RAM4Cb.SelectedValue == null ? null : (int?)Convert.ToInt32(RAM4Cb.SelectedValue);
                    computer.IdGPU = Int32.Parse(GPUCb.SelectedValue.ToString());
                    computer.IdHDD = Int32.Parse(HDDCb.SelectedValue.ToString());
                    computer.IdCPUСooling = Int32.Parse(CPUСoolingCb.SelectedValue.ToString());
                    computer.IdSSD = SSDCb.SelectedValue == null ? null : (int?)Convert.ToInt32(SSDCb.SelectedValue);
                    computer.IdComputerCase = Int32.Parse(ComputerCaseCb.SelectedValue.ToString());
                    computer.IdPowerSupply = Int32.Parse(PowerSupplyCb.SelectedValue.ToString());
                    computer.GuaranteeComputer = Convert.ToDateTime(DateDP.SelectedDate);
                    computer.SerialNumberComputer = SerialNumberComputerTB.Text;
                    DBEntities.GetContext().SaveChanges();
                    MBClass.InformationMB("Успешно");
                    NavigationService.Navigate(new ComputersListPage());
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
            NavigationService.Navigate(new PageFolder.EmployeePageFolder.ComputersFolder.ComputersListPage());
        }
    }
}
