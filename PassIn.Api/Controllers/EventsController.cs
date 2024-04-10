using Microsoft.AspNetCore.Mvc;

using PassIn.Communication.Requests;

namespace PassIn.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventsController : ControllerBase
{
    [HttpPost]
    // 'From Body', it's supposed to receive the 'RequestEventJson', and it will be named 'request'
    public IActionResult Register([FromBody] RequestEventJson request)
    {
        return Created();
    }
}
