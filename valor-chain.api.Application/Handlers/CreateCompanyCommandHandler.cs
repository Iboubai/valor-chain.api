using valor_chain.api.Application.Commands;
using valor_chain.api.Domain.Entities;
using valor_chain.api.Domain.Ports.Input;

namespace valor_chain.api.Application.Handlers;

public class CreateCompanyCommandHandler
{
    private readonly ICompanyManagementService _companyManagementService;

    public CreateCompanyCommandHandler(ICompanyManagementService companyManagementService)
    {
        _companyManagementService = companyManagementService;
    }

    public async Task<Company> Handle(CreateCompanyCommand command, CancellationToken cancellationToken)
    {
        return await _companyManagementService.CreateCompanyAsync(
            command.UserId,
            command.Name,
            command.Address,
            command.Siren
        );
    }
}