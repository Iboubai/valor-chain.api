using GnDapper.Interfaces;
using valor_chain.api.Domain.Entities;
using valor_chain.api.Domain.Ports.Output;

namespace valor_chain.api.Infrastructure.DataAccess;

public class ProfilRepository : IProfilRepository
{
    private readonly IRepository<Profil> _profilRepository;

    public ProfilRepository(IUnitOfWork unitOfWork)
    {
        _profilRepository = unitOfWork.Repository<Profil>();
    }

    public async Task<Profil> GetByIdAsync(Guid id)
    {
        return await _profilRepository.GetByIdAsync(id);
    }

    public async Task AddAsync(Profil profil)
    {
        var d = await _profilRepository.AddAsync(profil);
    }

    public async Task<bool> IsUserProfilExist(Guid userId, string profilName)
    {
        var profil = await _profilRepository.GetWithQuery($"SELECT * from  {_profilRepository.GetTableName()} WHERE UserId = '{userId}' AND ProfilName = '{profilName}'");
        return profil.Any();
    }

    public async Task DeleteAsync(Guid id)
    {
        await _profilRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<Profil>> GetAllAsync()
    {
        return await _profilRepository.GetAllAsync();
    }

    public async Task<IEnumerable<Profil>> GetProfilsByUserIdAsync(Guid userId)
    {
        return await _profilRepository.GetWithQuery($"SELECT * from  {_profilRepository.GetTableName()} WHERE UserId = '{userId}'");
    }
}