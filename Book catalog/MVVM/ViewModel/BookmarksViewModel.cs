using System.Collections.ObjectModel;
using System.Data;
using System.Xml.Linq;
using Book_catalog.Core;
using Book_catalog.MVVM.Model;

namespace Book_catalog.MVVM.ViewModel;

public class BookmarksViewModel
{
    private string _userSource;
    private XmlHelper _options;
    
    public ObservableCollection<Bookmark> Bookmark { get; private set; }
    
    public BookmarksViewModel()
    {
        _userSource = "C:\\Users\\Asus\\RiderProjects\\Book catalog\\Book catalog\\UserData.xml";
        _options = new XmlHelper();

        User? user = _options.ReadUserDataXml(_userSource);
        if (user != null)
        {
            Bookmark = user.Bookmarks;
        }
    }
}