using valor_chain.api.Domain.Entities;
using valor_chain.api.Domain.Ports.Input;

namespace valor_chain.api.Domain.Impl;

public class ProjectManagementService : IProjectManagementService
{
    public Task<Project> CreateProjectAsync(Guid companyId, string name, string description, DateTime startDate, DateTime plannedEndDate,
        string projectType)
    {
        throw new NotImplementedException();
    }

    public Task<Project> GetProjectByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Project>> GetProjectsByCompanyIdAsync(Guid companyId)
    {
        throw new NotImplementedException();
    }

    public Task UpdateProjectAsync(Guid id, string name, string description, DateTime plannedEndDate, string projectType)
    {
        throw new NotImplementedException();
    }

    public Task AddActivityToProjectAsync(Guid projectId, string name, string description, DateTime plannedStartDate,
        DateTime plannedEndDate)
    {
        throw new NotImplementedException();
    }

    public Task ChangeProjectStatusAsync(Guid id, string newStatus)
    {
        throw new NotImplementedException();
    }

    public Task<string> GenerateBusinessPlanForProjectAsync(Guid projectId)
    {
        throw new NotImplementedException();
    }
}