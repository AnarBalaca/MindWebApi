using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mind.Business.Base
{
    public interface IBaseServiceForPsychologist<TGet, TCreate, TUpdate>
    {
        Task<TGet> Get(string id);
        Task<TGet> Get(int id);
        Task<List<TGet>> GetAll();
        Task Create(TCreate entity);
        Task Update(string id, TUpdate entity);
        Task Delete(string id);
       
    }
}
