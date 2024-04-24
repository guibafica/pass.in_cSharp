using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;
using PassIn.Infrastructure.Entities;

namespace PassIn.Application.UseCases.CheckIns.ToDoCheckin;

public class ToDoAttendeeCheckinUseCase
{
    private readonly PassInDbContext _dbContext;

    // Constructor
    public ToDoAttendeeCheckinUseCase()
    {
        _dbContext = new PassInDbContext();
    }
    
    public ResponseRegisteredJson Execute(Guid attendeeId)
    {
        Validade(attendeeId);

        var entity = new CheckIn
        {
            Attendee_Id = attendeeId,
            Created_at = DateTime.UtcNow,
        };
            
        _dbContext.CheckIns.Add(entity);
        _dbContext.SaveChanges();
        
        return new ResponseRegisteredJson
        {
            Id = entity.Id,
        };
    }

    private void Validade(Guid attendeeId)
    {
        var existAttendee = _dbContext.Attendees.Any(attendee => attendee.Id == attendeeId);
        if (existAttendee == false)
        {
            throw new NotFoundException("The attendee with this ID was not found.");
        }

        var existCheckin = _dbContext.CheckIns.Any(ch => ch.Attendee_Id == attendeeId);
        if (existCheckin)
        {
            throw new ConflictException("Attendee cannot checkin twice for the same event.");
        }
    }
}
