using gn_core_entities.Location;
using valor_chain.api.Domain.Entities;

namespace valor_chain.api.Domain.Ports.Input
{
    public interface ILocationManagementService
    {
        Task<ApiResponse<IEnumerable<Region>>> GetAllRegionsAsync();
        Task<ApiResponse<Region>> GetRegionByIdAsync(int regionId);
        Task<ApiResponse<IEnumerable<Prefecture>>> GetAllPrefecturesAsync();
        Task<ApiResponse<Prefecture>> GetPrefectureByIdAsync(int prefectureId);
        Task<ApiResponse<IEnumerable<Prefecture>>> GetPrefecturesByRegionIdAsync(int regionId);
        Task<ApiResponse<IEnumerable<SousPrefecture>>> GetAllSousPrefecturesAsync();
        Task<ApiResponse<SousPrefecture>> GetSousPrefectureByIdAsync(int sousPrefectureId);
        Task<ApiResponse<IEnumerable<SousPrefecture>>> GetSousPrefecturesByPrefectureIdAsync(int prefectureId);
    }
}
