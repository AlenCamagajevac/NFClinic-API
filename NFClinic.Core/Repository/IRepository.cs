using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NFClinic.Core.Repository
{
	public interface IRepository<TEntity> where TEntity : class
	{
		Task<TEntity> GetAsync(object id);

		Task<IEnumerable<TEntity>> GetAllAsync();

		IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

		Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

		Task AddAsync(TEntity entity);

		Task AddRangeAsync(IEnumerable<TEntity> entities);

		void Remove(TEntity entity);

		void RemoveRange(IEnumerable<TEntity> entities);
	}
}
