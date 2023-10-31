namespace CatalogLogic;

public class Book
{
    public string? Author;
    public string? Name;
    public string? IconPath;

    public Book() : this("Data is not available") {}
    public Book(string? author) : this(author, "Data is not available") {}
    public Book(string? author, string? name) : this(author, name, 
        "C:\\Users\\Asus\\RiderProjects\\Book catalog\\Book catalog\\Icons\\BookIcon.png") {}
    public Book(string? author, string? name, string? iconPath)
    {
        Author = author;
        Name = name;
        IconPath = iconPath;
    }
}