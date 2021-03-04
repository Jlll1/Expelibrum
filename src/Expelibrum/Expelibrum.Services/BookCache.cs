using Expelibrum.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Expelibrum.Services
{
    public class BookCache : IBookCache
    {
        private readonly string _cachePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "books.json");
        private Dictionary<string, Book> _books = new Dictionary<string, Book>();

        public BookCache()
        {
            LoadCache();
        }

        public Book GetBook(string isbn)
        {
            if (!_books.ContainsKey(isbn))
            {
                return null;
            }
            else
            {
                return _books[isbn];
            }
        }

        public void UpdateBook(string isbn, Book book)
        {
            _books[isbn] = book;
            SaveCache();
        }

        private void SaveCache()
        {
            JsonSerializer serializer = new JsonSerializer();
            using (StreamWriter sw = new StreamWriter(_cachePath))
            {
                serializer.Serialize(sw, _books);
            }
        }

        private void LoadCache()
        {
            if (File.Exists(_cachePath))
                _books = JsonConvert.DeserializeObject<Dictionary<string, Book>>(File.ReadAllText(_cachePath));
        }
    }
}
