using System.Collections.ObjectModel;
using System.Reflection.Metadata;
using System.Windows;
using System.Windows.Input;
using Book_catalog.Core;
using Book_catalog.MVVM.Model;
using Book_catalog.MVVM.View;

namespace Book_catalog.MVVM.ViewModel;

public class OptionsViewModel : ObservableObject
{
    public RelayCommand AddUserCommand { get; private set; }
    
    private readonly XmlHelper _options;

    private string _userCatalogSource;

    private string _userDataSource;
    
    public ObservableCollection<User> Users { get; private set; }

    public int SelectedIndex { get; set; }
    
    private User _selectedUser;
    public User SelectedUser
    {
        get => _selectedUser;
        set
        {
            _selectedUser = value;
            _options.LoadUsersXml(_userCatalogSource, Users);
            _options.UpdateUserDataXml(_userDataSource, value);
            OnPropertyChanged("SelectedUser");
        }
    }
    
    public OptionsViewModel()
    {
        PathCollection pathCollection = new PathCollection();
        
        _options = new XmlHelper();
        
        _userCatalogSource = pathCollection.UserCatalogSource;
        _userDataSource = pathCollection.UserDataSource;

        Users = _options.ReadUsersXml(_userCatalogSource);
        
        User? userData = _options.ReadUserDataXml(_userDataSource);
        if (userData != null)
        {
            foreach (var user in Users)
            {
                if (userData.Equals(user))
                {
                    SelectedUser = user;
                }
            }
        }

        AddUserCommand = new RelayCommand(_ =>
        {
            AddUserView addUserView = new AddUserView();
            AddUserViewModel addUserVM = new AddUserViewModel();

            addUserView.DataContext = addUserVM;
            addUserVM.UserAdded += HandleUserAdded;

            addUserView.ShowDialog();
        });
    }

    private void HandleUserAdded(object? obj, UserEventArgs e)
    {
        User newUser = e.User;
        foreach (var user in Users)
        {
            if (user.Equals(newUser))
            {
                MessageBox.Show("Name already exist");
                return;
            }
        }
        
        Users.Add(newUser);
        SelectedUser = newUser;
        
        OnPropertyChanged("SelectedUser");
        MessageBox.Show("User added successfully");
    }
}