using Mind.Entity.Base;

namespace Mind.Entity.Entities
{
    public class Psychologist : BaseEntity, IEntity
    {
        public int ExperienceYear { get; set; }
        public double TherapyPrice { get; set; }
        public ICollection<Image>? Image { get; set; }
    }
}
