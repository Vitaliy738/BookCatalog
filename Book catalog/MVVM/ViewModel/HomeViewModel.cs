using System.Windows;
using Book_catalog.Core;
using Book_catalog.MVVM.Model;

namespace Book_catalog.MVVM.ViewModel;

public class HomeViewModel : ObservableObject
{
    public RelayCommand ReadAddCommand { get; set; }

    public User User { get; }
    
    public string? AlreadyRead { get; private set; }
    public string? PlannedRead { get; private set; }
    public string? AbandonedRead { get; private set; }
    public string? PostponedRead { get; private set; }
    
    public HomeViewModel()
    {
        User = new User();
        
        AlreadyRead = $"Already read: {User.Bookmarks.AlreadyRead.Count}";
        PlannedRead = $"Planned: {User.Bookmarks.Planed.Count}";
        AbandonedRead = $"Abandoned: {User.Bookmarks.Abandoned.Count}";
        PostponedRead = $"Postponed: {User.Bookmarks.Postponed.Count}";
        
        ReadAddCommand = new RelayCommand(_ =>
        {
            User.Bookmarks.AlreadyRead.Add(new Book());
            AlreadyRead = $"Already read: {User.Bookmarks.AlreadyRead.Count}";
            OnPropertyChanged("AlreadyRead");
        });
    }
}