using Mind.Core.EFRepository.EFBase;
using Mind.Entity.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mind.Data.Abstracts
{
    public interface IUserDal : IEntityRepositoryBase<AppUser>
    {
    }
}
