using System.Collections.ObjectModel;
using Book_catalog.Core;
using CatalogLogic;

namespace Book_catalog.MVVM.ViewModel;

public class CatalogViewModel : ObservableObject
{
    public ObservableCollection<Book> Books { get; set; }
    
    public CatalogViewModel()
    {
        Books = new ObservableCollection<Book>
        {
            new Book(),
            new Book(),
            new Book(),
            new Book(),
            new Book(),
            new Book(),
            new Book(),
            new Book(),
            new Book(),
            new Book(),
            new Book(),
            new Book()
        };
    }
}