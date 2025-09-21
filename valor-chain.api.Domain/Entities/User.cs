using gn_core_entities.Location;
using System.Text.Json.Serialization;

namespace valor_chain.api.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        [JsonIgnore]
        public string PasswordHash { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime BirthDate { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? LastModifiedDate { get; set; }

        public List<Profil> Profils { get; set; }

        public UserLocation Location { get; set; }

        public User()
        {
            Id = Guid.NewGuid();
            CreatedDate = DateTime.UtcNow;
            Profils = new List<Profil>();
            Location = new UserLocation() { UserId = Id };
        }

        public User(string firstName, string lastName, string email, string passwordHash, string phoneNumber, DateTime birthDate, bool isActive)
        {
            Id = Guid.NewGuid();
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            PasswordHash = passwordHash ?? throw new ArgumentNullException(nameof(passwordHash));
            PhoneNumber = phoneNumber ?? throw new ArgumentNullException(nameof(phoneNumber));
            BirthDate = birthDate;
            BirthDate = DateTime.UtcNow;
            CreatedDate = DateTime.UtcNow;
            IsActive = isActive;
            Profils = new List<Profil>();
            Location = new UserLocation() { UserId = Id };
        }

        public User(Guid id, string firstName, string lastName, string email, string passwordHash, string phoneNumber, DateTime birthDate, DateTime createdDate, DateTime? lastModifiedDate, bool isActive)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PasswordHash = passwordHash;
            PhoneNumber = phoneNumber;
            BirthDate = birthDate;
            CreatedDate = createdDate;
            LastModifiedDate = lastModifiedDate;
            IsActive = isActive;
            Profils = new List<Profil>();
            Location = new UserLocation() { UserId = Id };
        }

        public void UpdateUser(string firstName, string lastName, string email, string phoneNumber, DateTime birthDate)
        {
            FirstName = firstName ?? FirstName;
            LastName = lastName ?? LastName;
            Email = email ?? Email;
            PhoneNumber = phoneNumber ?? PhoneNumber;
            BirthDate = birthDate;
            LastModifiedDate = DateTime.UtcNow;
        }

        public void UpdateLocation(Region region, Prefecture prefecture, SousPrefecture sousPrefecture)
        {
            Location.Region = region;
            Location.Prefecture = prefecture;
            Location.SousPrefecture = sousPrefecture;
        }

        public void ChangePassword(string newPasswordHash)
        {
            PasswordHash = newPasswordHash ?? throw new ArgumentNullException(nameof(newPasswordHash));
            LastModifiedDate = DateTime.UtcNow;
        }

        public void AddProfil(Profil profil)
        {
            if (!HasRole(profil))
            {
                Profils.Add(profil);
            }
        }

        public void RemoveProfil(Profil profil)
        {
            if (Profils.Contains(profil))
            {
                Profils.Remove(profil);
            }
        }

        public bool HasRole(Profil profil)
        {
            return Profils != null && Profils.Contains(profil);
        }
    }
}
