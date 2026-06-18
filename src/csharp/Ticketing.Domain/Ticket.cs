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
		if (string.IsNullOrWhiteSpace(userId))
		{
			throw new ArgumentException("Assignee user id cannot be blank.", nameof(userId));
		}
		AssigneeUserId = userId;
	}

	public void ChangeStatus(TicketStatus newStatus)
	{
		string newStatusText = FormatStatus(newStatus);
		string oldStatusText = FormatStatus(Status);
		bool isAllowedTransition =
			(Status == TicketStatus.Open && newStatus == TicketStatus.InProgress) ||
			(Status == TicketStatus.InProgress && newStatus == TicketStatus.Resolved) ||
			(Status == TicketStatus.Resolved && newStatus == TicketStatus.Closed);
		if (!isAllowedTransition)
		{
			throw new InvalidOperationException($"Ticket Status cannot go from {oldStatusText} to {newStatusText}.");
		}
		Status = newStatus;
	}
	private static string FormatStatus(TicketStatus status)
	{
		string statusText = "";
		switch (status)
		{
			case TicketStatus.Open:
				{
					statusText = "Open";
				}
				break;
			case TicketStatus.InProgress:
				{
					statusText = "In Progress";
				}
				break;
			case TicketStatus.Resolved:
				{
					statusText = "Resolved";
				}
				break;
			case TicketStatus.Closed:
				{
					statusText = "Closed";
				}
				break;
		}
		return statusText;
	}
}