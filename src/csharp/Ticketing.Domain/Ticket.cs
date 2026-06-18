using System.Runtime.CompilerServices;

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
		string newStatusText = "";
		string oldStatusText = "";
		bool errorFlag = false;
		switch (newStatus)
		{
			case TicketStatus.Open:
				{
					newStatusText = "Open";
				}
				break;
			case TicketStatus.InProgress:
				{
					newStatusText = "In Progress";
				}
				break;
			case TicketStatus.Resolved:
				{
					newStatusText = "Resolved";
				}
				break;
			case TicketStatus.Closed:
				{
					newStatusText = "Closed";
				}
				break;
		}
		switch (Status)
		{
			case TicketStatus.Open:
				{
					oldStatusText = "Open";
				}
				break;
			case TicketStatus.InProgress:
				{
					oldStatusText = "In Progress";
				}
				break;
			case TicketStatus.Resolved:
				{
					oldStatusText = "Resolved";
				}
				break;
			case TicketStatus.Closed:
				{
					oldStatusText = "Closed";
				}
				break;
		}
		if (Status == TicketStatus.Open && newStatus != TicketStatus.InProgress)
		{
			errorFlag = true;
		}
		if (Status == TicketStatus.InProgress && newStatus != TicketStatus.Resolved)
		{
			errorFlag = true;
		}
		if (Status == TicketStatus.Resolved && newStatus != TicketStatus.Closed)
		{
			errorFlag = true;
		}
		if (Status == TicketStatus.Closed)
		{
			errorFlag = true;
		}
		if (errorFlag)
		{
			throw new InvalidOperationException($"Ticket Status cannot go from {oldStatusText} to {newStatusText}.");
		}
		Status = newStatus;
	}
}