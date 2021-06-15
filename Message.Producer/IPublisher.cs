using System.Threading.Tasks;

namespace Message.Producer
{
    public interface IPublisher
    {
        Task Send(object obj, bool shouldFail);
    }
}