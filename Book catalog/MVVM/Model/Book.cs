namespace Book_catalog.MVVM.Model;

public class Book
{
    public string Author { get; set; }
    public string Name { get; set; }
    public string Year { get; set; }
    public string Genre { get; set; }
    public string Description { get; set; }
    public string IconPath { get; set; }
    
    public Book() : this("Data is not available") {}
    public Book(string author) : this(author, "Data is not available") {}
    public Book(string author, string name) : this(author, name, "NONE") {}
    public Book(string author, string name, string year) : this(author, name, year, "NONE") {}
    public Book(string author, string name, string year, string genre) : this(author, name, year, genre, "C:\\Users\\Asus\\RiderProjects\\Book catalog\\Book catalog\\Icons\\BookIcon.png") {}
    public Book(string author, string name, string year, string genre, string iconPath) : this(author, name, year, genre, iconPath, "NONE") {}
    public Book(string author, string name, string year, string genre, string iconPath, string description)
    {
        Author = author;
        Name = name;
        Year = year;
        Genre = genre;
        Description = description;
        IconPath = iconPath;
    }

    public bool Equals(Book book)
    {
        if (this.Name == book.Name
            && this.IconPath == book.IconPath
            && this.Author == book.Author
            && this.Genre == book.Genre
            && this.Year == book.Year
            && this.Description == book.Description)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}