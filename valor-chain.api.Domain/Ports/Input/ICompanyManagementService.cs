using valor_chain.api.Domain.Entities;

namespace valor_chain.api.Domain.Ports.Input;

public interface ICompanyManagementService
{
    Task<Company> CreateCompanyAsync(Guid userId, string name, string address, string siren);

    Task<Company> GetCompanyByIdAsync(Guid id);

    Task<IEnumerable<Company>> GetCompaniesByUserIdAsync(Guid userId);

    Task UpdateCompanyAsync(Guid id, string name, string address, string siren);

    Task DeleteCompanyAsync(Guid id);

    Task AddPersonnelToCompanyAsync(Guid companyId, string firstName, string lastName, string role, string email);

    Task AddEquipmentToCompanyAsync(Guid companyId, string name, string type, string serialNumber, DateTime acquisitionDate);

    Task AddSiteToCompanyAsync(Guid companyId, string name, string address, string siteType);
}