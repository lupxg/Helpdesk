using Helpdesk.Domain.Common;

namespace Helpdesk.Domain.Entities;

public class TeamMember
{
    public Guid TeamId { get; private set; }
    public Guid UserId { get; private set; }

    // EF core
    private TeamMember() { }

    public TeamMember(Guid teamId, Guid userId)
    {
        TeamId = DomainGuard.ValidateId(teamId);
        UserId = DomainGuard.ValidateId(userId);
    }
}
