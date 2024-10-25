using System;
using System.IO;
using System.Reflection.Metadata;
using System.Windows;
using System.Windows.Documents;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace NoteApp
{
    public partial class MainWindow : Window
    {
        private readonly string filePath = "notes.txt"; // Путь к файлу для хранения заметок

        public MainWindow()
        {
            InitializeComponent();
            LoadNotes();  // Загружаем заметки при старте приложения
        }

        // Метод для загрузки заметок из файла
        private void LoadNotes()
        {
            if (File.Exists(filePath))
            {
                NoteTextBox.Text = File.ReadAllText(filePath);
                StatusLabel.Content = "Заметки загружены.";
            }
            else
            {
                StatusLabel.Content = "Нет сохраненных заметок.";
            }
        }

        // Метод для автосохранения заметок
        private void AutoSaveNotes()
        {
            File.WriteAllText(filePath, NoteTextBox.Text);
            StatusLabel.Content = "Заметки сохранены автоматически.";
        }

        // Обработчик события изменения текста
        private void NoteTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            AutoSaveNotes();  // Автосохранение при каждом изменении текста
        }

        // Экспорт заметок в PDF с использованием библиотеки iTextSharp
        // Экспорт заметок в PDF с использованием библиотеки iTextSharp
        private void ExportToPDF_Click(object sender, RoutedEventArgs e)
        {
            string pdfPath = "notes.pdf";

            try
            {
                // Используем полное имя класса Document из iTextSharp
                iTextSharp.text.Document pdfDoc = new iTextSharp.text.Document();
                PdfWriter.GetInstance(pdfDoc, new FileStream(pdfPath, FileMode.Create));
                pdfDoc.Open();
                pdfDoc.Add(new iTextSharp.text.Paragraph(NoteTextBox.Text));
                pdfDoc.Close();

                StatusLabel.Content = "Заметки экспортированы в PDF.";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка экспорта в PDF: {ex.Message}");
            }
        }

    }
}
