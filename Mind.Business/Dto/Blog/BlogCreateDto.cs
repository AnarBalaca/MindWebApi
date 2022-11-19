using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mind.Business.Dto.Blog;

public class BlogCreateDto
{
    public string? Title { get; set; }
    public string? Body { get; set; }
    public string? AuthorName { get; set; }
    public List<IFormFile>? ImageFiles { get; set; }
}
