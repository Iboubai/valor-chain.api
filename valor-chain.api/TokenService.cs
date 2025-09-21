using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using valor_chain.api.Domain.Entities;

namespace valor_chain.api;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateJwtToken(User user)
    {
        // 1. Récupérer la clé secrète depuis la configuration
        var jwtKey = _configuration["Jwt:Key"];
        if (string.IsNullOrEmpty(jwtKey))
        {
            throw new InvalidOperationException("La clé secrète JWT n'est pas configurée dans appsettings.json");
        }
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        // 2. Définir les "claims" (revendications) du token
        // Les claims sont les informations que vous stockez dans le token (ID, nom, email, rôles, etc.)
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()), // 'sub' est le standard pour l'ID de l'utilisateur
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Name, user.FirstName), // ou user.FullName selon votre modèle
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // Un identifiant unique pour ce token
                
            // Ajoutez ici d'autres claims si nécessaire, comme les rôles
            // if (user.Roles != null)
            // {
            //     foreach (var role in user.Roles)
            //     {
            //         claims.Add(new Claim(ClaimTypes.Role, role));
            //     }
            // }
        };

        // 3. Récupérer les métadonnées du token depuis la configuration
        var issuer = _configuration["Jwt:Issuer"];
        var audience = _configuration["Jwt:Audience"];
        var durationInMinutes = Convert.ToDouble(_configuration["Jwt:DurationInMinutes"]);

        // 4. Créer le token
        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(durationInMinutes),
            signingCredentials: credentials);

        // 5. Sérialiser le token en une chaîne de caractères
        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }
}