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
        
        AlreadyRead = $"Already read: {User.GetBookmarks().AlreadyRead.Count}";
        PlannedRead = $"Planned: {User.GetBookmarks().Planned.Count}";
        AbandonedRead = $"Abandoned: {User.GetBookmarks().Abandoned.Count}";
        PostponedRead = $"Postponed: {User.GetBookmarks().Postponed.Count}";
    }
}