using valor_chain.api.Domain.Entities;
using valor_chain.api.Domain.Ports.Output.Mapper;
using valor_chain.api.Infrastructure.DataAccess.Dtos;

namespace valor_chain.api.Infrastructure.DataAccess.Mappers
{
    public class UserLocationMapper : IMapper<UserLocation, UserLocationDto>
    {
        public UserLocationDto ToDto(UserLocation userLocation)
        {
            if (userLocation == null)
                return null;

            return new UserLocationDto(
                userLocation.Id,
                userLocation.UserId,
                userLocation.Region.Id,
                userLocation.Prefecture.Id,
                userLocation.SousPrefecture.Id,
                userLocation.CreatedDate,
                userLocation.IsActive
            );
        }

        public UserLocation ToEntity(UserLocationDto dto)
        {
            if (dto == null) return null;

            return new UserLocation(
                dto.Id,
                dto.UserId,
                null,//region,
                null,//prefecture,
                null,//sousPrefecture, 
                dto.CreatedDate, 
                dto.IsActive
                );
        }
    }
}
