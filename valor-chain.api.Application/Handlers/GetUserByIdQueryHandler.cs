using valor_chain.api.Application.Queries;
using valor_chain.api.Domain.Entities;
using valor_chain.api.Domain.Ports.Input;

namespace valor_chain.api.Application.Handlers;

public class GetUserByIdQueryHandler
{
    private readonly IUserManagementService _userManagementService;

    public GetUserByIdQueryHandler(IUserManagementService userManagementService)
    {
        _userManagementService = userManagementService;
    }

    public async Task<User> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
    {
        return await _userManagementService.GetUserByIdAsync(query.UserId);
    }
}