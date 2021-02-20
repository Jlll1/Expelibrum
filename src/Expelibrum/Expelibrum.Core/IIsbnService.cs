using Expelibrum.Model;
using System.Threading.Tasks;

namespace Expelibrum.Services
{
    public interface IIsbnService
    {
        Task<Book> GetBookFromIsbn(string isbn);
    }
}