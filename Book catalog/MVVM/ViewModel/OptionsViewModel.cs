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
    public ICommand EditUserCommand { get; private set; }
    public ICommand AddUserCommand { get; private set; }
    
    private readonly OptionsModel _options;

    private readonly string _userSource;

    private readonly string _userDataSource;
    
    public ObservableCollection<User> Users { get; private set; }

    public int SelectedIndex { get; set; }
    
    private User _selectedUser;
    public User SelectedUser
    {
        get => _selectedUser;
        set
        {
            _selectedUser = value;
            _options.LoadUsersToXml(_userSource, Users);
            _options.UppdateUserData(_userDataSource, value);
            // OnPropertyChanged("SelectedUser");
        }
    }
    
    public OptionsViewModel()
    {
        _options = new OptionsModel();
        
        _userSource = "C:\\Users\\Asus\\RiderProjects\\Book catalog\\Book catalog\\UserCatalog.xml";
        _userDataSource = "C:\\Users\\Asus\\RiderProjects\\Book catalog\\Book catalog\\UserData.xml";

        Users = _options.ReadUsersXml(_userSource);
        {
            User userData = _options.ReadUserDataXml(_userDataSource);
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
        
        EditUserCommand = new RelayCommand(_ =>
        {
            
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