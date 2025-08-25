namespace valor_chain.api.Application.Commands;

public class ChangePasswordCommand
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}