using GnDapper.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace valor_chain.api.Domain.Entities;

[Table("Sites")]
public class Site : BaseEntity
{
    public Guid Id { get; private set; }

    public Guid CompanyId { get; private set; }

    public string Name { get; private set; }
    
    public Site(Guid companyId, string name, string address, string
        siteType)
    {
        Id = Guid.NewGuid();
        CompanyId = companyId;
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }

    // Constructor for reconstitution from persistence
    public Site(Guid id, Guid companyId, string name, string address)
    {
        Id = id;
        CompanyId = companyId;
        Name = name;
    }

    public void UpdateDetails(string name, string address)
    {
        Name = name ?? Name;
    }
}