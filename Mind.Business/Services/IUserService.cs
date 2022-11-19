using Mind.Business.Base;
using Mind.Business.Dto.User;
using Mind.Entity.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Mind.Business.Services
{
    public interface IUserService : IBaseServiceForUsers<UserGetDto ,  UserUpdateDto >
    {
    }
}
