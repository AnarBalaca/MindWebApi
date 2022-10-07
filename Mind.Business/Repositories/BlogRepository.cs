using Mind.Business.Dto.Blog;
using Mind.Business.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mind.Business.Repositories;

public class BlogRepository : IBlogService
{
    public Task<BlogGetDto> Get(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<BlogGetDto>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task Create(BlogCreateDto entity)
    {
        throw new NotImplementedException();
    }

    public Task Update(int id, BlogUpdateDto entity)
    {
        throw new NotImplementedException();
    }
    public Task Delete(int id)
    {
        throw new NotImplementedException();
    }

}
