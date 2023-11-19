using System;
using System.Collections.ObjectModel;
using Microsoft.Win32;

namespace Book_catalog.MVVM.Model;

// [Serializable]
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

    private ObservableCollection<Bookmark> _bookmarks;
    public ObservableCollection<Bookmark> Bookmarks
    {
        get => GetBookmarks();
        set => SetBookmarks(value);
    }
    
    public User() : this("NONE"){}
    public User(string name) : this(name, "C:\\Users\\Asus\\RiderProjects\\Book catalog\\Book catalog\\Icons\\UserProfileIcon.png"){}
    public User(string name, string iconPath) : this(name, iconPath, new ObservableCollection<Bookmark>()){}

    public User(string name, string iconPath, ObservableCollection<Bookmark> bookmarks)
    {
        Name = name;
        IconPath = iconPath;
        Bookmarks = bookmarks;
    }

    public ObservableCollection<Bookmark> GetBookmarks()
    {
        return _bookmarks;
    }
    public void SetBookmarks(ObservableCollection<Bookmark> bookmark)
    {
        _bookmarks = bookmark;
    }

    public void AddBookmark(Bookmark bookmark)
    {
        for (int i = 0; i < Bookmarks.Count; i++)
        {
            if (Bookmarks[i].Equals(bookmark))
            {
                Bookmarks[i].Book = bookmark.Book;
                Bookmarks[i].BookmarksType = bookmark.BookmarksType;
                return;
            }
        }
        
        Bookmarks.Add(bookmark);
        return;
    }

    public bool Equals(User user)
    {
        return this.Name == user.Name;
    }
}