using PassIn.Communication.Requests;

namespace PassIn.Application.UseCases.Events.Register;

public class RegisterEventUseCase
{
    public void Execute(RequestEventJson request)
    {
        Validade(request);
    }

    private void Validade(RequestEventJson request)
    {
        if (request.MaximumAttendees <= 0)
        {
            throw new ArgumentException("The Maximum Attendees must be a positive value");
        }
        
        if (string.IsNullOrWhiteSpace(request.Title))
        {
            throw new ArgumentException("The Title must have a value");
        }
        
        if (string.IsNullOrWhiteSpace(request.Details))
        {
            throw new ArgumentException("The Details must have a value");
        }
    }
}
