using Mind.Core.EFRepository;
using Mind.Data.Abstracts;
using Mind.Data.DAL;
using Mind.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mind.Data.İmplementations
{
    public class BlogRepositoryDal : EFEntityRepositoryBase<Blog, AppDbContext>, IBlogDal
    {
        public BlogRepositoryDal(AppDbContext context) : base(context) { }
    }

}
