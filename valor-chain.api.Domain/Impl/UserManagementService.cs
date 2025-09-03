using Microsoft.Extensions.Logging;
using System.Net.Mail;
using valor_chain.api.Domain.Entities;
using valor_chain.api.Domain.Ports.Input;
using valor_chain.api.Domain.Ports.Output;
using valor_chain.api.Domain.Static;

namespace valor_chain.api.Domain.Impl
{
    public class UserManagementService : IUserManagementService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IProfilRepository _profilRepository;
        private readonly ILogger<UserManagementService> _logger;

        public UserManagementService(
            IUserRepository userRepository,
            ICompanyRepository companyRepository,
            ILogger<UserManagementService> logger, 
            IProfilRepository profilRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(_userRepository));
            _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(_companyRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _profilRepository = profilRepository ?? throw new ArgumentNullException(nameof(_profilRepository));
        }

        public async Task<ApiResponse<User>> CreateUserAsync(User user)
        {
            /*command.FirstName,
                command.LastName,
                command.Email,
                HashHelper.ComputeSha256Hash(command.Password), // HASH THIS!
                GetPhoneNumberInternationalFormat(command.PhoneNumber)*/


            var response = new ApiResponse<User>
            {
                Message = $"Attempting to add new User: {user.FirstName}, {user.LastName}, {user.Email}"
            };
            string message;
            _logger.LogInformation(response.Message);
            
            if (string.IsNullOrWhiteSpace(user.FirstName))
            {
                response.Category = ApiResponseType.InvalidParameters;
                message = "Attempted to add User with empty firstName.";
                response.Errors.Add(message);
                _logger.LogWarning(message);
            }

            if (string.IsNullOrWhiteSpace(user.LastName))
            {
                response.Category = ApiResponseType.InvalidParameters;
                message = "Attempted to add User with empty lastName.";
                response.Errors.Add(message);
                _logger.LogWarning(message);
            }

            if (string.IsNullOrWhiteSpace(user.Email))
            {
                response.Category = ApiResponseType.InvalidParameters;
                message = "Attempted to add User with empty email.";
                response.Errors.Add(message);
                _logger.LogWarning(message);
            }
            
            if (!IsValidEmail(user.Email))
            {
                response.Category = ApiResponseType.InvalidParameters;
                message = "Attempted to add User with invalid email.";
                response.Errors.Add(message);
                _logger.LogWarning(message);
            }

            if (response.Category == ApiResponseType.InvalidParameters)
                return response;

            //var newUser = new User(user.FirstName, user.LastName, user.Email, user.PasswordHash, user.PhoneNumber);
            await _userRepository.AddAsync(user);
            
            _logger.LogInformation("Successfully added new User with ID: {Id}", user.Id);
            response.Category = ApiResponseType.Success;
            response.Message = string.Empty;
            response.Data = user;
            return response;
        }
        
        public async Task<ApiResponse<User>> GetUserByIdAsync(Guid id)
        {
            var response = new ApiResponse<User>
            {
                Message = $"Attempting to retrieve User with ID: {id}"
            };
            string message;
            _logger.LogInformation(response.Message);

            if (id == Guid.Empty)
            {
                response.Category = ApiResponseType.BadRequest;
                message = "User ID cannot be empty.";
                response.Errors.Add(message);
                _logger.LogWarning(message);
                return response;
            }

            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
            {

                response.Category = ApiResponseType.NotFound;
                message = $"User with ID {id} not found.";
                response.Errors.Add(message);
                _logger.LogWarning(message);
                return response;
            }

            LoadProfils(user);

            _logger.LogInformation("Successfully retrieved User with ID: {id}", id);
            response.Category = ApiResponseType.Success;
            response.Message = string.Empty;
            response.Data = user;
            return response;
        }

