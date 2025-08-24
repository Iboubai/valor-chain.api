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

        public UserDto(Guid id, string firstName, string lastName, string email, string passwordHash, string phoneNumber, DateTime createdDate, DateTime? lastModifiedDate)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PasswordHash = passwordHash;
            PhoneNumber = phoneNumber;
            CreatedDate = createdDate;
            LastModifiedDate = lastModifiedDate;
        }
    }
}
