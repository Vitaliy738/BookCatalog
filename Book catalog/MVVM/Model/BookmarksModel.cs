using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Windows.Documents;

namespace Book_catalog.MVVM.Model;

public class BookmarksModel
{
    public DataTable FillDataTable(ObservableCollection<Bookmark>? bookmarks, Func<Bookmark, bool>? filter = null)
    {
        DataTable booksTable = new DataTable();
        
        booksTable.Columns.Add("Title", typeof(string));
        booksTable.Columns.Add("IconPath", typeof(string));

        if (bookmarks != null)
        {
            List<Bookmark> filteredBookmarks = bookmarks.Where(filter).ToList();

            foreach (var item in filteredBookmarks)
            {
                booksTable.Rows.Add(item.Book.Title, item.Book.IconPath);
            }

            return booksTable;
        }
        
        return booksTable;
    }

    public DataTable FillDataTables(ObservableCollection<Bookmark>? bookmarks)
    {
        DataTable booksTable = new DataTable();

        booksTable.Columns.Add("TitleReading", typeof(string));
        booksTable.Columns.Add("IconPathReading", typeof(string));
        
        booksTable.Columns.Add("TitlePlanned", typeof(string));
        booksTable.Columns.Add("IconPathPlanned", typeof(string));
        
        booksTable.Columns.Add("TitleAlreadyRead", typeof(string));
        booksTable.Columns.Add("IconPathAlreadyRead", typeof(string));
        
        booksTable.Columns.Add("TitleAbandoned", typeof(string));
        booksTable.Columns.Add("IconPathAbandoned", typeof(string));
        
        booksTable.Columns.Add("TitlePostponed", typeof(string));
        booksTable.Columns.Add("IconPathPostponed", typeof(string));
        
        booksTable.Columns.Add("TitleFavorite", typeof(string));
        booksTable.Columns.Add("IconPathFavorite", typeof(string));
        
        booksTable.Columns.Add("Bookmark", typeof(Bookmark));

        if (bookmarks != null)
        {
            foreach (var item in bookmarks)
            {
                if (item.BookmarksType == BookmarksType.Reading)
                {
                    booksTable.Rows.Add(item.Book.Title, item.Book.IconPath,
                        null, null,
                        null, null,
                        null, null,
                        null, null,
                        null, null,
                        item);
                }
                if (item.BookmarksType == BookmarksType.Planned)
                {
                    booksTable.Rows.Add(null, null,
                        item.Book.Title, item.Book.IconPath,
                        null, null,
                        null, null,
                        null, null,
                        null, null,
                        item);
                }
                if (item.BookmarksType == BookmarksType.AlreadyRead)
                {
                    booksTable.Rows.Add(null, null,
                        null, null,
                        item.Book.Title, item.Book.IconPath,
                        null, null,
                        null, null,
                        null, null,
                        item);
                }
                if (item.BookmarksType == BookmarksType.Abandoned)
                {
                    booksTable.Rows.Add(null, null,
                        null, null,
                        null, null,
                        item.Book.Title, item.Book.IconPath,
                        null, null,
                        null, null,
                        item);
                }
                if (item.BookmarksType == BookmarksType.Postponed)
                {
                    booksTable.Rows.Add(null, null,
                        null, null,
                        null, null,
                        null, null,
                        item.Book.Title, item.Book.IconPath,
                        null, null,
                        item);
                }
                if (item.BookmarksType == BookmarksType.Favorite)
                {
                    booksTable.Rows.Add(null, null,
                        null, null,
                        null, null,
                        null, null,
                        null, null,
                        item.Book.Title, item.Book.IconPath,
                        item);
                }
            }
        }
        
        return booksTable;
    }
}