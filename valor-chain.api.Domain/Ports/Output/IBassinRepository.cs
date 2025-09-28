using valor_chain.api.Domain.Entities.Exploitation;

namespace valor_chain.api.Domain.Ports.Output;

public interface IBassinRepository
{

    //Bassin
    Task<Bassin> CreateUserBassinAsync(Bassin bassin);
    void DeleteUserBassinAsync(int id);
    Task<IEnumerable<BassinStatus>> GetAllBassinStatus();
    Task<IEnumerable<BassinType>> GetAllBassinTypes();
    Task<IEnumerable<Bassin>> GetUserBassinsAsync(Guid userId);
    Task<Bassin> UpdateUserBassinAsync(Bassin bassin);
}