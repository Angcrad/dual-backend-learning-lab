using Ticketing.Domain;

namespace Ticketing.Domain.Tests;

public class TicketTests
{
    [Fact]
    public void Constructor_CreatesOpenTicketWithNoAssignee()
    {
        var ticket = new Ticket(
            id: "ticket-001",
            title: "Printer is broken",
            description: "Office printer shows error 51",
            createdByUserId: "user-001");

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
        var ticket = new Ticket(
            id: "ticket-001",
            title: "Printer is broken",
            description: "Office printer shows error 51",
            createdByUserId: "user-001");

        ticket.AssignTo("user-002");

        Assert.Equal("user-002", ticket.AssigneeUserId);
    }
}
