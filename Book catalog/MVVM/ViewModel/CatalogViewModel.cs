using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows;
using System.Windows.Input;
using Book_catalog.Core;
using CatalogLogic;

namespace Book_catalog.MVVM.ViewModel;

public class CatalogViewModel : ObservableObject
{
    public RelayCommand NameFilterCommand { get; set; }
    public RelayCommand AuthorFilterCommand { get; set; }
    public RelayCommand MultiFilterCommand { get; set; }
    
    // Колекція усіх книжок католога
    public ObservableCollection<Book> Books { get; set; }
    
    public DataTable Bookss { get; set; }

    // Значення для фільтрації за ім'ям
    private string _nameSearch;
    public string? NameSearch
    {
        get => _nameSearch;
        set
        {
            if (value != null)
            {
                _nameSearch = value;
                OnPropertyChanged("NameSearch");
            }
        }
    }

    // Значення для фільтрації за автором
    private string _authorSearch;
    public string? AuthorSearch
    {
        get => _authorSearch;
        set
        {
            if (value != null)
            {
                _authorSearch = value;
                OnPropertyChanged("AuthorSearch");
            }
        }
    }

    // Значення для філтрації за жанром
    private string _genreSearch;
    public string? GenreSearch
    {
        get => _genreSearch;
        set
        {
            if (value != null)
            {
                _genreSearch = value;
                OnPropertyChanged("GenreSearch");
            }
        }
    }

    // Значення для фільтрації за роком видання
    private string _yearSearch;
    public string? YearSearch
    {
        get => _yearSearch;
        set
        {
            if (value != null)
            {
                _yearSearch = value;
                OnPropertyChanged("YearSearch");
            }
        }
    }
    
    public CatalogViewModel()
    {
        List<string> list = new List<string>();
        list.Add("1");
        list.Add("2");
        list.Add("3");
        
        Books = new ObservableCollection<Book>
        {
            new Book(),
            new Book(),
            new Book(),
            new Book(),
            new Book(),
            new Book(),
            new Book(),
            new Book(),
            new Book(),
            new Book("Petre", "Religion", "1456", list),
            new Book("Vlad", "Echo", "2003"),
            new Book("Vitaliy", "Oleg", "2023"),
            new Book("Vitaliy", "Oleg - 2", "2023"),
            new Book("Anna", "Silent"),
            new Book("Vitaliy", "Danil voice")
        };

        Bookss = new DataTable();

        // Визначення структури DataTable
        Bookss.Columns.Add("Author", typeof(string));
        Bookss.Columns.Add("Name", typeof(string));
        Bookss.Columns.Add("Year", typeof(string));
        Bookss.Columns.Add("Genre", typeof(string));
        Bookss.Columns.Add("IconPath", typeof(string));

        // Додавання даних до DataTable
        foreach (var book in Books)
        {
            Bookss.Rows.Add(book.Author, book.Name, book.Year, book.Genre, book.IconPath);
        }

        NameFilterCommand = new RelayCommand(_ =>
        {
            if (!string.IsNullOrEmpty(NameSearch))
            {
                Bookss.DefaultView.RowFilter = $"Name LIKE '%{NameSearch}%'";
            }
            else
            {
                Bookss.DefaultView.RowFilter = "";
            }
        });
        
        AuthorFilterCommand = new RelayCommand(_ =>
        {
            if (!string.IsNullOrEmpty(NameSearch))
            {
                Bookss.DefaultView.RowFilter = $"Author LIKE '%{AuthorSearch}%'";
            }
            else
            {
                Bookss.DefaultView.RowFilter = "";
            }
        });
        
        MultiFilterCommand = new RelayCommand(_ =>
        {
            Bookss.DefaultView.RowFilter = $"Author LIKE '%{AuthorSearch}%'" +
                                           $"AND Name LIKE '%{NameSearch}%'" +
                                           $"AND Year LIKE '%{YearSearch}%'"; 
                                           //$"AND Genre LIKE '%{GenreSearch}%'";
        });
    }
}