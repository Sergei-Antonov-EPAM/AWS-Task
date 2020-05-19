using Entities;

namespace BLL.Interfaces
{
    public interface IBookLogic
    {
        public void Add(Book book);

        public Book[] GetAll();
    }
}
