using Microsoft.AspNetCore.Mvc;
using valor_chain.api.Domain.Entities;

namespace valor_chain.api;

public abstract class ValorChainControllerBase : ControllerBase
{
    public IActionResult WrappeResponse<T>(ApiResponse<T> response)
    {
        if (response.Category == ApiResponseType.Success)
            return Ok(response);
        if (response.Category == ApiResponseType.Unauthorized)
            return Unauthorized(response);
        if (response.Category == ApiResponseType.NotFound)
            return NotFound(response);
        if (response.Category == ApiResponseType.BadRequest)
            return BadRequest(response);
        if (response.Category == ApiResponseType.InvalidParameters)
            return Conflict(response);
        return NoContent();
    }
}