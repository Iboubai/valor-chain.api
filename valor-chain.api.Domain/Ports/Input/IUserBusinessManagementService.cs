using valor_chain.api.Domain.Entities;

namespace valor_chain.api.Domain.Ports.Input
{
    public interface IUserBusinessManagementService
    {
        Task<UserBusiness> CreateUserBusinessAsync(string firstName, string lastName, string email, string password);

        Task<UserBusiness> GetUserBusinessByIdAsync(Guid id);

        Task<UserBusiness> AuthenticateUserBusinessAsync(string email, string password);

        Task UpdateUserProfilAsync(Guid id, string firstName, string lastName, string email);

        Task ChangeUserBusinessPasswordAsync(Guid id, string newPassword);

        Task DeleteUserBusinessAsync(Guid id);

        Task<IEnumerable<UserBusiness>> GetAllUsersBusinessAsync();
    }
}
