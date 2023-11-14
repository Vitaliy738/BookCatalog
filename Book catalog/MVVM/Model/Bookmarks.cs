using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Book_catalog.MVVM.Model;

[Serializable]
public record Bookmarks
{
    // В процесі читання
    public ObservableCollection<Book> Reading { get; private set; }
    
    // Заплпновані
    public ObservableCollection<Book> Planned { get; private set; }
    
    // Вже прочитані
    public ObservableCollection<Book> AlreadyRead { get; private set; }
    
    // Покинуті
    public ObservableCollection<Book> Abandoned { get; private set; }
    
    // Відкладені
    public ObservableCollection<Book> Postponed { get; private set; }
    
    // Улюблені
    public ObservableCollection<Book> Favorite { get; private set; }

    // Constructor
    public Bookmarks() : this(new ObservableCollection<Book>()){}
    public Bookmarks(ObservableCollection<Book> reading) : this(reading, new ObservableCollection<Book>()){}
    
    public Bookmarks(ObservableCollection<Book> reading,
                     ObservableCollection<Book> planned) 
        : this(reading,planned , new ObservableCollection<Book>()){}
    
    public Bookmarks(ObservableCollection<Book> reading,
                     ObservableCollection<Book> planned,
                     ObservableCollection<Book> alreadRead) 
        : this(reading, planned, alreadRead ,new ObservableCollection<Book>()){}
    
    public Bookmarks(ObservableCollection<Book> reading,
                     ObservableCollection<Book> planned,
                     ObservableCollection<Book> alreadRead,
                     ObservableCollection<Book> abandoned) 
        : this(reading, planned, alreadRead, abandoned, new ObservableCollection<Book>()){}
    
    public Bookmarks(ObservableCollection<Book> reading,
                     ObservableCollection<Book> planned,
                     ObservableCollection<Book> alreadRead,
                     ObservableCollection<Book> abandoned,
                     ObservableCollection<Book> postponed) 
        : this(reading, planned, alreadRead, abandoned, postponed, new ObservableCollection<Book>()){}

    public Bookmarks(ObservableCollection<Book> reading,
                     ObservableCollection<Book> planned,
                     ObservableCollection<Book> alreadRead,
                     ObservableCollection<Book> abandoned,
                     ObservableCollection<Book> postponed,
                     ObservableCollection<Book> favorite)
    {
        Reading = reading;
        Planned = planned;
        AlreadyRead = alreadRead;
        Abandoned = abandoned;
        Postponed = postponed;
        Favorite = favorite;
    }
    
    private void ProcessBookOperation(Book book, ObservableCollection<Book> collection, Action<Book> operation)
    {
        operation(book);
    }

    public void AddBook(Book book, ObservableCollection<Book> collection)
    {
        ProcessBookOperation(book, collection, collection.Add);
    }

    public void RemoveBook(Book book, ObservableCollection<Book> collection)
    {
        var bookToRemove = collection.FirstOrDefault(b => b.Equals(book));
        if (bookToRemove != null)
        {
            ProcessBookOperation(bookToRemove, collection, b => collection.Remove(b));
        }
    }

    public bool ContainsBook(Book book, ObservableCollection<Book> collection)
    {
        return collection.Contains(book);
    }

}