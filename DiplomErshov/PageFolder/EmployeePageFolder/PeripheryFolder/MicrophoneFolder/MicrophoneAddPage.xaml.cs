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
    /// Логика взаимодействия для MicrophoneAddPage.xaml
    /// </summary>
    public partial class MicrophoneAddPage : Page
    {
        public MicrophoneAddPage()
        {
            InitializeComponent();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            var checkSerialNumberMicrophone = DBEntities.GetContext()
                .Microphone.FirstOrDefault(u => u.SerialNumberMicrophone == SerialTB.Text);
            if (checkSerialNumberMicrophone != null)
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
                    DBEntities.GetContext().Microphone.Add(new Microphone()
                    {
                        NameMicrophone = NameTB.Text,
                        SerialNumberMicrophone = SerialTB.Text,
                        GuaranteeMicrophone = Convert.ToDateTime(DateDP.SelectedDate),
                    });
                    DBEntities.GetContext().SaveChanges();
                    MBClass.InformationMB("Успешно");
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
