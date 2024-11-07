using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using Microsoft.Win32;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Xml;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace LibraryManagement.ViewModels
{
    public class ImportExportViewModel
    {
        private string _selectedImportOption;
        public object SelectedImportOption
        {
            get => _selectedImportOption;
            set
            {
                _selectedImportOption = (value as ComboBoxItem)?.Content.ToString();
                OnPropertyChanged();
                MessageBox.Show($"SelectedImportOption set to: {_selectedImportOption}");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private readonly BookRepository _bookRepository;
        private readonly StudentRepository _studentRepository;

        public ICommand ExportCommand { get; }
        public ICommand ImportCommand { get; }

        public bool ExportBooks { get; set; }
        public bool ExportStudents { get; set; }

        public ImportExportViewModel()
        {
            _bookRepository = new BookRepository(DatabaseConnection.Instance.GetConnection().ToString());
            _studentRepository = new StudentRepository(DatabaseConnection.Instance.GetConnection().ToString());

            ExportCommand = new RelayCommand(ExportData);
            ImportCommand = new RelayCommand(ImportData);
        }

        private void ExportData()
        {
            if (!ExportBooks && !ExportStudents)
            {
                MessageBox.Show("Please select at least one option to export.");
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "JSON files (*.json)|*.json",
                Title = "Save Exported Data"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                using (var writer = new StreamWriter(saveFileDialog.FileName))
                {
                    var exportData = new Dictionary<string, object>();

                    if (ExportBooks)
                    {
                        exportData["Books"] = _bookRepository.GetAllBooks();
                    }

                    if (ExportStudents)
                    {
                        exportData["Students"] = _studentRepository.GetAllStudents();
                    }

                    writer.Write(JsonConvert.SerializeObject(exportData, Newtonsoft.Json.Formatting.Indented));

                }
                MessageBox.Show("Data exported successfully as JSON.");
            }
        }

        private void ImportData()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "JSON files (*.json)|*.json",
                Title = "Open JSON File"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                using (var reader = new StreamReader(openFileDialog.FileName))
                {
                    var jsonData = reader.ReadToEnd();
                    var importData = JsonConvert.DeserializeObject<Dictionary<string, List<dynamic>>>(jsonData);

                    if (importData != null)
                    {
                        // Check the dropdown selection for what to import
                        if (SelectedImportOption.ToString() == "Bücher")
                        {
                            var books = JsonConvert.DeserializeObject<List<Book>>(JsonConvert.SerializeObject(importData["Books"]));
                            foreach (var book in books)
                            {
                                _bookRepository.AddBook(book);
                            }
                            MessageBox.Show("Books imported successfully.");
                        }
                        else if (SelectedImportOption.ToString() == "Schüler")
                        {
                            var students = JsonConvert.DeserializeObject<List<Student>>(JsonConvert.SerializeObject(importData["Students"]));
                            foreach (var student in students)
                            {
                                _studentRepository.AddStudent(student);
                            }
                            MessageBox.Show("Students imported successfully.");
                        }
                        else
                        {
                            MessageBox.Show("No valid data found for the selected import option.");
                        }

                    }
                }
            }
        }


    }
}
