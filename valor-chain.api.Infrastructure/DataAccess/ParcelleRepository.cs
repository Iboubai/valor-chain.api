using GnDapper.Interfaces;
using valor_chain.api.Domain.Entities.Exploitation;
using valor_chain.api.Domain.Ports.Output;
using valor_chain.api.Domain.Ports.Output.Mapper;
using valor_chain.api.Infrastructure.DataAccess.Dtos.Exploitation;
using valor_chain.api.Infrastructure.DataAccess.Mappers.Exploitation;

namespace valor_chain.api.Infrastructure.DataAccess;

public class ParcelleRepository : IParcelleRepository
{
    private readonly IRepository<ParcelleDto> _parcelleRepository;
    private readonly IRepository<ParcelleTypeDto> _parcelleTypeRepository;
    private readonly IMapper<Parcelle, ParcelleDto> _parcelleMapper;
    private readonly IMapper<ParcelleType, ParcelleTypeDto> _parcelleTypeMapper;

    public ParcelleRepository(IUnitOfWork unitOfWork)
    {
        _parcelleRepository = unitOfWork.Repository<ParcelleDto>();
        _parcelleTypeRepository = unitOfWork.Repository<ParcelleTypeDto>();
        _parcelleMapper = new ParcelleMapper();
        _parcelleTypeMapper = new ParcelleTypeMapper();
    }

    public async Task CreateUserParcelleAsync(Parcelle parcelle)
    {
        await _parcelleRepository.AddWithAutoIdIncrementAsync(_parcelleMapper.ToDto(parcelle));
    }

    public async Task<IEnumerable<ParcelleType>> GetAllParcelleTypes()
    {
        var parcelleTypes = await _parcelleTypeRepository.GetAllAsync();
        return parcelleTypes.Select(u => _parcelleTypeMapper.ToEntity(u));
    }

    public async Task<IEnumerable<Parcelle>> GetUserParcellesAsync(Guid userId)
    {

        var parcellesDto = await _parcelleRepository.GetWithQuery($"SELECT * from  {_parcelleRepository.GetTableName()} WHERE UserId = '{userId}'");
        if (parcellesDto == null || !parcellesDto.Any())
            return null;

        var parcelleList = new List<Parcelle>();

        foreach (var parcelleDto in parcellesDto)
        {
            var parcelle = _parcelleMapper.ToEntity(parcelleDto);
            var parcelleTypeDto = await _parcelleTypeRepository.GetByIdAsync(parcelleDto.TypeId);
            if (parcelleTypeDto != null)
                parcelle.Type = _parcelleTypeMapper.ToEntity(parcelleTypeDto);

            parcelleList.Add(parcelle);
        }

        return parcelleList.Select(u => u);
    }
}