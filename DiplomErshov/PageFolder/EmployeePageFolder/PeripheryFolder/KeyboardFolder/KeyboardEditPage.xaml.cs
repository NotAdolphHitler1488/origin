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
using DiplomErshov.PageFolder.EmployeePageFolder.PeripheryFolder.GarnitureFolder;
using Keyboard = DiplomErshov.DataFolder.Keyboard;

namespace DiplomErshov.PageFolder.EmployeePageFolder.PeripheryFolder.KeyboardFolder
{
    /// <summary>
    /// Логика взаимодействия для KeyboardEditPage.xaml
    /// </summary>
    public partial class KeyboardEditPage : Page
    {
        string saveSerial = "";
        private Keyboard originalKeyboard;
        public KeyboardEditPage(Keyboard keyboard)
        {
            InitializeComponent();
            DBEntities.nullContext();
            DBEntities.nullContext(); originalKeyboard = DBEntities.GetContext().Keyboard
                .FirstOrDefault(u => u.IdKeyboard == keyboard.IdKeyboard);
            DataContext = keyboard;
            this.originalKeyboard.IdKeyboard = keyboard.IdKeyboard;
            SerialTB.Text = saveSerial = keyboard.SerialNumberKeyboard;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            var checkSerialNumberKB = DBEntities.GetContext()
                            .Keyboard.FirstOrDefault(u => u.SerialNumberKeyboard == SerialTB.Text);
            if (checkSerialNumberKB != null && saveSerial != SerialTB.Text)
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
                    originalKeyboard = DBEntities.GetContext().Keyboard
                        .FirstOrDefault(u => u.IdKeyboard == originalKeyboard.IdKeyboard);
                    originalKeyboard.NameKeyboard = NameTB.Text;
                    originalKeyboard.SerialNumberKeyboard = SerialTB.Text;
                    originalKeyboard.GuaranteeKeyboard = Convert.ToDateTime(DateDP.SelectedDate);
                    DBEntities.GetContext().SaveChanges();
                    MBClass.InformationMB("Данные успешно отредактированы");
                    NavigationService.Navigate(new KeyboardListPage());
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
            NavigationService.Navigate(new PageFolder.EmployeePageFolder.PeripheryFolder.KeyboardFolder.KeyboardListPage());
        }
    }
}
