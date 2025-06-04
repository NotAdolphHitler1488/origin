using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using DiplomErshov.ClassFolder;
using DiplomErshov.DataFolder;

namespace DiplomErshov.PageFolder.EmployeePageFolder.OfficeStorageFolder
{
    public partial class OfficeStorageListPage : Page
    {
        private List<OfficeStorage> allItems;

        public OfficeStorageListPage()
        {
            InitializeComponent();
            LoadAllItems();
            PopulateList(allItems);
        }

        private void LoadAllItems()
        {
            allItems = DBEntities.GetContext()
                                 .OfficeStorage
                                 .ToList()
                                 .OrderBy(o => o.IdOfficeStorage)
                                 .ToList();
        }

        private void PopulateList(IEnumerable<OfficeStorage> items)
        {
            ItemsPanel.Children.Clear();

            foreach (var item in items)
            {
                // Создаём Expander с заголовком
                var expander = new Expander
                {
                    Header = $"{item.Computer.SerialNumberComputer} — {item.Staff.LastNameStaff}",
                    Background = new SolidColorBrush(Color.FromRgb(0x2E, 0x3C, 0x5E)),
                    Foreground = Brushes.White,
                    Margin = new Thickness(0, 0, 0, 8),
                    Padding = new Thickness(8),
                    FontFamily = new FontFamily("Segoe UI"),
                    FontSize = 16
                };

                // Основной вертикальный StackPanel внутри Expander
                var mainStack = new StackPanel
                {
                    Orientation = Orientation.Vertical,
                    Margin = new Thickness(0, 8, 0, 0)
                };

                // Добавляем текстовые блоки для отображения комплектующих
                mainStack.Children.Add(new TextBlock
                {
                    Text = $"Клавиатура: {item.Keyboard?.NameKeyboard ?? "-"}",
                    Foreground = Brushes.White,
                    FontSize = 14,
                    Margin = new Thickness(0, 0, 0, 4)
                });

                mainStack.Children.Add(new TextBlock
                {
                    Text = $"Мышь: {item.ComputerMouse?.NameComputerMouse ?? "-"}",
                    Foreground = Brushes.White,
                    FontSize = 14,
                    Margin = new Thickness(0, 0, 0, 4)
                });

                mainStack.Children.Add(new TextBlock
                {
                    Text = $"Монитор: {item.Monitor?.NameMonitor ?? "-"}",
                    Foreground = Brushes.White,
                    FontSize = 14,
                    Margin = new Thickness(0, 0, 0, 4)
                });

                mainStack.Children.Add(new TextBlock
                {
                    Text = $"Принтер: {item.Printer?.NamePrinter ?? "-"}",
                    Foreground = Brushes.White,
                    FontSize = 14,
                    Margin = new Thickness(0, 0, 0, 8)
                });

                // Стек с кнопками (внизу, слева)
                var buttonStack = new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Margin = new Thickness(0, 10, 0, 0)
                };

                // Кнопка "Редактировать"
                var editBtn = new Button
                {
                    Content = "Редактировать",
                    Background = new SolidColorBrush(Color.FromRgb(0x2E, 0x3C, 0x5E)),
                    Foreground = Brushes.White,
                    FontSize = 14,
                    Margin = new Thickness(0, 0, 8, 0),
                    Padding = new Thickness(8, 4, 8, 4),
                    Tag = item
                };
                editBtn.Click += EditBtn_Click;

                // Кнопка "Удалить"
                var delBtn = new Button
                {
                    Content = "Удалить",
                    Background = new SolidColorBrush(Color.FromRgb(0x2E, 0x3C, 0x5E)),
                    Foreground = Brushes.White,
                    FontSize = 14,
                    Padding = new Thickness(8, 4, 8, 4),
                    Tag = item
                };
                delBtn.Click += DelBtn_Click;

                // Добавляем кнопки в стек
                buttonStack.Children.Add(editBtn);
                buttonStack.Children.Add(delBtn);

                // Добавляем кнопки в основной контейнер
                mainStack.Children.Add(buttonStack);

                // Присваиваем контент Expander'у
                expander.Content = mainStack;

                // Добавляем Expander в панель
                ItemsPanel.Children.Add(expander);
            }
        }




        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            var query = SearchTb.Text.ToLower();
            var filtered = allItems.Where(o =>
                o.Computer.SerialNumberComputer.ToLower().Contains(query) ||
                o.Staff.LastNameStaff.ToLower().StartsWith(query)
            );
            PopulateList(filtered);
        }

        private void Plus_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new OfficeStorageAddPage());
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is OfficeStorage item)
            {
                NavigationService.Navigate(new OfficeStorageEditPage(item));
            }
        }

        private void DelBtn_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is OfficeStorage item)
            {
                if (MBClass.QestionMB($"Удалить запись №{item.IdOfficeStorage}?"))
                {
                    DBEntities.GetContext().OfficeStorage.Remove(item);
                    DBEntities.GetContext().SaveChanges();
                    LoadAllItems();
                    PopulateList(allItems);
                }
            }
        }

        // Обработчики меню троеточия
        private void ListComputerMouseBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageFolder.EmployeePageFolder.PeripheryFolder.ComputerMouseFolder.ComputerMouseListPage());
        }

        private void ListGarnitureCoolingBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageFolder.EmployeePageFolder.PeripheryFolder.GarnitureFolder.GarnitureListPage());
        }

        private void ListHeadphonesBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageFolder.EmployeePageFolder.PeripheryFolder.HeadphonesFolder.HeadphonesListPage());
        }

        private void ListKeyboardBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageFolder.EmployeePageFolder.PeripheryFolder.KeyboardFolder.KeyboardListPage());
        }

        private void ListMicrophoneBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageFolder.EmployeePageFolder.PeripheryFolder.MicrophoneFolder.MicrophoneListPage());
        }

        private void ListMonitorBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageFolder.EmployeePageFolder.PeripheryFolder.MonitorFolder.MonitorListPage());
        }

        private void ListPrinterBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageFolder.EmployeePageFolder.PeripheryFolder.PrinterFolder.PrinterListPage());
        }

        private void ListScannerBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageFolder.EmployeePageFolder.PeripheryFolder.ScannerFolder.ScannerListPage());
        }

        private void ListWebCameraBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageFolder.EmployeePageFolder.PeripheryFolder.WebCameraFolder.WebCameraListPage());
        }
    }
}
