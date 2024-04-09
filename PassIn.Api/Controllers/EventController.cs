using Microsoft.AspNetCore.Mvc;

using PassIn.Application.UseCases.Events.Register;
using PassIn.Communication.Requests;
using PassIn.Communication.Responses;

namespace PassIn.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventController : ControllerBase
{
  [HttpPost]
  // It's supposed to receive "RequestEventJson" from request's body -> "FromBody". 
  // And it's named by "request"
  public IActionResult Register([FromBody] RequestEventJson request) 
  {
    try
    {
      var useCase = new RegisterEventUseCase();

      useCase.Execute(request);

      return Created();
    }
    catch (ArgumentException ex)
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
