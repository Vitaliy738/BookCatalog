using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using CatalogLogic;

namespace Book_catalog.MVVM.Model;

public class CatalogModel
{
    public ObservableCollection<Book> ReadJson(string path)
    {
        ObservableCollection<Book> books = new ObservableCollection<Book>();
        
        using (StreamReader reader = File.OpenText(path))
        {
            while (!reader.EndOfStream)
            {
                string? jsonFromFile = reader.ReadLine();
                if (!string.IsNullOrWhiteSpace(jsonFromFile))
                {
                    Book deserializedBook = JsonSerializer.Deserialize<Book>(jsonFromFile) ?? throw new InvalidOperationException("Json reader exception");
                    books.Add(deserializedBook);
                }
            }
        }

        return books;
    }
}