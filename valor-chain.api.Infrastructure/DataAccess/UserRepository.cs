using valor_chain.api.Domain.Entities;
using valor_chain.api.Domain.Ports.Output;

namespace valor_chain.api.Infrastructure.DataAccess
{
    public class UserRepository : IUserRepository
    {
        public Task<User> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
