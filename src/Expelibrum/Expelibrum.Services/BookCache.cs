using Expelibrum.Model;
using System.Collections.Generic;

namespace Expelibrum.Services
{
    public class BookCache
    {
        private readonly Dictionary<string, Book> _books = new Dictionary<string, Book>();

        public BookCache()
        {
            // TODO : Deserialize json
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
        }

        public void SaveCacheToPath(string path)
        {
            //TODO: Serialize to json and save to file
        }
    }
}
