using valor_chain.api.Domain.Entities;
using valor_chain.api.Domain.Ports.Input;

namespace valor_chain.api.Domain.Impl
{
    public class UserManagementService : IUserManagementService
    {
        public Task<User> CreateUserAsync(string firstName, string lastName, string email, string password,
            string userType)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<User> AuthenticateUserAsync(string email, string password)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUserProfileAsync(Guid id, string firstName, string lastName, string email, string userType)
        {
            throw new NotImplementedException();
        }

        public Task ChangeUserPasswordAsync(Guid id, string newPassword)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUserAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }
    }
}
    
