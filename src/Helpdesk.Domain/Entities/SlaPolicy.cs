using Helpdesk.Domain.Common;
using Helpdesk.Domain.Enums;

namespace Helpdesk.Domain.Entities;

public class SlaPolicy : BaseEntity<Guid>
{
    public TicketPriority Priority { get; set; }
    public int ResponseTimeMinutes { get; set; }
    public int ResolutionTimeMinutes { get; set; } 
}
