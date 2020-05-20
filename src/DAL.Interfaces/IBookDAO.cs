using Entities;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IBookDAO
    {
        Task Add(Book book);

        Task Remove(string isbn);

        Task Update(string isbn, Book book);

        Task<Book[]> GetAll();

        Task<Book> GetByISBN(string isbn);
    }
}
