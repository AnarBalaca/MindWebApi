using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mind.Business.Dto.User
{
    public class ProfileImageDto
    {
        public string? ImageName { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}
