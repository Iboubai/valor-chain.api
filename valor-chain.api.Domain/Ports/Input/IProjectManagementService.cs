using valor_chain.api.Domain.Entities;

namespace valor_chain.api.Domain.Ports.Input;

public interface IProjectManagementService
{
    Task<Project> CreateProjectAsync(Guid companyId, string name, string description, DateTime startDate, DateTime plannedEndDate, string projectType);

    Task<Project> GetProjectByIdAsync(Guid id);

    Task<IEnumerable<Project>> GetProjectsByCompanyIdAsync(Guid companyId);

    Task UpdateProjectAsync(Guid id, string name, string description, DateTime plannedEndDate, string projectType);

    Task AddActivityToProjectAsync(Guid projectId, string name, string description, DateTime plannedStartDate, DateTime plannedEndDate);

    Task ChangeProjectStatusAsync(Guid id, string newStatus);

    Task<string> GenerateBusinessPlanForProjectAsync(Guid projectId);
}