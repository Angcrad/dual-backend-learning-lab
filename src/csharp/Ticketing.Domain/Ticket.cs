namespace Ticketing.Domain;

public class Ticket
{
    public string Id { get; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public TicketStatus Status { get; private set; }
    public string CreatedByUserId { get; }
    public string? AssigneeUserId { get; private set; }

    public Ticket(
        string id,
        string title,
        string description,
        string createdByUserId)
    {
        Id = id;
        Title = title;
        Description = description;
        CreatedByUserId = createdByUserId;
        Status = TicketStatus.Open;
        AssigneeUserId = null;
    }

    public void AssignTo(string userId)
    {
        AssigneeUserId = userId;
    }
}
