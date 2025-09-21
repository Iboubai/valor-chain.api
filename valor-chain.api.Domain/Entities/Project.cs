namespace valor_chain.api.Domain.Entities;

public class Project
{
    public Guid Id { get; private set; }

    public Guid CompanyId { get; private set; }

    public string Name { get; private set; }

    public string Description { get; private set; }

    public DateTime StartDate { get; private set; }

    public DateTime PlannedEndDate { get; private set; }

    public string Status { get; private set; } // In Progress, Completed,Canceled, etc.

    public string ProjectType { get; private set; } // Ex: Cereal Cultivation, Cattle Breeding, Fishing, Food Processing
    // Project specifics (e.g., agricultural speculations, execution schedule, activities)

    public Dictionary<string, string> SpecificDetails { get; private set; }

    public List<ProjectActivity> Activities { get; private set; }

    public Project(Guid companyId, string name, string description,
        DateTime startDate, DateTime plannedEndDate, string projectType)
    {
        Id = Guid.NewGuid();
        CompanyId = companyId;
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Description = description;
        StartDate = startDate;
        PlannedEndDate = plannedEndDate;
        Status = "In Progress";
        ProjectType = projectType ?? throw new
            ArgumentNullException(nameof(projectType));
        SpecificDetails = new Dictionary<string, string>();
        Activities = new List<ProjectActivity>();
    }

    // Constructor for reconstitution from persistence
    public Project(Guid id, Guid companyId, string name, string
        description, DateTime startDate, DateTime plannedEndDate, string status, string
        projectType, Dictionary<string, string> specificDetails)
    {
        Id = id;
        CompanyId = companyId;
        Name = name;
        Description = description;
        StartDate = startDate;
        PlannedEndDate = plannedEndDate;
        Status = status;
        ProjectType = projectType;
        SpecificDetails = specificDetails ?? new Dictionary<string, string>
            ();
        Activities = new List<ProjectActivity>();
    }

    public void UpdateProject(string name, string description, DateTime
        plannedEndDate, string projectType)
    {
        Name = name ?? Name;
        Description = description ?? Description;
        PlannedEndDate = plannedEndDate;
        ProjectType = projectType ?? ProjectType;
    }

    public void AddSpecificDetail(string key, string value)
    {
        SpecificDetails[key] = value;
    }

    public void AddActivity(ProjectActivity activity)
    {
        if (activity == null) throw new
            ArgumentNullException(nameof(activity));
        Activities.Add(activity);
    }

    public void ChangeStatus(string newStatus)
    {
        Status = newStatus ?? throw new
            ArgumentNullException(nameof(newStatus));
    }
}