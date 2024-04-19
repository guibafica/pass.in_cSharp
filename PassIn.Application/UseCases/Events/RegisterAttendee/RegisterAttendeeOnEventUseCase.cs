using System.Net.Mail;
using PassIn.Communication.Requests;
using PassIn.Exceptions;
using PassIn.Infrastructure;
using PassIn.Infrastructure.Entities;

namespace PassIn.Application.UseCases.Events.RegisterAttendee;

public class RegisterAttendeeOnEventUseCase
{
    public void Execute(Guid eventId, RequestRegisterEventJson request)
    {
        var dbContext = new PassInDbContext();
        
        Validate(eventId, request, dbContext);
    }

    private void Validate(Guid eventId, RequestRegisterEventJson request, PassInDbContext dbContext)
    {
        var existEvent = dbContext.Events.Any(ev => ev.Id == eventId);
        if (existEvent == false) throw new NotFoundException("An event with this Id don't exists.");
        
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            throw new ErrorOnValidationException("The Name must have a value");
        }

        if (EmailIsValid(request.Email) == false)
        {
            throw new ErrorOnValidationException("The Email is invalid");
        }

        var attendeeAlreadyRegistered = dbContext
            .Attendees
            .Any(attendee => attendee.Email.Equals(request.Email) && attendee.Event_Id == eventId);
        if (attendeeAlreadyRegistered)
        {
            throw new ErrorOnValidationException("You cannot register twice on the same event.");
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
