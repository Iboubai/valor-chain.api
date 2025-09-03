using valor_chain.api.Domain.Entities;

namespace valor_chain.api.Domain.Ports.Input
{
    public interface IUserManagementService
    {
        Task<ApiResponse<User>> CreateUserAsync(User user);

        Task<ApiResponse<User>> GetUserByIdAsync(Guid id);

        Task<ApiResponse<User>> AuthenticateUserAsync(string email, string password);
        
        Task UpdateUserAsync(Guid id, string firstName, string lastName, string email, string phoneNumber);

        Task<ApiResponse<User>> ChangeUserPasswordAsync(Guid id, string email, string newPassword);

        Task DeleteUserAsync(Guid id);

        Task<ApiResponse<IEnumerable<User>>> GetAllUsersAsync();

        Task<ApiResponse<Profil>> AddUserProfilAsync(Guid userId, string profilName);

        Task<ApiResponse<bool>> CheckEmailAsync(string email);

        Task<ApiResponse<bool>> CheckPhoneAsync(string phoneNumber);
    }
}
