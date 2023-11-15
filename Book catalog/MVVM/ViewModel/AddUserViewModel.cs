using System;
using System.Windows;
using System.Windows.Input;
using Book_catalog.Core;
using Book_catalog.MVVM.Model;

namespace Book_catalog.MVVM.ViewModel;

public class AddUserViewModel : ObservableObject
{
    public RelayCommand ChooseIconCommand { get; private set; }
    public RelayCommand AddUserCommand { get; private set; }

    public event EventHandler<UserEventArgs>? UserAdded;
    
    private string _name;
    public string Name
    {
        get => _name;
        set
        {
            if (!string.IsNullOrEmpty(value))
            {
                _name = value;
            }
            else
            {
                throw new Exception();
            }
        }
    }

    private string _chooseIcon;
    public string ChooseIcon
    {
        get => _chooseIcon;
        set
        {
            if (!string.IsNullOrEmpty(value))
            {
                _chooseIcon = value;
            }
            else
            {
                throw new Exception();
            }
        }
    }
    
    public AddUserViewModel()
    {
        ChooseIcon = "C:\\Users\\Asus\\RiderProjects\\Book catalog\\Book catalog\\Icons\\UserProfileIcon.png";
        Name = "1";
        
        ChooseIconCommand = new RelayCommand(_ =>
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Text files (*.jpg)|*.jpg|All files (*.*)|*.*";
            
            bool? result = openFileDialog.ShowDialog();
            
            if (result == true)
            {
                string selectedFilePath = openFileDialog.FileName;
                ChooseIcon = selectedFilePath;
                OnPropertyChanged("ChooseIcon");
            }
        });

        AddUserCommand = new RelayCommand(_ =>
        {
            if (Name.Length < 3)
            {
                MessageBox.Show("The name must consist of at least 3 letters");
            }
            else
            {
                User user = new User(Name, ChooseIcon);
                UserAdded?.Invoke(this, new UserEventArgs(user));
            }
            
        });
    }
}