using Book_catalog.Core;

namespace Book_catalog.MVVM.ViewModel;

public class MainViewModel : ObservableObject
{
    public RelayCommand HomeViewCommand { get; private set; }
    public RelayCommand CatalogViewCommand { get; private set; }
    public RelayCommand OptionsViewCommand { get; private set; }

    private CatalogViewModel? CatalogVM { get; set; }
    private HomeViewModel? HomeVM { get; set; }
    private OptionsViewModel? OptionsVM { get; set; }
    
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
    }
} 