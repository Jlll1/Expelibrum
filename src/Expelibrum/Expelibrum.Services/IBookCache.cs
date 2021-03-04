using Expelibrum.Model;

namespace Expelibrum.Services
{
    public interface IBookCache
    {
        Book GetBook(string isbn);
        void UpdateBook(string isbn, Book book);
    }
}