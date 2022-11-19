namespace Mind.Entity.Dto.Psychologist
{
    public class PsychologistGetDto
    {
        public int Id { get; set; }
        public int? ExperienceYear { get; set; }
        public int? PhoneNumber { get; set; }
        public double? TherapyPrice { get; set; }
        public string? LocalAdress { get; set; }
        public List<string>? ImageName { get; set; }



        public string? UserName { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Email { get; set; }
    }

}
