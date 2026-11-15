using Helpdesk.Domain.Common;

namespace Helpdesk.Domain.Entities;

public class Department : BaseEntity<Guid>
{
    public string Name { get; set; } = string.Empty;
}
