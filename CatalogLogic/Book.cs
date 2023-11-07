namespace CatalogLogic;

public class Book
{
    public string? Author { get; set; }
    public string? Name { get; set; }
    public string? Year { get; set; }
    public List<string>? Genre { get; set; }
    public string? IconPath { get; set; }

    public Book() : this("Data is not available") {}
    public Book(string author) : this(author, "Data is not available") {}
    public Book(string author, string name) : this(author, name, "NONE") {}
    public Book(string author, string name, string year) : this(author, name, year, new List<string>()) {}
    public Book(string author, string name, string year, List<string> genre) : this(author, name, year, genre, "C:\\Users\\Asus\\RiderProjects\\Book catalog\\Book catalog\\Icons\\BookIcon.png") {}
    public Book(string author, string name, string year, List<string> genre, string iconPath)
    {
        Author = author;
        Name = name;
        Year = year;
        Genre = genre;
        IconPath = iconPath;
    }
}