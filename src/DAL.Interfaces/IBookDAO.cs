using Entities;

namespace DAL.Interfaces
{
    public interface IBookDAO
    {
        public void Add(Book book);

        public Book[] GetAll();
    }
}
