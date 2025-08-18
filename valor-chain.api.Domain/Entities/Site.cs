using System;
using System.Diagnostics;
using System.Numerics;

namespace valor_chain.api.Domain.Entities;

public class Site
{
    public Guid Id { get; private set; }

    public Guid CompanyId { get; private set; }

    public string Name { get; private set; }

    public string Address { get; private set; }

    public string SiteType { get; private set; } // Farm, Processing Plant, Warehouse, Port

    public Site(Guid companyId, string name, string address, string
        siteType)
    {
        Id = Guid.NewGuid();
        CompanyId = companyId;
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Address = address ?? throw new
            ArgumentNullException(nameof(address));
        SiteType = siteType ?? throw new
            ArgumentNullException(nameof(siteType));
    }

    // Constructor for reconstitution from persistence
    public Site(Guid id, Guid companyId, string name, string address, string siteType)
    {
        Id = id;
        CompanyId = companyId;
        Name = name;
        Address = address;
        SiteType = siteType;
    }

    public void UpdateDetails(string name, string address, string siteType)
    {
        Name = name ?? Name;
        Address = address ?? Address;
        SiteType = siteType ?? SiteType;
    }
}