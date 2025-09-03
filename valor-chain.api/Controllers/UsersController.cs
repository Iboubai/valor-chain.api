using Microsoft.AspNetCore.Mvc;
using valor_chain.api.Application.Commands;
using valor_chain.api.Application.Handlers;
using valor_chain.api.Application.Queries;
using valor_chain.api.Domain.Entities;

namespace valor_chain.api.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ValorChainControllerBase
    {
        private readonly UserCommandHandler _userCommandHandler;
        private readonly ILogger<AuthController> _logger;

        public UsersController(
            UserCommandHandler userCommandHandler,
            ILogger<AuthController> logger)
        {
            _userCommandHandler = userCommandHandler;
            _logger = logger;
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

        [HttpPost("check-email")]
        public async Task<IActionResult> CheckEmail([FromBody] CheckEmailCommand command)
        {
            try
            {
                _logger.LogInformation("Received CheckEmail for {Email}", command.Email);
                var exist = await _userCommandHandler.CheckEmailHandle(command, CancellationToken.None);
                return WrappeResponse(exist);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error CheckEmail.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("check-phone")]
        public async Task<IActionResult> CheckPhone([FromBody] CheckPhoneCommand command)
        {
            try
            {
                _logger.LogInformation("Received CheckPhone for {PhoneNumber}", command.PhoneNumber);
                var exist = await _userCommandHandler.CheckPhoneHandle(command, CancellationToken.None);
                return WrappeResponse(exist);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error CheckPhone.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAllUser()
        {
            try
            {
                _logger.LogInformation("Received All User");
                ApiResponse<IEnumerable<User>> userList = await _userCommandHandler.GetAllUsersHandle(CancellationToken.None);
                return WrappeResponse(userList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving All User.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("getalluserprofils")]
        public async Task<IActionResult> GetAllUserProfil()
        {
            try
            {
                _logger.LogInformation("Received All UserProfil");
                var userProfilList = new ApiResponse<IEnumerable<string>>()
                {
                    Category = ApiResponseType.Success,
                    Data = Enum.GetNames(typeof(UserProfil)).ToList()
                };
                return WrappeResponse(userProfilList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving All UserProfil.");
                return StatusCode(500, "Internal server error");
            }
        }

        //[Authorize] // Protège cette route, accessible uniquement avec un token valide
        [HttpPost("users/changepassword")]
        public async Task<IActionResult> ChangeUserPassword([FromBody] ChangePasswordCommand command)
        {
            try
            {
                _logger.LogInformation("Change User password request for {Email}", command.Email);
                var query = new ChangePasswordQuery { Id = command.Id, Email = command.Email, Password = command.Password};
                var user = await _userCommandHandler.ChangeUserPasswordHandle(query, CancellationToken.None);
                return WrappeResponse(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during Change User password.");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
