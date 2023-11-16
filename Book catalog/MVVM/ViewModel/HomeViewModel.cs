using System.Linq;
using System.Windows;
using Book_catalog.Core;
using Book_catalog.MVVM.Model;
using Book_catalog.MVVM.View;

namespace Book_catalog.MVVM.ViewModel;

public class HomeViewModel : ObservableObject
{
    public RelayCommand ReadAddCommand { get; set; }

    private OptionsModel _options { get; set; }

    public User? User { get; }
    
    private readonly string _userDataSource;
    
    public string? AlreadyRead { get; private set; }
    public string? PlannedRead { get; private set; }
    public string? AbandonedRead { get; private set; }
    public string? PostponedRead { get; private set; }
    
    public HomeViewModel()
    {
        _userDataSource = "C:\\Users\\Asus\\RiderProjects\\Book catalog\\Book catalog\\UserData.xml";
        _options = new OptionsModel();

        User = _options.ReadUserDataXml(_userDataSource);
        if (User != null)
        {
            AlreadyRead = $"Already read: {User.Bookmarks.Where(bookmark => bookmark.BookmarksType == BookmarksType.AlreadyRead).Count()}";
            PlannedRead = $"Planned: {User.Bookmarks.Where(bookmark => bookmark.BookmarksType == BookmarksType.Planned).Count()}";
            AbandonedRead = $"Abandoned: {User.Bookmarks.Where(bookmark => bookmark.BookmarksType == BookmarksType.Abandoned).Count()}";
            PostponedRead = $"Postponed: {User.Bookmarks.Where(bookmark => bookmark.BookmarksType == BookmarksType.Postponed).Count()}";
        }
    }
}