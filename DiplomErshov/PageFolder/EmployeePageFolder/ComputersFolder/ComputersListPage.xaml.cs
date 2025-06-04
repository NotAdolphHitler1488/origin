using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DiplomErshov.ClassFolder;
using DiplomErshov.DataFolder;

namespace DiplomErshov.PageFolder.EmployeePageFolder.ComputersFolder
{
    /// <summary>
    /// Логика взаимодействия для ComputersListPage.xaml
    /// </summary>
    public partial class ComputersListPage : Page
    {
        private readonly List<Computer> allComputers;

        public ComputersListPage()
        {
            InitializeComponent();

            // Загружаем все компьютеры из БД, сортируем по серийному номеру
            allComputers = DBEntities.GetContext()
                                     .Computer
                                     .ToList()
                                     .OrderBy(c => c.SerialNumberComputer)
                                     .ToList();

            PopulateList(allComputers);
        }

        /// <summary>
        /// Заполняет ItemsPanel набором Expander-элементов на основании переданного списка компьютеров.
        /// </summary>
        private void PopulateList(IEnumerable<Computer> computers)
        {
            ItemsPanel.Children.Clear();

            foreach (var comp in computers)
            {
                var expander = new Expander
                {
                    Header = $"{comp.SerialNumberComputer} — {comp.CPU?.NameCPU ?? "-"}",
                    Background = new System.Windows.Media.SolidColorBrush(
                        System.Windows.Media.Color.FromRgb(0x2E, 0x3C, 0x5E)),
                    Foreground = System.Windows.Media.Brushes.White,
                    Margin = new Thickness(0, 0, 0, 8),
                    Padding = new Thickness(8),
                    FontFamily = new System.Windows.Media.FontFamily("Segoe UI"),
                    FontSize = 16
                };

                var grid = new Grid { Margin = new Thickness(0, 8, 0, 0) };
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(3, GridUnitType.Star) });

                var leftStack = new StackPanel { Margin = new Thickness(0, 0, 8, 0) };

                leftStack.Children.Add(new TextBlock
                {
                    Text = $"Мат. плата: {comp.MotherBoard?.NameMotherBoard ?? "-"}",
                    Foreground = System.Windows.Media.Brushes.White,
                    FontSize = 14,
                    Margin = new Thickness(0, 2, 0, 2)
                });
                leftStack.Children.Add(new TextBlock
                {
                    Text = $"RAM1: {comp.RAM1?.RAM.NameRAM ?? "-"}",
                    Foreground = System.Windows.Media.Brushes.White,
                    FontSize = 14,
                    Margin = new Thickness(0, 2, 0, 2)
                });
                leftStack.Children.Add(new TextBlock
                {
                    Text = $"RAM2: {comp.RAM2?.RAM.NameRAM ?? "-"}",
                    Foreground = System.Windows.Media.Brushes.White,
                    FontSize = 14,
                    Margin = new Thickness(0, 2, 0, 2)
                });
                leftStack.Children.Add(new TextBlock
                {
                    Text = $"RAM3: {comp.RAM3?.RAM.NameRAM ?? "-"}",
                    Foreground = System.Windows.Media.Brushes.White,
                    FontSize = 14,
                    Margin = new Thickness(0, 2, 0, 2)
                });
                leftStack.Children.Add(new TextBlock
                {
                    Text = $"RAM4: {comp.RAM4?.RAM.NameRAM ?? "-"}",
                    Foreground = System.Windows.Media.Brushes.White,
                    FontSize = 14,
                    Margin = new Thickness(0, 2, 0, 2)
                });
                leftStack.Children.Add(new TextBlock
                {
                    Text = $"GPU: {comp.GPU?.NameGPU ?? "-"}",
                    Foreground = System.Windows.Media.Brushes.White,
                    FontSize = 14,
                    Margin = new Thickness(0, 2, 0, 2)
                });
                leftStack.Children.Add(new TextBlock
                {
                    Text = $"HDD: {comp.HDD?.NameHDD ?? "-"}",
                    Foreground = System.Windows.Media.Brushes.White,
                    FontSize = 14,
                    Margin = new Thickness(0, 2, 0, 2)
                });
                leftStack.Children.Add(new TextBlock
                {
                    Text = $"SSD: {comp.SSD?.NameSSD ?? "-"}",
                    Foreground = System.Windows.Media.Brushes.White,
                    FontSize = 14,
                    Margin = new Thickness(0, 2, 0, 2)
                });
                leftStack.Children.Add(new TextBlock
                {
                    Text = $"Корпус: {comp.ComputerCase?.NameComputerCase ?? "-"}",
                    Foreground = System.Windows.Media.Brushes.White,
                    FontSize = 14,
                    Margin = new Thickness(0, 2, 0, 2)
                });
                leftStack.Children.Add(new TextBlock
                {
                    Text = $"БП: {comp.PowerSupply?.NamePowerSupply ?? "-"}",
                    Foreground = System.Windows.Media.Brushes.White,
                    FontSize = 14,
                    Margin = new Thickness(0, 2, 0, 2)
                });
                leftStack.Children.Add(new TextBlock
                {
                    Text = $"Охлаждение CPU: {comp.CPUСooling?.NameCPUСooling ?? "-"}",
                    Foreground = System.Windows.Media.Brushes.White,
                    FontSize = 14,
                    Margin = new Thickness(0, 2, 0, 2)
                });
                leftStack.Children.Add(new TextBlock
                {
                    Text = $"Гарантия: {comp.GuaranteeComputer:dd/MM/yyyy}",
                    Foreground = System.Windows.Media.Brushes.White,
                    FontSize = 14,
                    Margin = new Thickness(0, 2, 0, 2)
                });

