using Mind.Core.EFRepository;
using Mind.Data.Abstracts;
using Mind.Data.DAL;
using Mind.Entity.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mind.Data.İmplementations
{
    public class UserRepositoryDal : EFEntityRepositoryBase<AppUser, AppDbContext>, IUserDal
    {
        public UserRepositoryDal(AppDbContext context) : base(context) { }
    }
}
