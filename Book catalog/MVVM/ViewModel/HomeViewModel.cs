using System.Linq;
using System.Windows;
using Book_catalog.Core;
using Book_catalog.MVVM.Model;
using Book_catalog.MVVM.View;

namespace Book_catalog.MVVM.ViewModel;

public class HomeViewModel : ObservableObject
{
    public RelayCommand ReadAddCommand { get; set; }

    private XmlHelper _options { get; set; }

    public User? User { get; }
    
    private readonly string _userDataSource;
    
    public string? AlreadyRead { get; private set; }
    public string? ReadingRead { get; private set; }
    public string? PlannedRead { get; private set; }
    public string? AbandonedRead { get; private set; }
    public string? PostponedRead { get; private set; }
    
    public HomeViewModel()
    {
        _userDataSource = "../../../../Book catalog/UserData.xml";
        _options = new XmlHelper();

        User = _options.ReadUserDataXml(_userDataSource);
        if (User != null)
        {
            AlreadyRead = $"Already read: {User.GetBookmarks().Where(bookmark => bookmark.BookmarksType == BookmarksType.AlreadyRead).Count()}";
            ReadingRead = $"Reading: {User.GetBookmarks().Where(bookmark => bookmark.BookmarksType == BookmarksType.Reading).Count()}";
            PlannedRead = $"Planned: {User.GetBookmarks().Where(bookmark => bookmark.BookmarksType == BookmarksType.Planned).Count()}";
            AbandonedRead = $"Abandoned: {User.GetBookmarks().Where(bookmark => bookmark.BookmarksType == BookmarksType.Abandoned).Count()}";
            PostponedRead = $"Postponed: {User.GetBookmarks().Where(bookmark => bookmark.BookmarksType == BookmarksType.Postponed).Count()}";
        }
    }
}