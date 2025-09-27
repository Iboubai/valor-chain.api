using valor_chain.api.Domain.Entities.Exploitation;

namespace valor_chain.api.Domain.Ports.Output;

public interface ICheptelRepository
{

    //Cheptel
    Task<Cheptel> CreateUserCheptelAsync(Cheptel cheptel);
    void DeleteUserCheptelAsync(int id);
    Task<IEnumerable<CheptelStatus>> GetAllCheptelStatus();
    Task<IEnumerable<CheptelType>> GetAllCheptelTypes();
    Task<IEnumerable<Cheptel>> GetUserCheptelsAsync(Guid userId);
    Task<Cheptel> UpdateUserCheptelAsync(Cheptel cheptel);
}