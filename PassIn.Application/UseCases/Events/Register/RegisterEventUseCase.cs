using PassIn.Communication.Requests;

namespace PassIn.Application.UseCases.Events.Register;

public class RegisterEventUseCase
{
  public void Execute(RequestEventJson request)
  {
    Validate(request);
  }

  private void Validate(RequestEventJson request)
  {
    if (request.MaximumAttendees <= 0)
    {
      throw new ArgumentException("The Maximum Attendees value must be positive");
    }

    if (string.IsNullOrWhiteSpace(request.Title))
    {
      throw new ArgumentException("The Title value is invalid");
    } 
  
    if (string.IsNullOrWhiteSpace(request.Details))
    {
      throw new ArgumentException("The Details value is invalid");
    } 
  }
}
