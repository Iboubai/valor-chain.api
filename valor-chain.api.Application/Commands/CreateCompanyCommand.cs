namespace valor_chain.api.Application.Commands;

public class CreateCompanyCommand
{
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string Siren { get; set; }
}