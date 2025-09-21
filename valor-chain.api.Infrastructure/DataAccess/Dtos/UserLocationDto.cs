using GnDapper.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace valor_chain.api.Infrastructure.DataAccess.Dtos
{
    [Table("UserLocations")]
    public class UserLocationDto : BaseEntity
    {
        public int Id { get; set; }

        public Guid UserId { get; set; }

        public int RegionId { get; set; }

        public int PrefectureId { get; set; }

        public int SousPrefectureId { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool IsActive { get; set; }

        public UserLocationDto()
        {
            
        }

        public UserLocationDto(int id, Guid userId, int regionId, int prefectureId, int sousPrefectureId, DateTime createdDate, bool isActive)
        {
            Id = id;
            UserId = userId;
            RegionId = regionId;
            PrefectureId = prefectureId;
            SousPrefectureId = sousPrefectureId;
            CreatedDate = createdDate;
            IsActive = isActive;
        }
    }
}
