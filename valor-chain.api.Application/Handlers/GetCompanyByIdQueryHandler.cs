using valor_chain.api.Application.Queries;
using valor_chain.api.Domain.Entities;
using valor_chain.api.Domain.Ports.Input;

namespace valor_chain.api.Application.Handlers;

public class GetCompanyByIdQueryHandler
{
    private readonly ICompanyManagementService _companyManagementService;

    public GetCompanyByIdQueryHandler(ICompanyManagementService companyManagementService)
    {
        _companyManagementService = companyManagementService;
    }

    public async Task<Company> Handle(GetCompanyQuery query, CancellationToken cancellationToken)
    {
        // Here, you should hash the password before passing it to the domain service
        // For the example, we pass the password in plain text (not recommended for production)
        return await _companyManagementService.GetCompanyByIdAsync(query.CompanyId);
    }
}