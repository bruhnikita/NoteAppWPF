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
        private readonly string filePath = "notes.txt"; 

        public MainWindow()
        {
            InitializeComponent();
            LoadNotes();  
        }

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

        private void AutoSaveNotes()
        {
            File.WriteAllText(filePath, NoteTextBox.Text);
            StatusLabel.Content = "Заметки сохранены автоматически.";
        }

        private void NoteTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            AutoSaveNotes();  
        }

        private void ExportToPDF_Click(object sender, RoutedEventArgs e)
        {
            string pdfPath = "notes.pdf";

            try
            {
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
