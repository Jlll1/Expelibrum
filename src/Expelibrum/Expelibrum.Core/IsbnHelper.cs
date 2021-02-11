using Newtonsoft.Json.Linq;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Expelibrum.Core
{
    public class IsbnHelper
    {
        private const string ENDPOINT = @"https://openlibrary.org/api/books?bibkeys={0}&jscmd=data&format=json";

        public static async Task<Book> GetBookInfo(string isbn)
        {
            string url = string.Format(ENDPOINT, isbn);

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                string json = await response.Content.ReadAsStringAsync();

                return JObject.Parse(json)?.Properties()?.First()?.Value?.ToObject<Book>();
            }
        }
    }
}
