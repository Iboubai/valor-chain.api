using gn_core_entities.Location;
using valor_chain.api.Domain.Entities;

namespace valor_chain.api.Domain.Ports.Output;

public interface ILocationRepository
{
    Task<IEnumerable<Region>> GetAllRegionsAsync();
    Task<Region> GetRegionByIdAsync(int id);
    Task<IEnumerable<Prefecture>> GetAllPrefecturesAsync();
    Task<Prefecture> GetPrefectureByIdAsync(int id);
    Task<IEnumerable<Prefecture>> GetPrefecturesByRegionAsync(int regionId);
    Task<IEnumerable<SousPrefecture>> GetAllSousPrefecturesAsync();
    Task<SousPrefecture> GetSousPrefectureByIdAsync(int id);
    Task<IEnumerable<SousPrefecture>> GetSousPrefecturesByPrefectureAsync(int prefectureId);
    Task AddUserLocationAsync(UserLocation userLocation);
    Task<UserLocation> GetUserLocationAsync(Guid userId);
}