package ticketing.domain

class Ticket(
    val id: String,
    title: String,
    description: String,
    val createdByUserId: String
) {
    var title: String = title
        private set

    var description: String = description
        private set

    var status: TicketStatus = TicketStatus.Open
        private set

    var assigneeUserId: String? = null
        private set

    init {
        // TODO:
        // Reject blank id.
        // Reject blank title.
        // Reject blank createdByUserId.
        //
        // Kotlin equivalent:
        // if (id.isBlank()) { throw IllegalArgumentException("...") }
    }

    fun assignTo(userId: String) {
        // TODO:
        // Reject blank userId.
        // Then assign assigneeUserId.
    }

    fun changeStatus(newStatus: TicketStatus) {
        // TODO:
        // Implement same allowed transitions as C#:
        // Open -> InProgress
        // InProgress -> Resolved
        // Resolved -> Closed
        // Everything else throws IllegalStateException.
    }

    private fun formatStatus(status: TicketStatus): String {
        // TODO:
        // Return:
        // Open -> "Open"
        // InProgress -> "In Progress"
        // Resolved -> "Resolved"
        // Closed -> "Closed"

        return ""
    }
}