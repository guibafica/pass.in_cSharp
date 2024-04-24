using Microsoft.EntityFrameworkCore;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;

namespace PassIn.Application.UseCases.Attendees.GetAllByEventId;

public class GetAllAttendeesByEventIdUseCase
{
    private readonly PassInDbContext _dbContext;

    // Constructor
    public GetAllAttendeesByEventIdUseCase()
    {
        _dbContext = new PassInDbContext();
    }
    
    public ResponseAllAttendeesJson Execute(Guid eventId)
    {
        // Wrong way, because it isn't using the Foreign Key 
        // var attendees = _dbContext.Attendees.Where(attendee => attendee.Event_Id == eventId).ToList();
        var entity = _dbContext
            .Events
            .Include(ev => ev.Attendees)
            .ThenInclude(attendee => attendee.CheckIn) // After events, include from attendee, checkIn 
            .FirstOrDefault(ev => ev.Id == eventId);

        if (entity == null) throw new NotFoundException("An event with this ID does not exist.");

        return new ResponseAllAttendeesJson
        {
            Attendees = entity.Attendees.Select(attendee => new ResponseAttendeeJson
            {
                Id = attendee.Id,
                Name = attendee.Name,
                Email = attendee.Email,
                CreatedAt = attendee.Created_At,
                CheckedInAt = attendee.CheckIn?.Created_at,
            }).ToList()
        };
    }
}