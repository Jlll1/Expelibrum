using Expelibrum.Model;

namespace Expelibrum.Services
{
    /// <summary>
    /// Defines an interface to retrieve and store instances of Book type from cache
    /// </summary>
    public interface IBookCache
    {
        /// <summary>
        /// Gets an instance of Book type
        /// </summary>
        /// <param name="isbn">ISBN number of the book you want to get</param>
        /// <returns>Instance of Book object</returns>
        Book GetBook(string isbn);

        /// <summary>
        /// Updates the Book object associated with given ISBN
        /// </summary>
        /// <param name="isbn">ISBN number of the book you want to update</param>
        /// <param name="book">Book object you want associate with the given ISBN</param>
        void UpdateBook(string isbn, Book book);
    }
}