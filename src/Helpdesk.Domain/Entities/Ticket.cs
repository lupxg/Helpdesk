using Helpdesk.Domain.Common;
using Helpdesk.Domain.Enums;
using Helpdesk.Domain.Common;

namespace Helpdesk.Domain.Entities;

public class Ticket : BaseEntity<Guid>
{
    public long Number {  get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public TicketStatus Status { get; private set; }
    public TicketPriority Priority { get; private set; }
    public Guid? CreatedById { get; private set; }
    public Guid? AssignedToId { get; private set; }
    public Guid? TeamId { get; private set; }
    public DateTimeOffset? DueAt { get; private set; }
    public Guid SlaPolicyId { get; private set; }

    // EF
    private Ticket() { }

    // Methods
    public Ticket(string title, string description, TicketPriority priority, Guid slaPolicyId, DateTimeOffset now, Guid? createdById = null) : base(Guid.NewGuid())
    {
        if (!Enum.IsDefined(priority))
            throw new ArgumentOutOfRangeException(nameof(priority));

        if (now == default)
            throw new ArgumentException("Creation timestamp is required.", nameof(now));

        Title = DomainGuard.ValidateString(title)!;
        Description = DomainGuard.ValidateString(description)!;
        Priority = priority;
        SlaPolicyId = DomainGuard.ValidateId(slaPolicyId)!;
        CreatedById = DomainGuard.ValidateOptionalId(createdById);
        Status = TicketStatus.Open;

        MarkCreated(now);
    }

    public void AssignTo(Guid userId)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException("User ID cannot be empty.", nameof(userId));

        if (AssignedToId == userId)
            return;

        AssignedToId = userId;
    }

    public void AssignToTeam(Guid teamId)
    {
        if (teamId == Guid.Empty)
            throw new ArgumentException("Team ID cannot be empty.", nameof(teamId));

        if (TeamId == teamId)
            return;

        TeamId = teamId;
    }

    public void UnassignUser()
    {
        AssignedToId = null;
    }

    public void UnassignTeam()
    {
        TeamId = null;
    }

    public void ChangePriority(TicketPriority priority)
    {
        if (!Enum.IsDefined(priority))
            throw new ArgumentOutOfRangeException(nameof(priority));

        if (Priority == priority)
            return;

        Priority = priority;
    }

    public void SetDueAt(DateTimeOffset? dueAtUtc)
    {
        DueAt = dueAtUtc;
    }

    public void SetNumber(long number)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(number);

        if (Number != 0)
            throw new InvalidOperationException("The ticket number has already been assigned.");

        Number = number;
    }

    public void StartProgress()
    {
        EnsureStatus(TicketStatus.Open, TicketStatus.Pending);
        Status = TicketStatus.InProgress;
    }

    public void PutOnHold()
    {
        EnsureStatus(TicketStatus.InProgress);
        Status = TicketStatus.Pending;
    }

    public void Resolve()
    {
        EnsureStatus(TicketStatus.InProgress, TicketStatus.Pending);
        Status = TicketStatus.Resolved;
    }

    public void Close()
    {
        EnsureStatus(TicketStatus.Resolved);
        Status = TicketStatus.Closed;
    }

    public void Reopen()
    {
        EnsureStatus(TicketStatus.Resolved);
        Status = TicketStatus.InProgress;
    }

    private void EnsureStatus(params TicketStatus[] allowedStatuses)
    {
        if (!allowedStatuses.Contains(Status))
        {
            throw new InvalidOperationException($"The operation is not allowed when the ticket status is {Status}.");
        }
    }
}
