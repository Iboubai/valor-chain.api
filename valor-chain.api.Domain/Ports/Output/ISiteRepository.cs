using valor_chain.api.Domain.Entities;

namespace valor_chain.api.Domain.Ports.Output;

public interface ISiteRepository
{
    Task<Site> GetByIdAsync(Guid id);
    Task AddAsync(Site Site);
    Task UpdateAsync(Site Site);
    Task DeleteAsync(Guid id);
    Task<IEnumerable<Site>> GetAllAsync();
}