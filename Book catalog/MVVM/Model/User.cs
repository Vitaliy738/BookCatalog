using System;
using Microsoft.Win32;

namespace Book_catalog.MVVM.Model;

[Serializable]
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

    private Bookmarks _bookmarks;
    public Bookmarks Bookmarks
    {
        get => GetBookmarks();
        set => SetBookmarks(value);
    }
    
    public User() : this("NONE"){}
    public User(string name) : this(name, "C:\\Users\\Asus\\RiderProjects\\Book catalog\\Book catalog\\Icons\\UserProfileIcon.png"){}
    public User(string name, string iconPath) : this(name, iconPath, new Bookmarks()){}

    public User(string name, string iconPath, Bookmarks bookmarks)
    {
        Name = name;
        IconPath = iconPath;
        Bookmarks = bookmarks;
    }

    public Bookmarks GetBookmarks()
    {
        return _bookmarks;
    }
    public void SetBookmarks(Bookmarks bookmarks)
    {
        _bookmarks = bookmarks;
    }

    public bool Equals(User user)
    {
        return this.Name == user.Name
               && this.IconPath == user.IconPath;
    }
}