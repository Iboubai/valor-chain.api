using valor_chain.api.Domain.Entities;
using valor_chain.api.Domain.Ports.Output;

namespace valor_chain.api.Infrastructure.DataAccess;

public class ProjectRepository : IProjectRepository
{
    public Task<Project> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Project>> GetByCompanyIdAsync(Guid companyId)
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(Project project)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Project project)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}