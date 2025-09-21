using valor_chain.api.Domain.Entities;

namespace valor_chain.api
{
    public interface ITokenService
    {
        string GenerateJwtToken(User user);
    }
}
