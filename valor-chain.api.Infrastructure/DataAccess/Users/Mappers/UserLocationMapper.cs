using valor_chain.api.Domain.Entities;
using valor_chain.api.Domain.Ports.Output.Mapper;
using valor_chain.api.Infrastructure.DataAccess.Users.Dtos;

namespace valor_chain.api.Infrastructure.DataAccess.Users.Mappers
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
                dto.CreatedDate,
                dto.IsActive
                );
        }
    }
}