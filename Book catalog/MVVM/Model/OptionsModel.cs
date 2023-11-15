using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;

namespace Book_catalog.MVVM.Model;

public class OptionsModel
{
    public ObservableCollection<User> ReadUsersXml(string path)
    {
        ObservableCollection<User>? users;

        XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<User>));
        using (FileStream flStream = new FileStream(path, FileMode.OpenOrCreate))
        {
            users = serializer.Deserialize(flStream) as ObservableCollection<User>;
        }

        if (users != null)
        {
            return users;
        }
        
        return new ObservableCollection<User>();
    }

    public void LoadUsersToXml(string path, ObservableCollection<User> users)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<User>));

        using (FileStream fileStream = new FileStream(path, FileMode.Truncate))
        {
            serializer.Serialize(fileStream, users);
        }
    }

    public void UppdateUserData(string path, User user)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(User));

        using (FileStream fileStream = new FileStream(path, FileMode.Truncate))
        {
            serializer.Serialize(fileStream, user);
        }
    }

    public User ReadUserDataXml(string path)
    {
        User user;

        XmlSerializer serializer = new XmlSerializer(typeof(User));
        using (FileStream flStream = new FileStream(path, FileMode.OpenOrCreate))
        {
            user = serializer.Deserialize(flStream) as User;
        }

        if (user != null)
        {
            return user;
        }

        return null;
    }
}