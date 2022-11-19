using Mind.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mind.Business.Dto.User
{
    public class UserGetDto
    {
        
        public string? UserName { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Email { get; set; }
        public bool IsPsycho { get; set; }
        public int Age { get; set; }
    }
}
