using valor_chain.api.Domain.Entities;

namespace valor_chain.api.Domain.Ports.Input
{
    public interface IUserManagementService
    {
        Task<ApiResponse<User>> CreateUserAsync(string firstName, string lastName, string email,
            string password, string phoneNumber);

        Task<ApiResponse<User>> GetUserByIdAsync(Guid id);

        Task<User> AuthenticateUserAsync(string email, string password);

        Task UpdateUserAsync(Guid id, string firstName, string lastName, string email, string phoneNumber);

        Task ChangeUserPasswordAsync(Guid id, string newPassword);

        Task DeleteUserAsync(Guid id);

        Task<ApiResponse<IEnumerable<User>>> GetAllUsersAsync();

        Task<ApiResponse<Profil>> AddUserProfilAsync(Guid userId, string profilName);
    }
}
