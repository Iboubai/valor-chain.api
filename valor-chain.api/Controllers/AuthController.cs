using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using valor_chain.api.Application.Commands;
using valor_chain.api.Application.Handlers;
using valor_chain.api.Application.Queries;
using valor_chain.api.Domain.Entities;

namespace valor_chain.api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ValorChainControllerBase
    {
        private readonly UserCommandHandler _userCommandHandler;
        private readonly ITokenService _tokenService;
        private readonly ILogger<AuthController> _logger;
        public AuthController(
            UserCommandHandler userCommandHandler,
            ILogger<AuthController> logger, 
            ITokenService tokenService)
        {
            _userCommandHandler = userCommandHandler;
            _logger = logger;
            _tokenService = tokenService;
        }

        
        [HttpPost("login")]
        public async Task<IActionResult> AuthenticateUser([FromBody] LoginCommand command)
        {
            try
            {
                _logger.LogInformation("Authenticate User login request for {Email}", command.Email);

                // 1. Valider l'utilisateur
                var query = new GetLoginQuery { Email = command.Email, Password = command.Password};
                var user = await _userCommandHandler.AuthenticateUserHandle(query, CancellationToken.None);
                //ApiResponse<User> user = new ApiResponse<User>()
                //{
                //    Category = ApiResponseType.Success,
                //    Data = new User("Ibrahima", "Doumbouya", "doumbouyaibrahima@gmail.com", "ezT48hhc8J2M5G", "629817970")
                //};
                    

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
            //ApiResponse<User> user = new ApiResponse<User>()
            //{
            //    Category = ApiResponseType.Success,
            //    Data = new User("Ibrahima", "Doumbouya", "doumbouyaibrahima@gmail.com", "ezT48hhc8J2M5G", "629817970")
            //};

            if (user == null)
            {
                return NotFound();
            }

            // Renvoyez les données de l'utilisateur (sans le mot de passe !)
            return WrappeResponse(user);
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

        [HttpPost("logout")]
        public async Task<IActionResult> LogoutUser()
        {
            // Le principe d'une API stateless avec JWT est que le serveur n'a pas besoin
            // de gérer l'état de connexion. Le "logout" est principalement géré par le client,
            // qui doit supprimer/détruire le token JWT de son stockage.

            // Ce point de terminaison a deux objectifs :
            // 1. Permettre au client de notifier le serveur de la déconnexion (utile pour le logging).
            // 2. Fournir une confirmation au client que la demande de déconnexion a été acceptée.

            _logger.LogInformation("User {UserId} logged out successfully.", User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            // On renvoie simplement une réponse 200 OK pour indiquer que la déconnexion
            // a été prise en compte côté serveur.
            return Ok(new { message = "Logged out successfully" });
        }

    }
}
