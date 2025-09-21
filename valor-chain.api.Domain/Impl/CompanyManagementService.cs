using valor_chain.api.Domain.Entities;
using valor_chain.api.Domain.Ports.Input;

namespace valor_chain.api.Domain.Impl;

public class CompanyManagementService : ICompanyManagementService
{
    public Task<Company> CreateCompanyAsync(Guid userId, string name, string address, string siren)
    {
        throw new NotImplementedException();
    }

    public Task<Company> GetCompanyByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Company>> GetCompaniesByUserIdAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task UpdateCompanyAsync(Guid id, string name, string address, string siren)
    {
        throw new NotImplementedException();
    }

    public Task DeleteCompanyAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task AddPersonnelToCompanyAsync(Guid companyId, string firstName, string lastName, string role, string email)
    {
        throw new NotImplementedException();
    }

    public Task AddEquipmentToCompanyAsync(Guid companyId, string name, string type, string serialNumber,
        DateTime acquisitionDate)
    {
        throw new NotImplementedException();
    }

    public Task AddSiteToCompanyAsync(Guid companyId, string name, string address, string siteType)
    {
        throw new NotImplementedException();
    }
}