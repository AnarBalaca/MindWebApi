using Mind.Entity.Base;
using Mind.Entity.Identity;

namespace Mind.Entity.Entities
{
    public class Psychologist : BaseEntity, IEntity
    {
        public int? ExperienceYear { get; set; }
        public int? PhoneNumber { get; set; }
        public double? TherapyPrice { get; set; }
        public string? LocalAdress { get; set; }
        public string? UserId { get; set; }
        public AppUser? User { get; set; }
        public ICollection<Image>? Image { get; set; }
    }
}
