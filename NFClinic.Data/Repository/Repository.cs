using Microsoft.EntityFrameworkCore;
using NFClinic.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NFClinic.Data.Repository
{
	public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
	{
		protected readonly DbContext Context;
		private DbSet<TEntity> entities;

		public Repository(DbContext context)
		{
			Context = context;
			entities = Context.Set<TEntity>();
		}

		public async Task<TEntity> GetAsync(object id)
		{
			return await entities.FindAsync(id);
		}

		public async Task<IEnumerable<TEntity>> GetAllAsync()
		{
			return await entities.ToListAsync();
		}

		public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
		{
			return entities.Where(predicate);
		}

		public async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
		{
			return await entities.SingleOrDefaultAsync(predicate);
		}

		public async Task AddAsync(TEntity entity)
		{
			await entities.AddAsync(entity);
		}

		public async Task AddRangeAsync(IEnumerable<TEntity> entities)
		{
			await Context.Set<TEntity>().AddRangeAsync(entities);
		}

		public void Remove(TEntity entity)
		{
			Context.Set<TEntity>().Remove(entity);
		}

		public void RemoveRange(IEnumerable<TEntity> entities)
		{
			Context.Set<TEntity>().RemoveRange(entities);
		}
	}
}
