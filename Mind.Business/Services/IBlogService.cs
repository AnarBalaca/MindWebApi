using Mind.Business.Base;
using Mind.Business.Dto.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mind.Business.Services;

public interface IBlogService : IBaseService<BlogGetDto, BlogCreateDto, BlogUpdateDto> {}
