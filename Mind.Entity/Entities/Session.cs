using Mind.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mind.Entity.Entities
{
    public class Session : BaseEntity, IEntity
    {
        public DateTime Time { get; set; }
        public bool IsReserved { get; set; }

    }
}
