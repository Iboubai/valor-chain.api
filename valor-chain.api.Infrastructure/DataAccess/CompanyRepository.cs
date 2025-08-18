using valor_chain.api.Domain.Entities;
using valor_chain.api.Domain.Ports.Output;

namespace valor_chain.api.Infrastructure.DataAccess;

public class CompanyRepository : ICompanyRepository
{
    public Task<Company> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Company>> GetByUserIdAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(Company company)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Company company)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}