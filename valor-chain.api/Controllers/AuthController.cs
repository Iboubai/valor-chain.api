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

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUser()
        {
            // Récupère l'ID de l'utilisateur à partir du token (claim)
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

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

        [Authorize]
        [HttpPatch("{id}")] // Utilisation de PATCH pour les mises à jour partielles
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserCommand command)
        {
            // 1. Vérification de sécurité : L'utilisateur peut-il modifier ce profil ?
            // Un utilisateur ne devrait pouvoir modifier que son propre profil.
            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (currentUserId == null || Guid.Parse(currentUserId) != id)
            {
                // L'utilisateur essaie de modifier un profil qui n'est pas le sien.
                _logger.LogWarning("Security violation: User {CurrentUserId} attempted to modify profile of user {TargetUserId}.", currentUserId, id);
                return Forbid(); // Renvoie un statut 403 Forbidden
            }

            // 2. Validation du modèle (vérifie les annotations comme [StringLength])
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Renvoie un statut 400 avec les erreurs de validation
            }

            try
            {
                _logger.LogInformation("Received update request for user {UserId}", id);

                // 3. Appel au Command Handler pour exécuter la logique métier
                // Il est de la responsabilité du handler de trouver l'utilisateur,
                // d'appliquer les modifications et de sauvegarder en base de données.
                var updatedUserResult = await _userCommandHandler.UpdateUserHandle(id, command, CancellationToken.None);

                // 4. Gestion de la réponse du handler
                if (updatedUserResult == null)
                {
                    // Le handler n'a pas trouvé l'utilisateur en base de données.
                    return NotFound(new { message = "User not found" }); // Renvoie un statut 404
                }

                // Si tout s'est bien passé, le handler a déjà sauvegardé les modifications.
                // On peut renvoyer une réponse 200 OK avec l'utilisateur mis à jour,
                // ou simplement une réponse 204 No Content pour indiquer le succès.

                // Option A : Renvoyer l'objet mis à jour (pratique pour le frontend)
                //return WrappeResponse(updatedUserResult); // Renvoie 200 OK
                return WrappeResponse(updatedUserResult);

                // Option B : Renvoyer "No Content" (plus léger)
                // return NoContent(); // Renvoie 204 No Content
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating user {UserId}.", id);
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
