using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mind.Business.Dto.Psychologist
{
    public class PsychologistFullDto
    {
        public int Id { get; set; }
        public int? ExperienceYear { get; set; }
        public int? PhoneNumber { get; set; }
        public double? TherapyPrice { get; set; }
        public string? LocalAdress { get; set; }
        public List<string>? ImageName { get; set; }

        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Mailadress { get; set; }
        public int Age { get; set; }
    }
}
