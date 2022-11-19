using Microsoft.AspNetCore.Http;

namespace Mind.Entity.Dto.Psychologist
{
    public class PsychologistCreateDto
    {

        public int? ExperienceYear { get; set; }
        public int? PhoneNumber { get; set; }
        public double? TherapyPrice { get; set; }
        public string? LocalAdress { get; set; }
        public int? Age { get; set; }
        public List<IFormFile>? ImageFiles { get; set; }

    }
}
