using Expelibrum.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Expelibrum.Services
{
    /// <summary>
    /// Implements <see cref="IBookCache"/>
    /// </summary>
    public class BookCache : IBookCache
    {
        private readonly string _cachePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "books.json");
        private Dictionary<string, Book> _books = new Dictionary<string, Book>();

        public BookCache()
        {
            LoadCache();
        }

        /// <summary>
        /// Retrieves an instance of Book object associated with given ISBN from the cache file
        /// </summary>
        /// <param name="isbn">ISBN number associated with book you want to retrieve</param>
        /// <returns>Instance of a Book object if one is found, null otherwise</returns>
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

        /// <summary>
        /// Updates the Book object associated with given ISBN and stores it in the cache file
        /// </summary>
        /// <param name="isbn">ISBN number of the book you want to update</param>
        /// <param name="book">Book object you want associate with the given ISBN</param>
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
