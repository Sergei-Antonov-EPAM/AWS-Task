using BLL.Interfaces;
using DAL.Interfaces;
using Entities;

namespace BLL
{
    public class BookLogic : IBookLogic
    {
        private readonly IBookDAO _bookDAO;

        public BookLogic(IBookDAO bookDAO)
        {
            _bookDAO = bookDAO;
        }

        public void Add(Book book)
        {
            _bookDAO.Add(book);
        }

        public Book[] GetAll()
        {
            return _bookDAO.GetAll();
        }
    }
}
