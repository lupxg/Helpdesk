using Helpdesk.Domain.Common;

namespace Helpdesk.Domain.Entities;

/// <summary>
/// This entity represent a team to which a team member is assignet to. 
/// </summary>
public class SupportTeam : BaseEntity<Guid>
{
    public string Name { get; private set; } = string.Empty;

    // EF core
    private SupportTeam() { }

    public SupportTeam(string name) : base(Guid.NewGuid())
    {
        Name = DomainGuard.ValidateString(name);
    }

    public void Rename(string newName)
    {
        Name = DomainGuard.ValidateString(newName);
    }
}
