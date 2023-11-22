using System;
using Book_catalog.Core;
using Book_catalog.MVVM.Model;

namespace Book_catalog.MVVM.ViewModel;

public class ModifyViewModel : ObservableObject
{
    public RelayCommand ModifyCommand { get; set; }
    public RelayCommand ModifyIconCommand { get; set; }
    
    public event EventHandler<BookEventArgs>? BookModified; 
    
    public string ModifyTitle { get; set; }
    public string ModifyAuthor { get; set; }
    public string ModifyYear { get; set; }
    public string ModifyGenre { get; set; }
    public string ModifyShortDescription { get; set; }
    public string ModifyIconPath { get; set; }

    public ModifyViewModel(){}
    public ModifyViewModel(Book book)
    {
        ModifyTitle = book.Title;
        ModifyAuthor = book.Author;
        ModifyYear = book.Year;
        ModifyGenre = book.Genre;
        ModifyShortDescription = book.Description;
        ModifyIconPath = book.IconPath;

        ModifyCommand = new RelayCommand(_ =>
        {
            Book book = new Book(ModifyAuthor, ModifyTitle, ModifyYear,
                ModifyGenre, ModifyIconPath, ModifyShortDescription);
            
            BookModified?.Invoke(this, new BookEventArgs(book));
        });
        
        ModifyIconCommand = new RelayCommand(_ =>
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Text files (*.jpg)|*.jpg|All files (*.*)|*.*";
            
            bool? result = openFileDialog.ShowDialog();
            
            if (result == true)
            {
                string selectedFilePath = openFileDialog.FileName;
                ModifyIconPath = selectedFilePath;
                OnPropertyChanged("ModifyIconPath");
            }
        });
    }
}