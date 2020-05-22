using System.Threading.Tasks;

namespace Queues.Interfaces
{
    public interface IBookQueue
    {
        Task Push(string isbn);
    }
}
