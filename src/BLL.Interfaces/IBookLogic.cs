using Entities;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IBookLogic
    {
        Task Add(Book book);

        Task Remove(string isbn);

        Task Update(string isbn, Book book);

        Task<Book[]> GetAll();

        Task<Book> GetByISBN(string isbn);
    }
}
