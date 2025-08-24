using System.ComponentModel.DataAnnotations.Schema;
using GnDapper.Models;

namespace valor_chain.api.Domain.Entities;

[Table("Profils")]
public class Profil : BaseEntity
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public string ProfilName { get; private set; }
    public DateTime CreatedDate { get; private set; }

    public Profil(Guid id, Guid userId, string profilName, DateTime createdDate)
    {
        Id = id;
        UserId = userId;
        ProfilName = profilName;
        CreatedDate = createdDate;
    }

    public Profil(Guid userId, string profilName)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        ProfilName = profilName;
        CreatedDate = DateTime.UtcNow;
    }
}