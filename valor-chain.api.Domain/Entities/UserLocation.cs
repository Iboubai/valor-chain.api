using gn_core_entities.Location;

namespace valor_chain.api.Domain.Entities
{
    public class UserLocation
    {
        private UserLocation location;

        public int Id { get; set; }

        public Guid UserId { get; set; }

        public Region Region { get; set; }

        public Prefecture Prefecture { get; set; }

        public SousPrefecture SousPrefecture { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool IsActive { get; set; }

        public UserLocation()
        {            
        }

        public UserLocation(int id, Guid userId, DateTime createdDate, bool isActive)
        {
            Id = id;
            UserId = userId;
            CreatedDate = createdDate;
            IsActive = isActive;
        }
    }
}