                var buttonPanel = new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    Margin = new Thickness(0, 8, 0, 0)
                };

                var editBtn = new Button
                {
                    Content = "Редактировать",
                    Background = new System.Windows.Media.SolidColorBrush(
                        System.Windows.Media.Color.FromRgb(0x2E, 0x3C, 0x5E)),
                    Foreground = System.Windows.Media.Brushes.White,
                    FontSize = 14,
                    Margin = new Thickness(0, 0, 8, 0),
                    Padding = new Thickness(8, 4, 8, 4),
                    Tag = comp
                };
                editBtn.Click += EditBtn_Click;
                buttonPanel.Children.Add(editBtn);

                var deleteBtn = new Button
                {
                    Content = "Удалить",
                    Background = new System.Windows.Media.SolidColorBrush(
                        System.Windows.Media.Color.FromRgb(0x2E, 0x3C, 0x5E)),
                    Foreground = System.Windows.Media.Brushes.White,
                    FontSize = 14,
                    Margin = new Thickness(0),
                    Padding = new Thickness(8, 4, 8, 4),
                    Tag = comp
                };
                deleteBtn.Click += DeleteBtn_Click;
                buttonPanel.Children.Add(deleteBtn);

                leftStack.Children.Add(buttonPanel);

                Grid.SetColumn(leftStack, 0);
                grid.Children.Add(leftStack);

                var empty = new StackPanel();
                Grid.SetColumn(empty, 1);
                grid.Children.Add(empty);

                expander.Content = grid;
                ItemsPanel.Children.Add(expander);
            }
        }

        /// <summary>
        /// Фильтрация списка компьютеров при вводе текста в SearchTb
        /// </summary>
        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            var query = SearchTb.Text?.ToLower() ?? "";
            var filtered = allComputers.Where(c =>
                c.SerialNumberComputer.ToLower().Contains(query) ||
                (c.CPU?.NameCPU.ToLower().Contains(query) ?? false)
            );
            PopulateList(filtered);
        }

        /// <summary>
        /// Переход на страницу добавления компьютера
        /// </summary>
        private void Plus_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new ComputersAddPage());
        }

        /// <summary>
        /// Переход на страницу редактирования данного компьютера
        /// </summary>
        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is Computer comp)
            {
                NavigationService.Navigate(new ComputersEditPage(comp));
            }
        }

        /// <summary>
        /// Удаление компьютера из базы с перезаполнением списка
        /// </summary>
        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is Computer comp)
            {
                var result = MBClass.QestionMB(
                    $"Удалить компьютер с серийным номером {comp.SerialNumberComputer}?");
                if (result)
                {
                    try
                    {
                        DBEntities.GetContext().Computer.Remove(comp);
                        DBEntities.GetContext().SaveChanges();
                        MBClass.InformationMB("Компьютер удален");
                        allComputers.Remove(comp);
                        PopulateList(allComputers);
                    }
                    catch
                    {
                        MBClass.ErrorMB("Невозможно удалить: компьютер используется.");
                    }
                }
            }
        }

        #region Навигация по спискам комплектующих

        private void ListCPUBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ComputerComponentsFolder.CPUFolder.CPUListPage());
        }

        private void ListCPUCoolingBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ComputerComponentsFolder.CPUCoolingFolder.CPUCoolingListPage());
        }

        private void ListGPUBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ComputerComponentsFolder.GPUFolder.GPUListPage());
        }

        private void ListHDDBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ComputerComponentsFolder.HDDFolder.HDDListPage());
        }

        private void ListMotherBoardBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ComputerComponentsFolder.MotherBoardFolder.MotherBoardListPage());
        }

        private void ListComputerCaseBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ComputerComponentsFolder.ComputerCaseFolder.ComputerCaseListPage());
        }

        private void ListPowerSupplyBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ComputerComponentsFolder.PowerSupplyFolder.PowerSupplyListPage());
        }

        private void ListRAM1Btn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ComputerComponentsFolder.RAM1Folder.RAM1ListPage());
        }

        private void ListRAM2Btn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ComputerComponentsFolder.RAM2Folder.RAM2ListPage());
        }

        private void ListRAM3Btn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ComputerComponentsFolder.RAM3Folder.RAM3ListPage());
        }

        private void ListRAM4Btn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ComputerComponentsFolder.RAM4Folder.RAM4ListPage());
        }

        private void ListRAMBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ComputerComponentsFolder.RAMFolder.RAMListPage());
        }

        private void ListSSDBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ComputerComponentsFolder.SSDFolder.SSDListPage());
        }

        #endregion
    }
}
