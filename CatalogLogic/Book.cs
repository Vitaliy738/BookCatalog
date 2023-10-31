namespace CatalogLogic;

public class Book
{
    public string? Author { get; set; }
    public string? Name { get; set; }
    public string? IconPath { get; set; }

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