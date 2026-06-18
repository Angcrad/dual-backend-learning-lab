using Ticketing.Domain;

namespace Ticketing.Domain.Tests;

public class TicketTests
{
	private static Ticket CreateTicket()
	{
		return new Ticket(
			id: "ticket-001",
			title: "Printer is broken",
			description: "Office printer shows error 51",
			createdByUserId: "user-001");
	}

	[Fact]
	public void Constructor_CreatesOpenTicketWithNoAssignee()
	{
		var ticket = CreateTicket();

		Assert.Equal("ticket-001", ticket.Id);
		Assert.Equal("Printer is broken", ticket.Title);
		Assert.Equal("Office printer shows error 51", ticket.Description);
		Assert.Equal("user-001", ticket.CreatedByUserId);
		Assert.Equal(TicketStatus.Open, ticket.Status);
		Assert.Null(ticket.AssigneeUserId);
	}

	[Fact]
	public void AssignTo_SetsAssigneeUserId()
	{
		var ticket = CreateTicket();

		ticket.AssignTo("user-002");

		Assert.Equal("user-002", ticket.AssigneeUserId);
	}

	[Theory]
	[InlineData("")]
	[InlineData("   ")]
	public void Constructor_ThrowsWhenIdIsBlank(string id)
	{
		var exception = Assert.Throws<ArgumentException>(() =>
			new Ticket(
				id: id,
				title: "Printer is Broken",
				description: "Office printer shows error 51",
				createdByUserId: "user-001"));

		Assert.Equal("id", exception.ParamName);
	}
	
	[Theory]
	[InlineData("")]
	[InlineData("   ")]
	public void Constructor_ThrowsWhenCreatedByUserIdIsBlank(string createdByUserId)
	{
		var exception = Assert.Throws<ArgumentException>(() =>
			new Ticket(
				id: "ticket-001",
				title: "Printer is Broken",
				description: "Office printer shows error 51",
				createdByUserId: createdByUserId));

		Assert.Equal("createdByUserId", exception.ParamName);
	}

	[Theory]
	[InlineData("")]
	[InlineData("   ")]
	public void Constructor_ThrowsWhenTitleIsBlank(string title)
	{
		var exception = Assert.Throws<ArgumentException>(() =>
			new Ticket(
				id: "ticket-001",
				title: title,
				description: "Office printer shows error 51",
				createdByUserId: "user-001"));

		Assert.Equal("title", exception.ParamName);
	}

	[Theory]
	[InlineData("")]
	[InlineData("   ")]
	public void AssignTo_ThrowsWhenUserIdIsBlank(string userId)
	{
		var ticket = CreateTicket();

		var exception = Assert.Throws<ArgumentException>(() =>
			ticket.AssignTo(userId));

		Assert.Equal("userId", exception.ParamName);
	}

	[Fact]
	public void ChangeStatus_AllowsOpenToInProgress()
	{
		var ticket = CreateTicket();

		ticket.ChangeStatus(TicketStatus.InProgress);

		Assert.Equal(TicketStatus.InProgress, ticket.Status);
	}

	[Fact]
	public void ChangeStatus_AllowsInProgressToResolved()
	{
		var ticket = CreateTicket();

		ticket.ChangeStatus(TicketStatus.InProgress); 
		ticket.ChangeStatus(TicketStatus.Resolved);

		Assert.Equal(TicketStatus.Resolved, ticket.Status);
	}

	[Fact]
	public void ChangeStatus_AllowsResolvedToClosed()
	{
		var ticket = CreateTicket();

		ticket.ChangeStatus(TicketStatus.InProgress);
		ticket.ChangeStatus(TicketStatus.Resolved);
		ticket.ChangeStatus(TicketStatus.Closed);

		Assert.Equal(TicketStatus.Closed, ticket.Status);
	}

	[Fact]
	public void ChangeStatus_RejectsOpenToResolved()
	{
		var ticket = CreateTicket();

		Assert.Throws<InvalidOperationException>(() =>
			ticket.ChangeStatus(TicketStatus.Resolved));
	}

	[Fact]
	public void ChangeStatus_RejectsOpenToClosed()
	{
		var ticket = CreateTicket();

		Assert.Throws<InvalidOperationException>(() =>
			ticket.ChangeStatus(TicketStatus.Closed));
	}

	[Fact]
	public void ChangeStatus_RejectsInProgressToOpen()
	{
		var ticket = CreateTicket();
		ticket.ChangeStatus(TicketStatus.InProgress);
		Assert.Throws<InvalidOperationException>(() =>
			ticket.ChangeStatus(TicketStatus.Open));
	}

	[Fact]
	public void ChangeStatus_RejectsInProgressToClosed()
	{
		var ticket = CreateTicket();
		ticket.ChangeStatus(TicketStatus.InProgress);
		Assert.Throws<InvalidOperationException>(() =>
			ticket.ChangeStatus(TicketStatus.Closed));
	}

	[Fact]
	public void ChangeStatus_RejectsResolvedToOpen()
	{
		var ticket = CreateTicket();
		ticket.ChangeStatus(TicketStatus.InProgress);
		ticket.ChangeStatus(TicketStatus.Resolved);
		Assert.Throws<InvalidOperationException>(() =>
			ticket.ChangeStatus(TicketStatus.Open));
	}

	[Fact]
	public void ChangeStatus_RejectsResolvedToInProgress()
	{
		var ticket = CreateTicket();
		ticket.ChangeStatus(TicketStatus.InProgress);
		ticket.ChangeStatus(TicketStatus.Resolved);
		Assert.Throws<InvalidOperationException>(() =>
			ticket.ChangeStatus(TicketStatus.InProgress));
	}

	[Fact]
	public void ChangeStatus_RejectsClosedToOpen()
	{
		var ticket = CreateTicket();
		ticket.ChangeStatus(TicketStatus.InProgress);
		ticket.ChangeStatus(TicketStatus.Resolved);
		ticket.ChangeStatus(TicketStatus.Closed);
		Assert.Throws<InvalidOperationException>(() =>
			ticket.ChangeStatus(TicketStatus.Open));
	}

	[Fact]
	public void ChangeStatus_RejectsClosedToInProgress()
	{
		var ticket = CreateTicket();
		ticket.ChangeStatus(TicketStatus.InProgress);
		ticket.ChangeStatus(TicketStatus.Resolved);
		ticket.ChangeStatus(TicketStatus.Closed);
		Assert.Throws<InvalidOperationException>(() =>
			ticket.ChangeStatus(TicketStatus.InProgress));
	}

	[Fact]
	public void ChangeStatus_RejectsClosedToResolved()
	{
		var ticket = CreateTicket();
		ticket.ChangeStatus(TicketStatus.InProgress);
		ticket.ChangeStatus(TicketStatus.Resolved);
		ticket.ChangeStatus(TicketStatus.Closed);
		Assert.Throws<InvalidOperationException>(() =>
			ticket.ChangeStatus(TicketStatus.Resolved));
	}

	[Fact]
	public void AssignTo_DoesNotChangeAssigneeWhenUserIdIsBlank()
	{
		var ticket = CreateTicket();

		ticket.AssignTo("user-002");

		Assert.Throws<ArgumentException>(() =>
			ticket.AssignTo(" "));

		Assert.Equal("user-002", ticket.AssigneeUserId);
	}
}
