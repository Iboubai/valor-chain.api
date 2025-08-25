using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using valor_chain.api.Application.Commands;
using valor_chain.api.Application.Handlers;
using valor_chain.api.Application.Queries;
using valor_chain.api.Domain.Entities;

namespace valor_chain.api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class UsersController : ValorChainControllerBase
    {
        private readonly UserCommandHandler _userCommandHandler;
        private readonly ITokenService _tokenService;
        private readonly ILogger<UsersController> _logger;
        public UsersController(
            UserCommandHandler userCommandHandler,
            ILogger<UsersController> logger, 
            ITokenService tokenService)
        {
            _userCommandHandler = userCommandHandler;
            _logger = logger;
            _tokenService = tokenService;
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
        
        [HttpPost("users/login")]
        public async Task<IActionResult> AuthenticateUser([FromBody] LoginCommand command)
        {
            try
            {
                _logger.LogInformation("Authenticate User login request for {Email}", command.Email);

                // 1. Valider l'utilisateur
                var query = new GetLoginQuery { Email = command.Email, Password = command.Password};
                var user = await _userCommandHandler.AuthenticateUserHandle(query, CancellationToken.None);

                if (user.Category == ApiResponseType.NotFound)
                {
                    return NotFound(new { Message = user.Message });
                }

                if (user.Category == ApiResponseType.Unauthorized)
                {
                    return Unauthorized(new { Message = user.Message });
                }

                if (user.Data == null)
                {
                    // Retourne une erreur 401 (Non autorisé) si les identifiants sont incorrects
                    return Unauthorized(new { Message = "Invalid credentials" });
                }

                // 2. Générer le jeton JWT
                var token = _tokenService.GenerateJwtToken(user.Data);

                // 3. Renvoyer le jeton dans la réponse
                // La structure de l'objet doit correspondre à `signInResponseTokenPointer`
                return Ok(new
                {
                    token = token, // Le nom de cette propriété ("token") est crucial
                    user = user // Renvoyez aussi des infos utilisateur
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during Authenticate User.");
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

        [Authorize] // Protège cette route, accessible uniquement avec un token valide
        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUser()
        {
            // Récupère l'ID de l'utilisateur à partir du token (claim)
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var query = new GetUserByIdQuery { UserId = Guid.Parse(userId) };
            var user = await _userCommandHandler.GetUserByIdHandle(query, CancellationToken.None);

            if (user == null)
            {
                return NotFound();
            }

            // Renvoyez les données de l'utilisateur (sans le mot de passe !)
            return WrappeResponse(user);
        }
    }
}
