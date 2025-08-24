using valor_chain.api.Domain.Entities;

namespace valor_chain.api.Domain.Ports.Output;

public interface IProfilRepository
{
    Task<Profil> GetByIdAsync(Guid id);
    Task AddAsync(Profil profil);
    Task<bool> IsUserProfilExist(Guid userId, string profilName);
    Task DeleteAsync(Guid id);
    Task<IEnumerable<Profil>> GetAllAsync();
    Task<IEnumerable<Profil>> GetProfilsByUserIdAsync(Guid UserId);
}