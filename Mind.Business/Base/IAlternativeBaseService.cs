using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mind.Business.Base
{
    public interface IAlternativeBaseService<TGet , TCreate>
    {
        Task<TGet> Get(int id);
        Task<List<TGet>> GetAll();
        Task Create(TCreate entity);
        Task Delete(int id);
    }
}
