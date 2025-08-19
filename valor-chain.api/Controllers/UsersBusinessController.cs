using Microsoft.AspNetCore.Mvc;
using valor_chain.api.Application.Commands;
using valor_chain.api.Application.Handlers;
using valor_chain.api.Application.Queries;
using valor_chain.api.Domain.Entities;

namespace valor_chain.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersBusinessController : ControllerBase
    {
        private readonly CreateUserCommandHandler _createUserHandler;
        private readonly GetUserBusinessByIdQueryHandler _getUserBusinessBusinessByIdHandler;
        private readonly ILogger<UsersBusinessController> _logger;
        public UsersBusinessController(
            CreateUserCommandHandler createUserHandler,
            GetUserBusinessByIdQueryHandler getUserBusinessBusinessByIdHandler,
            ILogger<UsersBusinessController> logger)
        {
            _createUserHandler = createUserHandler;
            _getUserBusinessBusinessByIdHandler = getUserBusinessBusinessByIdHandler;
            _logger = logger;
        }
        [HttpPost]
        public async Task<IActionResult> CreateUserBusiness([FromBody] CreateUserCommand command)
        {
            try
            {
                _logger.LogInformation("Received UserBusiness creation request for {Email}", command.Email);
                User userBusiness = await _createUserHandler.Handle(command, CancellationToken.None);
                return CreatedAtAction(
                    nameof(GetUserBusinessById), 
                    new { id = userBusiness.Id }, 
                    (UserBusiness)userBusiness);
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating UserBusiness .");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserBusinessById(Guid id)
        {
            try
            {
                _logger.LogInformation("Received UserBusiness  retrieval request by ID: {UserBusinessId}", id);
                var query = new GetUserByIdQuery { UserId = id };
                var userBusiness = await _getUserBusinessBusinessByIdHandler.Handle(query, CancellationToken.None);
                if (userBusiness == null)
                {
                    return NotFound();
                }
                return Ok(userBusiness);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving UserBusiness by ID.");
                return StatusCode(500, "Internal server error");
            }
        }
        // Other endpoints for company, project management, etc.
    }

}
