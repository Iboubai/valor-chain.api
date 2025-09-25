using valor_chain.api.Domain.Entities;

namespace valor_chain.api.Domain.Ports.Output;

public interface IProfilRepository
{
    Task<UserProfil> GetByIdAsync(Guid id);
    Task AddAsync(UserProfil profil);
    Task<bool> IsUserProfilExist(Guid userId, string profilName);
    Task DeleteAsync(Guid id);
    Task<IEnumerable<UserProfil>> GetAllAsync();
    Task<IEnumerable<UserProfil>> GetProfilsByUserIdAsync(Guid UserId);
}
