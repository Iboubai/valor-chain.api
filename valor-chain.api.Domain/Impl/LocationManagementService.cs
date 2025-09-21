using gn_core_entities.Location;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using valor_chain.api.Domain.Entities;
using valor_chain.api.Domain.Ports.Input;
using valor_chain.api.Domain.Ports.Output;

namespace valor_chain.api.Domain.Impl
{
    public class LocationManagementService : ILocationManagementService
    {
        private readonly ILocationRepository _locationRepository;
        private readonly ILogger<LocationManagementService> _logger;

        public LocationManagementService(ILocationRepository locationRepository, ILogger<LocationManagementService> logger)
        {
            _locationRepository = locationRepository ?? throw new ArgumentNullException(nameof(locationRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<ApiResponse<IEnumerable<Region>>> GetAllRegionsAsync()
        {
            _logger.LogInformation("Attempting to get all regions");

            var res = await _locationRepository.GetAllRegionsAsync();

            _logger.LogInformation("Successfully get all regions");
            var response = new ApiResponse<IEnumerable<Region>>
            {
                Category = ApiResponseType.Success,
                Message = string.Empty,
                Data = res
            };
            return response;
        }

        public async Task<ApiResponse<Region>> GetRegionByIdAsync(int regionId)
        {
            var response = new ApiResponse<Region>
            {
                Message = $"Attempting to get region with ID: {regionId}"
            };
            string message;
            _logger.LogInformation(response.Message);

            var res = await _locationRepository.GetRegionByIdAsync(regionId);

            if (res == null)
            {

                response.Category = ApiResponseType.NotFound;
                message = $"Region with ID {regionId} not found.";
                response.Errors.Add(message);
                _logger.LogWarning(message);
                return response;
            }

            _logger.LogInformation("Successfully get region with ID: {regionId}", regionId);            
            response.Category = ApiResponseType.Success;
            response.Message = string.Empty;
            response.Data = res;
            return response;
        }

        public async Task<ApiResponse<IEnumerable<Prefecture>>> GetAllPrefecturesAsync()
        {
            _logger.LogInformation("Attempting to get all prefectures");

            var res = await _locationRepository.GetAllPrefecturesAsync();

            _logger.LogInformation("Successfully get all prefectures");
            var response = new ApiResponse<IEnumerable<Prefecture>>
            {
                Category = ApiResponseType.Success,
                Message = string.Empty,
                Data = res
            };
            return response;
        }

        public async Task<ApiResponse<Prefecture>> GetPrefectureByIdAsync(int prefectureId)
        {
            var response = new ApiResponse<Prefecture>
            {
                Message = $"Attempting to get prefecture with ID: {prefectureId}"
            };
            string message;
            _logger.LogInformation(response.Message);

            var res = await _locationRepository.GetPrefectureByIdAsync(prefectureId);

            if (res == null)
            {

                response.Category = ApiResponseType.NotFound;
                message = $"Prefecture with ID {prefectureId} not found.";
                response.Errors.Add(message);
                _logger.LogWarning(message);
                return response;
            }

            _logger.LogInformation("Successfully Get Prefecture with ID: {prefectureId}", prefectureId);
            response.Category = ApiResponseType.Success;
            response.Message = string.Empty;
            response.Data = res;
            return response;
        }
        public async Task<ApiResponse<IEnumerable<Prefecture>>> GetPrefecturesByRegionIdAsync(int regionId)
        {
            var response = new ApiResponse<IEnumerable<Prefecture>>
            {
                Message = $"Attempting to get prefecture by region with ID: {regionId}"
            };
            string message;
            _logger.LogInformation(response.Message);

            var res = await _locationRepository.GetPrefecturesByRegionAsync(regionId);

            if (res == null)
            {
                response.Category = ApiResponseType.NotFound;
                message = $"Prefecture by region with ID {regionId} not found.";
                response.Errors.Add(message);
                _logger.LogWarning(message);
                return response;
            }

            _logger.LogInformation("Successfully Get Prefecture by region with ID: {regionId}", regionId);
            response.Category = ApiResponseType.Success;
            response.Message = string.Empty;
            response.Data = res;
            return response;
        }

        public async Task<ApiResponse<IEnumerable<SousPrefecture>>> GetAllSousPrefecturesAsync()
        {
            _logger.LogInformation("Attempting to get all sousprefectures");

            var res = await _locationRepository.GetAllSousPrefecturesAsync();

            _logger.LogInformation("Successfully get all sousprefectures");
            var response = new ApiResponse<IEnumerable<SousPrefecture>>
            {
                Category = ApiResponseType.Success,
                Message = string.Empty,
                Data = res
            };
            return response;
        }

        public async Task<ApiResponse<SousPrefecture>> GetSousPrefectureByIdAsync(int sousPrefectureId)
        {
            var response = new ApiResponse<SousPrefecture>
            {
                Message = $"Attempting to get SousPrefecture with ID: {sousPrefectureId}"
            };
            string message;
            _logger.LogInformation(response.Message);

            var res = await _locationRepository.GetSousPrefectureByIdAsync(sousPrefectureId);

            if (res == null)
            {

                response.Category = ApiResponseType.NotFound;
                message = $"SousPrefecture with ID {sousPrefectureId} not found.";
                response.Errors.Add(message);
                _logger.LogWarning(message);
                return response;
            }

            _logger.LogInformation("Successfully Get SousPrefecture with ID: {SousPrefectureId}", sousPrefectureId);
            response.Category = ApiResponseType.Success;
            response.Message = string.Empty;
            response.Data = res;
            return response;
        }

        public async Task<ApiResponse<IEnumerable<SousPrefecture>>> GetSousPrefecturesByPrefectureIdAsync(int prefectureId)
        {
            var response = new ApiResponse<IEnumerable<SousPrefecture>>
            {
                Message = $"Attempting to get sous prefecture by prefecture with ID: {prefectureId}"
            };
            string message;
            _logger.LogInformation(response.Message);

            var res = await _locationRepository.GetSousPrefecturesByPrefectureAsync(prefectureId);

            if (res == null)
            {
                response.Category = ApiResponseType.NotFound;
                message = $"Sous Prefecture by prefecture with ID {prefectureId} not found.";
                response.Errors.Add(message);
                _logger.LogWarning(message);
                return response;
            }

            _logger.LogInformation("Successfully Get sous prefecture by prefecture with ID: {regionId}", prefectureId);
            response.Category = ApiResponseType.Success;
            response.Message = string.Empty;
            response.Data = res;
            return response;
        }
    }
}
