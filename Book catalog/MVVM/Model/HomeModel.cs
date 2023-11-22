using System;
using System.Collections.ObjectModel;
using System.Data;

namespace Book_catalog.MVVM.Model;

public class HomeModel
{
    public Book GetFavorite(int index, ObservableCollection<Bookmark> favorite)
    {
        try
        {
            return favorite[index].Book;
        }
        catch (Exception e)
        {
            return null;
        }
    }
}