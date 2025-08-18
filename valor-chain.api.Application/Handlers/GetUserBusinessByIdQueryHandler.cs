using valor_chain.api.Application.Queries;
using valor_chain.api.Domain.Entities;
using valor_chain.api.Domain.Ports.Input;

namespace valor_chain.api.Application.Handlers;

public class GetUserBusinessByIdQueryHandler
{
    private readonly IUserBusinessManagementService _userBusinessManagementService;

    public GetUserBusinessByIdQueryHandler(IUserBusinessManagementService userBusinessManagementService)
    {
        _userBusinessManagementService = userBusinessManagementService;
    }

    public async Task<UserBusiness> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
    {
        return await _userBusinessManagementService.GetUserBusinessByIdAsync(query.UserId);
    }
}