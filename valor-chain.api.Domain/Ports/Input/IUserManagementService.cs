using valor_chain.api.Domain.Entities;

namespace valor_chain.api.Domain.Ports.Input
{
    public interface IUserManagementService
    {
        Task<User> CreateUserAsync(string firstName, string lastName, string email, string password, string userType);

        Task<User> GetUserByIdAsync(Guid id);

        Task<User> AuthenticateUserAsync(string email, string password);

        Task UpdateUserProfileAsync(Guid id, string firstName, string lastName, string email, string userType);

        Task ChangeUserPasswordAsync(Guid id, string newPassword);

        Task DeleteUserAsync(Guid id);

        Task<IEnumerable<User>> GetAllUsersAsync();
    }
}
