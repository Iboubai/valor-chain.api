namespace valor_chain.api.Application.Queries;

public class ChangePasswordQuery
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}