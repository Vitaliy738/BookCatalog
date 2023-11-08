using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data;
using Book_catalog.Core;
using Book_catalog.MVVM.Model;
using CatalogLogic;

namespace Book_catalog.MVVM.ViewModel;

public class CatalogViewModel : ObservableObject
{
    // Події пошуку
    public RelayCommand SearchFilterCommand { get; private set; }
    public RelayCommand CancelFilterCommand { get; private set; }
    
    // Події сортування
    public RelayCommand NameSortCommand { get; private set; }
    public RelayCommand AuthorSortCommand { get; private set; }
    public RelayCommand YearSortCommand { get; private set; }
    public RelayCommand GenreSortCommand { get; private set; }
    
    public RelayCommand AddBookCommand { get; private set; }

    private readonly CatalogModel _catalogModel;
    
    // Колекція усіх книжок католога
    private ObservableCollection<Book> Books { get; set; }
    
    // Таблиця книжок
    public DataTable BooksTable { get; set; }

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
            }
        }
    }

    private bool IsSortedName { get; set; }
    private bool IsSortedAuthor { get; set; }
    private bool IsSortedYear { get; set; }
    private bool IsSortedGenre { get; set; }

    private string _catalogSource;
    public string? CatalogSource
    {
        get => _catalogSource;
        set
        {
            if (!string.IsNullOrEmpty(value))
            {
                _catalogSource = value;
            }
        }
    }
    
    public CatalogViewModel()
    {
        _catalogModel = new CatalogModel();
        
        IsSortedName = false;
        IsSortedGenre = false;
        IsSortedYear = false;
        IsSortedAuthor = false;
        
        CatalogSource = "C:\\Users\\Asus\\RiderProjects\\Book catalog\\Book catalog\\BookCatalog.xml";

        BooksTable = new DataTable();

        

        BooksTable.Columns.Add("Author", typeof(string));
        BooksTable.Columns.Add("Name", typeof(string));
        BooksTable.Columns.Add("Year", typeof(string));
        BooksTable.Columns.Add("Genre", typeof(string));
        BooksTable.Columns.Add("IconPath", typeof(string));
        BooksTable.Columns.Add("book", typeof(Book));

        Books = _catalogModel.ReadXml(CatalogSource);
        Books.CollectionChanged += BooksOnCollectionChanged;
        
        foreach (var book in Books)
        {
            BooksTable.Rows.Add(book.Author, book.Name, book.Year, book.Genre, 
                book.IconPath, book);
        }

        SearchFilterCommand = new RelayCommand(_ =>
        {
            BooksTable.DefaultView.RowFilter = $"Author LIKE '%{AuthorSearch}%'" +
                                           $"AND Name LIKE '%{NameSearch}%'" +
                                           $"AND Year LIKE '%{YearSearch}%'"; 
                                           //$"AND Genre LIKE '%{GenreSearch}%'";
        });

        CancelFilterCommand = new RelayCommand(_ =>
        {
            string cancel = "";
            BooksTable.DefaultView.RowFilter = $"Author LIKE '%{cancel}%'" +
                                           $"AND Name LIKE '%{cancel}%'" +
                                           $"AND Year LIKE '%{cancel}%'"; 
                                         //$"AND Genre LIKE '%{cancel}%'";
        });

        NameSortCommand = new RelayCommand(_ =>
        {
            if (!IsSortedName)
            {
                BooksTable.DefaultView.Sort = "Name ASC";
                IsSortedName = true;
            }
            else if(IsSortedName)
            {
                BooksTable.DefaultView.Sort = "Name DESC";
                IsSortedName = false;
            }
        });
        
        AuthorSortCommand = new RelayCommand(_ =>
        {
            if (!IsSortedAuthor)
            {
                BooksTable.DefaultView.Sort = "Author ASC";
                IsSortedAuthor = true;
            }
            else if(IsSortedAuthor)
            {
                BooksTable.DefaultView.Sort = "Author DESC";
                IsSortedAuthor = false;
            }
        });
        
        GenreSortCommand = new RelayCommand(_ =>
        {
            if (!IsSortedGenre)
            {
                BooksTable.DefaultView.Sort = "Genre ASC";
                IsSortedGenre = true;
            }
            else if(IsSortedGenre)
            {
                BooksTable.DefaultView.Sort = "Genre DESC";
                IsSortedGenre = false;
            }
        });
        
        YearSortCommand = new RelayCommand(_ =>
        {
            if (!IsSortedYear)
            {
                BooksTable.DefaultView.Sort = "Year ASC";
                IsSortedYear = true;
            }
            else if(IsSortedYear)
            {
                BooksTable.DefaultView.Sort = "Year DESC";
                IsSortedYear = false;
            }
        });
        
        AddBookCommand = new RelayCommand(_ =>
        {
            // ObservableCollection<string> list = new ObservableCollection<string>();
            // list.Add("1");
            // list.Add("1");
            // list.Add("1");
            //
            // ObservableCollection<Book> newBooks = _catalogModel.ReadXml(CatalogSource);
            // newBooks.Add(new Book("Genius", "Vitaliy", "2022", list,
            //     "C:\\Users\\Asus\\OneDrive\\Desktop\\OIP.jpg"));
            // _catalogModel.ReadXml(CatalogSource, newBooks);
            
            Books.Add(new Book());
        });
    }

    private void BooksOnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.Action == NotifyCollectionChangedAction.Add)
        {
            var newBooks = _catalogModel.ReadXml(CatalogSource);

            foreach (var book in e.NewItems)
                if (book is Book)
                {
                    var newBook = book as Book;

                    newBooks.Add(newBook);
                    BooksTable.Rows.Add(newBook.Author, newBook.Name, newBook.Year,
                        newBook.Genre, newBook.IconPath, newBook);
                }

            _catalogModel.LoadXml(CatalogSource, newBooks);
            OnPropertyChanged("BooksTable");
        }
    }
}