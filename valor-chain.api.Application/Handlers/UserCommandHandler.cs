using gn_core_entities.Location;
using valor_chain.api.Application.Commands;
using valor_chain.api.Application.Queries;
using valor_chain.api.Application.Security;
using valor_chain.api.Domain.Entities;
using valor_chain.api.Domain.Ports.Input;
using valor_chain.api.Domain.Static;

namespace valor_chain.api.Application.Handlers
{
    public class UserCommandHandler
    {
        private readonly IUserManagementService _userManagementService;

        public UserCommandHandler(IUserManagementService userManagementService)
        {
            _userManagementService = userManagementService;
        }

        public async Task<ApiResponse<User>> CreateUserHandle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            var user = new User()
            {
                FirstName = command.FirstName,
                LastName = command.LastName,
                Email = command.Email,
                PasswordHash = HashHelper.ComputeSha256Hash(command.Password),
                PhoneNumber = GetPhoneNumberInternationalFormat(command.PhoneNumber),
                CreatedDate = DateTime.UtcNow,
                BirthDate = command.BirthDate,
                IsActive = true
            };
            var addedUser = await _userManagementService.CreateUserAsync(user);

            var userLocation = new UserLocation()
            {
                UserId = addedUser.Data.Id,
                Region = new Region() { Id = command.region },
                Prefecture = new Prefecture() { Id = command.Prefecture },
                SousPrefecture = new SousPrefecture() { Id = command.subPrefecture },
                CreatedDate = DateTime.UtcNow,
                IsActive = true
            };

            await _userManagementService.AddUserLocationAsync(userLocation);
            foreach(string profil in command.valueChainLink)
            {
                await _userManagementService.AddUserProfilAsync(user.Id, profil);                
            }
            return addedUser;
        }
        
        public async Task<ApiResponse<User>> GetUserByIdHandle(GetUserByIdQuery query, CancellationToken cancellationToken)
        {
            return await _userManagementService.GetUserByIdAsync(query.UserId);
        }

        public async Task<ApiResponse<User>> AuthenticateUserHandle(GetLoginQuery query, CancellationToken cancellationToken)
        {
            return await _userManagementService.AuthenticateUserAsync(query.Email, HashHelper.ComputeSha256Hash(query.Password));
        }

        public async Task<ApiResponse<UserProfil>> AddUserProfilHandle(AddUserProfilCommand command,
            CancellationToken cancellationToken)
        {
            return await _userManagementService.AddUserProfilAsync(
                command.UserId,
                command.ProfilName
            );
        }

        public async Task<ApiResponse<IEnumerable<User>>> GetAllUsersHandle(CancellationToken cancellationToken)
        {
            return await _userManagementService.GetAllUsersAsync();
        }

        public async Task<ApiResponse<User>> ChangeUserPasswordHandle(ChangePasswordQuery query, CancellationToken none)
        {
            return await _userManagementService.ChangeUserPasswordAsync(query.Id, query.Email, HashHelper.ComputeSha256Hash(query.Password));
        }

        public async Task<ApiResponse<bool>> CheckEmailHandle(CheckEmailCommand command, CancellationToken none)
        {
            return await _userManagementService.CheckEmailAsync(command.Email);
        }

        public async Task<ApiResponse<bool>> CheckPhoneHandle(CheckPhoneCommand command, CancellationToken none)
        {
            return await _userManagementService.CheckPhoneAsync(GetPhoneNumberInternationalFormat(command.PhoneNumber));
        }

        public async Task<ApiResponse<User>> UpdateUserHandle(Guid userId, UpdateUserCommand command, CancellationToken none)
        {
            var user = new User()
            {
                Id = userId,
                FirstName = command.FirstName,
                LastName = command.LastName,
                PhoneNumber = GetPhoneNumberInternationalFormat(command.PhoneNumber)
            };
            return await _userManagementService.UpdateUserAsync(userId, user);
        }

        private string GetPhoneNumberInternationalFormat(string phoneNumber)
        {
            var phoneNumberInternationalFormat = string.Empty;
            PhoneNumberValidator.TryValidateAndFormat(phoneNumber, "GN", out phoneNumberInternationalFormat);
            return phoneNumberInternationalFormat;
        }
    }
}
