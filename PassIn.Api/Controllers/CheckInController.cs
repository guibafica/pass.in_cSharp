using Microsoft.AspNetCore.Mvc;
using PassIn.Application.UseCases.CheckIns.ToDoCheckin;
using PassIn.Communication.Responses;

namespace PassIn.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CheckInController : Controller
{
    [HttpPost]
    [Route("{attendeeId}")]
    [ProducesResponseType(typeof(ResponseRegisteredJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status409Conflict)]
    public IActionResult Checkin([FromRoute] Guid attendeeId)
    {
        var useCase = new ToDoAttendeeCheckinUseCase();

        var response = useCase.Execute(attendeeId);
        
        return Created(String.Empty, response);
    }
}