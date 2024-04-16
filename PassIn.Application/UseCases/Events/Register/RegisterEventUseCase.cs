using PassIn.Communication.Requests;
using PassIn.Exceptions;
using PassIn.Infrastructure;

namespace PassIn.Application.UseCases.Events.Register;

public class RegisterEventUseCase
{
    public void Execute(RequestEventJson request)
    {
        Validade(request);

        var dbContext = new PassInDbContext();

        var entity = new Infrastructure.Entities.Event
        {
            Title = request.Title,
            Details = request.Details,
            Maximum_Attendees = request.MaximumAttendees,
            Slug = request.Title.ToLower().Replace(" ", "-"),
        };

        dbContext.Events.Add(entity);
        dbContext.SaveChanges();
    }

    private void Validade(RequestEventJson request)
    {
        if (request.MaximumAttendees <= 0)
        {
            throw new PassInException("The Maximum Attendees must be a positive value");
        }
        
        if (string.IsNullOrWhiteSpace(request.Title))
        {
            throw new PassInException("The Title must have a value");
        }
        
        if (string.IsNullOrWhiteSpace(request.Details))
        {
            throw new PassInException("The Details must have a value");
        }
    }
}
