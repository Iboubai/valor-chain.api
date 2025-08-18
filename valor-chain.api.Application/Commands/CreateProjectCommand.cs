namespace valor_chain.api.Application.Commands;

public class CreateProjectCommand
{
    public Guid CompanyId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime PlannedEndDate { get; set; }
    public string ProjectType { get; set; }
}