using Microsoft.AspNetCore.Mvc;
using valor_chain.api.Application.Handlers;
using valor_chain.api.Application.Queries;

namespace valor_chain.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompaniesController : Controller
    {
        private readonly GetCompanyByIdQueryHandler _getCompanyByIdHandler;
        private readonly ILogger<CompaniesController> _logger;

        public CompaniesController(
            GetCompanyByIdQueryHandler getCompanyByIdHandler,
            ILogger<CompaniesController> logger)
        {
            _getCompanyByIdHandler = getCompanyByIdHandler;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompagnyById(Guid id)
        {
            try
            {
                _logger.LogInformation("Received Company retrieval request by ID: {CompanyId}", id);
                var query = new GetCompanyQuery { CompanyId = id };
                var company = await _getCompanyByIdHandler.Handle(query, CancellationToken.None);
                if (company == null)
                {
                    return NotFound();
                }
                return Ok(company);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Company by ID.");
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
