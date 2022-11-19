using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mind.Entity.Dto.Psychologist
{
    public class PsychologistUpdateDto
    {
        public int? ExperienceYear { get; set; }
        public int? PhoneNumber { get; set; }
        public double? TherapyPrice { get; set; }
        public string? LocalAdress { get; set; }    
    }
}
