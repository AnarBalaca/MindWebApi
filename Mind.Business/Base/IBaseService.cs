using Mind.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mind.Business.Base
{
    public interface IBaseService<TGet, TCreate, TUpdate>
  
    {
        Task<TGet> Get(int id);
        Task<List<TGet>> GetAll();
        Task Create(TCreate entity);
        Task Update(int id , TUpdate entity);
        Task Delete(int id);
    }
}
