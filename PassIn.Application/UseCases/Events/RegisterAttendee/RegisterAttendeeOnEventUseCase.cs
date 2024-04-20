using System.Net.Mail;
using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;

namespace PassIn.Application.UseCases.Events.RegisterAttendee;

public class RegisterAttendeeOnEventUseCase
{
    // Create a "Global", (only for this class, because it's private), context to be used for all functions below
    private readonly PassInDbContext _dbContext;
    
    // That's a constructor. It works similar to useEffect 
    public RegisterAttendeeOnEventUseCase()
    {
        _dbContext = new PassInDbContext();
    }
    
    public ResponseRegisteredJson Execute(Guid eventId, RequestRegisterEventJson request)
    {
        Validate(eventId, request);

        var entity = new Infrastructure.Entities.Attendee
        {
            Email = request.Email,
            Name = request.Name,
            Event_Id = eventId,
            Created_At = DateTime.UtcNow,
        };

        _dbContext.Attendees.Add(entity);
        _dbContext.SaveChanges();

        return new ResponseRegisteredJson
        {
            Id = entity.Id
        };
    }

    private void Validate(Guid eventId, RequestRegisterEventJson request)
    {
        var eventEntity = _dbContext.Events.Find(eventId);
        if (eventEntity is null) throw new NotFoundException("An event with this Id don't exists.");
        
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            throw new ErrorOnValidationException("The Name must have a value");
        }

        if (EmailIsValid(request.Email) == false)
        {
            throw new ErrorOnValidationException("The Email is invalid");
        }

        var attendeeAlreadyRegistered = _dbContext
            .Attendees
            .Any(attendee => attendee.Email.Equals(request.Email) && attendee.Event_Id == eventId);
        if (attendeeAlreadyRegistered)
        {
            throw new ErrorOnValidationException("You cannot register twice on the same event.");
        }

        var attendeeForEvent = _dbContext.Attendees.Count(attendee => attendee.Event_Id == eventId);
        if (attendeeForEvent >= eventEntity.Maximum_Attendees)
        {
            throw new ErrorOnValidationException("There is no room for this event.");
        }
    }

    private bool EmailIsValid(string email)
    {
        try
        {
            new MailAddress(email);

            return true;
        }
        catch
        {
            return false;
        }
    }
}
