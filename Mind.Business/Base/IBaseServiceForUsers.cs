using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mind.Business.Base
{
    public interface IBaseServiceForUsers<TGet, TUpdate>
    {
        Task<TGet> Get(string id);
        Task<List<TGet>> GetAll();
        Task Update( TUpdate entity);

    }

}
