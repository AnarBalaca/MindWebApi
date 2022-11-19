using Mind.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mind.Entity.Entities;

public class Image : IEntity
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int? PsychologistId { get; set; }
    public int? BlogId { get; set; }
    public Psychologist? Psychologist { get; set; }
    public Blog? Blog { get; set; }


}
