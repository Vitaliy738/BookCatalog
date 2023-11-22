using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Windows;
using System.Windows.Resources;
using System.Xml.Linq;
using Book_catalog.Core;
using Book_catalog.MVVM.Model;
using Book_catalog.MVVM.View;

namespace Book_catalog.MVVM.ViewModel;

public class BookmarksViewModel : ObservableObject
{
    public RelayCommand RemoveCommand { get; set; }
    public RelayCommand Command { get; set; }
    
    private string _userDataSource;
    private string _userCatalogSource;
    private User? _user;
    
    private XmlHelper _options;
    private BookmarksModel _bookmarksModel;
    
    private Bookmark _selectedBookmark;
    
    private int _selectedBookmarkIndex;
    public int SelectedBookmarkIndex
    {
        get => _selectedBookmarkIndex;
        set
        {
            _selectedBookmarkIndex = value;

            if (_selectedBookmarkIndex >= 0 && _selectedBookmarkIndex <= _bookmarks.Count)
            {
                DataRowView row = BooksTable.DefaultView[value];
                _selectedBookmark = (Bookmark)row["Bookmark"];
            }
        }
    }

    private ObservableCollection<Bookmark> _bookmarks;
    
    public DataTable BooksTable { get; set; }
    
    public BookmarksViewModel()
    {
        PathCollection pathCollection = new PathCollection();
        
        SelectedBookmarkIndex = -1;
        
        _userDataSource = pathCollection.UserDataSource;
        _userCatalogSource = pathCollection.UserCatalogSource;

        // Uri resourceUri = new Uri("pack://application:,,,UserData.xml");
        // _userSource = resourceUri.ToString();
        
        _options = new XmlHelper();
        _bookmarksModel = new BookmarksModel();

        _user = _options.ReadUserDataXml(_userDataSource);
        if (_user != null)
        {
            ObservableCollection<Bookmark> userFavorite = _user.GetFavorite();
            ObservableCollection<Bookmark> userBookmarks = _user.GetBookmarks();

            _bookmarks = new ObservableCollection<Bookmark>(userFavorite.Concat(userBookmarks));
        }
        
        _bookmarks.CollectionChanged += OnCollectionChangedRemoveBookmark;

        BooksTable = _bookmarksModel.FillDataTables(_bookmarks);
        
        RemoveCommand = new RelayCommand(_ =>
        {
            _bookmarks.Remove(_selectedBookmark);
        });
    }

    private void OnCollectionChangedRemoveBookmark(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (SelectedBookmarkIndex >= 0 && NotifyCollectionChangedAction.Remove == e.Action)
        {
            ObservableCollection<Bookmark>? userFavorite = new ObservableCollection<Bookmark>();
            ObservableCollection<Bookmark>? userBookmarks = new ObservableCollection<Bookmark>();

            foreach (var item in _bookmarks)
            {
                if (item.BookmarksType != BookmarksType.Favorite && item.BookmarksType != BookmarksType.NotInterested)
                {
                    userBookmarks.Add(item);
                }
            }
            
            foreach (var item in _bookmarks)
            {
                if (item.BookmarksType == BookmarksType.Favorite && item.BookmarksType != BookmarksType.NotInterested)
                {
                    userFavorite.Add(item);
                }
            }
            
            _user.SetBookmarks(userBookmarks);
            _user.SetFavorite(userFavorite);
            
            _options.UpdateUserDataXml(_userDataSource, _user);
            _options.UpdateUserBookmarks(_userCatalogSource, _user);

            BooksTable.Rows.RemoveAt(SelectedBookmarkIndex);
            OnPropertyChanged("BooksTable");
            
            SelectedBookmarkIndex = -1;
        }
    }
}