namespace Book_catalog.MVVM.Model;

public class BookEventArgs
{
    public Book Book { get; }

    public BookEventArgs(Book book)
    {
        Book = book;
    }
}