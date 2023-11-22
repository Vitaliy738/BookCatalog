using Book_catalog.Core;
using Book_catalog.MVVM.View;

namespace Book_catalog.MVVM.ViewModel;

public class MainViewModel : ObservableObject
{
    public RelayCommand HomeViewCommand { get; private set; }
    public RelayCommand CatalogViewCommand { get; private set; }
    public RelayCommand OptionsViewCommand { get; private set; }
    public RelayCommand BookmarksViewCommand { get; private set; }

    private CatalogViewModel? CatalogVM { get; set; }
    private HomeViewModel? HomeVM { get; set; }
    private OptionsViewModel? OptionsVM { get; set; }
    private BookmarksViewModel? BookmarksVM { get; set; }
    
    private object? _currentView;
    public object? CurrentView
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
        OptionsVM = new OptionsViewModel();
        BookmarksVM = new BookmarksViewModel();

        _currentView = HomeVM;

        HomeViewCommand = new RelayCommand(_ =>
        {
            CurrentView = HomeVM;
        });
        
        CatalogViewCommand = new RelayCommand(_ =>
        {
            CurrentView = CatalogVM;
        });
        
        OptionsViewCommand = new RelayCommand(_ =>
        {
            CurrentView = OptionsVM;
        });
        
        BookmarksViewCommand = new RelayCommand(_ =>
        {
            CurrentView = BookmarksVM;
        });
    }
} 