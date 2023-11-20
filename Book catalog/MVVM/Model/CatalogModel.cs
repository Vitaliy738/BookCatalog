using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Windows;
using System.Xml.Serialization;

namespace Book_catalog.MVVM.Model;

public class CatalogModel
{
   public DataTable FillBookTable(ObservableCollection<Book> books, ObservableCollection<Bookmark> bookmarks, ObservableCollection<Bookmark> favorite)
    {
        DataTable booksTable = new DataTable();

        booksTable.Columns.Add("Author", typeof(string));
        booksTable.Columns.Add("Name", typeof(string));
        booksTable.Columns.Add("Year", typeof(string));
        booksTable.Columns.Add("Genre", typeof(string));
        booksTable.Columns.Add("IconPath", typeof(string));
        booksTable.Columns.Add("ShortDescription", typeof(string));
        booksTable.Columns.Add("book", typeof(Book));
        booksTable.Columns.Add("bookmark", typeof(BookmarksType));
        
        booksTable.Columns.Add("Reading", typeof(bool));
        booksTable.Columns.Add("Planned", typeof(bool));
        booksTable.Columns.Add("AlreadyRead", typeof(bool));
        booksTable.Columns.Add("Abandoned", typeof(bool));
        booksTable.Columns.Add("Postponed", typeof(bool));
        
        booksTable.Columns.Add("Favorite", typeof(bool));
        
        if (books != null)
        {
            foreach (var book in books)
            {
                BookmarksType type = GetBookmarksType(book, bookmarks);
                bool isFavorite = CheckFavorite(book, favorite);
                
                if(type != BookmarksType.NotInterested)
                {
                    booksTable.Rows.Add(book.Author, 
                        book.Title, 
                        book.Year, 
                        book.Genre,
                        book.IconPath, 
                        book.Description, 
                        book, 
                        type,
                        type == BookmarksType.Reading,
                        type == BookmarksType.Planned,
                        type == BookmarksType.AlreadyRead,
                        type == BookmarksType.Abandoned,
                        type == BookmarksType.Postponed,
                        isFavorite);

                }
                else
                {
                    booksTable.Rows.Add(book.Author, 
                                        book.Title, 
                                        book.Year, 
                                        book.Genre,
                                        book.IconPath, 
                                        book.Description, 
                                        book, 
                                        BookmarksType.NotInterested, 
                                        false, false, false, false, false, false);
                }
            }
        }
        
        return booksTable;
    }

   private BookmarksType GetBookmarksType(Book book, ObservableCollection<Bookmark> bookmarks)
    {
        foreach (var item in bookmarks)
        {
            if (book.Title == item.Book.Title
                && book.Author == item.Book.Author
                && book.Year == item.Book.Year
                && book.Description == item.Book.Description)
            {
                return item.BookmarksType;
            }
        }
        
        return BookmarksType.NotInterested;
    }

   private bool CheckFavorite(Book book, ObservableCollection<Bookmark> favorite)
   {
       foreach (var item in favorite)
       {
           if (book.Title == item.Book.Title
               && book.Author == item.Book.Author
               && book.Year == item.Book.Year
               && book.Description == item.Book.Description)
           {
               return true;
           }
       }
       
       return false;
   }
}