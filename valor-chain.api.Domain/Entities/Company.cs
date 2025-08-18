namespace valor_chain.api.Domain.Entities;

public class Company
{
    public Guid Id { get; private set; }

    public Guid UserId { get; private set; } // The user who owns thecompany

    public string Name { get; private set; }

    public string Address { get; private set; }

    public DateTime CreatedDate { get; private set; }

    public DateTime? LastModifiedDate { get; private set; }

    public List<Personnel> Personnel { get; private set; }

    public List<Equipment> Equipment { get; private set; }

    public List<Site> Sites { get; private set; }

    public List<Project> Projects { get; private set; }

    public Company(Guid userId, string name, string address, string siren)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Address = address ?? throw new
        ArgumentNullException(nameof(address));
        CreatedDate = DateTime.UtcNow;
        Personnel = new List<Personnel>();
        Equipment = new List<Equipment>();
        Sites = new List<Site>();
        Projects = new List<Project>();
    }

    // Constructor for reconstitution from persistence
    public Company(Guid id, Guid userId, string name, string address, string siren, DateTime createdDate, DateTime? lastModifiedDate)
    {
        Id = id;
        UserId = userId;
        Name = name;
        Address = address;
        CreatedDate = createdDate;
        LastModifiedDate = lastModifiedDate;
        Personnel = new List<Personnel>();
        Equipment = new List<Equipment>();
        Sites = new List<Site>();
        Projects = new List<Project>();
    }

    public void UpdateDetails(string name, string address, string siren)
    {
        Name = name ?? Name;
        Address = address ?? Address;
        LastModifiedDate = DateTime.UtcNow;
    }

    public void AddPersonnel(Personnel personnelMember)
    {
        if (personnelMember == null) throw new
        ArgumentNullException(nameof(personnelMember));
        Personnel.Add(personnelMember);
    }

    public void AddEquipment(Equipment equipment)
    {
        if (equipment == null) throw new
        ArgumentNullException(nameof(equipment));
        Equipment.Add(equipment);
    }

    public void AddSite(Site site)
    {
        if (site == null) throw new ArgumentNullException(nameof(site));
        Sites.Add(site);
    }

    public void AddProject(Project project)
    {
        if (project == null) throw new
        ArgumentNullException(nameof(project));
        Projects.Add(project);
    }
    
}