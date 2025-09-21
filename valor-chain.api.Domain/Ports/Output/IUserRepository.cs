using valor_chain.api.Domain.Entities;

namespace valor_chain.api.Domain.Ports.Output;

public interface IUserRepository
{
    Task<User> GetByIdAsync(Guid id);
    Task<User> AuthenticateUserAsync(string email, string password);
    Task<User> GetByEmailAsync(string email);
    Task AddAsync(User User);
    Task<bool> IsUserExist(Guid userId);
    Task UpdateAsync(User User);
    Task<User> ChangeUserPasswordAsync(Guid id, string email, string password);
    Task DeleteAsync(Guid id);
    Task<IEnumerable<User>> GetAllAsync();
    Task<User> GetByPhoneNumberAsync(string phoneNumbre);
}
