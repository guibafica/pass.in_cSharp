namespace PassIn.Infrastructure.Entities;

public class CheckIn
{
    public Guid Id { get; set; }
    public DateTime Created_at { get; set; }
    public Guid Attendee_Id { get; set; }
}
