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
		if (string.IsNullOrWhiteSpace(id))
		{
			throw new ArgumentException("Ticket id cannot be blank.", nameof(id));
		}

		if (string.IsNullOrWhiteSpace(title))
		{
			throw new ArgumentException("Ticket title cannot be blank.", nameof(title));
		}

		if (string.IsNullOrWhiteSpace(createdByUserId))
		{
			throw new ArgumentException("Creator user id cannot be blank.", nameof(createdByUserId));
		}

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
		if(string.IsNullOrWhiteSpace(AssigneeUserId))
		{
			throw new ArgumentException("Assignee user id cannot be blank.", nameof(userId));
		}
	}

	public void ChangeStatus(TicketStatus newStatus)
	{
		if (Status == TicketStatus.Open && newStatus != TicketStatus.InProgress)
		{
			switch(newStatus)
			{
				case TicketStatus.Closed:
					{
						throw new InvalidOperationException("Ticket Status cannot go from Open to Closed.");
					}
				case TicketStatus.Resolved:
					{
						throw new InvalidOperationException("Ticket Status cannot go from Open to Resolved.");
					}
			}			
		}
		if (Status == TicketStatus.InProgress && newStatus == TicketStatus.Closed)
		{
			throw new InvalidOperationException("Ticket Status cannot go from In Progress to Closed.");
		}
		Status = newStatus;
	}
}