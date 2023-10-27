namespace ClassLibrary1;

public class User
{
    private string? _userName;
    public string? UserName
    {
        get => _userName;
        set
        {
            if (!string.IsNullOrEmpty(value)) _userName = value;
        }
    }

    public Bookmarks Bookmarks { get; private set; }

    public User()
    {
        Bookmarks = new Bookmarks();
    }
}