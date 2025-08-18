namespace valor_chain.api.Domain.Entities;

public class ProjectActivity
{
    public Guid Id { get; private set; }

    public Guid ProjectId { get; private set; }

    public string Name { get; private set; }

    public string Description { get; private set; }

    public DateTime PlannedStartDate { get; private set; }

    public DateTime PlannedEndDate { get; private set; }

    public DateTime? ActualStartDate { get; private set; }

    public DateTime? ActualEndDate { get; private set; }

    public string Status { get; private set; } // Planned, In Progress, Completed, Canceled

    public ProjectActivity(Guid projectId, string name, string description, DateTime plannedStartDate, DateTime plannedEndDate)
    {
        Id = Guid.NewGuid();
        ProjectId = projectId;
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Description = description;
        PlannedStartDate = plannedStartDate;
        PlannedEndDate = plannedEndDate;
        Status = "Planned";
    }

    // Constructor for reconstitution from persistence
    public ProjectActivity(Guid id, Guid projectId, string name, string description, DateTime plannedStartDate, DateTime plannedEndDate, DateTime? actualStartDate, DateTime? actualEndDate, string status)
    {
        Id = id;
        ProjectId = projectId;
        Name = name;
        Description = description;
        PlannedStartDate = plannedStartDate;
        PlannedEndDate = plannedEndDate;
        ActualStartDate = actualStartDate;
        ActualEndDate = actualEndDate;
        Status = status;
    }

    public void StartActivity()
    {
        if (Status == "Planned")
        {
            Status = "In Progress";
            ActualStartDate = DateTime.UtcNow;
        }
        else
        {
            throw new InvalidOperationException("Cannot start an activity that is not planned.");
        }
    }

    public void CompleteActivity()
    {
        if (Status == "In Progress")
        {
            Status = "Completed";
            ActualEndDate = DateTime.UtcNow;
        }
        else
        {
            throw new InvalidOperationException("Cannot complete an activity that is not in progress.");
        }
    }

    public void CancelActivity()
    {
        if (Status == "Planned" || Status == "In Progress")
        {
            Status = "Canceled";
        }
        else
        {
            throw new InvalidOperationException("Cannot cancel an already completed activity.");
        }
    }

    public void UpdatePlannedDates(DateTime newPlannedStartDate, DateTime
        newPlannedEndDate)
    {
        PlannedStartDate = newPlannedStartDate;
        PlannedEndDate = newPlannedEndDate;
    }
}