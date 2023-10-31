namespace CatalogLogic;

public record Bookmarks
{
    // В процесі читання
    public List<Book> Reading { get; private set; }
    
    // Заплпновані
    public List<Book> Planed { get; private set; }
    
    // Вже прочитані
    public List<Book> AlreadyRead { get; private set; }
    
    // Покинуті
    public List<Book> Abandoned { get; private set; }
    
    // Відкладені
    public List<Book> Postponed { get; private set; }
    
    // Улюблені
    public List<Book> Favorite { get; private set; }

    public Bookmarks()
    {
        Reading = new List<Book>();
        Planed = new List<Book>();
        AlreadyRead = new List<Book>();
        Abandoned = new List<Book>();
        Postponed = new List<Book>();
        Favorite = new List<Book>();
    }
}