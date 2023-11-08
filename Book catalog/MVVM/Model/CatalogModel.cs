using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;
using CatalogLogic;

namespace Book_catalog.MVVM.Model;

public class CatalogModel
{
    public ObservableCollection<Book> ReadXml(string path)
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

    public void LoadXml(string path, ObservableCollection<Book> books)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Book>));

        using (FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate))
        {
            serializer.Serialize(fileStream, books);
        }
    }
}