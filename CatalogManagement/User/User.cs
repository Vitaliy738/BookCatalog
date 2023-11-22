using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using Microsoft.Win32;

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

    public ObservableCollection<Bookmark> Bookmarks { get; internal set; }

    public ObservableCollection<Bookmark> Favorite { get; internal set; }
    
    public User() : this("NONE"){}
    public User(string name) : this(name, "../../../../Icons/UserProfileIcon.png"){}
    public User(string name, string iconPath) : this(name, iconPath, new ObservableCollection<Bookmark>()){}
    public User(string name, string iconPath, ObservableCollection<Bookmark> bookmarks) : this(name, iconPath, bookmarks, new ObservableCollection<Bookmark>()){}
    public User(string name, string iconPath, ObservableCollection<Bookmark> bookmarks, ObservableCollection<Bookmark> favorite)
    {
        Name = name;
        IconPath = iconPath;
        Bookmarks = bookmarks;
        Favorite = favorite;
    }

    // Методи для роботи з колекцією Bookmarks
    public ObservableCollection<Bookmark> GetBookmarks()
    {
        return Bookmarks;
    }
    public void SetBookmarks(ObservableCollection<Bookmark> bookmark)
    {
        Bookmarks = bookmark;
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
    public void RemoveBookmark(Book book)
    {
        for (int i = 0; i < Bookmarks.Count; i++)
        {
            if (Bookmarks[i].Book.Equals(book))
            {
                Bookmarks.RemoveAt(i);
                return;
            }
        }
    }
    
    // Методи для роботи з колекцією Favorite
    public ObservableCollection<Bookmark> GetFavorite()
    {
        return Favorite;
    }
    public void SetFavorite(ObservableCollection<Bookmark> favorite)
    {
        Favorite = favorite;
    }
    
    public void AddFavorite(Bookmark favorite)
    {
        for (int i = 0; i < Favorite.Count; i++)
        {
            if (favorite.BookmarksType != BookmarksType.Favorite)
            {
                throw new Exception("Favorite type exception");
            }
            
            if (Favorite[i].Equals(favorite))
            {
                Favorite[i].Book = favorite.Book;
                Favorite[i].BookmarksType = favorite.BookmarksType;
                return;
            }
        }
        
        Favorite.Add(favorite);
        return;
    }
    public void RemoveFavorite(Book book)
    {
        for (int i = 0; i < Favorite.Count; i++)
        {
            if (Favorite[i].Book.Equals(book))
            {
                Favorite.RemoveAt(i);
                return;
            }
        }
    }

    // Метод порівняння
    public bool Equals(User user)
    {
        return this.Name == user.Name;
    }
}