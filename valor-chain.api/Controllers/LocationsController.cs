using gn_core_entities.Location;
using Microsoft.AspNetCore.Mvc;
using valor_chain.api.Application.Handlers;
using valor_chain.api.Application.Queries;
using valor_chain.api.Domain.Entities;
//using valor_chain.api.Domain.Entities.Locations;

namespace valor_chain.api.Controllers
{
    [ApiController]
    [Route("api/locations")]
    public class LocationsController : ValorChainControllerBase
    {
        private readonly LocationCommandHandler _locationCommandHandler;
        private readonly ILogger<LocationsController> _logger;


        public LocationsController(LocationCommandHandler locationCommandHandler, ILogger<LocationsController> logger)
        {
            _locationCommandHandler = locationCommandHandler;
            _logger = logger;
        }

        [HttpGet("regions/getall")]
        public async Task<IActionResult> GetAllRegions()
        {
            try
            {
                _logger.LogInformation("Received Get All Regions");

                var regionList = await _locationCommandHandler.GetAllRegionsHandle(CancellationToken.None);

                return WrappeResponse(regionList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving All Regions.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("prefectures/{regionId}")]
        public async Task<IActionResult> GetPrefectures(int regionId)
        {
            try
            {
                _logger.LogInformation("Received GetPrefecture by regionId");
                var query = new GetPrefecturesByRegionIdQuery { RegionId= regionId };
                var prefectureList = await _locationCommandHandler.GetPrefecturesByRegionIdHandle(query, CancellationToken.None);
                
                return WrappeResponse(prefectureList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Prefecture by regionId.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("sous-prefectures/{prefectureId}")]
        public async Task<IActionResult> GetSousPrefectures(int prefectureId)
        {
            try
            {
                _logger.LogInformation("Received GetSousPrefectures by prefectureId");
                var query = new GetSousPrefecturesByPrefectureIdQuery { PrefectureId = prefectureId };
                var sousPrefectureList = await _locationCommandHandler.GetSousPrefecturesByPrefectureIdHandle(query, CancellationToken.None);

                return WrappeResponse(sousPrefectureList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving SousPrefecture by regionId.");
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
