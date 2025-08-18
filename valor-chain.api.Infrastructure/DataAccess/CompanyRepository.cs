using GnDapper.Interfaces;
using valor_chain.api.Domain.Entities;
using valor_chain.api.Domain.Ports.Output;

namespace valor_chain.api.Infrastructure.DataAccess;

public class CompanyRepository : ICompanyRepository
{
    private readonly IRepository<Company> _CompanyRepository;

    public CompanyRepository(IUnitOfWork unitOfWork)
    {
        _CompanyRepository = unitOfWork.Repository<Company>();
    }

    public async Task<Company> GetByIdAsync(Guid id)
    {
        return await _CompanyRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Company>> GetByUserIdAsync(Guid id)
    {
        //return await _CompanyRepository.GetByIdAsync(id);
        //TODO
        return new List<Company>();
    }

    public async Task AddAsync(Company Company)
    {
        await _CompanyRepository.AddAsync(Company);
    }

    public async Task UpdateAsync(Company Company)
    {
        await _CompanyRepository.UpdateAsync(Company);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _CompanyRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<Company>> GetAllAsync()
    {
        return await _CompanyRepository.GetAllAsync();
    }
}