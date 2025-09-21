using System.ComponentModel.DataAnnotations.Schema;
using GnDapper.Models;

namespace valor_chain.api.Infrastructure.DataAccess.Dtos
{
    [Table("Users")]
    public class UserDto : BaseEntity
    {
        public Guid Id { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string Email { get; private set; }

        public string PasswordHash { get; private set; }

        public string PhoneNumber { get; private set; }

        public DateTime CreatedDate { get; private set; }

        public DateTime? LastModifiedDate { get; private set; }

        public DateTime BirthDate { get; internal set; }

        public bool IsActive { get; private set; }

        public UserDto()
        {
            
        }

        public UserDto(Guid id, string firstName, string lastName, string email, string passwordHash, string phoneNumber, DateTime birthDate, DateTime createdDate, DateTime? lastModifiedDate, bool isActive)
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
        }
    }
}
