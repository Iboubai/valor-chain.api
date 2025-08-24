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

    public Task<User> GetByEmailAsync(string email)
    {
        throw new NotImplementedException();
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

    public async Task DeleteAsync(Guid id)
    {
        await _userRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return users.Select(u => _userMapper.ToEntity(u));
    }
}