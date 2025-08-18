namespace valor_chain.api.Domain.Entities;

public class Personnel
{
    public Guid Id { get; private set; }

    public Guid CompanyId { get; private set; }

    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public string Role { get; private set; }

    public string Email { get; private set; }

    public Personnel(Guid companyId, string firstName, string lastName,
        string role, string email)
    {
        Id = Guid.NewGuid();
        CompanyId = companyId;
        FirstName = firstName ?? throw new
            ArgumentNullException(nameof(firstName));
        LastName = lastName ?? throw new
            ArgumentNullException(nameof(lastName));
        Role = role ?? throw new ArgumentNullException(nameof(role));
        Email = email ?? throw new ArgumentNullException(nameof(email));
    }

    // Constructor for reconstitution from persistence
    public Personnel(Guid id, Guid companyId, string firstName, string
        lastName, string role, string email)
    {
        Id = id;
        CompanyId = companyId;
        FirstName = firstName;
        LastName = lastName;
        Role = role;
        Email = email;
    }

    public void UpdateInformation(string firstName, string lastName, string
        role, string email)
    {
        FirstName = firstName ?? FirstName;
        LastName = lastName ?? LastName;
        Role = role ?? Role;
        Email = email ?? Email;
    }
}