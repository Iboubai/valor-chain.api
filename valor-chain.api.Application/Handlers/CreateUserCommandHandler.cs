using valor_chain.api.Application.Commands;
using valor_chain.api.Domain.Entities;
using valor_chain.api.Domain.Ports.Input;

namespace valor_chain.api.Application.Handlers
{
    public class CreateUserCommandHandler
    {
        private readonly IUserBusinessManagementService _userBusinessManagementService;
        public CreateUserCommandHandler(IUserBusinessManagementService userBusinessManagementService)
        {
            _userBusinessManagementService = userBusinessManagementService;
        }
        public async Task<User> Handle(CreateUserCommand command,
            CancellationToken cancellationToken)
        {
            // Here, you should hash the password before passing it to the domain service
            // For the example, we pass the password in plain text (not recommended for production)
            return await _userBusinessManagementService.CreateUserBusinessAsync(
                command.FirstName,
                command.LastName,
                command.Email,
                command.Password // HASH THIS!
            );
        }
    }
}
