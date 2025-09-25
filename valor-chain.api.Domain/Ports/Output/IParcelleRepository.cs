using valor_chain.api.Domain.Entities.Exploitation;

namespace valor_chain.api.Domain.Ports.Output;

public interface IParcelleRepository
{
    Task CreateUserParcelleAsync(Parcelle parcelle);
    Task<IEnumerable<ParcelleType>> GetAllParcelleTypes();
    Task<IEnumerable<Parcelle>> GetUserParcellesAsync(Guid userId);
}