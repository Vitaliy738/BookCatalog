using System;
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
    private HomeModel _homeModel { get; set; }

    public User? User { get; }
    
    private readonly string _userDataSource;
    
    public Book FirstBook { get; set; }
    public Book SecondBook { get; set; }
    public Book ThirdBook { get; set; }
    public Book FourthBook { get; set; }
    
    public string? AlreadyRead { get; private set; }
    public string? ReadingRead { get; private set; }
    public string? PlannedRead { get; private set; }
    public string? AbandonedRead { get; private set; }
    public string? PostponedRead { get; private set; }
    
    public HomeViewModel()
    {
        _options = new XmlHelper();
        _homeModel = new HomeModel();
        
        PathCollection pathCollection = new PathCollection();
        _userDataSource = pathCollection.UserDataSource;

        User = _options.ReadUserDataXml(_userDataSource);
        if (User != null)
        {
            AlreadyRead = $"Already read: {User.GetBookmarks().Where(bookmark => bookmark.BookmarksType == BookmarksType.AlreadyRead).Count()}";
            ReadingRead = $"Reading: {User.GetBookmarks().Where(bookmark => bookmark.BookmarksType == BookmarksType.Reading).Count()}";
            PlannedRead = $"Planned: {User.GetBookmarks().Where(bookmark => bookmark.BookmarksType == BookmarksType.Planned).Count()}";
            AbandonedRead = $"Abandoned: {User.GetBookmarks().Where(bookmark => bookmark.BookmarksType == BookmarksType.Abandoned).Count()}";
            PostponedRead = $"Postponed: {User.GetBookmarks().Where(bookmark => bookmark.BookmarksType == BookmarksType.Postponed).Count()}";
        }

        FirstBook = _homeModel.GetFavorite(0, User.GetFavorite());
        SecondBook = _homeModel.GetFavorite(1, User.GetFavorite());
        ThirdBook = _homeModel.GetFavorite(2, User.GetFavorite());
        FourthBook = _homeModel.GetFavorite(3, User.GetFavorite());
    }
}