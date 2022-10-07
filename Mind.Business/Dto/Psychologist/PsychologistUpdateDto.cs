using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mind.Entity.Dto.Psychologist
{
    public class PsychologistUpdateDto
    {
        public string? Name { get; set; }
        public string? Surename { get; set; }
        public double Price { get; set; }
        public int ExperienceYear { get; set; }
        public double TherapyPrice { get; set; }
        public string? ImageName { get; set; }
    }
}
