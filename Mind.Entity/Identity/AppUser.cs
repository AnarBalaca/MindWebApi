using Microsoft.AspNetCore.Identity;
using Mind.Entity.Base;
using Mind.Entity.Entities;

namespace Mind.Entity.Identity;

public class AppUser : IdentityUser , IEntity
{
    public string? Firstname { get; set; }
    public string? Lastname { get; set; }
    public string? Mailadress { get; set; }
    public int Age { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsPsycho { get; set; }
    public int? ProfileImageId { get; set; }
    public Image? ProfileImage { get; set; }
    public Psychologist? Psychologist { get; set; }
    public ICollection<Blog>? Blog { get; set; }
}
