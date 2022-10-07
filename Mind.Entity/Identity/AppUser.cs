using Microsoft.AspNetCore.Identity;

namespace Mind.Entity.Identity;

public class AppUser : IdentityUser
{
    public string? Firstname { get; set; }
    public string? Lastname { get; set; }
}
