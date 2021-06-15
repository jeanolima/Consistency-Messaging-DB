using Message.Consumer.Model;
using System.Threading.Tasks;

namespace Message.Domain
{
    public interface IMessageService
    {
        Task Process(Person messageObject);
    }
}