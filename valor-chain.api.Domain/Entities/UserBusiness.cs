using System.ComponentModel.DataAnnotations.Schema;

namespace valor_chain.api.Domain.Entities;

public class UserBusiness : User
{
    public List<UserProfil> UserProfil { get; private set; }
        
    public List<Company> Companies { get; private set; }
    
    public UserBusiness(string firstName, string lastName, string email, string
        passwordHash) : base(firstName, lastName, email, passwordHash)
    {
        UserProfil = new List<UserProfil>();
        Companies = new List<Company>();
    }

    public UserBusiness(Guid id, string firstName, string lastName, string email, string passwordHash, DateTime createdDate, DateTime? lastModifiedDate) 
        : base(id, firstName, lastName, email, passwordHash, createdDate, lastModifiedDate)
    {
        Companies = new List<Company>();
    }

    public void AddUserProfil(UserProfil userProfil)
    {
        UserProfil.Add(userProfil);
    }

    public void AddCompany(Company company)
    {
        if (company == null) 
            throw new ArgumentNullException(nameof(company));
        Companies.Add(company);
    }
}