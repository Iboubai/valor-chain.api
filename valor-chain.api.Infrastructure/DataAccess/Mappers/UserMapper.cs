using valor_chain.api.Domain.Entities;
using valor_chain.api.Domain.Ports.Output.Mapper;
using valor_chain.api.Infrastructure.DataAccess.Dtos;

namespace valor_chain.api.Infrastructure.DataAccess.Mappers
{
    internal class UserMapper : IMapper<User, UserDto>
    {
        public UserDto ToDto(User user)
        {
            if (user == null) 
                return null;

            return new UserDto(
                user.Id,
                user.FirstName,
                user.LastName,
                user.Email,
                user.PasswordHash,
                user.PhoneNumber,
                user.BirthDate,
                user.CreatedDate,
                user.LastModifiedDate,
                user.IsActive
            );
        }

        public User ToEntity(UserDto dto)
        {
            if (dto == null) return null;
            
            return new User(
                dto.Id,
                dto.FirstName,
                dto.LastName,
                dto.Email,
                dto.PasswordHash,
                dto.PhoneNumber,
                dto.BirthDate,
                dto.CreatedDate,
                dto.LastModifiedDate,
                dto.IsActive
            );
        }
    }

    
}