        public async Task<ApiResponse<User>> AuthenticateUserAsync(string email, string password)
        {
            var response = new ApiResponse<User>
            {
                Message = $"Attempting to Authenticate User with Email: {email}"
            };
            string message;
            _logger.LogInformation(response.Message);

            if (string.IsNullOrWhiteSpace(email))
            {
                response.Category = ApiResponseType.InvalidParameters;
                message = "Attempted to Authenticate User with empty email.";
                response.Errors.Add(message);
                _logger.LogWarning(message);
            }

            if (!IsValidEmail(email))
            {
                response.Category = ApiResponseType.InvalidParameters;
                message = "Attempted to Authenticate User with invalid email.";
                response.Errors.Add(message);
                _logger.LogWarning(message);
            }

            if (response.Category == ApiResponseType.InvalidParameters)
                return response;

            var user = await _userRepository.GetByEmailAsync(email);

            if (user == null)
            {
                response.Category = ApiResponseType.NotFound;
                message = $"User not exist with Email {email}.";
                response.Errors.Add(message);
                _logger.LogWarning(message);
                return response;
            }


            user = await _userRepository.AuthenticateUserAsync(email, password);

            if (user == null)
            {
                response.Category = ApiResponseType.Unauthorized;
                message = $"Password is wrong for User with Email {email}.";
                response.Errors.Add(message);
                _logger.LogWarning(message);
                return response;
            }

            LoadProfils(user);

            _logger.LogInformation("Successfully Authenticate User with Email: {Email}", email);
            response.Category = ApiResponseType.Success;
            response.Message = string.Empty;
            response.Data = user;
            return response;
        }

        public async Task UpdateUserAsync(Guid id, string firstName, string lastName, string email, string phoneNumber)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<User>> ChangeUserPasswordAsync(Guid id, string email, string newPassword)
        {

            var response = new ApiResponse<User>
            {
                Message = $"Attempting to change password for User with Email: {email}"
            };
            string message;
            _logger.LogInformation(response.Message);

            if (id == Guid.Empty)
            {
                response.Category = ApiResponseType.BadRequest;
                message = "User ID cannot be empty.";
                response.Errors.Add(message);
                _logger.LogWarning(message);
                return response;
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                response.Category = ApiResponseType.InvalidParameters;
                message = "Attempted to change password for User with empty email.";
                response.Errors.Add(message);
                _logger.LogWarning(message);
            }

            if (!IsValidEmail(email))
            {
                response.Category = ApiResponseType.InvalidParameters;
                message = "Attempted to change password for User with invalid email.";
                response.Errors.Add(message);
                _logger.LogWarning(message);
            }

            if (string.IsNullOrWhiteSpace(newPassword))
            {
                response.Category = ApiResponseType.InvalidParameters;
                message = "Attempted to change password for User with empty newPassword.";
                response.Errors.Add(message);
                _logger.LogWarning(message);
            }

            if (response.Category == ApiResponseType.InvalidParameters)
                return response;


            var user = await _userRepository.ChangeUserPasswordAsync(id, email, newPassword);

            LoadProfils(user);

            _logger.LogInformation("Successfully change password for User with Email: {email}");
            response.Category = ApiResponseType.Success;
            response.Message = string.Empty;
            response.Data = user;
            return response;
        }

        public async Task DeleteUserAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<IEnumerable<User>>> GetAllUsersAsync()
        {
            var response = new ApiResponse<IEnumerable<User>>
            {
                Message = "Attempting to get all User"
            };
            _logger.LogInformation(response.Message);

            var usersResult = new List<User>();
            var userList = await _userRepository.GetAllAsync();
            foreach (var user in userList)
            {
                usersResult.Add(await LoadProfils(user));
            }
            
            _logger.LogInformation("Successfully get all User");
            response.Category = ApiResponseType.Success;
            response.Message = string.Empty;
            response.Data = usersResult;
            return response;
        }

        public async Task<ApiResponse<Profil>> AddUserProfilAsync(Guid userId, string profilName)
        {
            var response = new ApiResponse<Profil>
            {
                Message = $"Attempting to add new UserProfil to User: {userId}, {profilName}"
            };
            string message;
            _logger.LogInformation(response.Message);
            
            if (string.IsNullOrWhiteSpace(profilName) )
            {
                response.Category = ApiResponseType.BadRequest;
                message = "ProfilName is empty.";
                response.Errors.Add(message);
                _logger.LogWarning(message);
                return response;
            }

            if (!IsValidProfil(profilName))
            {
                response.Category = ApiResponseType.BadRequest;
                message = $"ProfilName is invalid : {profilName}.";
                response.Errors.Add(message);
                _logger.LogWarning(message);
                return response;
            }

            if (!await IsUserExist(userId))
            {
                response.Category = ApiResponseType.BadRequest;
                message = $"User does not exist : {userId}.";
                response.Errors.Add(message);
                _logger.LogWarning(message);
                return response;
            }

            if (await _profilRepository.IsUserProfilExist(userId, profilName))
            {
                response.Category = ApiResponseType.NotFound;
                message = $"Profil already exist : {profilName} for user : {userId}.";
                response.Errors.Add(message);
                _logger.LogWarning(message);
                return response;
            }

            var newProfil = new Profil(userId, profilName);
            await _profilRepository.AddAsync(newProfil);


            _logger.LogInformation("Successfully added new UserProfil with ID: {ProfilId}, {UserId}", newProfil.Id, newProfil.UserId);
            response.Category = ApiResponseType.Success;
            response.Message = string.Empty;
            response.Data = newProfil;
            return response;
        }

