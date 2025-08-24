using System.ComponentModel.DataAnnotations.Schema;
using GnDapper.Models;

namespace valor_chain.api.Domain.Entities
{
    public class User
    {
        public Guid Id { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string Email { get; private set; }

        public string PasswordHash { get; private set; }
        public string PhoneNumber { get; private set; }

        public DateTime CreatedDate { get; private set; }

        public DateTime? LastModifiedDate { get; private set; }
        
        public List<UserProfil> Profils { get; set; } 

        public User(string firstName, string lastName, string email, string
        passwordHash, string phoneNumber)
        {
            Id = Guid.NewGuid();
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            PasswordHash = passwordHash ?? throw new ArgumentNullException(nameof(passwordHash));
            PhoneNumber = phoneNumber;
            CreatedDate = DateTime.UtcNow;
            Profils = new List<UserProfil>();
        }

        public User(Guid id, string firstName, string lastName, string email, string passwordHash, string phoneNumber, DateTime createdDate, DateTime? lastModifiedDate)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PasswordHash = passwordHash;
            PhoneNumber = phoneNumber;
            CreatedDate = createdDate;
            LastModifiedDate = lastModifiedDate;
            Profils = new List<UserProfil>();
        }

        public void UpdateUser(string firstName, string lastName, string email, string phoneNumber)
        {
            FirstName = firstName ?? FirstName;
            LastName = lastName ?? LastName;
            Email = email ?? Email;
            PhoneNumber = phoneNumber ?? PhoneNumber;
            LastModifiedDate = DateTime.UtcNow;
        }

        public void ChangePassword(string newPasswordHash)
        {
            PasswordHash = newPasswordHash ?? throw new
            ArgumentNullException(nameof(newPasswordHash));
            LastModifiedDate = DateTime.UtcNow;
        }

        public void AddProfil(UserProfil profil)
        {
            if (!HasRole(profil))
            {
                Profils.Add(profil);
            }
        }

        public void RemoveProfil(UserProfil profil)
        {
            if (Profils.Contains(profil))
            {
                Profils.Remove(profil);
            }
        }

        public bool HasRole(UserProfil profil)
        {
            return Profils != null && Profils.Contains(profil);
        }
    }
}
