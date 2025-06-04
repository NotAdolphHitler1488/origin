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

namespace DiplomErshov.PageFolder.EmployeePageFolder.EmployeeComputersFolder
{
    /// <summary>
    /// Логика взаимодействия для EmployeeComputersListPage.xaml
    /// </summary>
    public partial class EmployeeComputersListPage : Page
    {
        private readonly List<Computer> allComputers;

        public EmployeeComputersListPage()
        {
            InitializeComponent();

            allComputers = DBEntities.GetContext()
                                     .Computer
                                     .ToList()
                                     .OrderBy(c => c.SerialNumberComputer)
                                     .ToList();

            PopulateList(allComputers);
        }

        private void PopulateList(IEnumerable<Computer> computers)
        {
            ItemsPanel.Children.Clear();

            foreach (var comp in computers)
            {
                var expander = new Expander
                {
                    Header = $"{comp.SerialNumberComputer} — {comp.CPU?.NameCPU ?? "-"}",
                    Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(0x2E, 0x3C, 0x5E)),
                    Foreground = System.Windows.Media.Brushes.White,
                    Margin = new System.Windows.Thickness(0, 0, 0, 8),
                    Padding = new System.Windows.Thickness(8),
                    FontFamily = new System.Windows.Media.FontFamily("Segoe UI"),
                    FontSize = 16
                };

                // Детали внутри Expander
                var grid = new Grid { Margin = new System.Windows.Thickness(0, 8, 0, 0) };
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new System.Windows.GridLength(2, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new System.Windows.GridLength(3, GridUnitType.Star) });

                var leftStack = new StackPanel { Margin = new System.Windows.Thickness(0, 0, 8, 0) };
                leftStack.Children.Add(new TextBlock
                {
                    Text = $"Мат. плата: {comp.MotherBoard?.NameMotherBoard ?? "-"}",
                    Foreground = System.Windows.Media.Brushes.White,
                    FontSize = 14,
                    Margin = new System.Windows.Thickness(0, 2, 0, 2)
                });
                leftStack.Children.Add(new TextBlock
                {
                    Text = $"RAM1: {comp.RAM1?.RAM.NameRAM ?? "-"}",
                    Foreground = System.Windows.Media.Brushes.White,
                    FontSize = 14,
                    Margin = new System.Windows.Thickness(0, 2, 0, 2)
                });
                leftStack.Children.Add(new TextBlock
                {
                    Text = $"RAM2: {comp.RAM2?.RAM.NameRAM ?? "-"}",
                    Foreground = System.Windows.Media.Brushes.White,
                    FontSize = 14,
                    Margin = new System.Windows.Thickness(0, 2, 0, 2)
                });
                leftStack.Children.Add(new TextBlock
                {
                    Text = $"RAM3: {comp.RAM3?.RAM.NameRAM ?? "-"}",
                    Foreground = System.Windows.Media.Brushes.White,
                    FontSize = 14,
                    Margin = new System.Windows.Thickness(0, 2, 0, 2)
                });
                leftStack.Children.Add(new TextBlock
                {
                    Text = $"RAM4: {comp.RAM4?.RAM.NameRAM ?? "-"}",
                    Foreground = System.Windows.Media.Brushes.White,
                    FontSize = 14,
                    Margin = new System.Windows.Thickness(0, 2, 0, 2)
                });
                leftStack.Children.Add(new TextBlock
                {
                    Text = $"GPU: {comp.GPU?.NameGPU ?? "-"}",
                    Foreground = System.Windows.Media.Brushes.White,
                    FontSize = 14,
                    Margin = new System.Windows.Thickness(0, 2, 0, 2)
                });
                leftStack.Children.Add(new TextBlock
                {
                    Text = $"HDD: {comp.HDD?.NameHDD ?? "-"}",
                    Foreground = System.Windows.Media.Brushes.White,
                    FontSize = 14,
                    Margin = new System.Windows.Thickness(0, 2, 0, 2)
                });
                leftStack.Children.Add(new TextBlock
                {
                    Text = $"SSD: {comp.SSD?.NameSSD ?? "-"}",
                    Foreground = System.Windows.Media.Brushes.White,
                    FontSize = 14,
                    Margin = new System.Windows.Thickness(0, 2, 0, 2)
                });
                leftStack.Children.Add(new TextBlock
                {
                    Text = $"Корпус: {comp.ComputerCase?.NameComputerCase ?? "-"}",
                    Foreground = System.Windows.Media.Brushes.White,
                    FontSize = 14,
                    Margin = new System.Windows.Thickness(0, 2, 0, 2)
                });
                leftStack.Children.Add(new TextBlock
                {
                    Text = $"БП: {comp.PowerSupply?.NamePowerSupply ?? "-"}",
                    Foreground = System.Windows.Media.Brushes.White,
                    FontSize = 14,
                    Margin = new System.Windows.Thickness(0, 2, 0, 2)
                });
                leftStack.Children.Add(new TextBlock
                {
                    Text = $"Охлаждение CPU: {comp.CPUСooling?.NameCPUСooling ?? "-"}",
                    Foreground = System.Windows.Media.Brushes.White,
                    FontSize = 14,
                    Margin = new System.Windows.Thickness(0, 2, 0, 2)
                });
                leftStack.Children.Add(new TextBlock
                {
                    Text = $"Гарантия: {comp.GuaranteeComputer:dd/MM/yyyy}",
                    Foreground = System.Windows.Media.Brushes.White,
                    FontSize = 14,
                    Margin = new System.Windows.Thickness(0, 2, 0, 2)
                });

                Grid.SetColumn(leftStack, 0);
                grid.Children.Add(leftStack);

                // Пустая правая колонка (для выравнивания), без кнопок
                Grid.SetColumn(new StackPanel(), 1);
                grid.Children.Add(new StackPanel());

                expander.Content = grid;
                ItemsPanel.Children.Add(expander);
            }
        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            var query = SearchTb.Text?.ToLower() ?? "";
            var filtered = allComputers.Where(c =>
                c.SerialNumberComputer.ToLower().Contains(query) ||
                (c.CPU?.NameCPU.ToLower().Contains(query) ?? false)
            );
            PopulateList(filtered);
        }

    }
}
