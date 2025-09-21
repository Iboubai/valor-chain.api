using gn_core_entities.Location;
using valor_chain.api.Application.Queries;
using valor_chain.api.Domain.Entities;
using valor_chain.api.Domain.Ports.Input;

namespace valor_chain.api.Application.Handlers
{
    public class LocationCommandHandler
    {
        private readonly ILocationManagementService _locationManagementService;

        public LocationCommandHandler(ILocationManagementService locationManagementService)
        {
            _locationManagementService = locationManagementService;
        }

        public async Task<ApiResponse<IEnumerable<Region>>> GetAllRegionsHandle(CancellationToken none)
        {
            return await _locationManagementService.GetAllRegionsAsync();
        }

        public async Task<ApiResponse<IEnumerable<Prefecture>>> GetPrefecturesByRegionIdHandle(GetPrefecturesByRegionIdQuery query, CancellationToken none)
        {
            return await _locationManagementService.GetPrefecturesByRegionIdAsync(query.RegionId);
        }

        public async Task<ApiResponse<IEnumerable<SousPrefecture>>> GetSousPrefecturesByPrefectureIdHandle(GetSousPrefecturesByPrefectureIdQuery query, CancellationToken none)
        {
            return await _locationManagementService.GetSousPrefecturesByPrefectureIdAsync(query.PrefectureId);
        }
    }
}
