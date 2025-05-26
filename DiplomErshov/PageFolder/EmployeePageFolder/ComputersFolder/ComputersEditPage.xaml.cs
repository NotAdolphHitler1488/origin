using System;
using System.Collections;
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
    /// Логика взаимодействия для ComputersEditPage.xaml
    /// </summary>
    public partial class ComputersEditPage : Page
    {
        string saveSerial = "";
        Computer Origcomputer = new Computer();
        public ComputersEditPage(Computer computer)
        {
            InitializeComponent();
            DBEntities.nullContext();
            DBEntities.nullContext(); Origcomputer = DBEntities.GetContext().Computer
                .FirstOrDefault(u => u.IdComputer == computer.IdComputer);
            DataContext = computer;
            this.Origcomputer.IdComputer = computer.IdComputer;
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
            RAM1Cb.SelectedValue = computer.RAM1.IdRAM1;
            RAM2Cb.SelectedValue = computer.RAM2.IdRAM2;
            RAM3Cb.SelectedValue = computer.RAM3.IdRAM3;
            RAM4Cb.SelectedValue = computer.RAM4.IdRAM4;
            SerialNumberComputerTB.Text = saveSerial = computer.SerialNumberComputer;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            var checkSerialNumberComputer = DBEntities.GetContext()
                .Computer.FirstOrDefault(u => u.SerialNumberComputer == SerialNumberComputerTB.Text);
            if (checkSerialNumberComputer != null && saveSerial != SerialNumberComputerTB.Text)
            {
                MBClass.ErrorMB("Такой серийный номер уже существует");
                SerialNumberComputerTB.Focus();
                return;
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
                    Origcomputer = DBEntities.GetContext().Computer
                        .FirstOrDefault(u => u.IdComputer == Origcomputer.IdComputer);
                    Origcomputer.IdCPU = Int32.Parse(
                        CPUCb.SelectedValue.ToString());
                    Origcomputer.IdMotherBoard = Int32.Parse(
                        MotherBoardCb.SelectedValue.ToString());
                    Origcomputer.IdRAM1 = Int32.Parse(
                        RAM1Cb.SelectedValue.ToString());
                    Origcomputer.IdRAM2 = Int32.Parse(
                        RAM2Cb.SelectedValue.ToString());
                    Origcomputer.IdRAM3 = Int32.Parse(
                        RAM3Cb.SelectedValue.ToString());
                    Origcomputer.IdRAM4 = Int32.Parse(
                        RAM4Cb.SelectedValue.ToString());
                    Origcomputer.IdGPU = Int32.Parse(
                        GPUCb.SelectedValue.ToString());
                    Origcomputer.IdHDD = Int32.Parse(
                        HDDCb.SelectedValue.ToString());
                    Origcomputer.IdCPUСooling = Int32.Parse(
                        CPUСoolingCb.SelectedValue.ToString());
                    Origcomputer.IdSSD = Int32.Parse(
                        SSDCb.SelectedValue.ToString());
                    Origcomputer.IdComputerCase = Int32.Parse(
                        ComputerCaseCb.SelectedValue.ToString());
                    Origcomputer.IdPowerSupply = Int32.Parse(
                        PowerSupplyCb.SelectedValue.ToString());
                    Origcomputer.GuaranteeComputer = Convert.ToDateTime(DateDP.SelectedDate);
                    Origcomputer.SerialNumberComputer = SerialNumberComputerTB.Text;
                    DBEntities.GetContext().SaveChanges();
                    MBClass.InformationMB("Данные успешно отредактированы");
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
