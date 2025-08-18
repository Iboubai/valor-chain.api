namespace valor_chain.api.Domain.Entities;

public class Equipment
{
    public Guid Id { get; private set; }

    public Guid CompanyId { get; private set; }

    public string Name { get; private set; }

    public string Type { get; private set; } // Tractor, Harvester, Fishing Boat, etc.

    public string SerialNumber { get; private set; }

    public DateTime AcquisitionDate { get; private set; }

    public string Status { get; private set; } // In Service, Under Maintenance, Out of Service

    public Equipment(Guid companyId, string name, string type, string serialNumber, DateTime acquisitionDate)
    {
        Id = Guid.NewGuid();
        CompanyId = companyId;
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Type = type ?? throw new ArgumentNullException(nameof(type));
        SerialNumber = serialNumber ?? throw new
            ArgumentNullException(nameof(serialNumber));
        AcquisitionDate = acquisitionDate;
        Status = "In Service";
    }

    // Constructor for reconstitution from persistence
    public Equipment(Guid id, Guid companyId, string name, string type, string serialNumber, DateTime acquisitionDate, string status)
    {
        Id = id;
        CompanyId = companyId;
        Name = name;
        Type = type;
        SerialNumber = serialNumber;
        AcquisitionDate = acquisitionDate;
        Status = status;
    }

    public void UpdateStatus(string newStatus)
    {
        Status = newStatus ?? throw new
            ArgumentNullException(nameof(newStatus));
    }

    public void UpdateDetails(string name, string type, string
        serialNumber)
    {
        Name = name ?? Name;
        Type = type ?? Type;
        SerialNumber = serialNumber ?? SerialNumber;
    }
}