using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Xml.Serialization;

namespace Book_catalog.MVVM.Model;

public class XmlHelper
{
    public ObservableCollection<User> ReadUsersXml(string path)
    {
        try
        {
            ObservableCollection<User>? users;
            
            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<User>));
            using (FileStream flStream = new FileStream(path, FileMode.Open))
            {
                users = serializer.Deserialize(flStream) as ObservableCollection<User>;
            }
        
            return users;
        }
        catch (Exception e)
        {
            MessageBox.Show(e.ToString());
            throw;
        }
    }

    public void LoadUsersXml(string path, ObservableCollection<User> users)
    {
        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<User>));
            using (FileStream fileStream = new FileStream(path, FileMode.Truncate))
            {
                serializer.Serialize(fileStream, users);
            }
        }
        catch (Exception e)
        {
            MessageBox.Show(e.ToString());
            throw;
        }
    }

    public void UpdateUserDataXml(string path, User user)
    {
        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(User));

            using (FileStream fileStream = new FileStream(path, FileMode.Truncate))
            {
                serializer.Serialize(fileStream, user);
            }
        }
        catch (Exception e)
        {
            MessageBox.Show(e.ToString());
            throw;
        }
    }

    public User ReadUserDataXml(string path)
    {
        try
        {
            User? user;

            XmlSerializer serializer = new XmlSerializer(typeof(User));
            using (FileStream flStream = new FileStream(path, FileMode.Open))
            {
                user = serializer.Deserialize(flStream) as User;
            }

            return user;
        }
        catch (Exception e)
        {
            MessageBox.Show(e.ToString());
            throw;
        }
    }
    
    public ObservableCollection<Book> ReadBooksXml(string path)
    {
        try
        {
            ObservableCollection<Book>? books;

            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Book>));
            using (FileStream flStream = new FileStream(path, FileMode.OpenOrCreate))
            {
                books = serializer.Deserialize(flStream) as ObservableCollection<Book>;
            }

            if (books != null)
            {
                return books;
            }

            return books = new ObservableCollection<Book>();
        }
        catch (Exception e)
        {
            MessageBox.Show(e.ToString());
            throw;
        }
    }

    public void LoadBooksXml(string path, ObservableCollection<Book> books)
    {
        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Book>));

            using (FileStream fileStream = new FileStream(path, FileMode.Truncate))
            {
                serializer.Serialize(fileStream, books);
            }
        }
        catch (Exception e)
        {
            MessageBox.Show(e.ToString());
            throw;
        }
        
    }
    
    public void UpdateUserBookmarks(string path, User updatedUser)
    {
        try
        {
            ObservableCollection<User> users = ReadUsersXml(path);
        
            User userToUpdate = users.FirstOrDefault(user => user.Name == updatedUser.Name);
        
            if (userToUpdate != null)
            {
                userToUpdate.SetBookmarks(updatedUser.Bookmarks);
                userToUpdate.SetFavorite(updatedUser.Favorite);
            }
        
            LoadUsersXml(path, users);
        }
        catch (Exception e)
        {
            MessageBox.Show(e.ToString());
            throw;
        }
    }
}