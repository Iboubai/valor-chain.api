using GnDapper.Interfaces;
using valor_chain.api.Domain.Entities;
using valor_chain.api.Domain.Ports.Output;

namespace valor_chain.api.Infrastructure.DataAccess;

public class SiteRepository : ISiteRepository
{
    private readonly IRepository<Site> _siteRepository;

    public SiteRepository(IUnitOfWork unitOfWork)
    {
        _siteRepository = unitOfWork.Repository<Site>();
    }

    public async Task<Site> GetByIdAsync(Guid id)
    {
        return await _siteRepository.GetByIdAsync(id);
    }

    public async Task AddAsync(Site Site)
    {
        await _siteRepository.AddAsync(Site);
    }

    public async Task UpdateAsync(Site Site)
    {
        await _siteRepository.UpdateAsync(Site);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _siteRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<Site>> GetAllAsync()
    {
        return await _siteRepository.GetAllAsync();
    }
}