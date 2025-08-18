using GnDapper.Interfaces;
using valor_chain.api.Domain.Entities;
using valor_chain.api.Domain.Ports.Output;

namespace valor_chain.api.Infrastructure.DataAccess;

public class UserRepository : IUserRepository
{
    private readonly IRepository<User> _userRepository;

    public UserRepository(IUnitOfWork unitOfWork)
    {
        _userRepository = unitOfWork.Repository<User>();
    }

    public async Task<User> GetByIdAsync(Guid id)
    {
        return await _userRepository.GetByIdAsync(id);
    }

    public Task<User> GetByEmailAsync(string email)
    {
        throw new NotImplementedException();
    }

    public async Task AddAsync(User User)
    {
        await _userRepository.AddAsync(User);
    }

    public async Task UpdateAsync(User User)
    {
        await _userRepository.UpdateAsync(User);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _userRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _userRepository.GetAllAsync();
    }
}