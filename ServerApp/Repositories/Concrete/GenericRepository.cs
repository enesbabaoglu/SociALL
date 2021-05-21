
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ServerApp.Repositories.Concrete;
using ServerApp.Repositories.Abstract;
using System.Threading.Tasks;
using System.Collections;

namespace ServerApp.Repositories.Concrete
{
    public class GenericRepository<T> : IGenericRepository<T>
    where T : class
    {
        SociAllContext _sociAllContext;
        DbSet<T> _dbSet;
        public GenericRepository(SociAllContext sociAllContext)
        {

            _sociAllContext = sociAllContext;
            _dbSet = _sociAllContext.Set<T>();

        }
        public void Create(T entity)
        {
            _dbSet.Add(entity);
            _sociAllContext.SaveChanges();
        }
        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
            _sociAllContext.SaveChanges();
        }

        // public virtual List<T> GetAll()
        // {
        //     return _dbSet.ToList();
        // }

        public T Get(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.FirstOrDefault(predicate);
        }

        public void Update(T entity)
        {
            _sociAllContext.Entry(entity).State = EntityState.Modified;
            _sociAllContext.SaveChanges();
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            var result = predicate ==null ?  _dbSet: _dbSet.Where(predicate);

            return result;
        }
         public IQueryable<T> GetAllWithIncludes(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            var query = predicate == null ? _dbSet : _dbSet.Where(predicate);
            return includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }
        public T GetWithIncludes(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            var query = _dbSet.Where(predicate);
            return includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty)).FirstOrDefault();
        }
    }
}
