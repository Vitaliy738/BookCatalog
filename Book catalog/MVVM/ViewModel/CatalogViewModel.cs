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
    public RelayCommand CloseRowDetailsCommand { get; private set; }
    
    // Події управління закладками:
    public RelayCommand ToPlanned { get; set; }
    public RelayCommand ToAlreadyRead { get; set; }
    public RelayCommand ToReading { get; set; }
    public RelayCommand ToFavorite { get; set; }
    public RelayCommand ToPostponed { get; set; }
    public RelayCommand ToAbandoned { get; set; }
    public RelayCommand ToNotInterested { get; set; }

    private readonly CatalogModel _catalogModel;
    private readonly XmlHelper _xmlHelper;
    
    // Колекція усіх книжок каталога
    private ObservableCollection<Book>? Books { get; set; }
    
    // Таблиця книжок
    public DataTable BooksTable { get; set; }

    // Значення для фільтрації за ім'ям
    private string _titleSearch;
    public string? TitleSearch
    {
        get => _titleSearch;
        set
        {
            if (value != null)
            {
                _titleSearch = value;
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
    private string _userDataSource;
    private string _usersDataSource;
    private User _userData;

    public CatalogViewModel()
    {
        _xmlHelper = new XmlHelper();
        _catalogModel = new CatalogModel();

        _selectedBookIndex = -1;

        IsSortedName = false;
        IsSortedGenre = false;
        IsSortedYear = false;
        IsSortedAuthor = false;

        _catalogSource = "C:\\Users\\Asus\\RiderProjects\\Book catalog\\Book catalog\\BookCatalog.xml";
        _userDataSource = "C:\\Users\\Asus\\RiderProjects\\Book catalog\\Book catalog\\UserData.xml";
        _usersDataSource = "C:\\Users\\Asus\\RiderProjects\\Book catalog\\Book catalog\\UserCatalog.xml";

        _userData = _xmlHelper.ReadUserDataXml(_userDataSource);

        ObservableCollection<Bookmark> bookmarks = _userData.Bookmarks;

        Books = _xmlHelper.ReadBooksXml(_catalogSource);
        Books.CollectionChanged += BooksOnCollectionChanged;

        BooksTable = _catalogModel.FillBookTable(Books, bookmarks);

        SearchFilterCommand = new RelayCommand(_ =>
        {
            BooksTable.DefaultView.RowFilter = $"Author LIKE '%{AuthorSearch}%'" +
                                               $"AND Name LIKE '%{TitleSearch}%'" +
                                               $"AND Year LIKE '%{YearSearch}%'" +
                                               $"AND Genre LIKE '%{GenreSearch}%'";
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
            else if (IsSortedName)
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
            else if (IsSortedAuthor)
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
            else if (IsSortedGenre)
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
            else if (IsSortedYear)
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

        CloseRowDetailsCommand = new RelayCommand(_ =>
        {
            SelectedBookIndex = -1;
            OnPropertyChanged("SelectedBookIndex");
        });

        ToPlanned = new RelayCommand(_ =>
        {
            _userData.AddBookmark(new Bookmark(_selectedBook, BookmarksType.Planned));
            _xmlHelper.UpdateUserDataXml(_userDataSource, _userData);
            _xmlHelper.UpdateUserBookmarks(_usersDataSource, _userData.Name, _userData.Bookmarks);

            DataRowView rowView = BooksTable.DefaultView[SelectedBookIndex];
            rowView["Reading"] = BookmarksType.Reading == BookmarksType.Planned;
            rowView["Planned"] = BookmarksType.Planned == BookmarksType.Planned;
            rowView["AlreadyRead"] = BookmarksType.AlreadyRead == BookmarksType.Planned;
            rowView["Abandoned"] = BookmarksType.Abandoned == BookmarksType.Planned;
            rowView["Postponed"] = BookmarksType.Postponed == BookmarksType.Planned;
            rowView["Favorite"] = BookmarksType.Favorite == BookmarksType.Planned;
            //rowView["NotInterested"] = true;
            rowView.EndEdit();

            OnPropertyChanged("BooksTable");
        });

        ToAlreadyRead = new RelayCommand(_ =>
        {
            _userData.AddBookmark(new Bookmark(_selectedBook, BookmarksType.AlreadyRead));
            _xmlHelper.UpdateUserDataXml(_userDataSource, _userData);
            _xmlHelper.UpdateUserBookmarks(_usersDataSource, _userData.Name, _userData.Bookmarks);

            DataRowView rowView = BooksTable.DefaultView[SelectedBookIndex];
            rowView["Reading"] = BookmarksType.Reading == BookmarksType.AlreadyRead;
            rowView["Planned"] = BookmarksType.Planned == BookmarksType.AlreadyRead;
            rowView["AlreadyRead"] = BookmarksType.AlreadyRead == BookmarksType.AlreadyRead;
            rowView["Abandoned"] = BookmarksType.Abandoned == BookmarksType.AlreadyRead;
            rowView["Postponed"] = BookmarksType.Postponed == BookmarksType.AlreadyRead;
            rowView["Favorite"] = BookmarksType.Favorite == BookmarksType.AlreadyRead;
            //rowView["NotInterested"] = true;
            rowView.EndEdit();

            OnPropertyChanged("BooksTable");
        });

        ToReading = new RelayCommand(_ =>
        {
            _userData.AddBookmark(new Bookmark(_selectedBook, BookmarksType.Reading));
            _xmlHelper.UpdateUserDataXml(_userDataSource, _userData);
            _xmlHelper.UpdateUserBookmarks(_usersDataSource, _userData.Name, _userData.Bookmarks);

            DataRowView rowView = BooksTable.DefaultView[SelectedBookIndex];
            rowView["Reading"] = BookmarksType.Reading == BookmarksType.Reading;
            rowView["Planned"] = BookmarksType.Planned == BookmarksType.Reading;
            rowView["AlreadyRead"] = BookmarksType.AlreadyRead == BookmarksType.Reading;
            rowView["Abandoned"] = BookmarksType.Abandoned == BookmarksType.Reading;
            rowView["Postponed"] = BookmarksType.Postponed == BookmarksType.Reading;
            rowView["Favorite"] = BookmarksType.Favorite == BookmarksType.Reading;
            //rowView["NotInterested"] = true;
            rowView.EndEdit();

            OnPropertyChanged("BooksTable");
        });

        ToFavorite = new RelayCommand(_ =>
        {
            _userData.AddBookmark(new Bookmark(_selectedBook, BookmarksType.Favorite));
            _xmlHelper.UpdateUserDataXml(_userDataSource, _userData);
            _xmlHelper.UpdateUserBookmarks(_usersDataSource, _userData.Name, _userData.Bookmarks);

            DataRowView rowView = BooksTable.DefaultView[SelectedBookIndex];
            rowView["Reading"] = BookmarksType.Reading == BookmarksType.Favorite;
            rowView["Planned"] = BookmarksType.Planned == BookmarksType.Favorite;
            rowView["AlreadyRead"] = BookmarksType.AlreadyRead == BookmarksType.Favorite;
            rowView["Abandoned"] = BookmarksType.Abandoned == BookmarksType.Favorite;
            rowView["Postponed"] = BookmarksType.Postponed == BookmarksType.Favorite;
            rowView["Favorite"] = BookmarksType.Favorite == BookmarksType.Favorite;
            //rowView["NotInterested"] = true;
            rowView.EndEdit();

            OnPropertyChanged("BooksTable");
        });

        ToPostponed = new RelayCommand(_ =>
        {
            _userData.AddBookmark(new Bookmark(_selectedBook, BookmarksType.Postponed));
            _xmlHelper.UpdateUserDataXml(_userDataSource, _userData);
            _xmlHelper.UpdateUserBookmarks(_usersDataSource, _userData.Name, _userData.Bookmarks);

            DataRowView rowView = BooksTable.DefaultView[SelectedBookIndex];
            rowView["Reading"] = BookmarksType.Reading == BookmarksType.Postponed;
            rowView["Planned"] = BookmarksType.Planned == BookmarksType.Postponed;
            rowView["AlreadyRead"] = BookmarksType.AlreadyRead == BookmarksType.Postponed;
            rowView["Abandoned"] = BookmarksType.Abandoned == BookmarksType.Postponed;
            rowView["Postponed"] = BookmarksType.Postponed == BookmarksType.Postponed;
            rowView["Favorite"] = BookmarksType.Favorite == BookmarksType.Postponed;
            //rowView["NotInterested"] = true;
            rowView.EndEdit();

            OnPropertyChanged("BooksTable");
        });

        ToAbandoned = new RelayCommand(_ =>
        {
            _userData.AddBookmark(new Bookmark(_selectedBook, BookmarksType.Abandoned));
            _xmlHelper.UpdateUserDataXml(_userDataSource, _userData);
            _xmlHelper.UpdateUserBookmarks(_usersDataSource, _userData.Name, _userData.Bookmarks);

            DataRowView rowView = BooksTable.DefaultView[SelectedBookIndex];
            rowView["Reading"] = BookmarksType.Reading == BookmarksType.Abandoned;
            rowView["Planned"] = BookmarksType.Planned == BookmarksType.Abandoned;
            rowView["AlreadyRead"] = BookmarksType.AlreadyRead == BookmarksType.Abandoned;
            rowView["Abandoned"] = BookmarksType.Abandoned == BookmarksType.Abandoned;
            rowView["Postponed"] = BookmarksType.Postponed == BookmarksType.Abandoned;
            rowView["Favorite"] = BookmarksType.Favorite == BookmarksType.Abandoned;
            //rowView["NotInterested"] = true;
            rowView.EndEdit();

            OnPropertyChanged("BooksTable");
        });

        //     ToNotInterested = new RelayCommand(_ =>
        //     {
        //         _userData.AddBookmark(new Bookmark(_selectedBook, BookmarksType.NotInterested));
        //         _xmlHelper);
        // }
    }

    private void BooksOnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        switch (e.Action)
        {
            case NotifyCollectionChangedAction.Add:
            {
                var newBooks = _xmlHelper.ReadBooksXml(_catalogSource);
 
                foreach (var book in e.NewItems)
                {
                    if (book is Book)
                    {
                        var newBook = book as Book;
                        
                        newBooks.Add(newBook);
                        BooksTable.Rows.Add(newBook.Author, 
                            newBook.Title, 
                            newBook.Year,
                            newBook.Genre, 
                            newBook.IconPath,
                            newBook.Description,
                            newBook,
                            BookmarksType.NotInterested, 
                            false, false, false, false, false, false);
                    }
                }

                _xmlHelper.LoadBooksXml(_catalogSource, newBooks);
                MessageBox.Show("Book has been successfully added");
                OnPropertyChanged("BooksTable");
                break;
            }
            case NotifyCollectionChangedAction.Remove:
            {
                if (SelectedBookIndex >= 0)
                {
                    DataRow[] rows = BooksTable.Select($"Author LIKE '%{_selectedBook.Author}%'" +
                                                       $"AND Name LIKE '%{_selectedBook.Title}%'" +
                                                       $"AND Year LIKE '%{_selectedBook.Year}%'" +
                                                       $"AND IconPath LIKE '%{_selectedBook.IconPath}%'");
                
                    if (rows.Length > 0)
                    {
                        BooksTable.Rows.Remove(rows[0]);
                        _selectedBookIndex = -1;
                        _xmlHelper.LoadBooksXml(_catalogSource, Books);
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
        Book newBook = e.Book;
        foreach (var book in Books)
        {
            if (book.Equals(newBook))
            {
                MessageBox.Show("A book with these parameters already exists(Title, author, year)");
                return;
            }
        }
        
        Books.Add(e.Book);
    }
    
    // Обробник події редагування книжки
    private void HandleBookModify(object? sender, BookEventArgs e)
    {
        Book newBook = e.Book;
        
        foreach (var book in Books)
        {
            if (book.Equals(newBook))
            {
                MessageBox.Show("A book with these parameters already exists(Title, author, year)");
                return;
            }
        }
        
        Books[SelectedBookIndex] = newBook;
        _xmlHelper.LoadBooksXml(_catalogSource, Books);

        DataRowView rowView = BooksTable.DefaultView[SelectedBookIndex];
        rowView["Author"] = newBook.Author;
        rowView["Name"] = newBook.Title;
        rowView["Year"] = newBook.Year;
        rowView["Genre"] = newBook.Genre;
        rowView["IconPath"] = newBook.IconPath;
        rowView["ShortDescription"] = newBook.Description;
        rowView["book"] = newBook;
        rowView.EndEdit();

        MessageBox.Show("The book was successfully modified");
        OnPropertyChanged("BooksTable");
    }
}