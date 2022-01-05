using NeoBoilerplate.Domain.Entities;
using System.Threading.Tasks;

namespace NeoBoilerplate.Application.Contracts.Persistence
{
    public interface IMessageRepository : IAsyncRepository<Message>
    {
        public Task<Message> GetMessage(string Code, string Lang);
    }
}
