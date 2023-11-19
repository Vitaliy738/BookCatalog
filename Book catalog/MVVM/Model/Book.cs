namespace Book_catalog.MVVM.Model;

public class Book
{
    public string Author { get; set; }
    public string Title { get; set; }
    public string Year { get; set; }
    public string Genre { get; set; }
    public string Description { get; set; }
    public string IconPath { get; set; }
    
    public Book() : this("NONE") {}
    public Book(string author) : this(author, "NONE") {}
    public Book(string author, string title) : this(author, title, "NONE") {}
    public Book(string author, string title, string year) : this(author, title, year, "NONE") {}
    public Book(string author, string title, string year, string genre) : this(author, title, year, genre, "C:\\Users\\Asus\\RiderProjects\\Book catalog\\Book catalog\\Icons\\BookIcon.png") {}
    public Book(string author, string title, string year, string genre, string iconPath) : this(author, title, year, genre, iconPath, "NONE") {}
    public Book(string author, string title, string year, string genre, string iconPath, string description)
    {
        Author = author;
        Title = title;
        Year = year;
        Genre = genre;
        Description = description;
        IconPath = iconPath;
    }

    public bool Equals(Book book)
    {
        return this.Title.Equals(book.Title) &&
               this.Author.Equals(book.Author) &&
               this.Year.Equals(book.Year);
    }
}