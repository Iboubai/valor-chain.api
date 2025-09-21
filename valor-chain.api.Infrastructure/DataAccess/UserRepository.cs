using GnDapper.Interfaces;
using valor_chain.api.Domain.Entities;
using valor_chain.api.Domain.Ports.Output;
using valor_chain.api.Domain.Ports.Output.Mapper;
using valor_chain.api.Infrastructure.DataAccess.Dtos;
using valor_chain.api.Infrastructure.DataAccess.Mappers;

namespace valor_chain.api.Infrastructure.DataAccess;

public class UserRepository : IUserRepository
{
    private readonly IRepository<UserDto> _userRepository;
    private readonly IMapper<User, UserDto> _userMapper;

    public UserRepository(IUnitOfWork unitOfWork)
    {
        _userRepository = unitOfWork.Repository<UserDto>();
        _userMapper = new UserMapper();
    }

    public async Task<User> GetByIdAsync(Guid id)
    {
        var userDto = await _userRepository.GetByIdAsync(id);
        return userDto == null ? null : _userMapper.ToEntity((UserDto)userDto);
    }

    public async Task<User> AuthenticateUserAsync(string email, string password)
    {
        var userDto = await _userRepository.GetWithQuery($"SELECT * from  {_userRepository.GetTableName()} WHERE Email = '{email}' AND PasswordHash = '{password}'");
        return !userDto.Any() ? null : _userMapper.ToEntity((UserDto)userDto.First());
    }

    public async Task<User> GetByEmailAsync(string email)
    {
        var query =
            $"SELECT * from  {_userRepository.GetTableName()} WHERE Email = '{email}'";
        var userDto = await _userRepository.GetWithQuery(query);
        return !userDto.Any() ? null : _userMapper.ToEntity((UserDto)userDto.First());
    }

    public async Task AddAsync(User user)
    {
        var d = await _userRepository.AddAsync(_userMapper.ToDto(user));
    }

    public async Task<bool> IsUserExist(Guid userId)
    {
        return await _userRepository.ExistsAsync(userId);
    }

    public async Task UpdateAsync(User user)
    {
        await _userRepository.UpdateAsync(_userMapper.ToDto(user));
    }

    public async Task<User> ChangeUserPasswordAsync(Guid id, string email, string password)
    {
        var query =
            $"UPDATE {_userRepository.GetTableName()} SET PasswordHash = '{password}' WHERE Id = '{id}' AND Email = '{email}' ; SELECT * from  {_userRepository.GetTableName()} WHERE Id = '{id}' AND Email = '{email}'";
        var userDto = await _userRepository.GetWithQuery(query);
        return !userDto.Any() ? null : _userMapper.ToEntity((UserDto)userDto.First());
    }

    public async Task DeleteAsync(Guid id)
    {
        await _userRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return users.Select(u => _userMapper.ToEntity(u));
    }

    public async Task<User> GetByPhoneNumberAsync(string phoneNumber)
    {
        var query =
            $"SELECT * from  {_userRepository.GetTableName()} WHERE PhoneNumber = '{phoneNumber}'";
        var userDto = await _userRepository.GetWithQuery(query);
        return !userDto.Any() ? null : _userMapper.ToEntity((UserDto)userDto.First());
    }
}
