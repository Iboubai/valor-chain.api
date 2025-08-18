using valor_chain.api.Domain.Entities;

namespace valor_chain.api.Domain.Ports.Output;

public interface ICompanyRepository
{
    Task<Company> GetByIdAsync(Guid id);
    Task<IEnumerable<Company>> GetByUserIdAsync(Guid id);
    Task AddAsync(Company company);
    Task UpdateAsync(Company company);
    Task DeleteAsync(Guid id);
}