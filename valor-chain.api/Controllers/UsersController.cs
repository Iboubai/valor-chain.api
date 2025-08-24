using Microsoft.AspNetCore.Mvc;
using valor_chain.api.Application.Commands;
using valor_chain.api.Application.Handlers;
using valor_chain.api.Application.Queries;

namespace valor_chain.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ValorChainControllerBase
    {
        private readonly UserCommandHandler _userCommandHandler;
        private readonly ILogger<UsersController> _logger;
        public UsersController(
            UserCommandHandler userCommandHandler,
            ILogger<UsersController> logger)
        {
            _userCommandHandler = userCommandHandler;
            _logger = logger;
        }

        [HttpPost("createuser")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
        {
            try
            {
                _logger.LogInformation("Received User creation request for {Email}", command.Email);
                var user = await _userCommandHandler.CreateUserHandle(command, CancellationToken.None);
                return WrappeResponse(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating User.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            try
            {
                _logger.LogInformation("Received User retrieval request by ID: {UserId}", id);
                var query = new GetUserByIdQuery { UserId = id };
                var user = await _userCommandHandler.GetUserByIdHandle(query, CancellationToken.None);
                return WrappeResponse(user);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving User by ID.");
                return StatusCode(500, "Internal server error");
            }
        }
        
        [HttpPost("addprofil")]
        public async Task<IActionResult> AddUserProfil([FromBody] AddUserProfilCommand command)
        {
            try
            {
                _logger.LogInformation("Received UserProfil creation request for {UserId}", command.UserId);
                var user = await _userCommandHandler.AddUserProfilHandle(command, CancellationToken.None);
                return WrappeResponse(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding Profil to User.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAllUser()
        {
            try
            {
                _logger.LogInformation("Received All User");
                var userList = await _userCommandHandler.GetAllUsersHandle(CancellationToken.None);
                return WrappeResponse(userList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving All User.");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
