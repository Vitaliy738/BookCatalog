using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Book_catalog.MVVM.Model;

public class Bookmark
{
    public Book Book { get; set; }
    public BookmarksType BookmarksType { get; set; }

    public Bookmark()
    {
        Book = null;
        BookmarksType = BookmarksType.NotInterested;
    }
    public Bookmark(Book book, BookmarksType bookmarksType)
    {
        Book = book;
        BookmarksType = bookmarksType;
    }

    public bool Equals(Bookmark bookmark)
    {
        return this.Book.Equals(bookmark.Book);
    }
}