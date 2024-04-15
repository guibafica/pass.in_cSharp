using Microsoft.AspNetCore.Mvc;

using PassIn.Application.UseCases.Events.Register;
using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using PassIn.Exceptions;

namespace PassIn.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventsController : ControllerBase
{
    [HttpPost]
    // Descriptions of possible Errors. It will appear on Swagger page 
    [ProducesResponseType(typeof(ResponseRegisteredEventJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    // 'From Body', it's supposed to receive the 'RequestEventJson', and it will be named 'request'
    public IActionResult Register([FromBody] RequestEventJson request)
    {
        try
        {
            var useCase = new RegisterEventUseCase();
        
            useCase.Execute(request);
        
            return Created();
        }
        catch (PassInException ex)
        {
            return BadRequest(new ResponseErrorJson(ex.Message));
        }
        catch
        {
            return StatusCode(
                StatusCodes.Status500InternalServerError, 
                new ResponseErrorJson("Unknown error")
            );
        }
    }
}
