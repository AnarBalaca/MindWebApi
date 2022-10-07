using Mind.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mind.Entity.Entities
{
    public class Blog : BaseEntity, IEntity
    {
        public string? Title { get; set; }
        public string? Body { get; set; }
        public ICollection<Image>? Images { get; set; }
    }
}
