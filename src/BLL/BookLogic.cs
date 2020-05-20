using BLL.Interfaces;
using DAL.Interfaces;
using Entities;
using Queues.Interfaces;
using System.Threading.Tasks;

namespace BLL
{
    public class BookLogic : IBookLogic
    {
        private readonly IBookDAO _bookDAO;
        private readonly IBookQueue _bookQueue;

        public BookLogic(IBookDAO bookDAO, IBookQueue bookQueue)
        {
            _bookDAO = bookDAO;
            _bookQueue = bookQueue;
        }

        public async Task Add(Book book)
        {
            await _bookQueue.Push(book.ISBN);
            await _bookDAO.Add(book);
        }

        public async Task<Book[]> GetAll()
        {
            return await _bookDAO.GetAll();
        }

        public async Task<Book> GetByISBN(string isbn)
        {
            return await _bookDAO.GetByISBN(isbn);
        }

        public async Task Remove(string isbn)
        {
            await _bookDAO.Remove(isbn);
        }

        public async Task Update(string isbn, Book book)
        {
            await _bookDAO.Update(isbn, book);
        }
    }
}
