using Mind.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Mind.Core.EFRepository.EFBase
{
    public interface IEntityRepositoryBase<TEntity>
        where TEntity : class , IEntity , new()
    {
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>>? expression, int? skip = 0, params string[] includes);
        Task<List<TEntity>>GetAllAsync(Expression<Func<TEntity, bool>>? expression, int? skip = 0 , int? take = int.MaxValue , params string[] includes);
        Task CreateAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
    }
}
