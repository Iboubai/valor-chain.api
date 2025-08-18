using Microsoft.Extensions.Logging;
using valor_chain.api.Domain.Entities;
using valor_chain.api.Domain.Ports.Input;
using valor_chain.api.Domain.Ports.Output;

namespace valor_chain.api.Domain.Impl
{
    public class UserBusinessBusinessManagementService : IUserBusinessManagementService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly ILogger<UserBusinessBusinessManagementService> _logger;

        public UserBusinessBusinessManagementService(
            IUserRepository userRepository,
            ICompanyRepository companyRepository,
            ILogger<UserBusinessBusinessManagementService> logger)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(_userRepository));
            _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(_companyRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<UserBusiness> CreateUserBusinessAsync(string firstName, string lastName, string email, string password)
        {
            _logger.LogInformation("Attempting to add new userBusiness: {firstName}, {lastName}, {email}", firstName, lastName, email);
            
            if (string.IsNullOrWhiteSpace(firstName))
            {
                _logger.LogWarning("Attempted to add userBusiness with empty firstName.");
                throw new ArgumentException("firstName cannot be empty.", nameof(firstName));
            }

            if (string.IsNullOrWhiteSpace(lastName))
            {
                _logger.LogWarning("Attempted to add userBusiness with empty lastName.");
                throw new ArgumentException("lastName cannot be empty.", nameof(lastName));
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                _logger.LogWarning("Attempted to add userBusiness with empty email.");
                throw new ArgumentException("email cannot be empty.", nameof(email));
            }


            var newUserBusiness = new UserBusiness(firstName, lastName, email, password);
            //await _userBusinessRepository.AddAsync(newUserBusiness);
            _logger.LogInformation("Successfully added new userBusiness with ID: {UserBusinessId}", newUserBusiness.Id);
            return newUserBusiness;
        }

        public async Task<UserBusiness> GetUserBusinessByIdAsync(Guid id)
        {
            _logger.LogInformation("Attempting to retrieve UserBusiness with ID: {UserBusinessId}", id);
            if (id == Guid.Empty)
            {
                _logger.LogWarning("Attempted to retrieve UserBusiness with empty ID.");
                throw new ArgumentException("UserBusiness ID cannot be empty.", nameof(id));
            }

            var user = await _userRepository.GetByIdAsync(id);
            var userBusiness = new UserBusiness(user.FirstName, user.LastName, user.Email, user.PasswordHash);

            var companies = await _companyRepository.GetByUserIdAsync(user.Id);
            if (companies.Any())
            {
                foreach (var company in companies)
                {
                    userBusiness.AddCompany(company);
                }
            }

            //var userBusiness = await _userBusinessRepository.GetByIdAsync(id);
            if (user == null)
            {
                _logger.LogWarning("UserBusiness with ID {UserId} not found.", id);
            }
            else
            {
                _logger.LogInformation("Successfully retrieved UserBusiness with ID: {UserId}", id);
            }
            return userBusiness;
        }

        public async Task<UserBusiness> AuthenticateUserBusinessAsync(string email, string password)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateUserProfilAsync(Guid id, string firstName, string lastName, string email)
        {
            throw new NotImplementedException();
        }

        public async Task ChangeUserBusinessPasswordAsync(Guid id, string newPassword)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteUserBusinessAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<UserBusiness>> GetAllUsersBusinessAsync()
        {
            throw new NotImplementedException();
        }
    }
}
    
