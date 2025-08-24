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
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            try
            {
                _logger.LogInformation("Received User login request for {Email}", command.Email);

                // 1. Valider l'utilisateur
                //                var user = await _authService.ValidateUser(command.Email, command.Password);
                var query = new GetUserByIdQuery { UserId = new Guid("D59B54DA-6D22-46C2-833F-D1B04F642BE2") };
                var user = await _userCommandHandler.GetUserByIdHandle(query, CancellationToken.None);

                if (user == null)
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
                _logger.LogError(ex, "Error during User login.");
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
