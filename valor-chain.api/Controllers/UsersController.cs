using Microsoft.AspNetCore.Mvc;
using valor_chain.api.Application.Commands;
using valor_chain.api.Application.Handlers;
using valor_chain.api.Application.Queries;

namespace valor_chain.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly CreateUserCommandHandler _createUserHandler;
        private readonly GetUserByIdQueryHandler _getUserByIdHandler;
        private readonly ILogger<UsersController> _logger;
        public UsersController(
            CreateUserCommandHandler createUserHandler,
            GetUserByIdQueryHandler getUserByIdHandler,
            ILogger<UsersController> logger)
        {
            _createUserHandler = createUserHandler;
            _getUserByIdHandler = getUserByIdHandler;
            _logger = logger;
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
        {
            try
            {
                _logger.LogInformation("Received user creation request for {Email}", command.Email);
                var user = await _createUserHandler.Handle(command,
                    CancellationToken.None);
                return CreatedAtAction(nameof(GetUserById), new
                {
                    id = user.Id
                }, user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating user.");
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            try
            {
                _logger.LogInformation("Received user retrieval request by ID: {UserId}", id);
                var query = new GetUserByIdQuery { UserId = id };
                var user = await _getUserByIdHandler.Handle(query,
                    CancellationToken.None);
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving user by ID.");
                return StatusCode(500, "Internal server error");
            }
        }
        // Other endpoints for company, project management, etc.
    }

}
