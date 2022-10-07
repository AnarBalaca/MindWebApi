namespace Mind.Entity.Dto.Psychologist
{
    public class PsychologistGetDto
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public int ExperienceYear { get; set; }
        public double TherapyPrice { get; set; }
        public List<string>? ImageName { get; set; }
    }

}
