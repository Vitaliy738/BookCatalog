using Book_catalog.Core;

namespace Book_catalog.MVVM.ViewModel;

public class MainViewModel : ObservableObject
{
    public RelayCommand HomeViewCommand { get; set; }
    public RelayCommand CatalogViewCommand { get; set; }
    
    public CatalogViewModel CatalogVM { get; set; }
    
    public HomeViewModel HomeVM { get; set; }
    
    private object _currentView;
    public object CurrentView
    {
        get => _currentView;
        set
        {
            _currentView = value;
            OnPropertyChanged();
        }
    }

    public MainViewModel()
    {
        HomeVM = new HomeViewModel();
        CatalogVM = new CatalogViewModel();

        _currentView = HomeVM;

        HomeViewCommand = new RelayCommand(o =>
        {
            CurrentView = HomeVM;
        });
        
        CatalogViewCommand = new RelayCommand(o =>
        {
            CurrentView = CatalogVM;
        });
    }
}