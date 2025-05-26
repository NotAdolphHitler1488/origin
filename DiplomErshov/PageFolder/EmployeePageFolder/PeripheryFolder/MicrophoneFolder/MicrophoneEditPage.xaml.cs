using DiplomErshov.ClassFolder;
using DiplomErshov.DataFolder;
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
using DiplomErshov.PageFolder.EmployeePageFolder.PeripheryFolder.KeyboardFolder;

namespace DiplomErshov.PageFolder.EmployeePageFolder.PeripheryFolder.MicrophoneFolder
{
    /// <summary>
    /// Логика взаимодействия для MicrophoneEditPage.xaml
    /// </summary>
    public partial class MicrophoneEditPage : Page
    {
        string saveSerial = "";
        private Microphone originalMicrophone;
        public MicrophoneEditPage(Microphone microphone)
        {
            InitializeComponent();
            DBEntities.nullContext();
            DBEntities.nullContext(); originalMicrophone = DBEntities.GetContext().Microphone
                .FirstOrDefault(u => u.IdMicrophone == microphone.IdMicrophone);
            DataContext = microphone;
            this.originalMicrophone.IdMicrophone = microphone.IdMicrophone;
            SerialTB.Text = saveSerial = microphone.SerialNumberMicrophone;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            var checkSerialNumberMicrophone = DBEntities.GetContext()
                            .Microphone.FirstOrDefault(u => u.SerialNumberMicrophone == SerialTB.Text);
            if (checkSerialNumberMicrophone != null && saveSerial != SerialTB.Text)
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
                    originalMicrophone = DBEntities.GetContext().Microphone
                        .FirstOrDefault(u => u.IdMicrophone == originalMicrophone.IdMicrophone);
                    originalMicrophone.NameMicrophone = NameTB.Text;
                    originalMicrophone.SerialNumberMicrophone = SerialTB.Text;
                    originalMicrophone.GuaranteeMicrophone = Convert.ToDateTime(DateDP.SelectedDate);
                    DBEntities.GetContext().SaveChanges();
                    MBClass.InformationMB("Данные успешно отредактированы");
                    NavigationService.Navigate(new MicrophoneListPage());
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
            NavigationService.Navigate(new PageFolder.EmployeePageFolder.PeripheryFolder.MicrophoneFolder.MicrophoneListPage());
        }
    }
}
