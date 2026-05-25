namespace Eventify.Domain.Constants;

/// <summary>
/// Defines the role names used in the EventifyPro application for authorization and access control.
/// </summary>
/// <remarks>
/// These role constants are used by ASP.NET Core Identity for role-based authorization.
/// Each role defines a set of permissions and capabilities within the system.
/// </remarks>
public static class RoleNames
{
    /// <summary>Administrator role with full system access and management capabilities.</summary>
    public const string Admin = "Admin";

    /// <summary>Event organizer role with permissions to create, manage, and publish events.</summary>
    public const string Organizer = "Organizer";

    /// <summary>Attendee role with permissions to browse, book, and attend events.</summary>
    public const string Attendee = "Attendee";

    /// <summary>Scanner role with permissions to scan tickets and validate attendee entry.</summary>
    public const string Scanner = "Scanner";
}

