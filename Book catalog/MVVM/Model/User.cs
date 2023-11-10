namespace Book_catalog.MVVM.Model;

public class User
{
    private string? _name;
    public string? Name
    {
        get => _name;
        set
        {
            if (!string.IsNullOrEmpty(value)) _name = value;
        }
    }
    
    private string? _iconPath;
    public string? IconPath
    {
        get => _iconPath;
        set
        {
            if (!string.IsNullOrEmpty(value))
            {
                _iconPath = value;
            }
        }
    }

    public Bookmarks Bookmarks { get; private set; }
    
    public User()
    {
        Bookmarks = new Bookmarks();

        _name = "Name";
        _iconPath = "C:\\Users\\Asus\\RiderProjects\\Book catalog\\Book catalog\\Icons\\UserProfileIcon.png";
    }
}