using valor_chain.api.Application.Commands;
using valor_chain.api.Domain.Entities;
using valor_chain.api.Domain.Ports.Input;

namespace valor_chain.api.Application.Handlers;

public class CreateProjectCommandHandler
{
    private readonly IProjectManagementService _projectManagementService;

    public CreateProjectCommandHandler(IProjectManagementService projectManagementService)
    {
        _projectManagementService = projectManagementService;
    }

    public async Task<Project> Handle(CreateProjectCommand command, CancellationToken cancellationToken)
    {
        return await _projectManagementService.CreateProjectAsync(
            command.CompanyId,
            command.Name,
            command.Description,
            command.StartDate,
            command.PlannedEndDate,
            command.ProjectType
        );
    }
}