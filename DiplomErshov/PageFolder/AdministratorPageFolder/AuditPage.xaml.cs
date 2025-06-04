using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DiplomErshov.DataFolder;
using Microsoft.Win32;
using System.IO;
using ClosedXML.Excel;
using System;




namespace DiplomErshov.PageFolder.AdministratorPageFolder
{
    /// <summary>
    /// Логика взаимодействия для AuditPage.xaml
    /// </summary>
    public partial class AuditPage : Page
    {
        public AuditPage()
        {
            InitializeComponent();
            LoadAuditData();
        }

        private void LoadAuditData()
        {
            AuditDataGrid.ItemsSource = DBEntities.GetContext().UserAudit
                .OrderByDescending(a => a.ChangeDate)
                .ToList();
        }
        private void MenuItem_Export_Click(object sender, RoutedEventArgs e)
        {
            if (AuditDataGrid.Items.Count == 0)
            {
                MessageBox.Show("Нет данных для экспорта.", "Экспорт", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel Workbook (*.xlsx)|*.xlsx",
                FileName = "AuditExport.xlsx"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("Аудит");

                        // Заголовки
                        worksheet.Cell(1, 1).Value = "ID Пользователя";
                        worksheet.Cell(1, 2).Value = "Логин";
                        worksheet.Cell(1, 3).Value = "Роль";
                        worksheet.Cell(1, 4).Value = "Действие";
                        worksheet.Cell(1, 5).Value = "Дата изменения";

                        int row = 2;

                        foreach (var item in AuditDataGrid.Items)
                        {
                            dynamic data = item;
                            worksheet.Cell(row, 1).Value = data.IdUser;
                            worksheet.Cell(row, 2).Value = data.LoginUser;
                            worksheet.Cell(row, 3).Value = data.IdRole;
                            worksheet.Cell(row, 4).Value = data.Action;
                            worksheet.Cell(row, 5).Value = data.ChangeDate.ToString("dd.MM.yyyy HH:mm:ss");
                            row++;
                        }

                        workbook.SaveAs(saveFileDialog.FileName);
                        MessageBox.Show("Экспорт успешно завершен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при экспорте: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = SearchTb.Text.Trim().ToLower();

            AuditDataGrid.ItemsSource = DBEntities.GetContext().UserAudit
                .Where(a => a.LoginUser.ToLower().Contains(searchText)
                         || a.Action.ToLower().Contains(searchText)
                         || a.ChangeDate.ToString().Contains(searchText))
                .OrderByDescending(a => a.ChangeDate)
                .ToList();
        }
    }
}
