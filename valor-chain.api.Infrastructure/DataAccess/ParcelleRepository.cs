using GnDapper.Interfaces;
using valor_chain.api.Domain.Entities;
using valor_chain.api.Domain.Entities.Exploitation;
using valor_chain.api.Domain.Ports.Output;
using valor_chain.api.Domain.Ports.Output.Mapper;
using valor_chain.api.Infrastructure.DataAccess.Dtos.Exploitation;
using valor_chain.api.Infrastructure.DataAccess.Mappers;
using valor_chain.api.Infrastructure.DataAccess.Mappers.Exploitation;

namespace valor_chain.api.Infrastructure.DataAccess;

public class ParcelleRepository : IParcelleRepository
{
    private readonly IRepository<ParcelleDto> _parcelleRepository;
    private readonly IRepository<ParcelleTypeDto> _parcelleTypeRepository;
    private readonly IRepository<ParcelleStatusDto> _parcelleStatusRepository;
    private readonly IMapper<Parcelle, ParcelleDto> _parcelleMapper;
    private readonly IMapper<ParcelleType, ParcelleTypeDto> _parcelleTypeMapper;
    private readonly IMapper<ParcelleStatus, ParcelleStatusDto> _parcelleStatusMapper;

    public ParcelleRepository(IUnitOfWork unitOfWork)
    {
        _parcelleRepository = unitOfWork.Repository<ParcelleDto>();
        _parcelleTypeRepository = unitOfWork.Repository<ParcelleTypeDto>();
        _parcelleStatusRepository = unitOfWork.Repository<ParcelleStatusDto>();
        _parcelleMapper = new ParcelleMapper();
        _parcelleTypeMapper = new ParcelleTypeMapper();
        _parcelleStatusMapper = new ParcelleStatusMapper();
    }

    public async Task<Parcelle> CreateUserParcelleAsync(Parcelle parcelle)
    {
        await _parcelleRepository.AddWithAutoIdIncrementAsync(_parcelleMapper.ToDto(parcelle));
        var parcelleTypeDto = await _parcelleTypeRepository.GetByIdAsync(parcelle.Type.Id);
        var parcelleStatusDto = await _parcelleStatusRepository.GetByIdAsync(parcelle.Status.Id);
        if (parcelleTypeDto != null)
        {
            parcelle.Type = _parcelleTypeMapper.ToEntity(parcelleTypeDto);
            parcelle.Status = _parcelleStatusMapper.ToEntity(parcelleStatusDto);
        }
        return parcelle;
    }

    public async void DeleteUserParcelleAsync(int id)
    {
        await _parcelleRepository.GetWithQuery($"UPDATE {_parcelleRepository.GetTableName()} SET IsActive = 0 WHERE Id = {id}; SELECT 1;");        
    }

    public async Task<IEnumerable<ParcelleStatus>> GetAllParcelleStatus()
    {
        var parcelleTypes = await _parcelleStatusRepository.GetAllAsync();
        return parcelleTypes.Select(u => _parcelleStatusMapper.ToEntity(u));
    }

    public async Task<IEnumerable<ParcelleType>> GetAllParcelleTypes()
    {
        var parcelleTypes = await _parcelleTypeRepository.GetAllAsync();
        return parcelleTypes.Select(u => _parcelleTypeMapper.ToEntity(u));
    }

    public async Task<IEnumerable<Parcelle>> GetUserParcellesAsync(Guid userId)
    {

        var parcellesDto = await _parcelleRepository.GetWithQuery($"SELECT * from  {_parcelleRepository.GetTableName()} WHERE UserId = '{userId}' AND IsActive = 1");
        if (parcellesDto == null || !parcellesDto.Any())
            return null;

        var parcelleList = new List<Parcelle>();

        foreach (var parcelleDto in parcellesDto)
        {
            var parcelle = _parcelleMapper.ToEntity(parcelleDto);
            var parcelleTypeDto = await _parcelleTypeRepository.GetByIdAsync(parcelleDto.TypeId);
            var parcelleStatusDto = await _parcelleStatusRepository.GetByIdAsync(parcelleDto.StatusId);
            if (parcelleTypeDto != null)
            {
                parcelle.Type = _parcelleTypeMapper.ToEntity(parcelleTypeDto);
                parcelle.Status = _parcelleStatusMapper.ToEntity(parcelleStatusDto);
            }

            parcelleList.Add(parcelle);
        }

        return parcelleList.Select(u => u);
    }

    public async Task<Parcelle> UpdateUserParcelleAsync(Parcelle parcelle)
    {

        await _parcelleRepository.UpdateAsync(_parcelleMapper.ToDto(parcelle));
                
        var parcelleTypeDto = await _parcelleTypeRepository.GetByIdAsync(parcelle.Type.Id);
        var parcelleStatusDto = await _parcelleStatusRepository.GetByIdAsync(parcelle.Status.Id);
        if (parcelleTypeDto != null)
        {
            parcelle.Type = _parcelleTypeMapper.ToEntity(parcelleTypeDto);
            parcelle.Status = _parcelleStatusMapper.ToEntity(parcelleStatusDto);
        }
        return parcelle;
    }
}