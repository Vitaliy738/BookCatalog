using System;
using System.Windows;
using Book_catalog.Core;
using Book_catalog.MVVM.Model;

namespace Book_catalog.MVVM.ViewModel;

public class BookAddViewModel : ObservableObject
{
    public RelayCommand ChooseIcon { get; set; }
    public RelayCommand AddBook { get; set; }
    
    public event EventHandler<BookEventArgs>? BookAdded; 
    
    public string Name { get; set; }
    public string Author { get; set; }
    public string Year { get; set; }
    public string Genre { get; set; }
    public string ShortDescription { get; set; }
    public string IconPath { get; set; }
    
    public BookAddViewModel()
    {
        Name = "NONE";
        Author = "NONE";
        Year = "NONE";
        Genre = "NONE";
        ShortDescription = "NONE";
        IconPath = "../../../../Icons/BookIcon.png";

        ChooseIcon = new RelayCommand(_ =>
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Text files (*.jpg)|*.jpg|All files (*.*)|*.*";
            
            bool? result = openFileDialog.ShowDialog();
            
            if (result == true)
            {
                string selectedFilePath = openFileDialog.FileName;
                IconPath = selectedFilePath;
                OnPropertyChanged("IconPath");
            }
        });

        AddBook = new RelayCommand(_ =>
        {
            if (Name.Length < 3 || Author.Length < 3)
            {
                MessageBox.Show("The title and author must consist of at least 3 letters");
                return;
            }
            
            Book book = new Book(Author, Name, Year, Genre, IconPath, ShortDescription);
            BookAdded?.Invoke(this, new BookEventArgs(book));
        });
    }
    
}