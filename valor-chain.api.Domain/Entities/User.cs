using System.ComponentModel.DataAnnotations.Schema;
using GnDapper.Models;

namespace valor_chain.api.Domain.Entities
{
    [Table("Users")]
    public class User : BaseEntity
    {
        public Guid Id { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string Email { get; private set; }

        public string PasswordHash { get; private set; }

        public DateTime CreatedDate { get; private set; }

        public DateTime? LastModifiedDate { get; private set; } 

        public User(string firstName, string lastName, string email, string
        passwordHash)
        {
            Id = Guid.NewGuid();
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            PasswordHash = passwordHash ?? throw new ArgumentNullException(nameof(passwordHash));
            CreatedDate = DateTime.UtcNow;
        }
        
        public User(Guid id, string firstName, string lastName, string email, string passwordHash, DateTime createdDate, DateTime? lastModifiedDate)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PasswordHash = passwordHash;
            CreatedDate = createdDate;
            LastModifiedDate = lastModifiedDate;
        }

        public void UpdateUser(string firstName, string lastName, string email)
        {
            FirstName = firstName ?? FirstName;
            LastName = lastName ?? LastName;
            Email = email ?? Email;
            LastModifiedDate = DateTime.UtcNow;
        }

        public void ChangePassword(string newPasswordHash)
        {
            PasswordHash = newPasswordHash ?? throw new
            ArgumentNullException(nameof(newPasswordHash));
            LastModifiedDate = DateTime.UtcNow;
        }
    }
}
