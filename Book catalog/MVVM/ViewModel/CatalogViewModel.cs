using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data;
using System.Windows;
using Book_catalog.Core;
using Book_catalog.MVVM.Model;
using Book_catalog.MVVM.View;

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
    
    // Події редагування каталогу
    public RelayCommand AddBookCommand { get; private set; }
    public RelayCommand DeleteBookCommand { get; private set; }
    public RelayCommand ModifyBookCommand { get; private set; }

    private readonly CatalogModel _catalogModel;
    
    // Колекція усіх книжок каталога
    private ObservableCollection<Book>? Books { get; set; }
    
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

    // Обранна книжка
    private Book _selectedBook;
    
    // Індекс обраної книжки
    private int _selectedBookIndex;
    public int SelectedBookIndex
    {
        get => _selectedBookIndex;
        set
        {
            _selectedBookIndex = value;

            if (_selectedBookIndex >= 0 && _selectedBookIndex <= Books.Count)
            {
                DataRowView row = BooksTable.DefaultView[value];
                _selectedBook = (Book)row["book"];
            }
        }
    }

    private bool IsSortedName { get; set; }
    private bool IsSortedAuthor { get; set; }
    private bool IsSortedYear { get; set; }
    private bool IsSortedGenre { get; set; }

    private string _catalogSource;
    
    public CatalogViewModel()
    {
        _catalogModel = new CatalogModel();

        _selectedBookIndex = -1;
        
        IsSortedName = false;
        IsSortedGenre = false;
        IsSortedYear = false;
        IsSortedAuthor = false;
        
        _catalogSource = "C:\\Users\\Asus\\RiderProjects\\Book catalog\\Book catalog\\BookCatalog.xml";

        BooksTable = new DataTable();

        BooksTable.Columns.Add("Author", typeof(string));
        BooksTable.Columns.Add("Name", typeof(string));
        BooksTable.Columns.Add("Year", typeof(string));
        BooksTable.Columns.Add("Genre", typeof(string));
        BooksTable.Columns.Add("IconPath", typeof(string));
        BooksTable.Columns.Add("book", typeof(Book));

        Books = _catalogModel.ReadXml(_catalogSource);
        if (Books != null)
        {
            Books.CollectionChanged += BooksOnCollectionChanged;
                    
            foreach (var book in Books)
            {
                BooksTable.Rows.Add(book.Author, book.Name, book.Year, book.Genre, 
                    book.IconPath, book);
            }
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
            AddBookView addBookView = new AddBookView();
            BookAddViewModel bookAddVM = new BookAddViewModel();

            addBookView.DataContext = bookAddVM;

            bookAddVM.BookAdded += HandleBookAdded;
            
            addBookView.ShowDialog();
        });
        
        DeleteBookCommand = new RelayCommand(_ =>
        {
            if (SelectedBookIndex >= 0 && SelectedBookIndex < Books.Count)
            {
                Books.Remove(_selectedBook);
            }
        });
        
        ModifyBookCommand = new RelayCommand(_ =>
        {
            if (SelectedBookIndex >= 0 && SelectedBookIndex < Books.Count)
            {
                ModifyViewModel modifyVM = new ModifyViewModel(_selectedBook);
                ModifyView modifyView = new ModifyView();

                modifyView.DataContext = modifyVM;
                modifyVM.BookModified += HandleBookModify;
                
                modifyView.ShowDialog();

            }
        });
    }

    private void BooksOnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        switch (e.Action)
        {
            case NotifyCollectionChangedAction.Add:
            {
                var newBooks = _catalogModel.ReadXml(_catalogSource);

                foreach (var book in e.NewItems)
                {
                    if (book is Book)
                    {
                        var newBook = book as Book;

                        newBooks.Add(newBook);
                        BooksTable.Rows.Add(newBook.Author, newBook.Name, newBook.Year,
                            newBook.Genre, newBook.IconPath, newBook);
                    }
                }

                _catalogModel.LoadXml(_catalogSource, newBooks);
                OnPropertyChanged("BooksTable");
                break;
            }
            case NotifyCollectionChangedAction.Remove:
            {
                if (SelectedBookIndex >= 0)
                {
                    DataRow[] rows = BooksTable.Select($"Author LIKE '%{_selectedBook.Author}%'" +
                                                       $"AND Name LIKE '%{_selectedBook.Name}%'" +
                                                       $"AND Year LIKE '%{_selectedBook.Year}%'" +
                                                       $"AND IconPath LIKE '%{_selectedBook.IconPath}%'");
                
                    if (rows.Length > 0)
                    {
                        BooksTable.Rows.Remove(rows[0]);
                        _selectedBookIndex = -1;
                        _catalogModel.LoadXml(_catalogSource, Books);
                        OnPropertyChanged("BooksTable");
                    }
                }

                break;
            }
        }
    }

    // Обробник події додавання книжки
    private void HandleBookAdded(object? sender, BookEventArgs e)
    {
        Books.Add(e.Book);
    }
    
    // Обробник події редагування книжки
    private void HandleBookModify(object? sender, BookEventArgs e)
    {
        Book book = e.Book;
        
        Books[SelectedBookIndex] = book;
        _catalogModel.LoadXml(_catalogSource, Books);

        DataRowView rowView = BooksTable.DefaultView[SelectedBookIndex];
        rowView["Author"] = book.Author;
        rowView["Name"] = book.Name;
        rowView["Year"] = book.Year;
        rowView["Genre"] = book.Genre;
        rowView["IconPath"] = book.IconPath;
        rowView["book"] = book;
        rowView.EndEdit();
        
        OnPropertyChanged("BooksTable");
    }

    public void Button_Click(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("dsf");
    }
}