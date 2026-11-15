namespace Helpdesk.Domain.Common;

public sealed class DomainGuard
{
    /// <summary>
    /// Validates required strings 
    /// </summary>
    /// <param name="property"></param>
    /// <returns>trimed string property</returns>
    /// <exception cref="ArgumentException"></exception>
    public static string ValidateString(string property)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(property, nameof(property));

        return property.Trim();
    }

    /// <summary>
    /// Validate required Ids
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static Guid ValidateId(Guid id)
    {
        if (id == Guid.Empty) throw new ArgumentException("Id cannot be empty", nameof(id));

        return id;
    }

    /// <summary>
    /// Validates optional Ids
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static Guid? ValidateOptionalId(Guid? id)
    {
        if (id == Guid.Empty) throw new ArgumentException("Id cannot be empty", nameof(id));

        return id;
    }
}