        private async Task<bool> IsUserExist(Guid userId)
        {
            _logger.LogInformation("Attempting to check User existance with ID: {UserId}", userId);
            if (userId == Guid.Empty)
            {
                _logger.LogWarning("Attempted to check User existance with empty ID.");
                throw new ArgumentException("User ID cannot be empty.", nameof(userId));
            }

            var exists = await _userRepository.IsUserExist(userId);

            _logger.LogInformation("Successfully checked User existance with ID: {UserId}", userId);
            return exists;
        }

        public async Task<ApiResponse<bool>> CheckEmailAsync(string email)
        {
            var response = new ApiResponse<bool>
            {
                Message = $"Attempting to CheckEmail with Email: {email}",
                Data = true
            };
            string message;
            _logger.LogInformation(response.Message);

            if (string.IsNullOrWhiteSpace(email))
            {
                response.Category = ApiResponseType.InvalidParameters;
                message = "Attempted to CheckEmail with empty email.";
                response.Errors.Add(message);
                _logger.LogWarning(message);
            }

            if (!IsValidEmail(email))
            {
                response.Category = ApiResponseType.InvalidParameters;
                message = "Attempted to CheckEmail with invalid email.";
                response.Errors.Add(message);
                _logger.LogWarning(message);
            }

            if (response.Category == ApiResponseType.InvalidParameters)
                return response;

            var user = await _userRepository.GetByEmailAsync(email);

            if (user != null)
            {
                response.Category = ApiResponseType.Success;
                message = $"User exist with Email {email}.";
                response.Errors.Add(message);
                _logger.LogWarning(message);
                return response;
            }

            _logger.LogInformation("Successfully CheckEmail with Email: {Email}", email);
            response.Category = ApiResponseType.Success;
            response.Message = string.Empty;
            response.Data = false;
            return response;
        }

        public async Task<ApiResponse<bool>> CheckPhoneAsync(string phoneNumbre)
        {
            var response = new ApiResponse<bool>
            {
                Message = $"Attempting to CheckPhone with PhoneNumbre: {phoneNumbre}",
                Data = true
            };
            string message;
            _logger.LogInformation(response.Message);

            if (!IsValidPhoneNumber(phoneNumbre))
            {
                response.Category = ApiResponseType.InvalidParameters;
                message = "Attempted to CheckPhone with invalid phone number.";
                response.Errors.Add(message);
                _logger.LogWarning(message);
            }

            if (response.Category == ApiResponseType.InvalidParameters)
                return response;

            var user = await _userRepository.GetByPhoneNumberAsync(phoneNumbre);

            if (user != null)
            {
                response.Category = ApiResponseType.Success;
                message = $"User exist with Phone Number {phoneNumbre}.";
                response.Errors.Add(message);
                _logger.LogWarning(message);
                return response;
            }

            _logger.LogInformation("Successfully CheckPhone with Email: {PhoneNumbre}", phoneNumbre);
            response.Category = ApiResponseType.Success;
            response.Message = string.Empty;
            response.Data = false;
            return response;
        }

        private bool IsValidProfil(string profil)
        {
            return Enum.TryParse<UserProfil>(profil, ignoreCase: true, out _);
        }

        private async Task<User> LoadProfils(User user)
        {
            var profils = await _profilRepository.GetProfilsByUserIdAsync(user.Id);
            if (profils.Any())
                foreach (var profil in profils)
                {
                    user.AddProfil(Enum.Parse<UserProfil>(profil.ProfilName));
                }
            return user;
        }


        public bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                var mailAddress = new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public bool IsValidPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return false;

            try
            {
                // Supposons que votre commande contient le numéro et le code du pays.
                // Pour la Guinée, le code est "GN".
                const string countryCode = "GN";

                // 1. Valider le format du numéro de téléphone
                if (!PhoneNumberValidator.IsValidNumber(phoneNumber, countryCode))
                {
                    // Si le format lui-même est invalide, on peut considérer qu'il n'existe pas
                    // ou renvoyer une erreur spécifique au client.
                    // Pour une simple vérification d'existence, on peut retourner `false`.
                    _logger.LogWarning("Numéro de téléphone au format invalide reçu : {PhoneNumber}", phoneNumber);
                    return false;
                }
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

    }
}
    
