namespace ClassLibrary1;

public record Bookmarks
{
    // В процесі читання
    private List<Book> Reading { get; set; }
    
    // Заплпновані
    private List<Book> WillRead { get; set; }
    
    // Вже прочитані
    private List<Book> AlreadyRead { get; set; }
    
    // Покинуті
    private List<Book> Abandoned { get; set; }
    
    // Відкладені
    private List<Book> Postponed { get; set; }

    public Bookmarks()
    {
        Reading = new List<Book>();
        WillRead = new List<Book>();
        AlreadyRead = new List<Book>();
        Abandoned = new List<Book>();
        Postponed = new List<Book>();
    }
}