using Microsoft.AspNetCore.Identity;

namespace TheatreCompany.Models;

public class ApplicationUser : IdentityUser
{
    // Add custom user properties here
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Street { get; set; }
    public string? City { get; set; }
    public string? PostCode { get; set; }
    public bool IsActive { get; set; } = true;
    public bool IsSuspended { get; set; } = false;
} 