using Expelibrum.Model;
using System.Threading.Tasks;

namespace Expelibrum.Services
{
    /// <summary>
    /// Defines an interface to get a Book object associated with given ISBN
    /// </summary>
    public interface IIsbnService
    {
        /// <summary>
        /// Retrieves an instance of a Book object associated with given ISBN
        /// </summary>
        /// <param name="isbn">ISBN associated with the book you want to retrieve</param>
        /// <returns>The task result contains an instance of a Book object associated with given ISBN</returns>
        Task<Book> GetBookFromIsbn(string isbn);
    }
}