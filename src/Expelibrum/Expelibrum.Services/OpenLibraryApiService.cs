using Newtonsoft.Json.Linq;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Expelibrum.Model;

namespace Expelibrum.Services
{
    /// <summary>
    /// Implements <see cref="IIsbnService"/>
    /// </summary>
    public class OpenLibraryApiService : IIsbnService
    {
        private const string ENDPOINT = @"https://openlibrary.org/api/books?bibkeys={0}&jscmd=data&format=json";
        private IBookCache _cache;

        public OpenLibraryApiService(IBookCache cache)
        {
            _cache = cache;
        }

        public async Task<Book> GetBookFromIsbn(string isbn)
        {
            Book book = _cache.GetBook(isbn);
            if (book != null)
            {
                return book;
            }
            else
            {
                string url = string.Format(ENDPOINT, isbn);

                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(url);
                    string json = await response.Content.ReadAsStringAsync();
                    book = JObject.Parse(json)?.Properties()?.First()?.Value?.ToObject<Book>();
                    _cache.UpdateBook(isbn, book);
                    return book;
                }
            }
            
        }
    }
}
